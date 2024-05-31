using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Req.OrderReq;

namespace Common.Req
{
    public class CreateOrderRequest
    {
        public List<OrderRequest> Items { get; set; }
        public StatusOrderRequest StatusOrder { get; set; }
    }
}
