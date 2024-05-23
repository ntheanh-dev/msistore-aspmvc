using Common.Req;
using DAL.Models;
using QLBH.Common.DAL;

namespace DAL
{
    public class OrderRepository : GenericRep<msistoreContext, Order>
    {

        public async Task<Order> CreateOrderAsync(long userId, List<OrderRequest> items)
        {
            using (var context = new msistoreContext())
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var user = await context.Userinfos.FindAsync(userId);
                        if (user == null)
                        {
                            await transaction.RollbackAsync();
                            throw new Exception($"User with ID {userId} not found");
                        }
                        var order = new Order
                        {
                            UserId = userId,
                            User = user,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                            Uuid = Guid.NewGuid().ToString().Substring(0, 30),
                            Orderitems = new List<Orderitem>()
                        };

                        foreach (var item in items)
                        {
                            var product = await context.Products.FindAsync(item.ProductId);
                            if (product == null)
                            {
                                await transaction.RollbackAsync();
                                throw new Exception($"Product with ID {item.ProductId} not found");
                            }

                            var orderItem = new Orderitem
                            {
                                ProdcutId = product.Id,
                                Quantity = item.Quantity,
                                Order = order
                            };

                            order.Orderitems.Add(orderItem);
                        }

                        context.Orders.Add(order);
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return order;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception("Failed to create order", ex);
                    }
                }
            }
        }
    }
}
