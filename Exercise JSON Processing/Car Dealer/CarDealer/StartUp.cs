namespace CarDealer
{
    using AutoMapper;
    using CarDealer.Data;
    using CarDealer.DTOs.Import;
    using CarDealer.Models;
    using Newtonsoft.Json;

    public class StartUp
    {
        public static void Main()
        {

            using CarDealerContext context = new CarDealerContext();
            string datasetsPath = "../../../Datasets/";


            //string suppliesImport = File.ReadAllText(datasetsPath+"suppliers.json");
            //Console.WriteLine(ImportSuppliers(context,suppliesImport));

            //string partsImport = File.ReadAllText(datasetsPath + "parts.json");
            //Console.WriteLine(ImportParts(context,partsImport));

            string carsImport = File.ReadAllText(datasetsPath + "cars.json");
            //Console.WriteLine(ImportParts(context,partsImport));

        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {

        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            IMapper mapper = CreateMapper();
            var deserializedParts=JsonConvert.DeserializeObject<ICollection<ImportPartsDto>>(inputJson);

            var parts = new HashSet<Part>();

            foreach(var part in deserializedParts)
            {
                if(context.Suppliers.Find(part.SupplierId) == null || part==null)
                {
                    continue;
                }
                var partToAdd=mapper.Map<Part>(part);
                parts.Add(partToAdd);
            }

           context.AddRange(parts);
           context.SaveChanges();
           return $"Successfully imported {parts.Count}.";
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            IMapper mapper = CreateMapper();

            var deserializedImports = JsonConvert.DeserializeObject<IEnumerable<ImportSuppliersDto>>(inputJson);

            ICollection<Supplier> modelSuppliers = mapper.Map<ICollection<Supplier>>(deserializedImports);

            context.Suppliers.AddRange(modelSuppliers);
            context.SaveChanges();
            return $"Successfully imported {modelSuppliers.Count}.";
        }

        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });

           return  new Mapper(config);
        }
    }


}