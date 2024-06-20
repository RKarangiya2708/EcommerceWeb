using Ecommerce.API.Data.Models;
using Ecommerce.API.DTO.Interface;
using Ecommerce.API.Repository.Interface;

namespace Ecommerce.API.DTO.Service;
public class EcomProductDto : IEcomProductDto
{
    private readonly IEcomProductService _product;
    public EcomProductDto(IEcomProductService productService)
    {
        _product = productService;
    }
    public Task<OperationResult> CreateProduct(ProductModel model) => _product.CreateProduct(model);

    public Task<OperationResult> DeleteProduct(int id, int userId) => _product.DeleteProduct(id, userId);

    public Task<OperationResult<List<ProductModel>>> GetProducts() => _product.GetProducts();

    public Task<OperationResult> UpdateProduct(ProductModel model, int id) => _product.UpdateProduct(model, id);
}