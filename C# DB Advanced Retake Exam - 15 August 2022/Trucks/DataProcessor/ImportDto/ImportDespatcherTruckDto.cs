namespace Trucks.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using Trucks.Shared;

    [XmlType("Truck")]
    public class ImportDespatcherTruckDto
    {
        [RegularExpression(GlobalConstants.TruckRegistrationNumberRegex)]
        [XmlElement(nameof(RegistrationNumber))]
        public string RegistrationNumber { get; set; }

        [Required]
        [StringLength(GlobalConstants.TruckVinNumberLength)]
        [XmlElement(nameof(VinNumber))]

        public string VinNumber { get; set; }

        [Range(GlobalConstants.TruckTankCapacityMinRange,GlobalConstants.TruckTankCapacityMaxRange)]
        [XmlElement(nameof(TankCapacity))]

        public int TankCapacity { get; set; }

        [Range(GlobalConstants.TruckCargoCapacityMinRange,GlobalConstants.TruckCargoCapacityMaxRange)]
        [XmlElement(nameof(CargoCapacity))]

        public int CargoCapacity { get; set; }

        [Required]
        [Range(GlobalConstants.CategoryTypeRangeMin,GlobalConstants.CategoryTypeRangeMax)]
        [XmlElement(nameof(CategoryType))]

        public int CategoryType { get; set; }

        [Required]
        [Range(GlobalConstants.MakeTypeRangeMin, GlobalConstants.MakeTypeRangeMax)]
        [XmlElement(nameof(MakeType))]

        public int MakeType { get; set; }
    }
}
