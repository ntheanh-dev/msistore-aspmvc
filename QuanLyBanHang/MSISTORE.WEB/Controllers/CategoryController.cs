using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBH.Common.Req;
using QLBH.Common.Rsp;
namespace MSISTORE.WEB.Controllers
{
    [Route("api/categories/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategoryService categoryService;
        public CategoryController() {
            categoryService = new CategoryService();
        }

        [HttpPost("get-by-id")]
        public IActionResult getCategoryById([FromBody] SimpleReq simpleReq)
        {
            var res = new SingleRsp();
            res = categoryService.Read(simpleReq.Id);
            return Ok(res);
        }

        [HttpGet("")]
        public IActionResult getCategories()
        {
            var res = new SingleRsp();
            res = categoryService.GetAll();
            return Ok(res);
        }

    }
}
