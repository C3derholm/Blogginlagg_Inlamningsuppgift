using Blogginlägg_Inlämningsuppgift.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blogginlägg_Inlämningsuppgift.Core.Services
{
    public class TokenService : ITokenService
    {
        private const string Issuer = "http://localhost:5205";
        private const string Audience = "http://localhost:5205";
        private const string Key = "mykey1234567&%%485734579453%&//1255362";
        public string CreateToken(int userId, string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, username)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var signingCredentials =
                new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
