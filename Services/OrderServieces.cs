using Entities;
using Microsoft.Extensions.Logging;
using Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderServieces : IOrderServieces
    {
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private readonly ILogger<IOrderServieces> _logger;

        public OrderServieces(IOrderRepository orderRepository, IProductRepository productRepository, ILogger<IOrderServieces> logger)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _logger = logger;
        }
        private async Task<bool> checkSum(Order order)
        {
           
            double sum = 0.0;
            foreach (OrderItem item in order.OrderItems)
            {
                Product product = await _productRepository.GetById(item.Productid);
                sum += product.Price * item.Quantity;
            }
            if (sum != order.OrderSum)
            {
                _logger.LogError($"user {order.UserId}  tried perchasing with a difffrent price {order.OrderSum} instead of {sum}");
                _logger.LogInformation($"user {order.UserId}  tried perchasing with a difffrent price {order.OrderSum} instead of {sum}");
                return false;
            }
            //await _orderRepository.Add(order);
            return true;
        }
        public async Task<Order> Add(Order order)
        {
            order.OrderDate = DateTime.Now;

            if (await checkSum(order))
            {
                return await _orderRepository.Add(order);
            }
            else
            {
                return null;
            }
        }

    }
}

