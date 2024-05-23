using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class Image
    {
        public long Id { get; set; }
        public string File { get; set; } = null!;
        public short Preview { get; set; }
        public long ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
