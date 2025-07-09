using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace rezervasyonAPI.Token
{
    public class JwtAuthenticationFilter : AuthorizationFilterAttribute
    {
        private const string Secret = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            var headers = request.Headers;

            if (!headers.Contains("Authorization"))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("Authorization header eksik.")
                };
                return;
            }

            var tokenRaw = headers.Authorization?.Parameter;

            if (string.IsNullOrEmpty(tokenRaw))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("Token boş.")
                };
                return;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "http://localhost:44363",

                    ValidateAudience = true,
                    ValidAudience = "http://localhost:44363",

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(Secret)),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(tokenRaw, validationParameters, out validatedToken);

                HttpContext.Current.User = principal;
            }
            catch (Exception)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("Geçersiz token.")
                };
            }
        }
    }
}