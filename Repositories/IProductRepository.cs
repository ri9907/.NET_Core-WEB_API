using Entities;

namespace Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> Get(int? minPrice, int? maxPrice, List<int>? categoriesId,string? description);
        Task<Product> GetById(int id);

    }
}