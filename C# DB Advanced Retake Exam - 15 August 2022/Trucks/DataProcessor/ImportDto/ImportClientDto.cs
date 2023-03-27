namespace Trucks.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using Trucks.Shared;

    public class ImportClientDto
    {

        [Required]
        [MinLength(GlobalConstants.ClientNameMinLength)]
        [MaxLength(GlobalConstants.ClientNameMaxLength)]

        public string Name { get; set; } 

        [Required]
        [MinLength(GlobalConstants.ClientNationalityMinLength)]
        [MaxLength(GlobalConstants.ClientNationalityMaxLength)]
        public string Nationality { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;

        public int[] Trucks { get; set; }

    }
}
