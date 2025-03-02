using Entities;

namespace Services
{
    public interface ICategoryServieces
    {
        Task<List<Category>> Get();
    }
}