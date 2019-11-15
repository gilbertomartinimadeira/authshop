using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Shop.Models;
using Microsoft.IdentityModel.Tokens;

namespace Shop.Services
{
    public static class TokenService
    {
        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            // um token descriptor tem 3 partes:
            // um subject
            // um expires
            // signin credentials
            var securityTokenDescription = new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(new Claim[]{

                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role,user.Role.ToString())

                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(securityTokenDescription);

            return tokenHandler.WriteToken(token);

        }
    }
}