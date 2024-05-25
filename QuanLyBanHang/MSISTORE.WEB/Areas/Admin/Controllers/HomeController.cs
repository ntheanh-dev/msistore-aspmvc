using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MSISTORE.WEB.Areas.Admin.Models;
using Newtonsoft.Json;

namespace MSISTORE.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        msistoreContext da = new msistoreContext();
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("User_admin");
            if(string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Login");
            }
            ViewBag.User = JsonConvert.DeserializeObject<User>(user);
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User_admin");
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel lgModel)
        {
            var username = lgModel.Username;
            var password = lgModel.Password;
            var userCheck = da.Users.Single(u => ( u.Username.Equals(username)));
            if (userCheck != null && userCheck.RoleId.Equals(1))
            {
                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, userCheck.Password);
                if (isPasswordCorrect)
                {
                    var user = JsonConvert.SerializeObject(userCheck);
                    HttpContext.Session.SetString("User_admin", user);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.LoginFail = "Sai mật khẩu";
                    return View();
                }
            }
            else
            {
                ViewBag.LoginFail = "Tài khoản không đúng";
                return View();
            }
        }
    }
}
