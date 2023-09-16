using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyToDoApi.Context;
using MyToDoApi.Dtos;
using MyToDoApi.Entity;
using MyToDoApi.Extensions;
using MyToDoApi.Repository;
using MyToDoApi.Sevice;
using UserMgrWebApi;

namespace MyToDoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [UnitOfWork(typeof(MyToDoDBContext))]
    public class ToDoController : ControllerBase
    {
        private IToDoRepository repository;
        private readonly MyToDoDBContext dBContext;
        private readonly ToDoService service;

        public ToDoController(IToDoRepository repository, MyToDoDBContext dBContext, ToDoService service)
        {
            this.repository = repository;
            this.dBContext = dBContext;
            this.service = service;
        }

        [HttpGet]
        public async Task<ApiResponse> Summary()
        {
            return await service.Summary();
        }
        [HttpGet]
        public async Task<ApiResponse> Get(int id)
        {
            return await service.GetSingleAsync(id);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery]ToDoQueryParameter query)
        {
            return await service.GetAllFilterAsync(query);
        }

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id)
        {
            return await service.DeleteAsync(id);
        }

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody]ToDoDto toDo)
        {
            return await service.AddAsync(toDo);
        }

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] ToDoDto toDo)
        {
            return await service.UpdateAsync(toDo); 
        }
    }
}
