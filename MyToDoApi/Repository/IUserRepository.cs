using Microsoft.AspNetCore.Identity;
using MyToDoApi.Dtos;
using MyToDoApi.Entity;
using MyToDoApi.Sevice;

namespace MyToDoApi.Repository
{
    public interface IUserRepository
    {
        Task<User?> FindByAccountAsync(string account);//根据用户名获取用户
        Task<bool> CreateUserAsync(User user);//创建用户
        
        /// <summary>
        /// 为了登录而检查用户名、密码是否正确
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="lockoutOnFailure">如果登录失败，则记录一次登陆失败</param>
        /// <returns></returns>
        public Task<User> CheckForSignInAsync(string account, string password);
        
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> RemoveUserAsync(int id);

       

        /// <summary>
        /// 重置密码。
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回值第三个是生成的密码</returns>
        public Task<(ApiResponse, User?, string? password)> ResetPasswordAsync(int id);

        Task<User> UpdateAsync(User user);
    }
}
