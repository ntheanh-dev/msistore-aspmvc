using BLL;
using Common.Req;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBH.Common.Req;
using QLBH.Common.Rsp;

namespace MSISTORE.WEB.Controllers
{
    [Route("api/producs")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductService productService;

        public ProductController()
        {
            this.productService = new ProductService();
        }
        [HttpPost("create-product")]
        public IActionResult CreaetProduct([FromBody] ProductRequest productRequest)
        {
            var res = new SingleRsp();
            res = productService.CreateProduct(productRequest);
            return Ok(res);
        }
        [HttpGet("")]
        public IActionResult SearchProduct([FromQuery]SearchProductReq searchProductReq)
        {
            var lastRecord = productService.GetLastRecord();
            Console.WriteLine("Last record: " + lastRecord.Id.ToString());

            var res = productService.Search(searchProductReq);
            return Ok(res);
        }
    }

}