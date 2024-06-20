using Ecommerce.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[SessionValidation]
public class CartController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
