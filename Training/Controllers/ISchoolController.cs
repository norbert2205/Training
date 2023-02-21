using System.Threading.Tasks;
using System.Web.Http;

namespace Training.Controllers
{
    public interface ISchoolController
    {
        Task<IHttpActionResult> Get(int id);

        Task<IHttpActionResult> Create(string name, string description, byte[] logo);

        Task<IHttpActionResult> Delete(int id);

        Task<IHttpActionResult> Update(int id, string name, string description, byte[] logo);

        Task<IHttpActionResult> GetAll();
    }
}