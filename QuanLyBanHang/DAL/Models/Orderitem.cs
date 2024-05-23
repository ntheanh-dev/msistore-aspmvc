using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Orderitem
    {
        public long Id { get; set; }
        public string Quantity { get; set; } = null!;
        public long OrderId { get; set; }
        public long ProdcutId { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Prodcut { get; set; } = null!;
    }
}
