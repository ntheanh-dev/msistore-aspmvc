using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class MsistoreImage
    {
        public long Id { get; set; }
        public string File { get; set; } = null!;
        public short Preview { get; set; }
        public long ProductId { get; set; }

        public virtual MsistoreProduct Product { get; set; } = null!;
    }
}
