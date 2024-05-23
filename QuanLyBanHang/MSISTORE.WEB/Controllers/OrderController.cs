using BLL;
using Common.Req.OrderReq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace MSISTORE.WEB.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService orderService;
        public OrderController()
        {
            this.orderService = new OrderService();
        }
        [HttpPost("order")]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
        {
            if (!User.Identity.IsAuthenticated || User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


           var orderDto =  await orderService.createOrderAsync(userId, new List<OrderRequest> { orderRequest });

            return Ok(orderDto);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
