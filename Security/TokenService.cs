using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AquaMonitor.Api.Security
{
    public static class TokenService
    {
        public static string GenerateToken(string username)
        {
            var key = Encoding.ASCII.GetBytes("SUA_CHAVE_SECRETA_SUPER_FORTE_AQUI_123");

            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenConfig);

            return handler.WriteToken(token);
        }
    }
}
