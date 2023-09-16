using Microsoft.EntityFrameworkCore;
using MyToDoApi.Context;
using MyToDoApi.Entity;
using MyToDoApi.Extensions;

namespace MyToDoApi.Repository
{
    public class MemoRepository : IMemoRepository
    {

        private readonly MyToDoDBContext _dbContext;

        public MemoRepository(MyToDoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Memo memo)
        {
            await _dbContext.Memos.AddAsync(memo);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _dbContext.Memos.SingleOrDefaultAsync(s => s.Id == id);
            if (item == null)
            {
                return false;
            }
            _dbContext.Memos.Remove(item);
            return true;
        }

        public async Task<Memo[]> GetAllAsync(QueryParameter query)
        {
            var res = _dbContext.Memos.Where(t => string.IsNullOrEmpty(query.Search) || t.Title.Contains(query.Search));
            return await res.Skip(query.PageSize * (query.PageIndex - 1)).Take(query.PageSize).ToArrayAsync();
        }

        public Task<Memo[]> GetAllForSumAsync()
        {
            var res = _dbContext.Memos.OrderByDescending(m => m.CreateTime).ToArrayAsync();
            return res;
        }

        public async Task<Memo> GetSingleAsync(int id)
        {
            return await _dbContext.Memos.SingleOrDefaultAsync(s => s.Id == id);

        }

        public async Task<Memo> UpdateAsync(Memo memo)
        {
            _dbContext.Memos.Update(memo);
            return memo;
        }
    }
}
