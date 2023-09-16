using MyToDoApi.Extensions;

namespace MyToDoApi.Sevice
{
    public interface IBaseService<T>
    {
        Task<ApiResponse> GetAllAsync(QueryParameter query);
        Task<ApiResponse> GetSingleAsync(int id);
        Task<ApiResponse> AddAsync(T t);
        Task<ApiResponse> DeleteAsync(int id);
        Task<ApiResponse> UpdateAsync(T t);
    }
}
