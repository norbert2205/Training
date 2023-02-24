using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Serilog;
using Training.Models;
using Training.Services;
using System.Net;

namespace Training.Controllers
{
    [RoutePrefix("api/[controller]")]
    public class AccountController : ApiController
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public AccountController(IUserService userService, ILogger logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                if (await _userService.FindUserAsync(_ => _.Email == registerRequest.Email) != null)
                {
                    return BadRequest("User already exists");
                }

                var user = new User
                {
                    FirstName = registerRequest.FirstName,
                    LastName = registerRequest.LastName,
                    Email = registerRequest.Email,
                    Login = registerRequest.Login,
                    Password = registerRequest.Password
                };

                await _userService.CreateUserAsync(user);

                return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, string.Empty);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest != null && await _userService.IsValidLoginAsync(loginRequest))
            {
                var key = "jwt_signing_secret_key";
                var issuer = "http://localhost";
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var permClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, loginRequest.Login)
                };

                var token = new JwtSecurityToken(issuer,
                    issuer,
                    permClaims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }

            return BadRequest("Username or Password Invalid!");
        }
    }
}