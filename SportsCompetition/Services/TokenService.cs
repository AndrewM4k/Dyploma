using Microsoft.IdentityModel.Tokens;
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
            var a = configuration["TokenSignKey"];
            _key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(a));
        }
        public string CreateToken(string username)
        {
            var claims = new List<Claim>()
            {
                new (JwtRegisteredClaimNames.NameId, username)
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
