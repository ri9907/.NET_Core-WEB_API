using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class UserRepositoryIntegrationTests:IClassFixture<DatabaseFixture>
    {
        private readonly ComputersContext _dbContext;
        private readonly UserRepositories _userRepositories;

        public UserRepositoryIntegrationTests(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _userRepositories = new UserRepositories(_dbContext);
        }

        [Fact]
        public async Task Login_ValifCredentials_ReturnsUser()
        {
            //_dbContext.Database.EnsureDeleted();
            //_dbContext.Database.EnsureCreated();
            var email = "test@gmail.com";
            var password = "password";
            var user = new User { Email = email, Password = password, FirstName = "test", LastName = "test22" };
            var userLogin = new User { Email = email, Password = password, FirstName = null, LastName = null };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var result = await _userRepositories.Login(userLogin);

            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task Register_ValidUser_AddsUserToDatabase()
        {
            // Arrange
            var user = new User { Email = "test@gmail.com", Password = "password", FirstName = "Test", LastName = "User" };

            // Act
            var registeredUser = await _userRepositories.Register(user);

            // Assert
            Assert.NotNull(registeredUser);
            Assert.Equal(user.Email, registeredUser.Email);
            Assert.Equal(user.FirstName, registeredUser.FirstName);
            Assert.Equal(user.LastName, registeredUser.LastName);

            // Clean up
            _dbContext.Users.Remove(registeredUser);
            await _dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task GetById_ValidId_ReturnsUser()
        {
            // Arrange
            var user = new User { FirstName = "Test", LastName = "User", Email = "test@example.com", Password = "pass" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            var userId = user.UserId;  // The database will auto-generate this ID

            // Act
            var result = await _userRepositories.GetById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.FirstName);
            Assert.Equal("User", result.LastName);
            Assert.Equal("test@example.com", result.Email);
            Assert.Equal("pass", result.Password);
        }
        [Fact]
        public async Task UpdateUser_ValidId_UpdatesUser()
        {
            // Arrange
            var originalUser = new User { Email = "test@example.com", Password = "Password", FirstName = "Original", LastName = "User" };
            await _dbContext.Users.AddAsync(originalUser);
            await _dbContext.SaveChangesAsync();
            var userId = originalUser.UserId;

            // Detach the original user to simulate detached state
            _dbContext.Entry(originalUser).State = EntityState.Detached;

            var updatedUser = new User { UserId = userId, Email = "updated@example.com", Password = "NewPass", FirstName = "Updated", LastName = "User" };

            // Act
            var result = await _userRepositories.UpdateUser(userId, updatedUser);

            // Reload the user from the database to confirm changes
            var reloadedUser = await _dbContext.Users.FindAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);  // Ensure the user's ID remains the same
            Assert.Equal(updatedUser.Email, result.Email);
            Assert.Equal(updatedUser.Password, result.Password);
            Assert.Equal(updatedUser.FirstName, result.FirstName);
            Assert.Equal(updatedUser.LastName, result.LastName);

            // Confirm changes in the database
            Assert.Equal(updatedUser.Email, reloadedUser.Email);
            Assert.Equal(updatedUser.Password, reloadedUser.Password);
            Assert.Equal(updatedUser.FirstName, reloadedUser.FirstName);
            Assert.Equal(updatedUser.LastName, reloadedUser.LastName);

            // Clean up
            _dbContext.Users.Remove(reloadedUser);
            await _dbContext.SaveChangesAsync();
        }
    }
}

