using Ecommerce.API.Data.Entity;
using Ecommerce.API.Data.Models;
using Ecommerce.API.Helper;
using Ecommerce.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.API.Repository.Service;
public class EcomUserService : IEcomUserService
{
    private readonly EcommerceDbContext _db;
    private readonly IConfiguration _configuration;

    public EcomUserService(EcommerceDbContext db, IConfiguration configuration)
    {
        _db = db;
        _configuration = configuration;
    }

    public async Task<OperationResult> RegisterUser(UserModel model)
    {
        try
        {
            if (model.Email is not null && model.Username is not null)
            {
                var isuserExists = await _db.Users.Where(x => x.Username == model.Username || x.Email == model.Email).FirstOrDefaultAsync();
                if (isuserExists is not null)
                    return new OperationResult(false, "Username or Email address has already been registered.", StatusCodes.Status302Found);

                Data.Entity.DbSet.User user = new()
                {
                    Email = model.Email,
                    CreatedAt = DateTime.UtcNow,
                    Username = model.Username,
                    Password = AesEncryption.Encrypt(model.Password),
                    IsAdmin = model.IsAdmin
                };
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return new OperationResult(true, "User created Successfully", StatusCodes.Status201Created);
            }
            else
            {
                return new OperationResult(false, "Please enter valid data in all fields.", StatusCodes.Status400BadRequest);
            }
        }
        catch (Exception ex)
        {
            return new OperationResult(false, "Something went wrong! Please try again later.", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<OperationResult<UserModel>> Login(LoginModel model)
    {
        var response = new OperationResult<UserModel>();
        if (model != null)
        {
            try
            {
                var user = await _db.Users.Where(x => x.Username == model.Username).FirstOrDefaultAsync();
                if (user != null)
                {
                    var validpassword = await _db.Users.Where(x => x.Username == model.Username && x.Password == AesEncryption.Encrypt(model.Password)).FirstOrDefaultAsync();
                    if (validpassword is null)
                    {
                        response.Message = "Invalid Password";
                        response.StatusCode = StatusCodes.Status401Unauthorized;
                        response.IsSuccess = false;
                        return response;
                    }
                    else
                    {
                        var authClaims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user.Username),
                                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            };
                        var tokenExpiryTime = DateTime.Now.AddHours(1);
                        var token = GetToken(authClaims, tokenExpiryTime);
                        var refreshToken = GenerateRefreshToken();

                        int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                        response.IsSuccess = true;
                        response.StatusCode = StatusCodes.Status200OK;
                        response.Message = "Login successful.";
                        response.Data = new UserModel()
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            TokenExpiryTime= tokenExpiryTime.ToString(),
                            Email = user.Email,
                            Id = user.Id,
                            Username = user.Username,
                            IsAdmin = user.IsAdmin
                        };
                        return response;
                    }
                }

                response.Message = "Data Not Found";
                response.StatusCode = StatusCodes.Status404NotFound;
                response.IsSuccess = false;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Something went wrong! Please try again later.";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.IsSuccess = false;
                return response;
            }
        }
        response.Message = "Data Not Found";
        response.StatusCode = StatusCodes.Status404NotFound;
        response.IsSuccess = false;
        return response;
    }


    private JwtSecurityToken GetToken(List<Claim> authClaims, DateTime tokenExpiryTime)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: tokenExpiryTime,
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }


}
