using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class MsistoreUserinfo
    {
        public MsistoreUserinfo()
        {
            MsistoreLikes = new HashSet<MsistoreLike>();
            MsistoreOrders = new HashSet<MsistoreOrder>();
        }

        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string HomeNumber { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public long UserId { get; set; }

        public virtual MsistoreUser User { get; set; } = null!;
        public virtual ICollection<MsistoreLike> MsistoreLikes { get; set; }
        public virtual ICollection<MsistoreOrder> MsistoreOrders { get; set; }
    }
}
