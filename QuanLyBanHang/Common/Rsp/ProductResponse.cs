using QLBH.Common.Req;
using QLBH.Common.Rsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rsp
{
    public class ProductResponse : SingleRsp
    {
        public ProductResponse() : base() { }
        public ProductResponse(string message): base(message) { }

        public ProductResponse(string message, string titleError) : base(message, titleError) { }

        public int Count {  get; set; }
        public object Results { get; set; }
        public int Total_pages { get; set; }
    }
}
