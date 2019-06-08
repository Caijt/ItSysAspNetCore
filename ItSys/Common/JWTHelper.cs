using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace ItSys.Common
{
    public class JWTHelper
    {
        public static string CreateToken2(Dictionary<string, object> payLoad, int expiresMinute, Dictionary<string, object> header = null)
        {
            if (header == null)
            {
                header = new Dictionary<string, object>
                {
                    { "alg", "HS256" },
                    { "typ", "JWT" }
                };
            }
            var now = DateTime.Now;

            payLoad.Add("expires", now.Add(TimeSpan.FromMinutes(expiresMinute)));
            var encodeHeader = Base64UrlEncoder.Encode(JsonConvert.SerializeObject(header));
            var encodePayLoad = Base64UrlEncoder.Encode(JsonConvert.SerializeObject(payLoad));
            var hs256 = new HMACSHA256(Encoding.ASCII.GetBytes("admincai"));
            var encodeSignature = Base64UrlEncoder.Encode(hs256.ComputeHash(Encoding.UTF8.GetBytes($"{encodeHeader}.{encodePayLoad}")));
            var encodeJwt = $"{encodeHeader}.{encodePayLoad}.{encodeSignature}";
            return encodeJwt;
        }

        public static string CreateToken()
        {

            var claims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.Sub,"caijt"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("helloworld-----cjt"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(        
                claims: claims,
                issuer: "caijt.com",
                audience: "caijt.com",
                notBefore: new DateTime(2019, 4, 13, 22, 47, 00),
                expires: new DateTime(2019,4,13, 22,47, 30),
                signingCredentials:creds
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }
    }
}
