using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        ComputersContext _computersContext;
        public OrderRepository(ComputersContext computersContext)
        {
            _computersContext = computersContext;
        }
        public async Task<Order> Add(Order order)
        {
            await _computersContext.Orders.AddAsync(order);
            await _computersContext.SaveChangesAsync();
            return order;
        }
    }
}
