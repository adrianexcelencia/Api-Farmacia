using System;
using System.Collections.Generic;

namespace APIformbuilder.Models.fomrbuilder
{
    public partial class User
    {
        public User()
        {
            Permisos = new HashSet<Permiso>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public string Email { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Permiso> Permisos { get; set; }
    }
}
