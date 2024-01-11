using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class TokenService
    {
        public Token CreateToken(CustomIdentityUser usuario, List<string> roles)
        {
            List<Claim> direitosUsuario = new List<Claim>
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString()),
                //new Claim(ClaimTypes.Role, role)
            };
            foreach(var role in roles)
            {
                direitosUsuario.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn"));

            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: direitosUsuario,
                signingCredentials: credenciais,
                expires: DateTime.UtcNow.AddHours(1)
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenString);
        }
    }
}
