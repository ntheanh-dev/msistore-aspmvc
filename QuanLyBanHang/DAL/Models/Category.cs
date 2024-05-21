using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Category
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
