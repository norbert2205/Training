using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
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
            var loginResponse = new LoginResponse
            {
                AccessTokenExpiry = DateTime.UtcNow
            };
            var isUsernamePasswordValid = false;

            if (login != null)
            {
                // make await call to the Database to check username and password. here we only checking if password value is admin
                isUsernamePasswordValid = login.Password == "admin";
            }
            // if credentials are valid
            if (isUsernamePasswordValid)
            {
                var token = CreateToken(login.User);
                loginResponse.Token = token;
                loginResponse.RefreshToken = new RefreshToken
                {
                    Expiration = DateTime.UtcNow
                };
                loginResponse.RefreshToken = CreateRefreshToken(login.User);
                loginResponse.UserId = login.User;

                _accountService.RecordUserLogin(loginResponse);

                //return the token
                return Ok(new { loginResponse });
            }

            // if username/password are not valid send unauthorized status code in response               
            return BadRequest("Username or Password Invalid!");
        }

        [HttpPost]
        public async Task<IHttpActionResult> RefreshToken([FromBody] LoginRequest refreshTokenResource)
        {
            // find username + accessToken combination in the database

            var user = await _accountService.GetUser(_ => _.UserId == refreshTokenResource.User);
            var refreshToken = await _accountService.GetRefreshTokenDetail(_ => _.User == refreshTokenResource.User);

            if (user == null) { return BadRequest(); }
            if (refreshToken == null) { return BadRequest(); }
            if (user.Token != refreshTokenResource.Token) { return BadRequest("Not a Valid Access Token"); }

            if (refreshToken.Expiration < DateTime.Now)
            {

                await _accountService.RemoveRefreshToken(refreshTokenResource.User);
                return BadRequest("Refresh Token Expired. Please Login again");
            }

            //Update the user access token + refresh token

            var loginResponse = new LoginResponse
            {
                AccessTokenExpiry = DateTime.UtcNow
            };

            var token = CreateToken(refreshTokenResource.User);
            loginResponse.AccessTokenExpiry = DateTime.Now.AddMinutes(2);
            loginResponse.Token = token;
            loginResponse.RefreshToken = new RefreshToken();
            loginResponse.RefreshToken = refreshToken;
            loginResponse.UserId = refreshTokenResource.User;

            //update database with new access token/refresh token combination for the user.
            await _accountService.RecordUserLogin(loginResponse);


            return Ok(new { loginResponse });

        }

        [HttpPost]
        public IHttpActionResult RevokeToken([FromBody] LoginRequest resource)
        {
            _accountService.RemoveRefreshToken(resource.User);
            return Ok();
        }

        private string CreateToken(string username)
        {
            var issuedAt = DateTime.UtcNow;
            var expires = DateTime.UtcNow.AddMinutes(10);
            var tokenHandler = new JwtSecurityTokenHandler();

            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            });

            const string sec = "501b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);


            var token = tokenHandler.CreateJwtSecurityToken("localhost", "localhost",
                        claimsIdentity, issuedAt, expires, issuedAt, signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        private RefreshToken CreateRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                TokenType = "Refresh",
                Expiration = DateTime.Now.AddMinutes(5),
                User = username
            };

            return refreshToken;
        }

    }
}