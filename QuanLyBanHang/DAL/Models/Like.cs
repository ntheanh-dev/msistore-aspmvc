using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Like
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
