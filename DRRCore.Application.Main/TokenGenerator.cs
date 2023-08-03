using DRRCore.Application.Interfaces;
using DRRCore.Transversal.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DRRCore.Application.Main
{
    public class TokenGenerator : ITokenGenerator
    {

        private readonly IConfiguration Configuration;
        public TokenGenerator(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<Response<string>> GetTokenAsync()
        {
            var response = new Response<string>();

            try
            {
                var claims = new[]
                  {
                     new Claim(ClaimTypes.Name, "RolEjemplo")
                   };

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer= Configuration["Jwt:Issuer"],
                    Audience= Configuration["Jwt:Audience"],
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                response.Data = tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }
    }
}
