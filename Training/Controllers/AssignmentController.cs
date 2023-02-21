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
        public IHttpActionResult GetAll()
        {
            var item = _service.GetAssignments();

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var item = _service.GetAssignment(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IHttpActionResult Create(string name, string question, string answer, string correctAnswer, int grade)
        {
            var assignment = new Assignment
            {
                Name = name,
                Question = question,
                Answer = answer,
                CorrectAnswer = correctAnswer,
                Grade = grade
            };

            _service.InsertAssignment(assignment);

            return CreatedAtRoute("DefaultApi", new { id = assignment.Id }, assignment);
        }

        [HttpPost]
        public IHttpActionResult Delete(int id)
        {
            _service.DeleteAssignment(_service.GetAssignment(id));
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update(int id, string name, string question, string answer, string correctAnswer, int grade)
        {
            var assignment = _service.GetAssignment(id);

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
    }
}
