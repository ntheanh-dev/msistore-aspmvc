﻿using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Order
    {
        public Order()
        {
            Orderitems = new HashSet<Orderitem>();
            Statusorders = new HashSet<Statusorder>();
        }

        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public short IsActive { get; set; }
        public string Uuid { get; set; } = null!;
        public long? UserId { get; set; }

        public virtual Userinfo? User { get; set; }
        public virtual ICollection<Orderitem> Orderitems { get; set; }
        public virtual ICollection<Statusorder> Statusorders { get; set; }
    }
}
