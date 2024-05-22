using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rsp
{
    public class UserRsp
    {
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Username { get; set; }
        public string Email { get; set; }
        public string avatar { get; set; }
    }
}
