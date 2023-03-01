using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Training.Models;

namespace Training.Controllers
{
    public interface IUserController
    {
        Task<IHttpActionResult> Delete(int id, CancellationToken token);

        Task<IHttpActionResult> Get(int id, CancellationToken token);

        Task<IHttpActionResult> GetAll(CancellationToken token);

        Task<IHttpActionResult> Update(int id, [FromBody] User newUser, CancellationToken token);

        Task<IHttpActionResult> Register([FromBody] User user, CancellationToken token);

        Task<IHttpActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken token);
    }
}