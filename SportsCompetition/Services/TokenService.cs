using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SportsCompetition.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutorisationApi.Services
{
    public class TokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration)
        { 
            _key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["TokenSecret"]));
        }
        public string CreateToken(IdentityUser<Guid> user)
        {
            var claims = new List<Claim>()
            {
                new (JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new (JwtRegisteredClaimNames.UniqueName, user.UserName),
            };

            var creds = new SigningCredentials(_key,
                SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
