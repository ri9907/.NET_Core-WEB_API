using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        ComputersContext _computersContext;

        public ProductRepository(ComputersContext computersContext)
        {
            _computersContext = computersContext;
        }
        public async Task<List<Product>> Get(int? minPrice, int? maxPrice, List<int>? categories, string? description)
        {
            var query = _computersContext.Products.Where(product =>
            (description == null ? (true) : (product.Description.Contains(description)))
            && ((minPrice == null) ? (true) : (product.Price >= minPrice))
            && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
            && ((categories.Count() == 0) ? (true) : (categories.Contains(product.CategoryId))))
                .OrderBy(Product => Product.Price);

            List<Product> products = await query.ToListAsync();
            return products;
        }

        public async Task<Product> GetById(int id)
        {
            return await _computersContext.Products.FindAsync(id);
        }
    }
}
