using System.Threading.Tasks;
using System.Web.Http;

namespace Training.Controllers
{
    public interface ICourseController
    {
        Task<IHttpActionResult> Get(int id);

        Task<IHttpActionResult> Create(string name);

        Task<IHttpActionResult> Delete(int id);

        Task<IHttpActionResult> Update(int id, string name);

        Task<IHttpActionResult> GetAll();
    }
}