using DAL;
using DAL.Models;
using QLBH.Common.BLL;
using QLBH.Common.Rsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public class CategoryService : GenericSvc<CategoryRepository, Category>
    {
        private CategoryRepository categoryRepostiory;
        public CategoryService()
        {
            categoryRepostiory = new CategoryRepository();
        }
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Resutls = _rep.Read(id);
            return res;
        }

        public SingleRsp GetAll()
        {
            var res = new SingleRsp();
            res.Resutls = _rep.All;
            return res;
        }
    }
}