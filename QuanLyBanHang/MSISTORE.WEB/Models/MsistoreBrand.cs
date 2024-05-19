using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class MsistoreBrand
    {
        public MsistoreBrand()
        {
            MsistoreProducts = new HashSet<MsistoreProduct>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<MsistoreProduct> MsistoreProducts { get; set; }
    }
}
