using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class MsistoreOrder
    {
        public MsistoreOrder()
        {
            MsistoreOrderitems = new HashSet<MsistoreOrderitem>();
            MsistoreStatusorders = new HashSet<MsistoreStatusorder>();
        }

        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public short IsActive { get; set; }
        public string Uuid { get; set; } = null!;
        public long? UserId { get; set; }

        public virtual MsistoreUserinfo? User { get; set; }
        public virtual ICollection<MsistoreOrderitem> MsistoreOrderitems { get; set; }
        public virtual ICollection<MsistoreStatusorder> MsistoreStatusorders { get; set; }
    }
}
