namespace SoftJail.DataProcessor.ImportDto.Department
{
    using Newtonsoft.Json;
    using SoftJail.Common;
    using System.ComponentModel.DataAnnotations;

    public class ImportCellDto
    {
        [Required]
        [Range(ValidationConstants.CellCellNumberMinNumber, ValidationConstants.CellCellNumberMaxNumber)]
        [JsonProperty(nameof(CellNumber))]
        public int CellNumber { get; set; }

        [JsonProperty(nameof(HasWindow))]
        public bool HasWindow { get; set; }

    }
}
