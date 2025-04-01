using System;
using System.Collections.Generic;

namespace APIformbuilder.Models.fomrbuilder
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RolId { get; set; }
        public string RolName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
