namespace CarDealer.DTOs.Import
{
    using CarDealer.Models;
    using System.Xml.Serialization;

    [XmlType(nameof(Part))]
    public class ImportPartDto
    {
        [XmlElement("name")]
        public string Name { get; set; } = null!;


        [XmlElement("price")]
        public decimal Price { get; set; }


        [XmlElement("quantity")]
        public int Quantity { get; set; }

        [XmlElement("supplierId")]
        public int? SupplierId { get; set; }

    }
}
