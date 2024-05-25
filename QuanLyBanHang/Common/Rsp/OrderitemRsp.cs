using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rsp
{
    public class OrderitemRsp
    {
        public long Quantity { get; set; }
        public string ProdcutName { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
