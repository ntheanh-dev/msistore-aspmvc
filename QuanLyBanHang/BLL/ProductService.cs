using Common.Req;
using DAL;
using DAL.Models;
using QLBH.Common.BLL;
using QLBH.Common.Req;
using QLBH.Common.Rsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductService:GenericSvc<ProductRepository,Product>
    {
        private ProductRepository repostiory;
        public ProductService() {
            repostiory = new ProductRepository();
        }

        #region -- Overrides ---
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            var m = _rep.Read(id);
            res.Resutls = m;

            return res;
        }

        public override SingleRsp Update(Product m)
        {
            var res = new SingleRsp();

            var m1 = m.Id > 0 ? _rep.Read((int)m.Id) : null;
            if(m1 == null)
            {
                res.SetError("EZ103", "No data");
            }else
            {
                res = base.Update(m);
                res.Resutls = m;
            }
            return res;
        }

        #endregion

        #region -- Methods----
        public SingleRsp CreateProduct(ProductRequest product) {
            var res = new SingleRsp();
            Product newP = new Product();
            newP.Id = product.Id;
            newP.Name = product.Name;
            newP.OldPrice = product.OldPrice;
            newP.NewPrice = product.NewPrice;
            newP.BrandId = product.BrandId;
            newP.CategoryId = product.CategoryId;
            newP.Description = product.Description;
            newP.Detail = product.Detail;
            res = repostiory.CreateProduct(newP);
            return res;
        }

        public SingleRsp Search(SearchProductReq req)
        {
            var res = repostiory.SearchProduct(req);
            return res;
        }

        public Product GetLastRecord()
        {
            return repostiory.GetLastRecord();
        }

        #endregion
    }
}
