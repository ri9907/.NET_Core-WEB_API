using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;

namespace TestProject
{
    public class UnitTestUserRepository
    {
        [Fact]
        public async Task Login_ValidCredentials_ReturnsUser()
        {
            var user = new User { Email = "test@example.com", Password = "Password" };
            var mockContext = new Mock<ComputersContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepositories(mockContext.Object);
            var result = await userRepository.Login(user);
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task GetById_ValidId_ReturnsUser()
        {
            // Arrange
            var user = new User { UserId = 1, FirstName = "Test", LastName = "User" };
            var users = new List<User> { user };

            var mockContext = new Mock<ComputersContext>();
            var mockDbSet = new Mock<DbSet<User>>();

            mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());
            mockDbSet.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync((object[] ids) => users.Find(u => u.UserId == (int)ids[0]));

            mockContext.Setup(x => x.Users).Returns(mockDbSet.Object);

            var userRepository = new UserRepositories(mockContext.Object);

            // Act
            var result = await userRepository.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.UserId);
            Assert.Equal("Test", result.FirstName);
            Assert.Equal("User", result.LastName);
        }

        [Fact]
        public async Task GetById_InvalidId_ReturnsNull()
        {
            // Arrange
            var user = new User { UserId = 1, FirstName = "Test", LastName = "User" };
            var users = new List<User> { user };

            var mockContext = new Mock<ComputersContext>();
            var mockDbSet = new Mock<DbSet<User>>();

            mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());
            mockDbSet.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync((object[] ids) => users.Find(u => u.UserId == (int)ids[0]));

            mockContext.Setup(x => x.Users).Returns(mockDbSet.Object);

            var userRepository = new UserRepositories(mockContext.Object);

            // Act
            var result = await userRepository.GetById(-1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Register_ValidCredentials_ReturnsUser()
        {
            var user = new User { Email = "t@exaaample.com", Password = "password" };
            var mockContext = new Mock<ComputersContext>();
            var users = new List<User>() { user };

            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UserRepositories(mockContext.Object);
            //Act
            var result = await userRepository.Register(user);
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUser_ValidUser_UpdatesUser()
        {
            // Arrange
            var user = new User { FirstName = "dvori", LastName = "rottman", Email = "dvori@gmail.com", Password = "password" };
            var updatedUser = new User { FirstName = "updated", LastName = "user", Email = "updated@gmail.com", Password = "newpassword" };

            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<ComputersContext>();

            mockSet.Setup(m => m.Update(It.IsAny<User>()));
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var userRepository = new UserRepositories(mockContext.Object);

            // Act
            var result = await userRepository.UpdateUser(user.UserId, updatedUser);

            // Assert
            Assert.Equal("updated@gmail.com", result.Email);
        }

    }
}

