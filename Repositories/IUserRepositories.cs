using Entities;

namespace Repositories
{
    public interface IUserRepositories
    {
        Task<User> GetById(int id);
        Task<User> Login(User user);
        Task<User> Register(User user);
        Task<User> UpdateUser(int id, User newUser);
    }
}