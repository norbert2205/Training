using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Training.Models;
using Training.Services;
using Type = Training.Models.Type;

namespace Training.Controllers
{
    [RoutePrefix("api/[controller]")]
    public class UserController : ApiController, IUserController
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
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
                // logError
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
                // logError
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

                _service.CreateUser(user);

                return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
            }
            catch (Exception e)
            {
                // logError
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id, CancellationToken token)
        {
            try
            {
                _service.DeleteUser(await _service.GetUserAsync(id));
                return Ok();
            }
            catch (Exception e)
            {
                // logError
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update(int id, string firstName, string lastName, string phoneNumber, Type type, CancellationToken token)
        {
            try
            {
                var user = await _service.GetUserAsync(id);

                if (user is null)
                {
                    return NotFound();
                }

                user.FirstName = firstName;
                user.LastName = lastName;
                user.PhoneNumber = phoneNumber;
                user.Type = type;

                _service.UpdateUser(user);
                return Ok();
            }
            catch (Exception e)
            {
                // logError
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}
