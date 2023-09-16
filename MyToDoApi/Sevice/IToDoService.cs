using MyToDoApi.Dtos;
using MyToDoApi.Extensions;

namespace MyToDoApi.Sevice
{
    public interface IToDoService:IBaseService<ToDoDto>
    {
        Task<ApiResponse> GetAllFilterAsync(ToDoQueryParameter parameter);
        Task<ApiResponse> Summary();
    }
}
