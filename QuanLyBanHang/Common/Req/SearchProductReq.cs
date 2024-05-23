using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace QLBH.Common.Req
{
    public class SearchProductReq
    {
        public int page { get; set; } = 0;
        public int page_size { get; set; } = 0;
        public int cateId { get; set; } = 0;
        public decimal fromPrice { get; set; } = decimal.Zero;
        public decimal toPrice { get; set; } = decimal.Zero;
        public int limit { get; set; } = 0;
        public string kw { get; set; } = "";

        public SearchProductReq() { }
        public SearchProductReq(int page, int page_size, int cateId, decimal fromPrice, decimal toPrice, int limit, string kw)
        {
            this.page = page;
            this.page_size = page_size;
            this.cateId = cateId;
            this.fromPrice = fromPrice;
            this.toPrice = toPrice;
            this.limit = limit;
            this.kw = kw;
        }
    }
}