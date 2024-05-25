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
        public async Task<OrderdDTO> createOrderAsync(long userId, OrderRequest order)
        {
            try
            {
                if (order == null)
                {
                    throw new ArgumentException("Order items cannot be null or empty.");
                }

                var (OrderEnitty, ProductOrdered) = await _orderRepository.CreateOrderAsync(userId, order);

                // Check for null
                if (OrderEnitty == null || ProductOrdered == null)
                {
                    throw new Exception("Failed to retrieve order details.");
                }

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
                        orderItem.Prodcut?.Id, // Assuming Product is a navigation property
                        orderItem.Prodcut?.Name,
                        orderItem.Quantity,
                        Image = ProductOrdered.SelectMany(_ => _.Images)
                            .Where(_ => _.Preview == 1)
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
