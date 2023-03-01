using Microsoft.IdentityModel.Tokens;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Training.Models;
using Training.Services;
using Type = Training.Models.Type;

namespace Training.Controllers
{
    [Authorize]
    [RoutePrefix("api/[controller]")]
    public class UserController : ApiController, IUserController
    {
        private readonly IUserService _service;
        private readonly ILogger _logger;

        public UserController(IUserService service, ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll(CancellationToken token)
        {
            try
            {
                var items = await _service.GetUsersAsync();

                if (items == null)
                {
                    return NotFound();
                }

                return Ok(items);
            }
            catch (Exception e)
            {
                _logger.Error(e, string.Empty);
                return StatusCode(HttpStatusCode.InternalServerError);
            }


        }


        [HttpGet]
        public async Task<IHttpActionResult> Get(int id, CancellationToken token)
        {
            try
            {
                var item = await _service.FindUserAsync(_ => _.Id == id);

                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            }
            catch (Exception e)
            {
                _logger.Error(e, string.Empty);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create(string firstName, string lastName, string phoneNumber, Type type, CancellationToken token)
        {
            try
            {
                var user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    Type = type
                };

                await _service.CreateUserAsync(user);

                return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
            }
            catch (Exception e)
            {
                _logger.Error(e, string.Empty);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id, CancellationToken token)
        {
            try
            {
                await _service.DeleteUserAsync(await _service.FindUserAsync(_ => _.Id == id));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e, string.Empty);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update(int id, [FromBody] User newUser)
        {
            try
            {
                newUser.Id = id;
                await _service.UpdateUserAsync(newUser);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e, string.Empty);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> Register([FromBody] User user)
        {
            try
            {
                if (await _service.FindUserAsync(_ => _.Email == user.Email) != null)
                {
                    return BadRequest("User already exists");
                }

                await _service.CreateUserAsync(user);

                return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, string.Empty);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest != null)
            {
                var user = await _service.FindUserAsync(_ =>
                    _.Username == loginRequest.Username && _.Password == loginRequest.Password);

                if (user != null)
                {
                    var key = "jwt_signing_secret_key";
                    var issuer = "http://localhost";
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var permClaims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Name, loginRequest.Username)
                    };

                    var token = new JwtSecurityToken(issuer,
                        issuer,
                        permClaims,
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: credentials);

                    user.Token = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(user);
                }
            }

            return BadRequest("Username or Password Invalid!");
        }

        [HttpGet]
        public async Task<IHttpActionResult> CreatePdf(CancellationToken token)
        {
            var stream = await PreparePdf();
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.GetBuffer())
            };

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "test.pdf"
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            var response = ResponseMessage(result);

            return response;
        }

        private Task<MemoryStream> PreparePdf()
        {
            return Task.Factory.StartNew(() =>
            {
                var doc = new PdfDocument();
                var page = doc.AddPage();
                var gfx = XGraphics.FromPdfPage(page);
                var font = new XFont("Verdana", 20, XFontStyle.Regular);

                gfx.DrawString("Hello World", font, XBrushes.Black,
                    new XRect(0, 0, page.Width, page.Height),
                    XStringFormats.Center);
                var stream = new MemoryStream();
                doc.Save(stream);

                return stream;
            });
        }
    }
}
