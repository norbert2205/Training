using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Training.Models;

namespace Training.Controllers
{
    public interface ICourseController
    {
        Task<IHttpActionResult> Get(int id, CancellationToken token);

        Task<IHttpActionResult> Create(Course course, CancellationToken token);

        Task<IHttpActionResult> Delete(int id, CancellationToken token);

        Task<IHttpActionResult> GetAll(CancellationToken token);

        Task<IHttpActionResult> Update([FromBody] Course newCourse, CancellationToken token);
    }
}