using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class MsistoreOrderitem
    {
        public long Id { get; set; }
        public string Quantity { get; set; } = null!;
        public long OrderId { get; set; }
        public long ProductId { get; set; }

        public virtual MsistoreOrder Order { get; set; } = null!;
        public virtual MsistoreProduct Product { get; set; } = null!;
    }
}
