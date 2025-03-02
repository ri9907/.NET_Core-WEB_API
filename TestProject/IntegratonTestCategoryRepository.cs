using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class IntegratonTestCategoryRepository : IClassFixture<DatabaseFixture>
    {
        private readonly ComputersContext _dbContext;
        private readonly CategoryRepository _categoryRepository;

        public IntegratonTestCategoryRepository(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _categoryRepository = new CategoryRepository(_dbContext);
        }

        [Fact]
        public async Task Get_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<Category>
        {
            new Category { CategoryName = "Category 1" },
            new Category { CategoryName = "Category 2" },
            new Category { CategoryName = "Category 3" }
        };

            await _dbContext.Categories.AddRangeAsync(categories);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Contains(result, c => c.CategoryName == "Category 1");
            Assert.Contains(result, c => c.CategoryName == "Category 2");
            Assert.Contains(result, c => c.CategoryName == "Category 3");

            // Clean up
            _dbContext.Categories.RemoveRange(categories);
            await _dbContext.SaveChangesAsync();
        }
    }

}
