using System;
using System.Collections.Generic;

#nullable disable

namespace DatabasesDotNet.DbModels
{
    public partial class Customer
    {
        public Customer()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int CusCode { get; set; }
        public string CusFName { get; set; }
        public string CusLName { get; set; }
        public string CusInitial { get; set; }
        public int? CusAreaCode { get; set; }
        public string CusPhone { get; set; }
        public double CusBalance { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
