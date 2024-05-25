using Microsoft.AspNetCore.Mvc;

namespace MSISTORE.WEB.Areas.Admin.Controllers
{
    public class StatsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
