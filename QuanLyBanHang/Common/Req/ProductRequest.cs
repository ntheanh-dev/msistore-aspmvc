using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Req
{
    public class ProductRequest
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public short IsActive { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public long? BrandId { get; set; }
        public long CategoryId { get; set; }
    }
}
