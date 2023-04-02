namespace Boardgames.DataProcessor.ImportDto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using GlobalConstants;

    [XmlType("Creator")]
    public class ImportCreatorDto
    {
        [Required]
        [MinLength(GlobalConstants.CreatorFirstNameMinLength)]
        [MaxLength(GlobalConstants.CreatorFirstNameMaxLength)]
        [XmlElement(nameof(FirstName))]

        public string FirstName { get; set; } = null!;


        [Required]
        [MinLength(GlobalConstants.CreatorLastNameMinLength)]
        [MaxLength(GlobalConstants.CreatorLastNameMaxLength)]
        [XmlElement(nameof(LastName))]

        public string LastName { get; set; }= null!;

        [XmlArray("Boardgames")]
        public ImportCreatorBoardgameDto[] Boardgame { get; set; }=null!;
    }
}
