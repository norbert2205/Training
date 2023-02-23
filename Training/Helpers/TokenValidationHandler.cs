using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Training.Helpers
{
    public class TokenValidationHandler : DelegatingHandler
    {
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;

            if (!request.Headers.TryGetValues("Authorization", out var authorizationHeaders)
                || authorizationHeaders.Count() > 1)
            {
                return false;
            }

            var bearerToken = authorizationHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;

            return true;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;

            //determine whether a jwt exists or not
            if (!TryRetrieveToken(request, out var token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                return base.SendAsync(request, cancellationToken);
            }

            try
            {
                const string sec =
                    "501b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
                var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(sec));
                var handler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidAudience = "localhost",
                    ValidIssuer = "localhost",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = LifeTimeValidator,
                    IssuerSigningKey = securityKey
                };

                Thread.CurrentPrincipal = handler.ValidateToken(token, validationParameters, out var securityToken);
                HttpContext.Current.User = handler.ValidateToken(token, validationParameters, out securityToken);

                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException e)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception e)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }

            //Since this solution pushes the request further down the pipeline, instead of immediately returning, this 
            //solution consumes more resources for each request, so this might not be the right solution for everybody.
            //Alternatively one could manually add the missing headers before returning,
            //but I don't think that would be a good idea.

            //- this line would return correct CORS header.- credit to 
            // return base.SendAsync(request, cancellationToken);

            return Task<HttpResponseMessage>.Factory.
                StartNew(() => new HttpResponseMessage(statusCode), cancellationToken);
        }

        private bool LifeTimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }

            return false;
        }
    }
}