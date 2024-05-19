using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class MsistoreLike
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long UserId { get; set; }

        public virtual MsistoreProduct Product { get; set; } = null!;
        public virtual MsistoreUserinfo User { get; set; } = null!;
    }
}
