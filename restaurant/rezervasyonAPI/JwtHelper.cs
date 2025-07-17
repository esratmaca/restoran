using Microsoft.IdentityModel.Tokens; 
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace rezervasyonAPI
{

    public class JwtHelper
    {

        public static string GenerateToken(string username, string role, int userId, int expireMinutes = 60)
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = DateTime.UtcNow.AddMinutes(expireMinutes);

            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("userId", userId.ToString()) 
            };

           
            if (!string.IsNullOrEmpty(role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); 
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);

           
            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(sec));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            
            const string issuer = "http://localhost:44363";
            const string audience = "http://localhost:44363";

            var token = (JwtSecurityToken)
                            tokenHandler.CreateJwtSecurityToken(
                                issuer: issuer,
                                audience: audience,
                                subject: claimsIdentity,
                                notBefore: issuedAt,
                                expires: expires,
                                signingCredentials: signingCredentials);

            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}