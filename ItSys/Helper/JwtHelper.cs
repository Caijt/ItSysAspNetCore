using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ItSys.Helper
{
    public class JwtHelper
    {
        public static string CreateToken(int userId)
        {
            var SecretKey = AppSettingsHelper.Configuration["JwtSettings:SecretKey"];
            var Audience = AppSettingsHelper.Configuration["JwtSettings:Audience"];
            var Issuer = AppSettingsHelper.Configuration["JwtSettings:Issuer"];

            var claims = new Claim[] {
                new Claim("uid",userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,"caijt"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                claims: claims,
                issuer: Issuer,
                audience: Audience,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }
    }
}
