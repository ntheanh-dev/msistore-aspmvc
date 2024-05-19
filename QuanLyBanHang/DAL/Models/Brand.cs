using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
