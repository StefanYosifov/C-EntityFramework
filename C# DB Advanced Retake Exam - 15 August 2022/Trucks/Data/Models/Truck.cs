namespace Trucks.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Trucks.Common;
    using Trucks.Data.Models.Enums;

    public class Truck
    {
        public Truck()
        {
            this.ClientsTrucks = new HashSet<ClientTruck>();
        }


        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.TruckRegistrationNumberMaxLength)]
        [RegularExpression(ValidationConstants.RegistrationNumberValidation)]
        public string RegistrationNumber  { get; set; }

        [MaxLength(ValidationConstants.TruckVinNumberMaxLength)]
        public string VinNumber  { get; set; }

        [Range(ValidationConstants.TankMinCapacity,ValidationConstants.TankMaxCapacity)]
        public int TankCapacity  { get; set; }

        [Range(ValidationConstants.CargoMinCapacity,ValidationConstants.CargoMaxCapacity)]
        public int CargoCapacity  { get; set; }

        public CategoryType CategoryType  { get; set; }

        public MakeType MakeType { get; set; }

        [ForeignKey(nameof(Despatcher))]
        public int DespatcherId { get; set; }
        public Despatcher Despatcher { get; set; }
        public virtual ICollection<ClientTruck> ClientsTrucks { get; set; }


    }
}
