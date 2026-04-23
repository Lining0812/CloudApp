using CloudApp.Core.Confige;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CloudApp.Infrastructure.Extensions
{
    public static class JwtTokenBuilder
    {
        public static string BuildToken(List<Claim> claims,JwtSetting jwtSetting)
        {
            string key = jwtSetting.SecKey;
            DateTime expire = DateTime.UtcNow.AddSeconds(jwtSetting.ExpireSeconds);

            byte[] secBytes = Encoding.UTF8.GetBytes(key);
            var secKey = new SymmetricSecurityKey(secBytes);
            var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: expire,
                signingCredentials: credentials
            );
            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
