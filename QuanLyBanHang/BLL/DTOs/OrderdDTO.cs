namespace BLL.DTOs
{
    public class OrderdDTO
    {
        public long? UserId { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
