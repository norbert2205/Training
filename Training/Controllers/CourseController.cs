using System.Web.Http;
using Training.Models;
using Training.Services;

namespace Training.Controllers
{
    [RoutePrefix("api/[controller]")]
    public class CourseController : ApiController, ICourseController
    {
        private readonly ICourseService _service;

        public CourseController(ICourseService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var item = _service.GetCourses();

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var item = _service.GetCourse(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IHttpActionResult Create(string name)
        {
            var course = new Course
            {
                Name = name
            };

            _service.InsertCourse(course);

            return CreatedAtRoute("DefaultApi", new { id = course.Id }, course);
        }

        [HttpPost]
        public IHttpActionResult Delete(int id)
        {
            _service.DeleteCourse(_service.GetCourse(id));
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update(int id, string name)
        {
            var course = _service.GetCourse(id);

            if (course is null)
            {
                return NotFound();
            }

            course.Name = name;
            _service.UpdateCourse(course);
            return Ok();
        }
    }
}
