namespace Artillery.DataProcessor.ImportDto
{
    using Artillery.GlobalConstants;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Manufacturers")]
    public class ImportManufacturerDto
    {
        [Required]
        [MinLength(GlobalConstants.ManufacturerNameMinLength)]
        [MaxLength(GlobalConstants.ManufacturerNameMaxLength)]
        [XmlElement(nameof(ManufacturerName))]
        public string ManufacturerName { get; set; } = null!;



        [Required]
        [MinLength(GlobalConstants.ManufacturerFoundedMinLength)]
        [MaxLength(GlobalConstants.ManufacturerFoundedMaxLength)]
        [XmlElement(nameof(Founded))]
        public string Founded { get; set; } = null!;

    }
}
