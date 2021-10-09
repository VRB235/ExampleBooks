using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication1;

namespace BackendBanplusprSAC.Utils
{
    public class Cryptography
    {
        private static IConfiguration _configuration = Startup.StaticConfig;
        /// <summary>
        /// Generador de JWT Token
        /// </summary>
        /// <param name="fullName">Nombre completo</param>
        /// <param name="user">Usuario</param>
        /// <param name="id">Identificador del usuario</param>
        /// <returns>string con token</returns>
        public static string GenerateToken(string user, int userId)
        {
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user),
                new Claim("UserId", userId.ToString())
            };

            ClaimsIdentity claimIdentity = new ClaimsIdentity(claims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimIdentity,
                Expires = DateTime.UtcNow.AddMinutes(160),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);
        }
    }
}
