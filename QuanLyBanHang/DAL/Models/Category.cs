using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
