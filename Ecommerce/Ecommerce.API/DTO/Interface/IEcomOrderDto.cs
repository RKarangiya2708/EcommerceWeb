using Ecommerce.API.Data.Models;

namespace Ecommerce.API.DTO.Interface;

public interface IEcomOrderDto
{
    public Task<OperationResult> CreateOrder(OrderModel model);
    public Task<OperationResult<List<OrderModel>>> GetOrders(int userId);
    public Task<OperationResult<List<OrderItemModel>>> GetOrderDetail(int id);
}
