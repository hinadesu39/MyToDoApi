using AutoMapper;
using MyToDoApi.Dtos;
using MyToDoApi.Entity;
using MyToDoApi.Repository;

namespace MyToDoApi.Sevice
{
    public class UserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<UserDto>> Login(string Account, string Password)
        {
            //Password = Password.GetMD5();
            var res = await userRepository.CheckForSignInAsync(Account, Password);
            var u = mapper.Map<UserDto>(res);
            return new ApiResponse<UserDto>() { Message = "login", Result = u, Status = u==null?false:true };
        }

        public async Task<ApiResponse> Register(UserDto user)
        {
            var u = mapper.Map<User>(user);
            var res = await userRepository.FindByAccountAsync(u.Account);
            if (res == null)
            {
                u.CreateTime = DateTime.Now;
                await userRepository.CreateUserAsync(u);

                return new ApiResponse() { Message = "register", Result = "ok", Status = true };
            }
            return new ApiResponse() { Message = "register", Result = "该用户名已存在", Status = false };
        }

        public async Task<ApiResponse> Update(UserDto user)
        {
            var u = mapper.Map<User>(user);
            var res = await userRepository.FindByAccountAsync(user.Account);
            if(res == null)
            {
                return new ApiResponse() { Message ="Update", Result = "false", Status = false };
            }
            res.UpdateTime = DateTime.Now;
            res.UserAvatar = u.UserAvatar;
            res.Password = u.Password;
            res.UserName = u.UserName;
            var UpdateRes = await userRepository.UpdateAsync(res);
            if(UpdateRes == null)
            {
                return new ApiResponse() { Message = "Update", Result = "false", Status = false };
            }
            return new ApiResponse() { Message = "Update", Result = UpdateRes, Status = true };
        }
    }
}
