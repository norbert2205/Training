using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Training.Models;

namespace Training.Controllers
{
    public interface IAssignmentController
    {
        Task<IHttpActionResult> Get(int id, CancellationToken token);

        Task<IHttpActionResult> Create(Assignment assignment, CancellationToken token);

        Task<IHttpActionResult> Delete(int id, CancellationToken token);

        Task<IHttpActionResult> GetAll(CancellationToken token);

        Task<IHttpActionResult> Update([FromBody] Assignment newAssignment);
    }
}