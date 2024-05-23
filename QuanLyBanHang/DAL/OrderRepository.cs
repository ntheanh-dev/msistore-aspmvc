using Common.Req;
using DAL.Models;
using QLBH.Common.BLL;
using QLBH.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderRepository: GenericRep<msistoreContext,Order>
    {
        private readonly msistoreContext _context;
        public OrderRepository(msistoreContext context)
        {
            _context = context;
        }
        public async Task<Order> createOrderAsync(int userId, List<OrderRequest> items)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var order = new Order { 
                        UserId = userId,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        Uuid = Guid.NewGuid().ToString(),
                        Orderitems = new List<Orderitem>()
                    };
                    foreach (var item in items)
                    {
                        var product = _context.Products.Find(item.ProductId);
                    }


                }catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("An error occurred while creating the order. Transaction rolled back.", ex);
                }
            }

        }
    }
}
