using AutoMapper;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Globalization;

namespace myProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderServieces _orderServieces;
        private IMapper _mapper;

        public OrdersController(IOrderServieces orderServieces, IMapper mapper)
        {
            _orderServieces = orderServieces;
            _mapper = mapper;

        }
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Post([FromBody] OrderDTO orderDto)
        {
            Order order = _mapper.Map<OrderDTO, Order>(orderDto);
            Order newOrder = await _orderServieces.Add(order);
            OrderDTO newOrderDto = _mapper.Map<Order, OrderDTO>(newOrder);
            if (newOrderDto != null)
                return Ok(newOrderDto);
            return BadRequest();
        }
    }
}
