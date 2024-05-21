using DAL.Models;
using QLBH.Common.DAL;
using QLBH.Common.Rsp;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL
{
    public class UserRepository : GenericRep<msistoreContext, User>
    {

        public async Task<SingleRsp> AddUserAsync(User newUser)
        {
            var res = new SingleRsp();
            try
            {
                using (var context = new msistoreContext())
                {
                    await context.Users.AddAsync(newUser);
                    await context.SaveChangesAsync();
                    
                }
            }
            catch (Exception ex)
            {
                res.SetError($"Error: {ex.Message}");
            }
            return res;
        }
        public async Task<SingleRsp> AddUserinfoAsync(Userinfo userinfo)
        {
            var res = new SingleRsp();
            try
            {
                using (var context = new msistoreContext())
                {
                    await context.Userinfos.AddAsync(userinfo);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                res.SetError($"Error: {ex.Message}");
            }
            return res;
        }


        public async Task<List<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            using (var context = new msistoreContext())
            {
                return await context.Users.Where(predicate).ToListAsync();
            }
        }
    }
}
