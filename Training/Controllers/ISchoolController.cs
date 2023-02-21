using System.Web.Http;

namespace Training.Controllers
{
    public interface ISchoolController
    {
        IHttpActionResult Get(int id);
        IHttpActionResult Create(string name, string description, byte[] logo);
        IHttpActionResult Delete(int id);
        IHttpActionResult Update(int id, string name, string description, byte[] logo);
        IHttpActionResult GetAll();
    }
}