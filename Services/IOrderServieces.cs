using Entities;

namespace Services
{
    public interface IOrderServieces
    {
        Task<Order> Add(Order order);
    }
}