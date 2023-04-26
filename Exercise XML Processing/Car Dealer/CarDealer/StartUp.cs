namespace CarDealer
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CarDealer.Data;
    using CarDealer.DTOs.Export;
    using CarDealer.DTOs.Import;
    using CarDealer.Models;
    using CarDealer.Utilities;
    using System.Text;
    using System.Xml.Serialization;

    public class StartUp
    {
        public static void Main()
        {

            using var context = new CarDealerContext();
            string xmlDatasets = "../../../Datasets/";

            //string suppliersXml = File.ReadAllText(xmlDatasets + "suppliers.xml");
            //Console.WriteLine(ImportSuppliers(context, suppliersXml));


            //string partsXml = File.ReadAllText(xmlDatasets + "parts.xml");
            //Console.WriteLine(ImportParts(context, partsXml));

            Console.WriteLine(GetCarsWithDistance(context));

        }

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            IMapper mapper = CreateMapper();

            var bmwCars = context.Cars
                .Where(c => c.Make == "Bmw")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ProjectTo<ExportCarDto>(mapper.ConfigurationProvider)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            XmlRootAttribute root = new XmlRootAttribute("cars");
            XmlSerializer serializer = new XmlSerializer(typeof(ExportCarDto[]), root);

            using StringWriter writer = new StringWriter(sb);
            serializer.Serialize(writer, bmwCars);

            return sb.ToString().TrimEnd();
        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            IMapper mapper = CreateMapper();

            var exportCarDtos = context.Cars.Where(c => c.TravelledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ProjectTo<ExportCarDto>(mapper.ConfigurationProvider)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            XmlRootAttribute root = new XmlRootAttribute("cars");
            XmlSerializer serializer=new XmlSerializer(typeof(ExportCarDto[]),root);

            using StringWriter writer=new StringWriter(sb);
            serializer.Serialize(writer,exportCarDtos);

            return sb.ToString().TrimEnd();
        }


        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlHelper helper = new XmlHelper();
            IMapper mapper = CreateMapper();

            ImportCarDto[] importCarDtos= helper.Deserialize<ImportCarDto[]>(inputXml, "Cars");

            ICollection<Car> validCars = new HashSet<Car>();

            foreach(var carDto in importCarDtos)
            {
                if(string.IsNullOrEmpty(carDto.Make) || string.IsNullOrEmpty(carDto.Model))
                {
                    continue;
                }

                ICollection<PartCar> parts = new HashSet<PartCar>();

                foreach(var importedCarParts in carDto.Parts.DistinctBy(p => p.PartId))
                {
                    if (!context.Parts.Any(p => p.Id == importedCarParts.PartId))
                    {
                        continue;
                    }

                    //PartCar carPart = new PartCar()
                    //{
                    //    PartId = importedCarParts.PartId
                    //};
                    //validCars.Add(carPart);
                }
            }
                return "a";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlHelper helper = new XmlHelper();
            IMapper mapper = CreateMapper();

            ImportPartDto[] partDtos = helper.Deserialize<ImportPartDto[]>(inputXml, "Parts");
            ICollection<Part> validParts = new HashSet<Part>();


            foreach(var part in partDtos)
            {
                if (string.IsNullOrEmpty(part.Name))
                {
                    continue;
                }
                if(context.Suppliers.Any(s => s.Id == part.SupplierId))
                {
                    continue;
                }

                var validPart=mapper.Map<Part>(part);
                validParts.Add(validPart);
            }

            context.AddRange(validParts);
            context.SaveChanges();

            return $"Successfully imported {validParts.Count}"; ;
        }
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            XmlHelper helper = new XmlHelper();
            ImportSuplierDto[] suplierDtos = helper.Deserialize<ImportSuplierDto[]>(inputXml, "Suppliers");

            ICollection<Supplier> suppliers = new HashSet<Supplier>();
            IMapper mapper = CreateMapper();
            foreach(var supplierDto in suplierDtos)
            {
                if (string.IsNullOrEmpty(supplierDto.Name))
                {
                    continue;
                }

                var supplier = mapper.Map<Supplier>(supplierDto);
                suppliers.Add(supplier);

            }

            context.AddRange(suppliers);
            context.SaveChanges();
            return $"Successfully imported {suppliers.Count}";
        }

        private static IMapper CreateMapper()
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });

            IMapper mapper = new Mapper(configuration);

            return mapper;
           
        }
    }
}