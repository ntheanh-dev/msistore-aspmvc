using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Exchange
    {
        public long Id { get; set; }
        public long OrderItemId { get; set; }
        public long? UserId { get; set; }
        public string? Status { get; set; }
        public string? Reason { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Orderitem OrderItem { get; set; } = null!;
        public virtual User? User { get; set; }
    }
}
