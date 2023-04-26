namespace CarDealer.DTOs.Import
{
    using CarDealer.Models;
    using System.Xml.Serialization;

    [XmlType(nameof(Customer))]
    public class ImportCustomerDto
    {
        [XmlElement("name")]
        public string Name { get; set; } = null!;

        [XmlElement("birthDate")]
        public string BirthDate { get; set; }

        [XmlElement("isYoungDriver")]
        public bool IsYoungDriver { get; set; }
    }
}
