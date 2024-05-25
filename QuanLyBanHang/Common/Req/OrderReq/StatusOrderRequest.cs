using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Req.OrderReq
{
    public class StatusOrderRequest
    {
        public string DeliveryMethod { get; set; } = null!;
        public string DeliveryStage { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
    }
}
