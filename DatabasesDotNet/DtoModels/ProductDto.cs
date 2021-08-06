using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DatabasesDotNet.DtoModels
{
    [Serializable()]
    public class ProductDto : ISerializable
    {
        public string PCode { get; set; }
        public string PDescript { get; set; }
        public DateTime PInDate { get; set; }
        public int PQoh { get; set; }
        public int PMin { get; set; }
        public double PPrice { get; set; }
        public double? PDiscount { get; set; }
        public int? VCode { get; set; }

        public ProductDto() {}

        public ProductDto(SerializationInfo info, StreamingContext context)
        {
            PCode = (string)info.GetValue("PCode", typeof(string));
            PDescript = (string)info.GetValue("PDescript", typeof(string));
            PInDate = (DateTime)info.GetValue("PInDate", typeof(DateTime));
            PQoh = (int)info.GetValue("PQoh", typeof(int));
            PMin = (int)info.GetValue("PMin", typeof(int));
            PPrice = (double)info.GetValue("PPrice", typeof(double));
            PDiscount = (double)info.GetValue("PDiscount", typeof(double));
            VCode = (int?)info.GetValue("VCode", typeof(int?));

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("PCode", PCode);
            info.AddValue("PDescript", PDescript);
            info.AddValue("PInDate", PInDate);
            info.AddValue("PQoh", PQoh);
            info.AddValue("PMin", PMin);
            info.AddValue("PPrice", PPrice);
            info.AddValue("PDiscount", PDiscount);
            info.AddValue("VCode", VCode);

        }
    }
}
