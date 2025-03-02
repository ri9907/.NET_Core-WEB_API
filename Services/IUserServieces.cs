using Entities;

namespace Services
{
    public interface IUserServieces
    {
        int checkPassword(string pasword);
        Task<User> GetById(int id);
        Task<User> Login(User user);
        Task<User> Register(User user);
        Task<User> UpdateUser(int id, User newUser);
    }
}