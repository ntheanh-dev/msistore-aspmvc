using Common.Rsp;
using DAL.Models;
using QLBH.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderitemRepository: GenericRep<msistoreContext, Orderitem>
    {
        private readonly msistoreContext _context;
        public OrderitemRepository()
        {
            _context = new msistoreContext();
        }

        public List<OrderitemRsp> GetItems(long orderId, long userId)
        {
            var query = from orderItem in _context.Orderitems
                        join product in _context.Products on orderItem.ProdcutId equals product.Id
                        join order in _context.Orders on orderItem.OrderId equals order.Id
                        where order.UserId.Equals(userId) && orderItem.OrderId.Equals(orderId)
                        select new OrderitemRsp
                        {
                            Quantity = orderItem.Quantity,
                            ProdcutName = product.Name,
                            UnitPrice = orderItem.UnitPrice
                        };

            return query.ToList();
        }
    }
}
