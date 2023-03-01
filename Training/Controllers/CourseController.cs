using Serilog;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Training.Models;
using Training.Services;

namespace Training.Controllers
{
    [Authorize]
    [RoutePrefix("api/[controller]")]
    public class CourseController : ApiController, ICourseController
    {
        private readonly ICourseService _service;
        private readonly ILogger _logger;

        public CourseController(ICourseService service, ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll(CancellationToken token)
        {
            try
            {
                var item = await _service.GetCoursesAsync(token);

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

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id, CancellationToken token)
        {
            try
            {
                var item = await _service.GetCourseAsync(id, token);

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
        public async Task<IHttpActionResult> Create([FromBody] Course course, CancellationToken token)
        {
            try
            {
                await _service.CreateCourseAsync(course, token);

                return CreatedAtRoute("DefaultApi", new { id = course.Id }, course);
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
                await _service.DeleteCourseAsync(await _service.GetCourseAsync(id, token), token);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e, string.Empty);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update([FromBody] Course newCourse, CancellationToken token)
        {
            try
            {
                await _service.UpdateCourseAsync(newCourse, token);
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
