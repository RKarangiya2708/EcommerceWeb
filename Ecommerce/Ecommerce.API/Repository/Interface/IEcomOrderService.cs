using Ecommerce.API.Data.Models;

namespace Ecommerce.API.Repository.Interface;
public interface IEcomOrderService
{
    public Task<OperationResult> CreateOrder(OrderModel model);
    public Task<OperationResult<List<OrderModel>>> GetOrders(int userId);
    public Task<OperationResult<List<OrderItemModel>>> GetOrderDetail(int id);
}
