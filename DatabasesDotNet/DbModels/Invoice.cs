using System;
using System.Collections.Generic;

#nullable disable

namespace DatabasesDotNet.DbModels
{
    public partial class Invoice
    {
        public Invoice()
        {
            Lines = new HashSet<Line>();
        }

        public int InvNumber { get; set; }
        public DateTime? InvDate { get; set; }
        public int? CusCode { get; set; }

        public virtual Customer CusCodeNavigation { get; set; }
        public virtual ICollection<Line> Lines { get; set; }
    }
}
