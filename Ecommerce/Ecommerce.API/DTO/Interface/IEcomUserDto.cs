using Ecommerce.API.Data.Models;

namespace Ecommerce.API.DTO.Interface;

public interface IEcomUserDto
{
    public Task<OperationResult> RegisterUser(UserModel model);
    public Task<OperationResult<UserModel>> Login(LoginModel model);
}
