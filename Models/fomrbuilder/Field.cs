using System;
using System.Collections.Generic;

namespace APIformbuilder.Models.fomrbuilder
{
    public partial class Field
    {
        public Field()
        {
            Answers = new HashSet<Answer>();
        }

        public int IdField { get; set; }
        public string Nombre { get; set; }
        public int? Orden { get; set; }
        public string Etiqueta { get; set; }
        public string Tipo { get; set; }
        public int? Requerido { get; set; }
        public string Marcador { get; set; }
        public string Opciones { get; set; }
        public int? Visible { get; set; }
        public string Clase { get; set; }
        public int? Estado { get; set; }
        public int? IdConfigForm { get; set; }
        public DateTime? FechaEliminacion { get; set; }

        public virtual ConfigForm IdConfigFormNavigation { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
