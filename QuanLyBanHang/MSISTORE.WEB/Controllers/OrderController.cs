using Common.Req;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MSISTORE.WEB.Controllers
{
    public class OrderController : Controller
    {
        [HttpPost("order")]
        [Authorize]
        public IActionResult CreateOrder([FromBody] OrderRequest orderRequest)
        {
            return View(orderRequest);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
