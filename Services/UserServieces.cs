using Entities;
using Repositories;
using Zxcvbn;
using System.Text.Json;

namespace Services
{
    public class UserServieces : IUserServieces
    {
        private IUserRepositories _userRepositories;
        public UserServieces(IUserRepositories userRepositories)
        {
            _userRepositories = userRepositories;
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepositories.GetById(id);
        }

        public async Task<User> Register(User user)
        {
            return await _userRepositories.Register(user);
        }

        public async Task<User> Login(User user)
        {
            return await _userRepositories.Login(user);
        }

        public async Task<User> UpdateUser(int id, User newUser)
        {
            if (checkPassword(newUser.Password) < 2)
                return null;
            newUser.UserId = id;
            return await _userRepositories.UpdateUser(id, newUser);
        }
        public int checkPassword(string pasword)
        {
            var result = Zxcvbn.Core.EvaluatePassword(pasword);
            return result.Score;
        }
    }
}
