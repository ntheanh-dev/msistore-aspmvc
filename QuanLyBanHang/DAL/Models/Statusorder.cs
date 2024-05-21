using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class StatusOrder
    {
        public  long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public short IsActive { get; set; }
        public short IsPaid { get; set; }
        public string DeliveryMethod { get; set; } = null!;
        public string DeliveryStage { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public long OrderId { get; set; }
    }
}
