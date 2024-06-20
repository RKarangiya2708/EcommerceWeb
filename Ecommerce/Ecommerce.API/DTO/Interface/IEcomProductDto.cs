using Ecommerce.API.Data.Models;

namespace Ecommerce.API.DTO.Interface;

public interface IEcomProductDto
{
    public Task<OperationResult<List<ProductModel>>> GetProducts();
    public Task<OperationResult> CreateProduct(ProductModel model);
    public Task<OperationResult> UpdateProduct(ProductModel model, int id);
    public Task<OperationResult> DeleteProduct(int id, int userId);
}
