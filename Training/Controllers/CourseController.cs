using System.Net;
using System;
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var item = await _service.GetCoursesAsync();

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

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var item = await _service.GetCourseAsync(id);

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
        public async Task<IHttpActionResult> Create(string name)
        {
            try
            {
                var course = new Course
                {
                    Name = name
                };

                _service.CreateCourse(course);

                return CreatedAtRoute("DefaultApi", new { id = course.Id }, course);
            }
            catch (Exception e)
            {
                // logError
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                _service.DeleteCourse(await _service.GetCourseAsync(id));
                return Ok();
            }
            catch (Exception e)
            {
                // logError
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update(int id, string name)
        {
            try
            {
                var course = await _service.GetCourseAsync(id);

                if (course is null)
                {
                    return NotFound();
                }

                course.Name = name;
                _service.UpdateCourse(course);
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
