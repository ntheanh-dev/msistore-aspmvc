using AutoMapper;
using BLL;
using Common.DAL;
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
        public IActionResult Index()
        {
            return View();
        }
    }
}
