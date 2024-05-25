using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Req.OrderReq
{
    public class OrderRequest
    {
        public long ProductId { get; set; }
        public string Quantity { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HomeNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string DeliveryMethod { get; set; } = null!;
        public string DeliveryStage { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;



    }
}
