using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class Like
    {
        public Guid Id { get; set; }
        public long ProductId { get; set; }
        public long UserId { get; set; }
    }
}
