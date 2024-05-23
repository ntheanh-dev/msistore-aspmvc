using AutoMapper;
using Common.Rsp;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using QLBH.Common.DAL;
using QLBH.Common.Req;
using QLBH.Common.Rsp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductRepository : GenericRep<msistoreContext, Product>
    {
        private IMapper _mapper;

        # region -- Overrides--------
        public override Product Read(int id)
        {
            var res = All.FirstOrDefault(x => x.Id == id);
            return res;
        }


        public int Remove(int id)
        {
            var m = base.All.First(x => x.Id == id);
            m = base.Delete(m);
            return 1;
        }
        # endregion

        # region -- Methods --------
        public SingleRsp CreateProduct(Product product)
        {
            var res = new SingleRsp();
            using (var context = new msistoreContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Products.Add(product);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }
        //public SingleRsp SearchProduct(string kw)
        //{
        //    var res = new SingleRsp();
        //    res.Data = All.Where(x => x.Description.Contains(kw) || x.Detail.Contains(kw) || x.Name.Contains(kw));
        //    return res;
        //}
        public SingleRsp SearchProduct(SearchProductReq request)
        {
            var res = new ProductResponse();

            var query = All.AsQueryable();

            //// Filter by category id
            if (request.cateId != 0)
            {
                query = query.Where(p => p.CategoryId == request.cateId);
            }

            //// Filter by price range
            if (request.fromPrice > 0)
            {
                query = query.Where(p => p.NewPrice >= request.fromPrice);
            }

            if (request.toPrice > 0)
            {
                query = query.Where(p => p.NewPrice <= request.toPrice);
            }


            //// Filter by keyword
            if (!string.IsNullOrEmpty(request.kw))
            {
                query = query.Where(x => x.Description.Contains(request.kw) || x.Detail.Contains(request.kw) || x.Name.Contains(request.kw));
            }


            int total = query.Count();

            // Calculate total pages
            if (request.page != 0 && request.page_size != 0)
            {
                int totalPages = (int)Math.Ceiling((double)total / request.page_size);

                //// Pagination
                query = query.Skip((request.page - 1) * request.page_size).Take(request.page_size);
                res.Total_pages = totalPages;

            }
            int count = query.Count();


            query = query.Include(p => p.Images);
            var products = query.ToList();
            res.Results = products;
            res.Count = count;
            return res;
        }
        public SingleRsp GetProductById(int id)
        {
            var res = new SingleRsp();
            var query = All.AsQueryable().Where(p => p.Id == id).Include(x => x.Images).First();
            res.Resutls = query;
            return res;
        }
        #endregion
    }
}