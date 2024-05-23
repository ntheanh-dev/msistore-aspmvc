using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class MsistoreProduct
    {
        public MsistoreProduct()
        {
            MsistoreImages = new HashSet<MsistoreImage>();
            MsistoreLikes = new HashSet<MsistoreLike>();
            MsistoreOrderitems = new HashSet<MsistoreOrderitem>();
        }

        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public short IsActive { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public long? BrandId { get; set; }
        public long CategoryId { get; set; }

        public virtual MsistoreBrand? Brand { get; set; }
        public virtual MsistoreCategory Category { get; set; } = null!;
        public virtual ICollection<MsistoreImage> MsistoreImages { get; set; }
        public virtual ICollection<MsistoreLike> MsistoreLikes { get; set; }
        public virtual ICollection<MsistoreOrderitem> MsistoreOrderitems { get; set; }
    }
}
