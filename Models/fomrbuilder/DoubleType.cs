using System;
using System.Collections.Generic;

namespace APIformbuilder.Models.fomrbuilder
{
    public partial class DoubleType
    {
        public int DoubleId { get; set; }
        public int DefaultValue { get; set; }
        public string ValueList { get; set; }
        public string MinConfiguration { get; set; }
        public int MinValue { get; set; }
        public string MaxConfiguration { get; set; }
        public int MaxValue { get; set; }
        public int AssumedValue { get; set; }
        public bool Anulado { get; set; }
    }
}
