using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Req.FeedbackReq
{
    public class FeedbackRequest
    {
        public int? Rating { get; set; }
        public long? UserId { get; set; }
        public long? OrderId { get; set; }
        public long? ProductId { get; set; }
        public string? Comment { get; set; }
    }
}
