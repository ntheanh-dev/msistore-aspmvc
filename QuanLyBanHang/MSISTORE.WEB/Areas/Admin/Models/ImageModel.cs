using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MSISTORE.WEB.Areas.Admin.Models
{
    public class ImageModel
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public short Preview { get; set; }

        [Required]
        public long ProductId { get; set; }
    }
}
