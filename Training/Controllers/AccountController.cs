using System.Web.Http;

namespace Training.Controllers
{
    [Authorize]
    [RoutePrefix("api/[controller]")]
    public class AccountController : ApiController
    {
        // [HttpPost]
        // public IHttpActionResult Register(string name, string password)
        // {
        //     // var user = 
        // }
    }
}