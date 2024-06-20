using Ecommerce.API.Data.Models;
using Ecommerce.API.DTO.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("api/")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IEcomProductDto _product;

    [HttpGet]
    [Route("products")]
    public async Task<OperationResult<List<ProductModel>>> GetProducts() {
        return await _product.GetProducts();
    }

    [HttpPost]
    [Route("products")]
    public async Task<OperationResult> CreateProduct(ProductModel model)
    {
        return await _product.CreateProduct(model);
    }

    [HttpPut]
    [Route("products/{id}")]
    public async Task<OperationResult> UpdateProduct(ProductModel model, int id)
    {
        return await _product.UpdateProduct(model, id);
    }

    [HttpDelete]
    [Route("products/{id}")]
    public async Task<OperationResult> DeleteProduct(int id, int userId)
    {
        return await _product.DeleteProduct(id, userId);
    }

    public ProductController(IEcomProductDto productDto) => _product = productDto;
}
