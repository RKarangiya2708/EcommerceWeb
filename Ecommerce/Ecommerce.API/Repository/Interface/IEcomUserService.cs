using Ecommerce.API.Data.Models;

namespace Ecommerce.API.Repository.Interface;
public interface IEcomUserService
{
    public Task<OperationResult> RegisterUser(UserModel model);
    public Task<OperationResult<UserModel>> Login(LoginModel model);
}
