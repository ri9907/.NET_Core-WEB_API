using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductServieces : IProductServieces
    {
        private IProductRepository _productRepository;
        public ProductServieces(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Product>> Get(int? minPrice, int? maxPrice, List<int>? categoriesId, string? description)
        {
            return await _productRepository.Get( minPrice,  maxPrice, categoriesId, description);
        }
    }
}
