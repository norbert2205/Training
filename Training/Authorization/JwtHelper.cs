using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Training.Models;

namespace Training.Authorization
{
    public static class JwtHelper
    {
        public static string CreateJwt(LoginRequest loginRequest)
        {
            var key = "jwt_signing_secret_key";
            var issuer = "http://localhost";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, loginRequest.Username)
            };

            var token = new JwtSecurityToken(issuer,
                issuer,
                permClaims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}