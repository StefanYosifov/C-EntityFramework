namespace Trucks.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using Trucks.Data.Models;
    using Trucks.Data.Models.Enums;
    using Trucks.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedDespatcher
            = "Successfully imported despatcher - {0} with {1} trucks.";

        private const string SuccessfullyImportedClient
            = "Successfully imported client - {0} with {1} trucks.";

        public static string ImportDespatcher(TrucksContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            string rootElement = "Despatchers";

            ImportDespatchersDto[] despatchersDtos = DeserializeXml<ImportDespatchersDto>(xmlString, rootElement);

            ICollection<Despatcher> despatchers = new HashSet<Despatcher>();

            foreach(var despatcher in despatchersDtos)
            {
                if (!IsValid(despatcher))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (string.IsNullOrWhiteSpace(despatcher.Position))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Despatcher validDespatcher = new Despatcher()
                {
                    Name = despatcher.Name,
                    Position = despatcher.Position,
                };

                foreach(var truck in despatcher.Trucks)
                {
                    if (!IsValid(truck))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    Truck validTruck = new Truck()
                    {
                        RegistrationNumber = truck.RegistrationNumber,
                        VinNumber = truck.VinNumber,
                        TankCapacity = truck.TankCapacity,
                        CargoCapacity = truck.CargoCapacity,
                        CategoryType = (CategoryType)truck.CategoryType,
                        MakeType = (MakeType)truck.MakeType,
                    };
                    validDespatcher.Trucks.Add(validTruck);
                }
                despatchers.Add(validDespatcher);
                sb.AppendLine(string.Format(SuccessfullyImportedDespatcher, validDespatcher.Name, validDespatcher.Trucks.Count));
            }
            context.Despatchers.AddRange(despatchers);
            context.SaveChanges();
            return sb.ToString().Trim();

        }

        public static T[] DeserializeXml<T>(string xmlString,string rootElement)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T[]),new XmlRootAttribute(rootElement));
            using var stringReader = new StringReader(xmlString);

            return (T[])xmlSerializer.Deserialize(stringReader);
        }

        public static string ImportClient(TrucksContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            ImportClientDto[] ClientDtos=JsonConvert.DeserializeObject<ImportClientDto[]>(jsonString);

            ICollection<Client> validClients = new HashSet<Client>();

            foreach(var clientDto in ClientDtos)
            {
                if (!IsValid(clientDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (clientDto.Type == "usual")
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Client validClient = new Client()
                {
                    Name = clientDto.Name,
                    Nationality = clientDto.Nationality,
                    Type = clientDto.Type,
                };

                foreach(var truckId in clientDto.Trucks.Distinct())
                {
                    var truck = context.Trucks.Find(truckId);
                    if (truck==null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    validClient.ClientsTrucks.Add(new ClientTruck()
                    {
                        Truck = truck
                    });
                }
                validClients.Add(validClient);
                sb.AppendLine(string.Format(SuccessfullyImportedClient, validClient.Name, validClient.ClientsTrucks.Count));

            }
            context.Clients.AddRange(validClients);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}