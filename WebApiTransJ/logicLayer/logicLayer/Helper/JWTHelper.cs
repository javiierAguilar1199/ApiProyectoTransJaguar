using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Helper
{
    public class JWTHelper
    {
        public string CreateToken(string username, string roles, string direccion,string Correo, string Nombre, string secretKey)
        {

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, username));
            claims.AddClaim(new Claim(ClaimTypes.Name, Nombre));
            claims.AddClaim(new Claim(ClaimTypes.Email, Correo));
         



            string[] arrayRols = roles.Split(';');
        
            /*Roles*/
            foreach (string rol in arrayRols)
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, rol));
            }

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = claims,
                //Expires = DateTime.UtcNow.AddHours(.20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(createdToken);
        }
    }
}