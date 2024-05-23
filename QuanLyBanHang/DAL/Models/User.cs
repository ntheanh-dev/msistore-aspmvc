using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class User
    {
        public long Id { get; set; }
        public string Password { get; set; } = null!;
        public DateTime? LastLogin { get; set; }
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DateJoined { get; set; }
        public string? Avatar { get; set; }
        public long RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Userinfo? Userinfo { get; set; }
    }
}
