namespace Footballers.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;


    [XmlType("Coach")]
    public class ImportCoachDto
    {

        [Required]
        [StringLength(40, MinimumLength = 2)]
        [XmlElement(nameof(Name))]
        public string Name { get; set; } = null!;

        [Required]
        [XmlElement(nameof(Nationality))]
        public string Nationality { get; set; } = null!;

        [XmlArray(nameof(Footballers))]
        public ImportFootballerDto[] ImportFootballerDtos { get; set; }

    }
}
