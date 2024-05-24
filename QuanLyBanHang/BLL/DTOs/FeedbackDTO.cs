using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class FeedbackDTO
    {
        public long Id { get; set; }
        public int? Rating { get; set; }
        public long? UserId { get; set; }
        public long? OrderId { get; set; }
        public long? ProductId { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}
