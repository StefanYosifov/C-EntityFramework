namespace Trucks.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using Data;
    using Newtonsoft.Json;
    using Trucks.Data.Models;
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
            return "";
        }
        public static string ImportClient(TrucksContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            
            ImportClientDto[] clientDtos = JsonConvert.DeserializeObject<ImportClientDto[]>(jsonString);

            ICollection<Client> clients = new HashSet<Client>();
            ICollection<Truck> trucks = new HashSet<Truck>();

            foreach(var client in clientDtos)
            {
                if (IsValid(client))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                foreach(var truck in trucks.Distinct())
                {
                    if (context.Trucks.Any(ct => ct.Id != truck.Id))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    
                    trucks.Add(truck);
                }
                Client validClient=Mapper.Map<Client>(client);
                clients.Add(validClient);
                sb.AppendLine(String.Format(SuccessfullyImportedClient,client.Name,client.Trucks.Count()));
            }

            context.AddRange(clients);
            context.SaveChanges();
            return sb.ToString().Trim();

        }

        private static bool IsValid(object dto)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
