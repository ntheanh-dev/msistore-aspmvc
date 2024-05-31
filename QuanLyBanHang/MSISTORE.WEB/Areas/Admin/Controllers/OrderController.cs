using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSISTORE.WEB.Areas.Admin.Models;
using Newtonsoft.Json;

namespace MSISTORE.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
	public class OrderController : Controller
	{
        private msistoreContext da = new msistoreContext();
		public ActionResult Index()
		{
            var user = HttpContext.Session.GetString("User_admin");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Login", "Home");
            }
            var os = da.Orders.Include(o=> o.Statusorders).Include(_ => _.User).ThenInclude(_ =>_.User).ToList();
            return View(os);
		}
        public ActionResult Edit(int id)
        {
            var user = HttpContext.Session.GetString("User_admin");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.User = JsonConvert.DeserializeObject<User>(user);
            var o = da.Orders.FirstOrDefault(o => o.Id.Equals(id));
            var status = da.Statusorders.FirstOrDefault(s => s.OrderId.Equals(id));
			OrderRsp order = new OrderRsp();
            order.CreatedAt = o.CreatedAt;
            order.UpdatedAt = o.UpdatedAt;
            order.UserId = o.UserId;
            order.Uuid = o.Uuid;
            order.IsActive = o.IsActive;
            order.DeliveryStage = status.DeliveryStage;
			var oi = da.Orderitems.Where(oi => oi.OrderId.Equals(id))
                                  .Join(da.Products, oi => oi.ProdcutId, p => p.Id,
                                  (oi, p) => new
                                  {
                                      id = oi.Id,
                                      name = p.Name,
                                      quantity = oi.Quantity,
                                      unit_price = oi.UnitPrice,
                                  });
            ViewBag.OrderItem = oi.ToList();
            return View(order);
        }
        [HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var order = da.Orders.FirstOrDefault(o => o.Id.Equals(id));
                if (order == null)
                {
                    throw new Exception("Not Found");
                }

                order.IsActive = short.Parse(collection["IsActive"]);

                var statuses = da.Statusorders.Where(s => s.OrderId.Equals(id)).ToList();

                foreach (var status in statuses)
                {
                    status.DeliveryStage = collection["DeliveryStage"];
                }

                da.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

    }
}
