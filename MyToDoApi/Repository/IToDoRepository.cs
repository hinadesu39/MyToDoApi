using MyToDoApi.Entity;
using MyToDoApi.Extensions;

namespace MyToDoApi.Repository
{
    public interface IToDoRepository
    {

        Task<ToDo[]> GetAllAsync(QueryParameter query);
        Task<ToDo[]> GetAllForSumAsync();
        Task<ToDo[]> GetAllFilterAsync(ToDoQueryParameter query);
        Task<ToDo> GetSingleAsync(int id);
        Task AddAsync(ToDo toDo);
        Task<bool> DeleteAsync(int id);
        Task<ToDo> UpdateAsync(ToDo toDo);
    }
}
