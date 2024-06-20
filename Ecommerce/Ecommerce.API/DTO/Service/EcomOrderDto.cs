using Ecommerce.API.Data.Models;
using Ecommerce.API.DTO.Interface;
using Ecommerce.API.Repository.Interface;

namespace Ecommerce.API.DTO.Service;
public class EcomOrderDto : IEcomOrderDto
{
    private readonly IEcomOrderService _order;
    public EcomOrderDto(IEcomOrderService order) => _order = order;

    public Task<OperationResult> CreateOrder(OrderModel model) => _order.CreateOrder(model);

    public Task<OperationResult<List<OrderItemModel>>> GetOrderDetail(int id) => _order.GetOrderDetail(id);

    public Task<OperationResult<List<OrderModel>>> GetOrders(int userId) => _order.GetOrders(userId);
}
