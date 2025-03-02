using Entities;

namespace Services
{
    public interface IProductServieces
    {
        Task<List<Product>> Get(int? minPrice, int? maxPrice, List<int>? categoriesId, string? description);
    }
}