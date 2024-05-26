using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MSISTORE.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ImageController : Controller
    {
        msistoreContext da = new msistoreContext();

        public IActionResult Index()
        {
            var ds = da.Images.Include(p=> p.Prodcut).OrderByDescending(p => p.Id).ToList();
            return View(ds);
        }
        public IActionResult CreateImageProduct()
        {
            return View();
        }
    }
}
