namespace Trucks.DataProcessor.ImportDto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Trucks.Shared;

    [XmlType("Despatcher")]
    public class ImportDespatchersDto
    {
        [Required]
        [MinLength(GlobalConstants.DespatcherNameMinLength)]
        [MaxLength(GlobalConstants.DespatcherNameMaxLength)]
        [XmlElement(nameof(Name))]
        public string Name { get; set; }

        [XmlElement(nameof(Position))]
        public string Position { get; set; }

        [XmlArray(nameof(Trucks))]
        public ImportDespatcherTruckDto[] Trucks { get; set; }
    }
}
