namespace CarDealer.DTOs.Import
{
    using CarDealer.Models;
    using System.Xml.Serialization;

    [XmlType(nameof(Supplier))]
    public class ImportSuplierDto
    {
        [XmlElement("name")]
        public string Name { get; set; } = null!;

        [XmlElement("isImporter")]

        public bool IsImporter { get; set; }

    }
}
