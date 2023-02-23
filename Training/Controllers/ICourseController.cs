using System.Threading.Tasks;
using System.Web.Http;
using Training.Models;

namespace Training.Controllers
{
    public interface ICourseController
    {
        Task<IHttpActionResult> Get(int id);

        Task<IHttpActionResult> Create(string name);

        Task<IHttpActionResult> Delete(int id);

        Task<IHttpActionResult> GetAll();

        Task<IHttpActionResult> Update([FromBody] Course newCourse);
    }
}