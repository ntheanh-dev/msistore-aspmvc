using Microsoft.AspNetCore.Mvc;

namespace MSISTORE.WEB.Controllers
{
    public class UserController : Controller
    {

        [HttpPost("create-user")]
        public IActionResult CreateUser()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
