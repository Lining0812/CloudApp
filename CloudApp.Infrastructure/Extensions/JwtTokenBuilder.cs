using CloudApp.Core.Confige;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CloudApp.Infrastructure.Extensions
{
    public static class JwtTokenBuilder
    {
        public static string BuildToken(List<Claim> claims, JwtSetting jwtSetting, TimeSpan? lifetime = null)
        {
            string key = jwtSetting.SecKey;
            DateTime expire = lifetime.HasValue
                ? DateTime.UtcNow.Add(lifetime.Value)
                : DateTime.UtcNow.AddSeconds(jwtSetting.ExpireSeconds);

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

    public static class RegisterTicketBuilder
    {
        public const string TypeClaim = "typ";
        public const string TypeValue = "register_ticket";
        public const string OpenIdClaim = "wx_openid";
        public const string UnionIdClaim = "wx_unionid";

        public static string Create(string openId, string? unionId, JwtSetting jwtSetting,
                                    TimeSpan? lifetime = null)
        {
            var claims = new List<Claim>
        {
            new Claim(TypeClaim, TypeValue),
            new Claim(OpenIdClaim, openId),
            new Claim(UnionIdClaim, unionId ?? string.Empty)
        };
            return JwtTokenBuilder.BuildToken(claims, jwtSetting, lifetime ?? TimeSpan.FromMinutes(10));
        }

        /// <summary>校验失败返回 null，不抛异常，由调用方决定文案</summary>
        public static RegisterTicketPayload? TryParse(string? ticket, JwtSetting jwtSetting)
        {
            if (string.IsNullOrWhiteSpace(ticket)) return null;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecKey));
            try
            {
                var principal = new JwtSecurityTokenHandler().ValidateToken(ticket,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        // 不给默认的 5 分钟宽限（那会把 10 分钟票据拖成 15 分钟），
                        // 但也要留 30 秒容差：云托管多副本部署时各实例时钟存在毫秒~秒级偏差，
                        // ClockSkew=0 会让"刚签发的票据"在快一点的实例上被判过期。
                        ClockSkew = TimeSpan.FromSeconds(30),
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key
                    }, out _);

                if (principal.FindFirst(TypeClaim)?.Value != TypeValue) return null;   // 关键校验

                var openId = principal.FindFirst(OpenIdClaim)?.Value;
                if (string.IsNullOrWhiteSpace(openId)) return null;

                var unionId = principal.FindFirst(UnionIdClaim)?.Value;
                return new RegisterTicketPayload
                {
                    OpenId = openId,
                    UnionId = string.IsNullOrWhiteSpace(unionId) ? null : unionId
                };
            }
            catch { return null; }   // 签名错 / 已过期 / 格式错
        }
    }

    public class RegisterTicketPayload
    {
        public string OpenId { get; set; } = string.Empty;
        public string? UnionId { get; set; }
    }
}
