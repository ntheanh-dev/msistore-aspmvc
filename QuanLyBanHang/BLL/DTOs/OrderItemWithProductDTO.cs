using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class OrderItemWithProductDTO
    {
        public long Id { get; set; }
        public long Quantity { get; set; }
        public long OrderId { get; set; }
        public long ProdcutId { get; set; }
        public decimal UnitPrice { get; set; }

        public ProductOrderDTO Prodcut { get; set; }
    }
}
