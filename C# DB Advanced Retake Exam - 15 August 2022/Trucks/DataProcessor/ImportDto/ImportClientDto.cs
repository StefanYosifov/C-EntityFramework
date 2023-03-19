namespace Trucks.DataProcessor.ImportDto
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using Trucks.Common;
    using Trucks.Data.Models;

    public class ImportClientDto
    {
        [MinLength(ValidationConstants.ClientNameMinLength)]
        [MaxLength(ValidationConstants.ClientNameMaxLength)]
        [JsonProperty(nameof(Name))]
        public string Name { get; set; }


        [MinLength(ValidationConstants.ClientNationalityMinLength)]
        [MaxLength(ValidationConstants.ClientNationalityMaxLength)]
        [JsonProperty(nameof(Nationality))]

        public string Nationality { get; set; }

        [JsonProperty(nameof(Type))]
        public string Type { get; set; }


        [JsonProperty(nameof(Trucks))]
        public Truck[] Trucks { get; set; }

    }
}
