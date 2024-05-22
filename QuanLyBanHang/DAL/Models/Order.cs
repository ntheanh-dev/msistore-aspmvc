using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
            StatusOrders = new HashSet<StatusOrder>();
        }

        public long Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public short IsActive { get; set; }
        public string Uuid { get; set; } = null!;
        public long? UserId { get; set; }

        public virtual Userinfo? User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<StatusOrder> StatusOrders { get; set; }
    }
}
