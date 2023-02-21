using System.Net;
using System;
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var item = await _service.GetSchoolsAsync();

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
                var item = await _service.GetSchoolAsync(id);

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
        public async Task<IHttpActionResult> Create(string name, string description, byte[] logo)
        {
            try
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
                _service.DeleteSchool(await _service.GetSchoolAsync(id));
                return Ok();
            }
            catch (Exception e)
            {
                // logError
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update(int id, string name, string description, byte[] logo)
        {
            try
            {
                var school = await _service.GetSchoolAsync(id);

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
            catch (Exception e)
            {
                // logError
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}
