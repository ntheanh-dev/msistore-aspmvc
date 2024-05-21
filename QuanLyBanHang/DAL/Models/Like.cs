using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Like
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long UserId { get; set; }
    }
}
