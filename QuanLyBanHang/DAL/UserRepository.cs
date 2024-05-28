using DAL.Models;
using QLBH.Common.DAL;
using QLBH.Common.Rsp;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL
{
    public class UserRepository : GenericRep<msistoreContext, User>
    {

        //create user
        public async Task<SingleRsp> AddUserAsync(User newUser,Userinfo userinfo)
        {
            var res = new SingleRsp();
            try
            {
                using (var context = new msistoreContext())
                {
                    using var transaction = await context.Database.BeginTransactionAsync();
                    context.Users.Add(newUser);
                    await context.SaveChangesAsync();

                    userinfo.UserId = newUser.Id;
                    
                    context.Userinfos.Add(userinfo);
                    await context.SaveChangesAsync();

                    transaction.Commit();
                    
                }
            }
            catch (Exception ex)
            {
                res.SetError($"Error: {ex.Message}");
            }
            return res;
        }
       

        //check user
        public async Task<List<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            using (var context = new msistoreContext())
            {
                return await context.Users.Where(predicate).ToListAsync();
            }
        }
        public async Task<User> GetUserByIdAsync(long userId)
        {
            return await All.Include(u => u.Userinfo).FirstOrDefaultAsync(u => u.Id == userId);
        }

        //update User
        public async Task<SingleRsp> UpdateAsync(User updateUser)
        {
            var res = new SingleRsp();

            using (var transaction = await Context.Database.BeginTransactionAsync())
            {
                try
                {
                    var existingUser = await All.FirstOrDefaultAsync(u => u.Id == updateUser.Id);

                    if (existingUser == null)
                    {
                        res.SetError("User not found.");
                        return res;
                    }

                    existingUser.FirstName = updateUser.FirstName;
                    existingUser.LastName = updateUser.LastName;
                    existingUser.Email = updateUser.Email;  
                    existingUser.Avatar = updateUser.Avatar;

                    if (updateUser.Userinfo != null)
                    {
                        var existingUserinfo = await Context.Userinfos.FirstOrDefaultAsync(ui => ui.UserId == updateUser.Id);

                        if (existingUserinfo == null)
                        {
                            existingUserinfo = new Userinfo
                            {
                                UserId = updateUser.Id,
                                Country = updateUser.Userinfo.Country,
                                City = updateUser.Userinfo.City,
                                Street = updateUser.Userinfo.Street,
                                HomeNumber = updateUser.Userinfo.HomeNumber,
                                PhoneNumber = updateUser.Userinfo.PhoneNumber
                            };
                            Context.Userinfos.Add(existingUserinfo);
                        }
                        else
                        {
                            existingUserinfo.Country = updateUser.Userinfo.Country;
                            existingUserinfo.City = updateUser.Userinfo.City;
                            existingUserinfo.Street = updateUser.Userinfo.Street;
                            existingUserinfo.HomeNumber = updateUser.Userinfo.HomeNumber;
                            existingUserinfo.PhoneNumber = updateUser.Userinfo.PhoneNumber;
                        }
                    }

                    await Context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    res.Resutls = updateUser;
                    return res;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    res.SetError(ex.StackTrace);
                }
            }

            return res;
        }

    }
}
