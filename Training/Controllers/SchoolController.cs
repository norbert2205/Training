using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Serilog;
using Training.Models;
using Training.Services;

namespace Training.Controllers
{
    [RoutePrefix("api/[controller]")]
    public class SchoolController : ApiController, ISchoolController
    {
        private readonly ISchoolService _service;
        private readonly ILogger _logger;

        public SchoolController(ISchoolService service, ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll(CancellationToken token)
        {
            try
            {
                var item = await _service.GetSchoolsAsync(token);

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
                var item = await _service.GetSchoolAsync(id, token);

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
        public async Task<IHttpActionResult> Create(School school, CancellationToken token)
        {
            try
            {
                await _service.CreateSchoolAsync(school, token);

                return CreatedAtRoute("DefaultApi", new { id = school.Id }, school);
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
                await _service.DeleteSchoolAsync(await _service.GetSchoolAsync(id, token), token);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e, string.Empty);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update([FromBody] School newSchool, CancellationToken token)
        {
            try
            {
                await _service.UpdateSchoolAsync(newSchool, token);
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
