using AutoMapper;
using BLL;
using Common.Req.FeedbackReq;
using Common.Req.OrderReq;
using Common.Rsp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSISTORE.WEB.Controllers
{
    [Route("api/feedbacks")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly FeedbackService _feedbackService;
        public FeedbackController(IConfiguration configuration, IMapper mapper)
        {
            _userService = new UserService(configuration, mapper);
            _feedbackService = new FeedbackService();
        }

        // GET: api/<FeedbackController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<FeedbackController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<FeedbackController>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] FeedbackRequest feedback)
        {
            if (!User.Identity.IsAuthenticated || User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            try
            {
                var f = await _feedbackService.CreateFeedbackAsync(userId, feedback);
                return Ok(f);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT api/<FeedbackController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<FeedbackController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
