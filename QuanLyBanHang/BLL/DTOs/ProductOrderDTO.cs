using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class ProductOrderDTO
    {
        public string Name { get; set; } = null!;
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
    }
}
