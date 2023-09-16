using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyToDoApi.Context;
using MyToDoApi.Entity;
using MyToDoApi.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyToDoApi.Repository
{
    public class TodoRepository : IToDoRepository
    {
        private readonly MyToDoDBContext _dbContext;

        public TodoRepository(MyToDoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(ToDo toDo)
        {
            await _dbContext.ToDos.AddAsync(toDo);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _dbContext.ToDos.SingleOrDefaultAsync(s => s.Id == id);
            if (item == null)
            {
                return false;
            }
            _dbContext.ToDos.Remove(item);
            return true;
        }

        public async Task<ToDo[]> GetAllAsync(QueryParameter query)
        {


            var res = _dbContext.ToDos.Where(t => string.IsNullOrEmpty(query.Search) || t.Title.Contains(query.Search));
            return await res.Skip(query.PageSize * (query.PageIndex - 1)).Take(query.PageSize).ToArrayAsync();


        }
        public async Task<ToDo[]> GetAllFilterAsync(ToDoQueryParameter query)
        {


            var res = _dbContext.ToDos.Where(t => (string.IsNullOrEmpty(query.Search) || t.Title.Contains(query.Search)) && query.Status == null ? true : t.Status.Equals(query.Status));
            return await res.Skip(query.PageSize * (query.PageIndex - 1)).Take(query.PageSize).ToArrayAsync();


        }

        public Task<ToDo[]> GetAllForSumAsync()
        {
            var res = _dbContext.ToDos.OrderByDescending(t => t.CreateTime).ToArrayAsync();
            return res;
        }

        public async Task<ToDo> GetSingleAsync(int id)
        {
            return await _dbContext.ToDos.SingleOrDefaultAsync(s => s.Id == id);

        }

        public async Task<ToDo> UpdateAsync(ToDo toDo)
        {
            _dbContext.ToDos.Update(toDo);
            return toDo;
        }
    }
}
