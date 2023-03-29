namespace Artillery.DataProcessor.ImportDto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Artillery.GlobalConstants;

    [XmlType("Country")]
    public class ImportCountryDto
    {
        [Required]
        [MinLength(GlobalConstants.CountryNameMinLength)]
        [MaxLength(GlobalConstants.CountryNameMaxLength)]
        public string CountryName { get; set; } = null!;

        [Required]
        [Range(GlobalConstants.CountryArmySizeMinSize,GlobalConstants.CountryArmySizeMaxSize)]
        public int ArmySize { get; set; }

    }
}
