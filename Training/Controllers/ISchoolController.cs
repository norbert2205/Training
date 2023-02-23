using System.Threading.Tasks;
using System.Web.Http;
using Training.Models;

namespace Training.Controllers
{
    public interface ISchoolController
    {
        Task<IHttpActionResult> Get(int id);

        Task<IHttpActionResult> Create(string name, string description, byte[] logo);

        Task<IHttpActionResult> Delete(int id);

        Task<IHttpActionResult> GetAll();

        Task<IHttpActionResult> Update([FromBody] School newSchool);
    }
}