using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Req
{
    public class OrderRequest
    {
        public long ProductId { get; set; }

        public string Quantity { get; set; }
    }
}
