using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class OrderItem
    {
        public Guid Id { get; set; }
        public string Quantity { get; set; } = null!;
        public long OrderId { get; set; }
        public long ProductId { get; set; }
    }
}
