using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class Orderitem
    {
        public long Id { get; set; }
        public string Quantity { get; set; } = null!;
        public long OrderId { get; set; }
        public long ProdcutId { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Prodcut { get; set; } = null!;
    }
}
