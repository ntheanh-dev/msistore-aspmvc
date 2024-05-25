using Microsoft.AspNetCore.Mvc;

namespace MSISTORE.WEB.Areas.Admin.Models
{
    public class LoginModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
    }
}
