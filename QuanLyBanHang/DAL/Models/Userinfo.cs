using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Userinfo
    {
        public Userinfo()
        {
            Orders = new HashSet<Order>();
        }

        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string HomeNumber { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public long UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
