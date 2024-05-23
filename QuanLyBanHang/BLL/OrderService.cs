using Common.Req;
using DAL;
using DAL.Models;
using QLBH.Common.BLL;

namespace BLL
{
    public class OrderService : GenericSvc<OrderRepository, Order>
    {
        private readonly OrderRepository _orderRepository;
        public OrderService() { 
            _orderRepository = new OrderRepository();
        }
        public async Task<Order> createOrderAsync(long userId,List<OrderRequest> orders)
        {
            try
            {
                if(orders == null || orders.Count == 0)
                {
                    throw new ArgumentException("Order items cannot be null or empty.");
                }
                return await _orderRepository.CreateOrderAsync(userId, orders);
            }catch (Exception ex)
            {
                throw new Exception("Failed to create order.", ex);
            }
        }
    }
}
