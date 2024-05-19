using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class Order
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public short IsActive { get; set; }
        public string Uuid { get; set; } = null!;
        public long? UserId { get; set; }
    }
}
