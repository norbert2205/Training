using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Text;

[assembly: OwinStartup(typeof(Training.Startup))]

namespace Training
{
    public class Startup
    {
        // public void Configuration(IAppBuilder app)
        // {
        //     app.UseJwtBearerAuthentication(
        //         new JwtBearerAuthenticationOptions
        //         {
        //             AuthenticationMode = AuthenticationMode.Active,
        //             TokenValidationParameters = new TokenValidationParameters()
        //             {
        //                 ValidateIssuer = true,
        //                 ValidateAudience = true,
        //                 ValidateIssuerSigningKey = true,
        //                 ValidIssuer = "http://mysite.com", //some string, normally web url,  
        //                 ValidAudience = "http://mysite.com",
        //                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_secret_key_12345"))
        //             }
        //         });
        // }
    }
}
