using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Req
{
    public class UserReq
    {
        public string Password { get; set; } = null!;
        public DateTime? LastLogin { get; set; }
        public short IsSuperuser { get; set; } = 0;
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public short IsStaff { get; set; } = 0;
        public short IsActive { get; set; } = 1;
        public DateTime? DateJoined { get; set; }
        public IFormFile? Avatar { get; set; }
        public long RoleId { get; set; } = 1;
    }
}
