using jwt.redis.netcoreapi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace jwt.redis.netcoreapi.Filters
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var result = true;

            if (!context.HttpContext.Request.Headers.ContainsKey("token"))
                result = false;

            if (result)
            {
                var accessToken = context.HttpContext.Request.Headers["token"].FirstOrDefault()?.Split(" ").Last();
                var userId = GetUserId(accessToken);
                if (accessToken == GetToken(userId))
                {
                    SetToken(userId, accessToken);
                    context.HttpContext.Items["User"] = userId;
                    return;
                }
                else
                {
                    result = false;
                }

            }
            context.Result = new ContentResult
            {
                Content = "Token Doğrulanamadı",
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.Unauthorized
            };

        }
        private int GetUserId(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("2b1b4477-1388-4905-a5da-f5e6e05050f2");
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            return userId;
        }

        private string GetToken(int userId)
        {
            string key = $"token_{userId}";
            var result = RedisStore.RedisCache.StringGet(key);
            if (result.HasValue)
                return result.ToString().Trim('"');
            return string.Empty;
        }

        private string SetToken(int userId, string token)
        {
            string key = $"token_{userId}";
            var redis = RedisStore.RedisCache;
            redis.StringSet(key, token, new TimeSpan(2, 0, 0, 0));
            return token;
        }
    }
}
