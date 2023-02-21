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

        public AssignmentController(IAssignmentService service)
        {
            _service = service;
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
                // logError
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create(string name, string question, string answer, string correctAnswer, int grade, CancellationToken token)
        {
            try
            {
                var assignment = new Assignment
                {
                    Name = name,
                    Question = question,
                    Answer = answer,
                    CorrectAnswer = correctAnswer,
                    Grade = grade
                };

                _service.CreateAssignment(assignment);

                return CreatedAtRoute("DefaultApi", new { id = assignment.Id }, assignment);
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
                _service.DeleteAssignment(await _service.GetAssignmentAsync(id));
                return Ok();
            }
            catch (Exception e)
            {
                // logError
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update(int id, string name, string question, string answer, string correctAnswer, int grade,
            CancellationToken token)
        {
            try
            {
                var assignment = await _service.GetAssignmentAsync(id);

                if (assignment is null)
                {
                    return NotFound();
                }

                assignment.Name = name;
                assignment.Question = question;
                assignment.Answer = answer;
                assignment.CorrectAnswer = correctAnswer;
                assignment.Grade = grade;
                _service.UpdateAssignment(assignment);
                return Ok();
            }
            catch (Exception e)
            {
                // logError
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
                // logError
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}
