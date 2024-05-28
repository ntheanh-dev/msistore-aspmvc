using BLL;
using Common.Req.ExchangeReq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MSISTORE.WEB.Controllers
{
    public class ExchangeController : Controller
    {
        private ExchangeService _exchangeService;
        public ExchangeController()
        {
            _exchangeService = new ExchangeService();
        }

        [HttpPost("/api/change")]
        [Authorize]
        public async Task<IActionResult> CreateChange([FromBody] ExchangeReq request)
        {
            if (!User.Identity.IsAuthenticated || User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            try
            {
                var exchange = await _exchangeService.CreateExchange(request, userId);

                return Ok(exchange);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPatch("/api/change/{exchangeId:long}/update")]
        [Authorize]
        public async Task<IActionResult> UpdateChange(long exchangeId, [FromBody] ExchangeReq request)
        {
            if (!User.Identity.IsAuthenticated || User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            try
            {
                var result = await _exchangeService.updateExchange(exchangeId, request.Reason, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("/api/{exchangeId:long}/deleted")]
        [Authorize]
        public async Task<IActionResult> DeletedChange(long exchangeId)
        {
            if (!User.Identity.IsAuthenticated || User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            try
            {
                var result = await _exchangeService.DeletedExchange(exchangeId,userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
