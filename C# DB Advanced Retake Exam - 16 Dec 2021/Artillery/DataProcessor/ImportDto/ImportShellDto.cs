namespace Artillery.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using Artillery.GlobalConstants;

    [XmlType("Shell")]
    public class ImportShellDto
    {

        [Required]
        [Range(GlobalConstants.ShellWeightMinRange,GlobalConstants.ShellWeightMaxRange)]
        [XmlElement(nameof(ShellWeight))]
        public double ShellWeight { get; set; }

        [Required]
        [MinLength(GlobalConstants.ShellCaliberMinLength)]
        [MaxLength(GlobalConstants.ShellCaliberMaxLength)]
        [XmlElement(nameof(Caliber))]
        public string Caliber { get; set; } = null!;

    }
}
