using System;
using System.Collections.Generic;

namespace APIformbuilder.Models.fomrbuilder
{
    public partial class Funcionalidade
    {
        public Funcionalidade()
        {
            Permisos = new HashSet<Permiso>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Permiso> Permisos { get; set; }
    }
}
