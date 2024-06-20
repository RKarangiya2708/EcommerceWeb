using Ecommerce.API.Data.Models;
using Ecommerce.API.DTO.Interface;
using Ecommerce.API.Repository.Interface;

namespace Ecommerce.API.DTO.Service;

public class EcomUserDto : IEcomUserDto
{
    private readonly IEcomUserService _user;
 
    public EcomUserDto(IEcomUserService userService)
    {
        this._user = userService;
    }

    public Task<OperationResult<UserModel>> Login(LoginModel model)
    {
        return _user.Login(model);
    }

    public Task<OperationResult> RegisterUser(UserModel model)
    {
        return _user.RegisterUser(model);
    }
}
