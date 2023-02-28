using System;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Text;
using System.Web.Http;
using Serilog;

[assembly: OwinStartup(typeof(Training.Startup))]

namespace Training
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .Enrich.FromLogContext()
                .CreateLogger();

            try
            {
                var config = new HttpConfiguration();
                Bootstrapper.Bootstrap(config);
                WebApiConfig.Register(config);

                appBuilder.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "http://localhost",
                        ValidAudience = "http://localhost",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jwt_signing_secret_key"))
                    }
                });
                appBuilder.UseWebApi(config);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "INIT FAILED");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
