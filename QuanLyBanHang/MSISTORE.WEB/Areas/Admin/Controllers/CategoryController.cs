using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MSISTORE.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        msistoreContext da = new msistoreContext();
        public ActionResult Index()
        {
            //var user = HttpContext.Session.GetString("User_admin");
            //if (string.IsNullOrEmpty(user))
            //{
            //    return RedirectToAction("Login");
            //}
            //ViewBag.User = JsonConvert.DeserializeObject<User>(user);
            var ds = da.Categories.ToList();
            return View(ds);
        }

        public ActionResult Details(int id)
        {
            var c = da.Categories.FirstOrDefault(c => c.Id.Equals(id));
            return View(c);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category categoryReq)
        {
            try
            {
                Category c = new Category();
                c = categoryReq;
                da.Categories.Add(c);
                da.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }


        public ActionResult Edit(int id)
        {
            var c = da.Categories.FirstOrDefault(c => c.Id.Equals(id));
            return View(c);
        }

        [HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Category c = da.Categories.FirstOrDefault(c => c.Id.Equals(id));
                c.Name = collection["Name"];
                da.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public ActionResult Delete(int id)
        {
            Category c = da.Categories.FirstOrDefault(c => c.Id.Equals(id));
            da.Categories.Remove(c);
            da.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
