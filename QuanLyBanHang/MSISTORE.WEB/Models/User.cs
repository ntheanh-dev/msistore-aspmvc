using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class User
    {
        public Guid Id { get; set; }
        public string Password { get; set; } = null!;
        public DateTime? LastLogin { get; set; }
        public short IsSuperuser { get; set; }
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public short IsStaff { get; set; }
        public short IsActive { get; set; }
        public DateTime DateJoined { get; set; }
        public string Avatar { get; set; } = null!;
        public long RoleId { get; set; }
    }
}
