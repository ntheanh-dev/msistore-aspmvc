using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace MSISTORE.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        msistoreContext da = new msistoreContext();
        public ActionResult Index()
        {
            var ds = da.Brands.ToList();
            return View(ds);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Brand brand)
        {
            try
            {
                Brand b = new Brand();
                b = brand;
                da.Brands.Add(b);
                da.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }

        public ActionResult Details(int id)
        {
            var b = da.Brands.FirstOrDefault(b => b.Id.Equals(id));
            return View(b);
        }

        public ActionResult Edit(int id)
        {
            var b = da.Brands.FirstOrDefault(b => b.Id.Equals(id));
            return View(b);
        }

        [HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Brand b = da.Brands.FirstOrDefault(b => b.Id.Equals(id));
                b.Name = collection["Name"];
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
            Brand b = da.Brands.FirstOrDefault(b => b.Id.Equals(id));
            da.Remove(b);
            da.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
