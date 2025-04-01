using System;
using System.Collections.Generic;

namespace APIformbuilder.Models.fomrbuilder
{
    public partial class StringType
    {
        public long StringId { get; set; }
        public string DefaultValue { get; set; }
        public string ValueList { get; set; }
        public string MaskLibrary { get; set; }
        public string AssumedValue { get; set; }
        public int Length { get; set; }
        public bool Anulado { get; set; }
    }
}
