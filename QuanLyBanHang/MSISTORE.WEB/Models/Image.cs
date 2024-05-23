using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class Image
    {
        public long Id { get; set; }
        public string File { get; set; } = null!;
        public short Preview { get; set; }
        public long ProdcutId { get; set; }

        public virtual Product Prodcut { get; set; } = null!;
    }
}
