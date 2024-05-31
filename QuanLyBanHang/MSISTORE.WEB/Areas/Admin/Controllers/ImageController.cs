using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSISTORE.WEB.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MSISTORE.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ImageController : Controller
    {

        msistoreContext da = new msistoreContext();
        private readonly Cloudinary _cloudinary;

        public ImageController(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }
        // GET: ImageController1
        public ActionResult Index()
        {
            var user = HttpContext.Session.GetString("User_admin");
            if (string.IsNullOrEmpty(user))
            {
				return RedirectToAction("Login", "Home");
			}
            ViewBag.User = JsonConvert.DeserializeObject<User>(user);
            var ds = da.Images.Include(s => s.Prodcut).OrderByDescending(s=>s.Id).ToList();
            return View(ds);
        }

        // GET: ImageController1/Details/5
        public ActionResult Details(int id)
        {
			var user = HttpContext.Session.GetString("User_admin");
			if (string.IsNullOrEmpty(user))
			{
				return RedirectToAction("Login", "Home");
			}
			ViewBag.User = JsonConvert.DeserializeObject<User>(user);
			return View();
        }

        // GET: ImageController1/Create
        public ActionResult Create()
        {
			var user = HttpContext.Session.GetString("User_admin");
			if (string.IsNullOrEmpty(user))
			{
				return RedirectToAction("Login", "Home");
			}
			ViewBag.User = JsonConvert.DeserializeObject<User>(user);
			var productIds = da.Products.Select(p => new { p.Id, p.Name }).ToList();
            ViewBag.ProductIds = productIds;
            return View();
        }


        // POST: ImageController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ImageModel imageModel)
        {
            try
            {
                    // Upload image to Cloudinary
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(imageModel.File.FileName, imageModel.File.OpenReadStream())
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    var imageUrl = uploadResult.SecureUri.AbsoluteUri;

                    // Create new Image entity
                    var image = new Image
                    {
                        File = imageUrl,
                        Preview = imageModel.Preview,
                        ProdcutId = imageModel.ProductId
                    };

                    da.Images.Add(image);
                    await da.SaveChangesAsync();
                    return RedirectToAction("Index");
               
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: ImageController1/Edit/5
        public ActionResult Edit(int id)
        {
			var user = HttpContext.Session.GetString("User_admin");
			if (string.IsNullOrEmpty(user))
			{
				return RedirectToAction("Login", "Home");
			}
			ViewBag.User = JsonConvert.DeserializeObject<User>(user);
			return View();
        }

        // POST: ImageController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ImageController1/Delete/5
        public ActionResult Delete(long id)
        {
			var user = HttpContext.Session.GetString("User_admin");
			if (string.IsNullOrEmpty(user))
			{
				return RedirectToAction("Login", "Home");
			}
			ViewBag.User = JsonConvert.DeserializeObject<User>(user);
			var image = da.Images.Include(i => i.Prodcut).FirstOrDefault(i => i.Id == id);
            if (image == null)
            {
                return NotFound();
            }
            return View(image);
        }

        // POST: ImageController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(long id, IFormCollection collection)
        {
            try
            {
                var image = await da.Images.FindAsync(id);
                if (image == null)
                {
                    return NotFound();
                }

                // Delete image from Cloudinary
                var deletionParams = new DeletionParams(image.File);
                var result = await _cloudinary.DestroyAsync(deletionParams);

                if (result.Result == "not found")
                {
                    da.Images.Remove(image);
                    await da.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
