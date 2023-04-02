namespace Boardgames.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using GlobalConstants;

    [XmlType("Boardgame")]
    public class ImportCreatorBoardgameDto
    {

        [Required]
        [MinLength(GlobalConstants.BoardgameNameMinLength)]
        [MaxLength(GlobalConstants.BoardgameNameMaxLength)]
        [XmlElement(nameof(Name))]
        public string Name { get; set; } = null!;

        [Required]
        [Range(GlobalConstants.BoardRatingMinValue, GlobalConstants.BoardRatingMaxValue)]
        [XmlElement(nameof(Rating))]
        public double Rating { get; set; }

        [Required]
        [Range(GlobalConstants.BoardYearPublishedMin, GlobalConstants.BoardYearPublishedMax)]
        [XmlElement(nameof(YearPublished))]
        public int YearPublished { get; set; }

        [Required]
        [Range(0,4)]
        [XmlElement(nameof(CategoryType))]

        public int CategoryType { get; set; }


        [Required]
        [XmlElement(nameof(Mechanics))]
        public string Mechanics { get; set; } = null!;
    }
}
