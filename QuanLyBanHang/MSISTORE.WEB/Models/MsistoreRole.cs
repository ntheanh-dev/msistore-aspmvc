using System;
using System.Collections.Generic;

namespace MSISTORE.WEB.Models
{
    public partial class MsistoreRole
    {
        public MsistoreRole()
        {
            MsistoreUsers = new HashSet<MsistoreUser>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<MsistoreUser> MsistoreUsers { get; set; }
    }
}
