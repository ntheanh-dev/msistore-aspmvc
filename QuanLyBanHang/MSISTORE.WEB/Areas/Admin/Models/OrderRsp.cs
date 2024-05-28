using DAL.Models;

namespace MSISTORE.WEB.Areas.Admin.Models
{
	public class OrderRsp : Order
	{
		public string DeliveryStage { get; set; } = null!;
	}
}
