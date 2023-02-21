using System.Web.Http;
using Training.Models;

namespace Training.Controllers
{
    public interface IUserController
    {
        IHttpActionResult Get(int id);
        IHttpActionResult Create(string firstName, string lastName, string phoneNumber, Type type);
        IHttpActionResult Delete(int id);
        IHttpActionResult Update(int id, string firstName, string lastName, string phoneNumber, Type type);
        IHttpActionResult GetAll();
    }
}