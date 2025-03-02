using Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Repositories
{
    public class UserRepositories : IUserRepositories
    {
        ComputersContext _computersContext;
        public UserRepositories(ComputersContext computersContext)
        {
            _computersContext = computersContext;
        }
        public async Task<User> GetById(int id)
        {
            return await _computersContext.Users.FindAsync(id);
        }

        public async Task<User> Register(User user)
        {

            await _computersContext.Users.AddAsync(user);
            await _computersContext.SaveChangesAsync();
            return await GetById(user.UserId);
        }

        public async Task<User> Login(User user)
        {
            return await _computersContext.Users.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefaultAsync();
        }
        public async Task<User> UpdateUser(int id, User user)
        {
            user.UserId = id;
            _computersContext.Users.Update(user);
            await _computersContext.SaveChangesAsync();
            return user;

        }
    }
}
