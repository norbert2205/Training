using System.Web.Http;

namespace Training.Controllers
{
    public interface ICourseController
    {
        IHttpActionResult Get(int id);
        IHttpActionResult Create(string name);
        IHttpActionResult Delete(int id);
        IHttpActionResult Update(int id, string name);
        IHttpActionResult GetAll();
    }
}