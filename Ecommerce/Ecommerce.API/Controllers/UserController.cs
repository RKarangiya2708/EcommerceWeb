using Ecommerce.API.Data.Models;
using Ecommerce.API.DTO.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IEcomUserDto _user;

    [HttpPost]
    [Route("Register")]
    public async Task<OperationResult> Register([FromBody] UserModel model)
    {
        return await _user.RegisterUser(model);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<OperationResult<UserModel>> LoginUser([FromBody] LoginModel model)
    {
        return await _user.Login(model);
    }

    public UserController(IEcomUserDto user) => _user = user;
}
