using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Like
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long UserId { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Userinfo User { get; set; } = null!;
    }
}
