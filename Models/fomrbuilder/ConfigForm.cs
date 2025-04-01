using System;
using System.Collections.Generic;

namespace APIformbuilder.Models.fomrbuilder
{
    public partial class ConfigForm
    {
        public ConfigForm()
        {
            Answers = new HashSet<Answer>();
            Fields = new HashSet<Field>();
        }

        public int IdConfigForm { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Field> Fields { get; set; }
    }
}
