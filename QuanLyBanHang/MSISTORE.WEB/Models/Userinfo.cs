using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class Userinfo
    {
        public Userinfo()
        {
            Likes = new HashSet<Like>();
            Orders = new HashSet<Order>();
        }

        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HomeNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public long UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
