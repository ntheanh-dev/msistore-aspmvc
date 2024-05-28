using Common.Req.ExchangeReq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using QLBH.Common.DAL;
using QLBH.Common.Rsp;

namespace DAL
{
    public class ExchangeRepository : GenericRep<msistoreContext, Exchange>
    {
        // create exhchange product
        public async Task<Exchange> createExchange (ExchangeReq exchangeReq , long userId)
        {
            try
            {
                using (var context = new msistoreContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        var orderItem = context.Orderitems
                            .Include(o => o.Order).ThenInclude(o => o.Statusorders)
                            .FirstOrDefault(o => o.Id == exchangeReq.OrderItemId);

                        if (orderItem == null || orderItem.Order == null || orderItem.Order.UserId != userId)
                        {
                            throw new Exception("Không tìm thấy món hàng hoặc người dùng không có quyền đổi trả món hàng này.");
                        }
                        var latestStatus = orderItem.Order.Statusorders
                            .OrderByDescending(s => s.UpdatedAt) // Assuming there's an UpdatedAt field to determine the latest status
                            .FirstOrDefault();
                        if(latestStatus == null || latestStatus.DeliveryStage != "Delivered") {

                            throw new Exception("Chỉ có thể đổi trả những đơn hàng đã được giao.");
                        }
                        try
                        {
                            var exchange = new Exchange
                            {
                                OrderItemId = exchangeReq.OrderItemId,
                                UserId = userId,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now,
                                Reason = exchangeReq.Reason,
                                Status = "Proccessing"
                            };

                            context.Exchanges.Add(exchange);
                            await context.SaveChangesAsync();

                            await transaction.CommitAsync();

                            return exchange;
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SingleRsp> UpdateExchangeReason(long exchangeId, string newReason ,long userId)
        {
            var res = new SingleRsp();
            try
            {
                using (var context = new msistoreContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        var exchange = await context.Exchanges.FindAsync(exchangeId);
                        if (exchange == null)
                        {
                            throw new Exception("Không tìm thấy đơn hàng đổi trả này.");
                        }

                        if (exchange.UserId != userId)
                        {
                            throw new Exception("Người dùng không có quyền thay đổi trả hàng này.");
                        }

                        exchange.Reason = newReason;
                        exchange.UpdatedAt = DateTime.Now;

                        await context.SaveChangesAsync();
                        await transaction.CommitAsync();

                        // Trả về kết quả
                        res.Resutls = exchange;
                        return res;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        // deleted exchange
        public async Task<SingleRsp> DeleteExchange(long exchangeId, long userId)
        {
            var res = new SingleRsp();
            try
            {
                using (var context = new msistoreContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        var exchange = await context.Exchanges.FindAsync(exchangeId);
                        if (exchange == null)
                        {
                            throw new Exception("Không tìm thấy đơn hàng đổi trả này.");
                        }

                        if (exchange.UserId != userId)
                        {
                            throw new Exception("Người dùng không có quyền xóa đổi trả hàng này.");
                        }

                        context.Exchanges.Remove(exchange);
                        await context.SaveChangesAsync();
                        await transaction.CommitAsync();

                        res.Resutls = "Xóa đổi trả hàng thành công.";
                        return res;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
