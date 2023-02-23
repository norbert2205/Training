using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Serilog;
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
    }
}
