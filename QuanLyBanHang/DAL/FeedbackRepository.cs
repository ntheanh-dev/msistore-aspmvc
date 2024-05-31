using Common.Req.FeedbackReq;
using Common.Req.OrderReq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using QLBH.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FeedbackRepository : GenericRep<msistoreContext, Feedback>
    {
        public async Task<Feedback> CreateFeedbackAsync(long userId, FeedbackRequest feedback)
        {
            using (var context = new msistoreContext())
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var user = await context.Users.FindAsync(userId);
                        var product = await context.Products.FindAsync(feedback.ProductId);
                        var order = await context.Orders
                        .Include(_ => _.Statusorders)
                        .FirstOrDefaultAsync(o => o.Id == feedback.OrderId);
                        var allOrdersDelivered = order.Statusorders.All(s => s.DeliveryStage == "Delivered");


                        if (!allOrdersDelivered)
                        {
                            throw new Exception("The order must be delivered before leaving feedback");
                        }


                        if (user == null)
                        {
                            await transaction.RollbackAsync();
                            throw new Exception($"User with ID {userId} not found");
                        }
                        if (product == null)
                        {
                            await transaction.RollbackAsync();
                            throw new Exception($"Product with ID {userId} not found");
                        }
                        if (order == null)
                        {
                            await transaction.RollbackAsync();
                            throw new Exception($"Order with ID {userId} not found");
                        }

                        if(!order.UserId.Equals(userId)) {
                            await transaction.RollbackAsync();
                            throw new Exception("Bad request");
                        }
                        if(context.Feedbacks.FirstOrDefault(
                            f => f.UserId.Equals(userId) && f.OrderId.Equals(feedback.OrderId) && f.ProductId.Equals(feedback.ProductId)) != null)
                        {
                            await transaction.RollbackAsync();
                            throw new Exception("You has feedbacked this product before");
                        }

                        var newFeedback = new Feedback { 
                            CreatedAt = DateTime.Now,
                            Rating = feedback.Rating,
                            Comment = feedback.Comment,
                            UserId = userId,
                            User = user,
                            ProductId = product.Id,
                            Product = product,
                            Order = order,
                            OrderId = order.Id,
                        };


                        context.Feedbacks.Add(newFeedback);
                        await context.SaveChangesAsync();
                        await transaction.CommitAsync();

                        return (newFeedback);
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw ex;
                    }
                }
            }
        }
    }
}
