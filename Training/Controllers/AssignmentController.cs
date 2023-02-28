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
    [RoutePrefix("api/[controller]")]
    public class AssignmentController : ApiController, IAssignmentController
    {
        private readonly IAssignmentService _service;
        private readonly ILogger _logger;

        public AssignmentController(IAssignmentService service, ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id, CancellationToken token)
        {
            try
            {
                var item = await _service.GetAssignmentAsync(id);

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
        public async Task<IHttpActionResult> Create([FromBody] Assignment assignment, CancellationToken token)
        {
            try
            {
                await _service.CreateAssignmentAsync(assignment);

                return CreatedAtRoute("DefaultApi", new { id = assignment.Id }, assignment);
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
                await _service.DeleteAssignmentAsync(await _service.GetAssignmentAsync(id));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e, string.Empty);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update([FromBody] Assignment newAssignment)
        {
            try
            {
                await _service.UpdateAssignmentAsync(newAssignment);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e, string.Empty);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll(CancellationToken token)
        {
            try
            {
                var item = await _service.GetAssignmentsAsync();

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
    }
}
