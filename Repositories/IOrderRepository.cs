using Entities;

namespace Repositories
{
    public interface IOrderRepository
    {
        Task<Order> Add(Order order);
    }
}