using Alura.Usuarios.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Alura.Usuarios.API.Services
{
    public class TokenService
    {
        IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Usuario usuario)
        {
            Claim[] claims =
            [
                new Claim(ClaimTypes.Sid, usuario.Id),
                new Claim(ClaimTypes.NameIdentifier, usuario.UserName ?? string.Empty),
                new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString("d")),
                new Claim("loginTimeStamp", DateTime.UtcNow.AddHours(-3).ToString())
            ];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(10);

            var token = new JwtSecurityToken
                    (
                        expires: expires,
                        claims: claims,
                        signingCredentials: signingCredentials
                    );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}