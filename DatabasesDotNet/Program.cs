using DatabasesDotNet.DbModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using System.Linq;
using System.Text.Json;
using DatabasesDotNet.DtoModels;

namespace DatabasesDotNet
{
    [Serializable()]
    public class Employee : ISerializable 
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public double Salary { get; set; }

        public Employee() {}
        public Employee(SerializationInfo info, StreamingContext context)
        {
            ID = (int)info.GetValue("ID", typeof(int));
            FirstName = (string)info.GetValue("FirstName", typeof(string));
            LastName = (string)info.GetValue("LastName", typeof(string));
            Salary = (double)info.GetValue("Salary", typeof(double));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", ID);
            info.AddValue("FirstName",FirstName);
            info.AddValue("LastName", LastName);
            info.AddValue("Salary", Salary);



        }

        public override string ToString()
        {
            return $"{ID} {FirstName} {LastName} {Salary}";
        }
    }
    class Program
    {
        private static readonly salecoContext _context = new salecoContext();
        static void Main(string[] args)
        {
            var products = _context.Products.ToList();
            var productsDto = new List<ProductDto>();


            foreach (var p in products)
            {
                ProductDto prod = new ProductDto
                {
                    PCode = p.PCode,
                    PDescript = p.PDescript,
                    PDiscount = p.PDiscount,
                    PInDate = p.PInDate,
                    PMin = p.PMin,
                    PPrice = p.PPrice,
                    PQoh = p.PQoh,
                    VCode = p.VCode
                };
                productsDto.Add(prod);
            }


           
            string xmlProductsDto = "productsDto.xml";
            ToXmlFile(xmlProductsDto, productsDto);

            string jsonProductsDto = "productsDto.json";
            ToJsonFile(jsonProductsDto, productsDto);

            string binaryProductsDto = "productsDto.dat";
            ToBinaryFile(binaryProductsDto, productsDto);

            List<SerializedFile> fileList = new List<SerializedFile>
            {
                new SerializedFile{
                    Name = xmlProductsDto,
                    Size = new FileInfo(xmlProductsDto).Length },
                new SerializedFile{
                    Name = jsonProductsDto,
                    Size = new FileInfo(jsonProductsDto).Length },
                new SerializedFile{
                    Name = binaryProductsDto,
                    Size = new FileInfo(binaryProductsDto).Length },

            };

            fileList.Sort();
            int place = 1;
            foreach (var file in fileList)
            {
                Console.WriteLine($"{place++} {file.Name} : {file.Size} bytes");
            }


        //    string binaryFile = "Employee.dat";

        //    List<Employee> employees = new List<Employee>
        //    {
        //        new Employee { ID = 101, FirstName = "Mark", LastName = "Johnson", Salary = 1850 },
        //        new Employee { ID = 102, FirstName = "Lucy", LastName = "Doe", Salary = 1900 },
        //        new Employee { ID = 103, FirstName = "Tracy", LastName = "Swanson", Salary = 2150 },
        //        new Employee { ID = 104, FirstName = "John", LastName = "Hill", Salary = 2200 },
        //    };

        //    using (Stream st = File.Open(binaryFile, FileMode.Create))
        //    {
        //        BinaryFormatter bf = new BinaryFormatter();

        //        bf.Serialize(st, employees);
        //    }

        //    using (Stream st = File.Open(binaryFile, FileMode.Open))
        //    {
        //        BinaryFormatter bf = new BinaryFormatter();

        //        var list = (List<Employee>)bf.Deserialize(st);

        //        foreach (var emp in list)
        //        {
        //            Console.WriteLine(emp);
        //        }
        //    }
        }

        public static void ToBinaryFile<T>(string file, T obj)
        {
            using (Stream st = File.Open(file, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(st, obj);
            }
        }

        public static void ToJsonFile<T>(string file, T obj)
        {
            string json = JsonSerializer.Serialize(obj);
                File.WriteAllText(file, json);
        }
        public static T FromXmlSerializer<T>(string file)
        {
            XmlSerializer xmls = new XmlSerializer(typeof(T));
            var xmlContent = File.ReadAllText(file);

            using (StringReader sr = new StringReader(xmlContent))
            {
                return (T)xmls.Deserialize(sr);
            }

        }

        public static void ToXmlFile<T>(string file, T obj)
        {
            using (StringWriter sw = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmls = new XmlSerializer(typeof(T));
                xmls.Serialize(sw, obj);
                File.WriteAllText(file, sw.ToString());
            }
        }

    }
}
