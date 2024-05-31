using AutoMapper;
using BLL.DTOs;
using Common.Req.OrderReq;
using Common.Rsp;
using DAL;
using DAL.Models;
using QLBH.Common.BLL;
using QLBH.Common.Rsp;
using static System.Net.Mime.MediaTypeNames;

namespace BLL
{
    public class OrderService : GenericSvc<OrderRepository, Order>
    {
        private readonly OrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(IMapper mapper) { 
            _orderRepository = new OrderRepository();
            _mapper = mapper;
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

        public async Task<SingleRsp> GetOrdersByUser(long userId)
        {
            var rs = new SingleRsp();
            List<Order> orders = _orderRepository.GetOrdersByUser(userId);
            //var list = _mapper.Map<List<OrderRespDTO>>(orders);
            var list = _orderRepository.GetOrdersByUser(userId);
            rs.SetData("200", list);
            return rs;
        }

    }
}
