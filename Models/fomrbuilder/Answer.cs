using System;
using System.Collections.Generic;

namespace APIformbuilder.Models.fomrbuilder
{
    public partial class Answer
    {
        public int IdAnswer { get; set; }
        public int? IdConfigForm { get; set; }
        public int? IdField { get; set; }
        public string Valor { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public int? IdentificadorFila { get; set; }

        public virtual ConfigForm IdConfigFormNavigation { get; set; }
        public virtual Field IdFieldNavigation { get; set; }
    }
}
