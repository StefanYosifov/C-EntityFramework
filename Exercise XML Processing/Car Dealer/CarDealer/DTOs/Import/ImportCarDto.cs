namespace CarDealer.DTOs.Import
{
    using CarDealer.Models;
    using System.Xml.Serialization;

    [XmlType(nameof(Car))]
    public class ImportCarDto
    {
        [XmlElement("make")]
        public string Make { get; set; } = null!;

        [XmlElement("model")]
        public string Model { get; set; } = null!;

        [XmlElement("traveledDistance")]
        public long TravelledDistance { get; set; }

        [XmlElement("parts")]
        public ImportCarPartDto[] Parts { get; set; }

    }
}
