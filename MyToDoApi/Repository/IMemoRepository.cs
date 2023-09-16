using MyToDoApi.Entity;
using MyToDoApi.Extensions;
using MyToDoApi.Sevice;

namespace MyToDoApi.Repository
{
    public interface IMemoRepository
    {
        Task<Memo[]> GetAllAsync(QueryParameter query);
        Task<Memo[]> GetAllForSumAsync();
        Task<Memo> GetSingleAsync(int id);
        Task AddAsync(Memo memo);
        Task<bool> DeleteAsync(int id);
        Task<Memo> UpdateAsync(Memo memo);
    }
}
