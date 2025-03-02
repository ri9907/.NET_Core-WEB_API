using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class IntegrationProductTest : IClassFixture<DatabaseFixture>
    {
        private readonly ComputersContext _dbContext;
        private readonly ProductRepository _productRepository;

        public IntegrationProductTest(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _productRepository = new ProductRepository(_dbContext);
        }
        [Fact]
        public async Task Get_ValidFilters_ReturnsFilteredProducts()
        {
            var categories = new List<Category>
     {
         new Category {CategoryName = "Category 1" },
         new Category {CategoryName = "Category 2" },
         new Category {CategoryName = "Category 3" }
     };

            _dbContext.Categories.AddRange(categories);
            await _dbContext.SaveChangesAsync();
            var categoryId1 = categories.First(c => c.CategoryName == "Category 1").CategoryId;
            var categoryId2 = categories.First(c => c.CategoryName == "Category 2").CategoryId;
            var categoryId3 = categories.First(c => c.CategoryName == "Category 3").CategoryId;
            // Arrange
            var products = new List<Product>
     {
         new Product { CategoryId = categoryId1, Description = "Product A", Price = 150, ProductName="a", ImageUrl="./a"},
         new Product { CategoryId = categoryId2, Description = "Product B", Price = 200, ProductName="b", ImageUrl="./b"},
         new Product { CategoryId = categoryId3, Description = "Product C", Price = 300, ProductName="c", ImageUrl="./c"}
     };

            _dbContext.Products.AddRange(products);
            await _dbContext.SaveChangesAsync();

            int? minPrice = 100;
            int? maxPrice = 250;
            List<int>? categoriesFilter = new List<int> { 1, 2 };
            string? description = "Product";

            // Act
            var result = await _productRepository.Get(minPrice, maxPrice, categoriesFilter, description);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Description == "Product A");
            Assert.Contains(result, p => p.Description == "Product B");

            // Clean up
            _dbContext.Products.RemoveRange(products);
            await _dbContext.SaveChangesAsync();
        }
    }
}
