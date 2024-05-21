using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using QLBH.Common.DAL;
using System.Linq;
namespace DAL
{
    public class CategoryRepository : GenericRep<msistoreContext, Category>
    {
        public CategoryRepository() { }

        public override Category Read(int id)
        {
            var res = All.FirstOrDefault(x => x.Id == id);
            return res;
        }
    }
}