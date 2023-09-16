using Microsoft.EntityFrameworkCore;
using MyToDoApi.Context;
using MyToDoApi.Dtos;
using MyToDoApi.Entity;
using MyToDoApi.Sevice;
using System.Diagnostics;

namespace MyToDoApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyToDoDBContext _dbContext;

        public UserRepository(MyToDoDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> CheckForSignInAsync(string account, string password)
        {
            var res =  await FindByAccountAsync(account);
            if (res == null)
            {
                return null;
            }
            if (res.Password != password)
            {
                return null;
            }
            return res;
        }

        public async Task<bool> CreateUserAsync(User user) 
        {
            await _dbContext.Users.AddAsync(user);
            return true;
        }

        public async Task<User?> FindByAccountAsync(string account)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Account == account);
        }

        public Task<bool> RemoveUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(ApiResponse, User?, string? password)> ResetPasswordAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            return user;
        }
    }
}
