using System.Web.Http;

namespace Training.Controllers
{
    public interface IAssignmentController
    {
        IHttpActionResult Get(int id);
        IHttpActionResult Create(string name, string question, string answer, string correctAnswer, int grade);
        IHttpActionResult Delete(int id);
        IHttpActionResult Update(int id, string name, string question, string answer, string correctAnswer, int grade);
        IHttpActionResult GetAll();
    }
}