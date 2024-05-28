using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MSISTORE.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        msistoreContext da = new msistoreContext();
        public ActionResult Index()
        {
            var user = HttpContext.Session.GetString("User_admin");
            if (string.IsNullOrEmpty(user))
            {
				return RedirectToAction("Login", "Home");
			}
            ViewBag.User = JsonConvert.DeserializeObject<User>(user);
            var ds = da.Products.Include(p => p.Brand).Include(p => p.Category).Include(p=> p.Images).OrderByDescending(p => p.Id).ToList();
            return View(ds);
        }

        public ActionResult Details(int id)
        {
			var user = HttpContext.Session.GetString("User_admin");
			if (string.IsNullOrEmpty(user))
			{
				return RedirectToAction("Login", "Home");
			}
			ViewBag.User = JsonConvert.DeserializeObject<User>(user);
			var p = da.Products.Include(p => p.Brand).Include(p => p.Category).Include(p=> p.Images).FirstOrDefault(c => c.Id.Equals(id));
            return View(p);
        }

        public ActionResult Create()
        {
			var user = HttpContext.Session.GetString("User_admin");
			if (string.IsNullOrEmpty(user))
			{
				return RedirectToAction("Login", "Home");
			}
			ViewBag.User = JsonConvert.DeserializeObject<User>(user);
			ViewBag.Brand = da.Brands.ToList();
            ViewBag.Category = da.Categories.ToList();
            return View();
        }

       [HttpPost]
public async Task<ActionResult> Create(Product product)
{
    try
    {
        using (var context = new msistoreContext())
        {
            product.CreatedAt = DateTime.Now.Date;
            product.UpdatedAt = DateTime.Now.Date;

            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }
    catch (Exception ex)
    {
        return Content($"An error occurred while creating the product: {ex.Message}");
    }
}




        public ActionResult Edit(int id)
        {
			var user = HttpContext.Session.GetString("User_admin");
			if (string.IsNullOrEmpty(user))
			{
				return RedirectToAction("Login", "Home");
			}
			ViewBag.User = JsonConvert.DeserializeObject<User>(user);
			var p = da.Products.Include(p => p.Brand).Include(p => p.Category).FirstOrDefault(c => c.Id.Equals(id));
            ViewBag.Brand = da.Brands.ToList();
            ViewBag.Category = da.Categories.ToList();
            return View(p);
        }

        [HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Product p = da.Products.FirstOrDefault(c => c.Id.Equals(id));
                p.IsActive = short.Parse(collection["IsActive"]);
                p.Name = collection["Name"];
                p.Description= collection["Description"];
                p.Detail = collection["Detail"];
                p.OldPrice = decimal.Parse(collection["OldPrice"]);
                p.NewPrice = decimal.Parse(collection["NewPrice"]);
                p.BrandId = long.Parse(collection["BrandId"]);
                p.CategoryId = long.Parse(collection["CategoryId"]);

                da.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public  ActionResult Delete(int id)
        {
            var user = HttpContext.Session.GetString("User_admin");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.User = JsonConvert.DeserializeObject<User>(user);

            using (var context = new msistoreContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var product = context.Products
                            .Include(p => p.Images)
                            .FirstOrDefault(p => p.Id == id);

                        if (product == null)
                        {
                            return NotFound("Product not found.");
                        }

                         context.Images.RemoveRange(product.Images);

                         context.Products.Remove(product);

                         context.SaveChangesAsync();

                         transaction.CommitAsync();

                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        transaction.RollbackAsync();
                        throw ex;
                    }
                }
            }
        }

    }
}
