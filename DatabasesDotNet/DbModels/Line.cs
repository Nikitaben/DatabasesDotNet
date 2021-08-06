using System;
using System.Collections.Generic;

#nullable disable

namespace DatabasesDotNet.DbModels
{
    public partial class Line
    {
        public int LineNumber { get; set; }
        public int? LineUnits { get; set; }
        public double? LinePrice { get; set; }
        public string PCode { get; set; }
        public int InvNumber { get; set; }

        public virtual Invoice InvNumberNavigation { get; set; }
        public virtual Product PCodeNavigation { get; set; }
    }
}
