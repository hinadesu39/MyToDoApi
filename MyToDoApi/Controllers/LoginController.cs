using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyToDoApi.Context;
using MyToDoApi.Dtos;
using MyToDoApi.Entity;
using MyToDoApi.Repository;
using MyToDoApi.Sevice;
using UserMgrWebApi;

namespace MyToDoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [UnitOfWork(typeof(MyToDoDBContext))]
    public class LoginController : ControllerBase
    {
        private IUserRepository repository;
        private readonly MyToDoDBContext dBContext;
        private readonly UserService service;

        public LoginController(IUserRepository repository, MyToDoDBContext dBContext, UserService service)
        {
            this.repository = repository;
            this.dBContext = dBContext;
            this.service = service;
        }

        [HttpPost]
        public async Task<ApiResponse> Register([FromBody]UserDto user)
        {
            return await service.Register(user);
        }

        [HttpPost]

        public async Task<ApiResponse<UserDto>> Login([FromBody] UserDto user)
        {
            
            return await service.Login(user.Account, user.Password);
        }

        [HttpPost]

        public async Task<ApiResponse> Update([FromBody] UserDto user)
        {
            return await service.Update(user);
        }
    }
}
