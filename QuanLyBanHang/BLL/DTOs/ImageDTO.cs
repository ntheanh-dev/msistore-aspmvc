using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class ImageDTO
    {
        public long Id { get; set; }
        public string File { get; set; }
        public short Preview { get; set; }
    }
}
