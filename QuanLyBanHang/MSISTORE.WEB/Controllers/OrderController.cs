using AutoMapper;
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
        private readonly OrderitemService orderitemService;
        public OrderController(IMapper mapper)
        {
            this.orderService = new OrderService(mapper);
            this.orderitemService = new OrderitemService();
        }
        [HttpPost("order")]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest orderRequests)
        {
            if (!User.Identity.IsAuthenticated || User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


           var orderDto =  await orderService.createOrderAsync(userId, orderRequests);
            return Ok(orderDto);
        }

        [HttpGet("order")]
        [Authorize]
        public async Task<IActionResult> GetOrdersByUser()
        {
            if (!User.Identity.IsAuthenticated || User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var listOrder = await orderService.GetOrdersByUser(userId);
            return Ok(listOrder);
        }

        [HttpGet("order/{orderId:long}")]
        [Authorize]
        public async Task<IActionResult> GetItems([FromRoute] long orderId)
        {
            if (!User.Identity.IsAuthenticated || User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var items = await orderitemService.GetItems(orderId, userId);
            return Ok(items); 
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
