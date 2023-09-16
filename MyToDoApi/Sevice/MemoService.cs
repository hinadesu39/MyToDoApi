using AutoMapper;
using MyToDoApi.Dtos;
using MyToDoApi.Entity;
using MyToDoApi.Extensions;
using MyToDoApi.Repository;

namespace MyToDoApi.Sevice
{
    public class MemoService : IBaseService<MemoDto>
    {
        private readonly IMemoRepository memoRepository;
        private readonly IMapper mapper;
        public MemoService(IMemoRepository memoRepository, IMapper mapper)
        {
            this.memoRepository = memoRepository;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> AddAsync(MemoDto t)
        {
            var memo = mapper.Map<Memo>(t);
            memo.CreateTime = DateTime.Now;
            await memoRepository.AddAsync(memo);
            return new ApiResponse() { Message = "Add", Result = memo, Status = true };
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            var res = await memoRepository.DeleteAsync(id);
            return new ApiResponse() { Message = "Delete", Result = res, Status = true };
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameter query)
        {
            var res = await memoRepository.GetAllAsync(query);
            return new ApiResponse() { Message = "GetAll", Result = res, Status = true };

        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            var res = await memoRepository.GetSingleAsync(id);
            return new ApiResponse() { Message = "GetSingle", Result = res, Status = true };
        }

        public async Task<ApiResponse> UpdateAsync(MemoDto t)
        {
            var memo = mapper.Map<Memo>(t);
            var item = await memoRepository.GetSingleAsync(memo.Id);
            if (item == null)
            {
                return new ApiResponse() { Message = "Update", Result = "flase", Status = false };
            }
            item.UpdateTime = DateTime.Now;
            item.Title = t.Title;
            item.Content = t.Content;
            var res = await memoRepository.UpdateAsync(item);
            if(res == null)
            {
                return new ApiResponse() { Message = "Update", Result = "flase", Status = false };
            }
            return new ApiResponse() { Message = "Update", Result = res, Status = true };
        }
    }
}
