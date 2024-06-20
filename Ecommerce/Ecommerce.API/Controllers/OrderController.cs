using Ecommerce.API.Data.Models;
using Ecommerce.API.DTO.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IEcomOrderDto _order;

        [HttpPost]
        [Route("orders")]
        public async Task<OperationResult> CreateOrder(OrderModel model) {
           return await _order.CreateOrder(model);
        } 

        [HttpGet]
        [Route("orders")]
        public async Task<OperationResult<List<OrderModel>>> GetOrders(int userId) {
            return await _order.GetOrders(userId);
        }

        [HttpGet]
        [Route("orders/{id}")]
        public async Task<OperationResult<List<OrderItemModel>>> GetOrderDetail(int id)
        {
            return await _order.GetOrderDetail(id);
        }

        public OrderController(IEcomOrderDto orderDto) => _order = orderDto;
    }

}
