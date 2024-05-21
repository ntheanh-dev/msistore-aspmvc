using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class OrderItem
    {
        public long Id { get; set; }
        public string Quantity { get; set; } = null!;
        public long OrderId { get; set; }
        public long ProductId { get; set; }
    }
}
