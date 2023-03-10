using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Serilog;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Training.Authorization;
using Training.Models;
using Training.Services;

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
                var items = await _service.GetUsersAsync(token);

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
                var item = await _service.FindUserAsync(_ => _.Id == id, token);

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

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id, CancellationToken token)
        {
            try
            {
                await _service.DeleteUserAsync(await _service.FindUserAsync(_ => _.Id == id, token), token);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e, string.Empty);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update(int id, [FromBody] User newUser, CancellationToken token)
        {
            try
            {
                newUser.Id = id;
                await _service.UpdateUserAsync(newUser, token);
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
        public async Task<IHttpActionResult> Register([FromBody] User user, CancellationToken token)
        {
            try
            {
                if (await _service.FindUserAsync(_ => _.Email == user.Email, token) != null)
                {
                    return BadRequest("User already exists");
                }
                
                //TODO: handle assigning permissions
                user.Type = UserType.Student;

                await _service.CreateUserAsync(user, token);

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
        public async Task<IHttpActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken token)
        {
            if (loginRequest != null)
            {
                var user = await _service.FindUserAsync(_ =>
                    _.Username == loginRequest.Username && _.Password == loginRequest.Password, token);

                if (user != null)
                {
                    user.Token = JwtHelper.CreateJwt(loginRequest);

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
