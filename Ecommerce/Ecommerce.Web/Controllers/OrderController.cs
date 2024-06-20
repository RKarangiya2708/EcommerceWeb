using Ecommerce.Web.Helper;
using Ecommerce.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Ecommerce.Web.Controllers;

[SessionValidation]
public class OrderController : Controller
{
    public enum Status
    {
        Pending = 1,
        Processing = 2,
        Shipped = 3,
    }

    private readonly IApiHelper _apiHelper;
    public OrderController(IApiHelper apiHelper)
    {
        _apiHelper = apiHelper;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<APIResponseResult<List<OrderModel>>> GetOrders()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var responseData = await _apiHelper.MakeApiCallAsync("orders?userId=" + userId, HttpMethod.Get, HttpContext, null);
        var responseAsync = await CommonMethod.HandleApiResponseAsync<List<OrderModel>>(responseData);
        return responseAsync;
    }

    [HttpPost]
    public async Task<APIResponseResult<List<OrderItemModel>>> GetOrderDetails([FromBody] OrderModel model)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var responseData = await _apiHelper.MakeApiCallAsync("orders/" + model.Id, HttpMethod.Get, HttpContext, null);
        var responseAsync = await CommonMethod.HandleApiResponseAsync<List<OrderItemModel>>(responseData);
        return responseAsync;
    }

    [HttpPost]
    public async Task<APIResponseResult<string>> CreateOrder([FromBody] OrderModel model)
    {
        model.Status = Status.Processing.ToString();
        model.UserId = HttpContext.Session.GetInt32("UserId").Value;
        var responseData = await _apiHelper.MakeApiCallAsync("orders", HttpMethod.Post, HttpContext, model);
        var responseAsync = await CommonMethod.HandleApiResponseAsync<string>(responseData);
        return responseAsync;
    }
}