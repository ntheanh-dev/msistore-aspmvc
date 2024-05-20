using System;

namespace QLBH.Common.Req
{
    public class SearchProductReq
    {
        public int page { get; set; } = 0;
        public int page_size { get; set; } = 0;
        public Guid cateId { get; set; } = Guid.Empty;
        public decimal fromPrice { get; set; } = decimal.Zero;
        public decimal toPrice { get; set; } = decimal.Zero;
        public int limit { get; set; } = 0;
        public string kw { get; set; } = "";

        public SearchProductReq() { }

        public SearchProductReq(int page, int page_size, Guid cateId, decimal fromPrice, decimal toPrice, int limit, string kw)
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
