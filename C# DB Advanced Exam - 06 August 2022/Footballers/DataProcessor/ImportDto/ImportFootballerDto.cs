namespace Footballers.DataProcessor.ImportDto
{
    using Footballers.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Footballer")]
    public class ImportFootballerDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        [XmlElement(nameof(Name))]
        public string Name { get; set; } = null!;

        [Required]
        [XmlElement(nameof(ContractStartDate))]
        public string ContractStartDate { get; set; }

        [Required]
        [XmlElement(nameof(ContractEndDate))]
        public string ContractEndDate { get; set; }

        [Required]
        [XmlElement(nameof(PositionType))]
        [Range(0,3)]
        public int PositionType { get; set; }

        [Required]
        [XmlElement(nameof(BestSkillType))]
        [Range(0, 4)]
        public int BestSkillType { get; set; }

    }
}
