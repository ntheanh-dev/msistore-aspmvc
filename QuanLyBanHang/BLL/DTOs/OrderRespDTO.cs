using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class OrderRespDTO
    {
        public long? UserId { get; set; }
        //public List<OrderItemWithProductDTO> OrderItems { get; set; }

        public StatusOrderOTD Statusorders { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string Uuid { get; set; } = null!;
    }
}
