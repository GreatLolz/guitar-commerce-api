using GuitarCommerceAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GuitarCommerceAPI.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IConfiguration configuration;

        public IdentityService(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtSettings:Key")!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var descriptor = new JwtSecurityToken(
                    issuer: configuration.GetValue<string>("JwtSettings:Issuer")!,
                    audience: configuration.GetValue<string>("JwtSettings:Audience")!,
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(descriptor);
        }
    }
}
