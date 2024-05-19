using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class Userinfo
    {
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string HomeNumber { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public Guid UserId { get; set; }
    }
}
