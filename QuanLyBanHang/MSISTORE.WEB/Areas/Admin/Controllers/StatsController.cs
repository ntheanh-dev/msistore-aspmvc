using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MSISTORE.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatsController : Controller
    {
        private readonly msistoreContext da = new msistoreContext();
        
        public ActionResult Index([FromQuery] string year, [FromQuery] string type)
        {
            var user = HttpContext.Session.GetString("User_admin");
            if (string.IsNullOrEmpty(user))
            {
				return RedirectToAction("Login", "Home");
			}
            ViewBag.User = JsonConvert.DeserializeObject<User>(user);
            if (!string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(type))
			{
				return GetRs(year,type);
			}
            var query = from o in da.Orders
                        join oi in da.Orderitems
                        on o.Id equals oi.OrderId
                        group new { o, oi } by new
                        {
                            Year = o.CreatedAt.Year,
                            Month = o.CreatedAt.Month,
                        } into g
                        select new
                        {
                            Time = g.Key.Month,
                            Revenue = g.Sum(x => x.oi.Quantity * x.oi.UnitPrice),
                        };
            var rs = query.ToList();
            ViewBag.Result = rs;
            return View();
        }

        public ActionResult GetRs([FromQuery] string year, [FromQuery] string type)
        {
			var query = Enumerable.Empty<dynamic>();
            if (type == "MONTH")
            {
				query = from o in da.Orders
						join oi in da.Orderitems
						on o.Id equals oi.OrderId
						group new { o, oi } by new
						{
							Year = year,
							Month = o.CreatedAt.Month,
						} into g
						select new
						{
							Time = g.Key.Month,
							Revenue = g.Sum(x => x.oi.Quantity * x.oi.UnitPrice),
						};
			}
            else {
				query = from o in da.Orders
						join oi in da.Orderitems
						on o.Id equals oi.OrderId
						group new { o, oi } by new
						{
							Year = year,
							Quarter = (int)Math.Ceiling(o.CreatedAt.Month / 3.0)
						} into g
						select new
						{
							Time = g.Key.Quarter,
							Revenue = g.Sum(x => x.oi.Quantity * x.oi.UnitPrice),
						};
			}
			var rs = query.ToList();
			ViewBag.Result = rs;
			return View("Index");
		}
    }
}
