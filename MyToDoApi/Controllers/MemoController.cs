using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyToDoApi.Context;
using MyToDoApi.Dtos;
using MyToDoApi.Extensions;
using MyToDoApi.Repository;
using MyToDoApi.Sevice;
using UserMgrWebApi;

namespace MyToDoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [UnitOfWork(typeof(MyToDoDBContext))]
    public class MemoController : ControllerBase
    {
        private IMemoRepository repository;
        private readonly MyToDoDBContext dBContext;
        private readonly MemoService service;

        public MemoController(IMemoRepository repository, MyToDoDBContext dBContext, MemoService service)
        {
            this.repository = repository;
            this.dBContext = dBContext;
            this.service = service;
        }

        [HttpGet]
        public async Task<ApiResponse> Get(int id)
        {
            return await service.GetSingleAsync(id);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery]QueryParameter query)
        {
            return await service.GetAllAsync(query);
        }

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id)
        {
            return await service.DeleteAsync(id);
        }

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody]MemoDto memo)
        {
            return await service.AddAsync(memo);
        }

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody]MemoDto memo)
        {
            return await service.UpdateAsync(memo);
        }
    }
}
