using PizzaApp.DomainModels.Entites;
using System.IdentityModel.Tokens.Jwt;

namespace PizzaApp.Services.UserServices.Interfaces
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> GenerateTokenAsync(User user);
    }
}
