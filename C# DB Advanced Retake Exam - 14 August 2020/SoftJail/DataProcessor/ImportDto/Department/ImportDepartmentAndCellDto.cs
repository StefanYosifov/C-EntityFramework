namespace SoftJail.DataProcessor.ImportDto.Department
{
    using Newtonsoft.Json;
    using SoftJail.Common;
    using System.ComponentModel.DataAnnotations;

    public class ImportDepartmentAndCellDto
    {
        [Required]
        [MinLength(ValidationConstants.DepartmentNameMinLength)]
        [MaxLength(ValidationConstants.DepartmentNameMaxLength)]
        [JsonProperty(nameof(Name))]
        public string Name { get; set; }

        [JsonProperty(nameof(Cells))]
        public ImportCellDto[] Cells { get; set; }
    }
}
