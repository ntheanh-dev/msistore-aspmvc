using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Req.UserRequest
{
    public class UpdateUserReq
    {
        public string ? FirstName { get; set; } = null!;
        public string ? LastName { get; set; } = null!;
        public string ? Username { get; set; } = null!;
        public string ? Email { get; set; } = null!;
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HomeNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile ? Avatar { get; set; }
    }
}
