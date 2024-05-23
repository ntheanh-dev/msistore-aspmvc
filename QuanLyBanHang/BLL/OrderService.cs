using BLL.DTOs;
using Common.Req.OrderReq;
using DAL;
using DAL.Models;
using QLBH.Common.BLL;
using static System.Net.Mime.MediaTypeNames;

namespace BLL
{
    public class OrderService : GenericSvc<OrderRepository, Order>
    {
        private readonly OrderRepository _orderRepository;
        public OrderService() { 
            _orderRepository = new OrderRepository();
        }
        public async Task<OrderdDTO> createOrderAsync(long userId,List<OrderRequest> orders)
        {
            try
            {
                if(orders == null || orders.Count == 0)
                {
                    throw new ArgumentException("Order items cannot be null or empty.");
                }
                var (OrderEnitty, ProductOrdered) =  await _orderRepository.CreateOrderAsync(userId, orders);
                var orderDTO = new OrderdDTO
                {
                    UserId = OrderEnitty.UserId,
                    CreatedAt = OrderEnitty.CreatedAt,
                    UpdatedAt = OrderEnitty.UpdatedAt,
                    OrderItems = OrderEnitty.Orderitems.Select(orderItem => new OrderItemDTO
                    {
                        Items = new List<object>
                        {
                            new
                            {
                                orderItem.Prodcut.Id,
                                orderItem.Prodcut.Name,
                                orderItem.Quantity,
                                Image =  ProductOrdered.SelectMany(_ => _.Images)
                                            .Where(_=> _.Preview == 1)
                                            .Select(image => image.File)
                                            .FirstOrDefault(),
                                orderItem.UnitPrice
                            }
                        }
                        
                    }).ToList(),
                };
                return orderDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create order.", ex);
            }
        }
    }
}
