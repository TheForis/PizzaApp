using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PizzaApp.DomainModels.Entites;
using PizzaApp.Services.UserServices.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace PizzaApp.Services.UserServices.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<JwtSecurityToken> GenerateTokenAsync(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };
            var token = GetJWT(claims);
            return Task.FromResult(token);
        }

        private JwtSecurityToken GetJWT(List<Claim> claims)
        {
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:SecretTokenPhrase"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddYears(1) ,
                claims: claims,
                signingCredentials: new SigningCredentials(authSigninKey,SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
