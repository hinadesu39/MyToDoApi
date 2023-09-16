using AutoMapper;
using MyToDo.Common.Models;
using MyToDoApi.Dtos;
using MyToDoApi.Entity;
using MyToDoApi.Extensions;
using MyToDoApi.Repository;
using System.Collections.ObjectModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyToDoApi.Sevice
{
    public class ToDoService : IBaseService<ToDoDto>,IToDoService
    {
        private readonly IToDoRepository toDoRepository;
        private readonly IMemoRepository memoRepository;
        private readonly IMapper mapper;
        public ToDoService(IToDoRepository toDoRepository, IMapper mapper, IMemoRepository memoRepository)
        {
            this.toDoRepository = toDoRepository;
            this.mapper = mapper;
            this.memoRepository = memoRepository;
        }

        public async Task<ApiResponse> AddAsync(ToDoDto t)
        {
            var todo = mapper.Map<ToDo>(t);
            todo.CreateTime = DateTime.Now;
            await toDoRepository.AddAsync(todo);
            return new ApiResponse() { Message = "Add", Result = todo, Status = true };
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var res = await toDoRepository.DeleteAsync(id);
            return new ApiResponse()  { Message = "Delete", Result = res, Status = true };
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameter query)
        {
            var res =await toDoRepository.GetAllAsync(query);
            return new ApiResponse() { Message = "GetAll", Result = res, Status = true };

        }

        public async Task<ApiResponse> GetAllFilterAsync(ToDoQueryParameter parameter)
        {
            var res = await toDoRepository.GetAllFilterAsync(parameter);
            return new ApiResponse() { Message = "GetAll", Result = res, Status = true };
        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            var res = await toDoRepository.GetSingleAsync(id);
            return new ApiResponse() { Message = "GetSingle", Result = res, Status = true };
        }

        public async Task<ApiResponse> Summary()
        {
            var toDos = await toDoRepository.GetAllForSumAsync();
            var memos = await memoRepository.GetAllForSumAsync();

            SummaryDto summaryDto = new SummaryDto();

            summaryDto.Sum = toDos.Count();
            summaryDto.CompletedCount = toDos.Where(t => t.Status == 1).Count();
            summaryDto.CompletedRate = ((double)summaryDto.CompletedCount / summaryDto.Sum).ToString("0%");
            summaryDto.MemoCount = memos.Count();   

            summaryDto.ToDoList = new ObservableCollection<ToDoDto>(mapper.Map<List<ToDoDto>>(toDos.Where(t=>t.Status==0)));
            summaryDto.MemoList = new ObservableCollection<MemoDto>(mapper.Map<List<MemoDto>>(memos));
            return new ApiResponse() { Message = "Summary", Result = summaryDto, Status = true };
        }

        public async Task<ApiResponse> UpdateAsync(ToDoDto t)
        {
            var todo = mapper.Map<ToDo>(t);
            var item= await toDoRepository.GetSingleAsync(todo.Id);
            if(item == null)
            {
                return new ApiResponse() { Message = "Update", Result = "false", Status = false };
            }
            item.UpdateTime = DateTime.Now;
            item.Title = t.Title;
            item.Content = t.Content;
            item.Status = t.Status;
            var res= await toDoRepository.UpdateAsync(item);
            if (item == null)
            {
                return new ApiResponse() { Message = "Update", Result = "false", Status = false };
            }
            return new ApiResponse() { Message = "Update", Result = res, Status = true };
        }

    }
}
