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
                var item = await _service.GetUserAsync(id);

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
                // var assignment = new Assignment
                // {
                //     Question = "test?",
                //     Answer = "test",
                //     CorrectAnswer = "test2",
                //     Grade = 2
                // };
                //
                // var course = new Course
                // {
                //     Name = "testCourse"
                // };

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

        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id, CancellationToken token)
        {
            try
            {
                await _service.DeleteUserAsync(await _service.GetUserAsync(id));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e, string.Empty);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update([FromBody] User newUser)
        {
            try
            {
                await _service.UpdateUserAsync(newUser);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e, string.Empty);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> CreatePdf(CancellationToken token)
        {
            var stream = await PreparePdf();
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.GetBuffer())
            };

            // result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            // {
            //     FileName = "test.pdf"
            // };

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
