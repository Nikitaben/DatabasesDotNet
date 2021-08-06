using System;
using System.Collections.Generic;

#nullable disable

namespace DatabasesDotNet.DbModels
{
    public partial class Vendor
    {
        public Vendor()
        {
            Products = new HashSet<Product>();
        }

        public int VCode { get; set; }
        public string VName { get; set; }
        public string VContact { get; set; }
        public int VAreaCode { get; set; }
        public string VPhone { get; set; }
        public string VState { get; set; }
        public string VOrder { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
