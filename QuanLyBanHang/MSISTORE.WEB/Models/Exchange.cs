using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class Exchange
    {
        public long Id { get; set; }
        public long OrderItemId { get; set; }

        public virtual Orderitem OrderItem { get; set; } = null!;
    }
}
