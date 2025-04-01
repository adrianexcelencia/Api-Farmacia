using System;
using System.Collections.Generic;

namespace APIformbuilder.Models.fomrbuilder
{
    public partial class Permiso
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PermisoId { get; set; }

        public virtual Funcionalidade PermisoNavigation { get; set; }
        public virtual User Usuario { get; set; }
    }
}
