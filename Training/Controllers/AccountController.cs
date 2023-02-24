using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using Training.Models;
using Training.Services;

namespace Training.Controllers
{
    [RoutePrefix("api/[controller]")]
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IHttpActionResult Authenticate([FromBody] LoginRequest login)
        {
            var isUsernamePasswordValid = false;

            if (login != null)
            {
                // make await call to the Database to check username and password. here we only checking if password value is admin
                isUsernamePasswordValid = login.Password == "admin";
            }
            // if credentials are valid
            if (isUsernamePasswordValid)
            {
                var key = "jwt_signing_secret_key"; //Secret key which will be used later during validation    
                var issuer = "http://localhost";

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var permClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, login.User)
                };

                var token = new JwtSecurityToken(issuer,
                    issuer,
                    permClaims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials);
                
                //return the token
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }

            // if username/password are not valid send unauthorized status code in response               
            return BadRequest("Username or Password Invalid!");
        }
    }
}