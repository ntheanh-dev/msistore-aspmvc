using CloudinaryDotNet.Actions;
using Common.Req.OrderReq;
using Common.Rsp;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using QLBH.Common.DAL;
using QLBH.Common.Rsp;

namespace DAL
{
    public class OrderRepository : GenericRep<msistoreContext, Order>
    {
        // Order
        public async Task<(Order order, List<Product> ProductOrdered)> CreateOrderAsync(long userId, OrderRequest orderRequest)
        {
            using (var context = new msistoreContext())
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Retrieve user information
                        var user = await context.Userinfos.FindAsync(userId);
                        if (user == null)
                        {
                            throw new Exception("User not found");
                        }

                        Order order = null;

                        foreach (var orderItem in orderRequest.OrderItems)
                        {
                            // Find the product
                            var product = await context.Products.FindAsync(orderItem.ProductId);
                            if (product == null)
                            {
                                await transaction.RollbackAsync();
                                throw new Exception($"Product with ID {orderItem.ProductId} not found");
                            }

                            // Create order only once
                            order ??= new Order
                            {
                                UserId = userId,
                                User = user,
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                Uuid = Guid.NewGuid().ToString().Substring(0, 30),
                                Orderitems = new List<Orderitem>()
                            };

                            // Add order item for each quantity of the product
                            for (int i = 0; i < orderItem.Quantity; i++)
                            {
                                var orderItemEntity = new Orderitem
                                {
                                    ProdcutId = product.Id,
                                    Quantity = 1,
                                    Order = order,
                                    UnitPrice = product.NewPrice
                                };

                                var statusOrder = new Statusorder
                                {
                                    CreatedAt = DateTime.UtcNow,
                                    UpdatedAt = DateTime.UtcNow,
                                    IsActive = 1,
                                    IsPaid = 0,
                                    DeliveryMethod = orderRequest.DeliveryMethod,
                                    DeliveryStage = orderRequest.DeliveryStage,
                                    PaymentMethod = orderRequest.PaymentMethod,
                                    Order = order
                                };

                                order.Orderitems.Add(orderItemEntity);
                                context.Statusorders.Add(statusOrder);
                            }

                        }

                        await context.SaveChangesAsync();
                        await transaction.CommitAsync();

                        // Retrieve ordered products
                        var productIds = orderRequest.OrderItems.Select(oi => oi.ProductId);

                        var productOrdered = await context.Products.AsNoTracking()
                            .Include(_ => _.Images)
                            .Where(p => productIds.Contains(p.Id))
                            .ToListAsync();

                        return (order, productOrdered);
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception("Failed to create order", ex);
                    }
                }
            }
        }

        //View Order from User
        public List<Order> GetOrdersByUser(long userId)
        {
            //return All.Where(x => x.UserId == userId)
            //    .Select(x => new OrderRsp
            //    {
            //        Id = x.Id,
            //        CreatedAt = x.CreatedAt,
            //        UserId = x.UserId
            //    }).ToList();
            return All.AsQueryable().Where(x => x.UserId == userId)
                .Include(x => x.Orderitems).ThenInclude(x => x.Prodcut).ThenInclude(x => x.Feedbacks)
                .Include(x => x.Orderitems).ThenInclude(oi => oi.Prodcut).ThenInclude(p => p.Images)
                .Include(x => x.Statusorders)
                .ToList();
        }

    }
}
