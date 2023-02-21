using System.Collections.Generic;
using System.Web.Http;
using Training.Models;
using Training.Services;

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
        public IHttpActionResult GetAll()
        {
            var item = _service.GetUsers();

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }


        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var item = _service.GetUser(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IHttpActionResult Create(string firstName, string lastName, string phoneNumber, Type type)
        {
            var assignment = new Assignment
            {
                Question = "test?",
                Answer = "test",
                CorrectAnswer = "test2",
                Grade = 2
            };

            var course = new Course
            {
                Name = "testCourse"
            };

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Type = type
            };

            _service.InsertUser(user);

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        [HttpPost]
        public IHttpActionResult Delete(int id)
        {
            _service.DeleteUser(_service.GetUser(id));
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update(int id, string firstName, string lastName, string phoneNumber, Type type)
        {
            var user = _service.GetUser(id);

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
    }
}
