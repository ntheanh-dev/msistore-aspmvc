using System;
using System.Collections.Generic;
using System.Text;

namespace QLBH.Common.Req
{
    public class SearchProductReq
    {
        
        public int Page { get; set; }
        public int Size { get; set; }
        //public int ID { get; set; }
        //public int Type { get; set; }
        public string Keyword { get; set; }
    }
}
