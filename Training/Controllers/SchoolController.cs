using System.Web.Http;
using Training.Models;
using Training.Services;

namespace Training.Controllers
{
    [RoutePrefix("api/[controller]")]
    public class SchoolController : ApiController, ISchoolController
    {
        private readonly ISchoolService _service;

        public SchoolController(ISchoolService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var item = _service.GetSchools();

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }        
        
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var item = _service.GetSchool(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IHttpActionResult Create(string name, string description, byte[] logo)
        {
            var school = new School
            {
                Name = name,
                Description = description,
                Logo = logo
            };

            _service.InsertSchool(school);

            return CreatedAtRoute("DefaultApi", new { id = school.Id }, school);
        }

        [HttpPost]
        public IHttpActionResult Delete(int id)
        {
            _service.DeleteSchool(_service.GetSchool(id));
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update(int id, string name, string description, byte[] logo)
        {
            var school = _service.GetSchool(id);

            if (school is null)
            {
                return NotFound();
            }

            school.Name = name;
            school.Description = description;
            school.Logo = logo;
            _service.UpdateSchool(school);
            return Ok();
        }
    }
}
