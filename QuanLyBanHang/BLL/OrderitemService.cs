using DAL.Models;
using DAL;
using QLBH.Common.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBH.Common.Rsp;

namespace BLL
{
    public class OrderitemService : GenericSvc<OrderitemRepository, Orderitem>
    {
        private OrderitemRepository _orderitemRepository;
        public OrderitemService()
        {
            _orderitemRepository = new OrderitemRepository();
        }

        public async Task<SingleRsp> GetItems(long orderId, long userId)
        {
            var rs = new SingleRsp();
            var list = _orderitemRepository.GetItems(orderId, userId);
            long TotalPrice = 0;
            foreach( var i in list )
            {
                TotalPrice += (long)(i.Quantity * i.UnitPrice);
            }
            var responseData = new
            {
                Items = list,
                TotalPrice = TotalPrice
            };
            rs.SetData("200", responseData);
            return rs;
        }
    }
}
