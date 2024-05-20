using AutoMapper;
using BLL;
using Common.Req;
using Microsoft.AspNetCore.Mvc;

namespace MSISTORE.WEB.Controllers
{
    public class UserController : Controller
    {
       private readonly UserService _userService;
        
        public UserController(IConfiguration configuration, IMapper mapper) {
            _userService = new UserService(configuration, mapper);
        }
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromForm] UserReq userReq)
        {
            return Ok(await _userService.CreateUserAsync(userReq));
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginReq loginReq)
        {
            var res = await _userService.AuthenticateJWTAsync(loginReq);
            return Ok(res.Resutls);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
