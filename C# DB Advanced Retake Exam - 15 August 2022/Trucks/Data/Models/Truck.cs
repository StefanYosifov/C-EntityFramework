namespace Trucks.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Trucks.Data.Models.Enums;
    using Trucks.Shared;

    public class Truck
    {
        public Truck()
        {
            this.ClientsTrucks = new HashSet<ClientTruck>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(GlobalConstants.TruckRegistrationNumberMaxLength)]
        public string RegistrationNumber { get; set; }

        [Required]
        [MaxLength(GlobalConstants.TruckVinNumberLength)]
        public string VinNumber { get; set; } = null!;

        [MaxLength(GlobalConstants.TruckTankCapacityMaxRange)]
        public int TankCapacity { get; set; }

        [MaxLength(GlobalConstants.TruckCargoCapacityMaxRange)]

        public int CargoCapacity { get; set; }

        [Required]
        public CategoryType CategoryType { get; set; }

        [Required]
        public MakeType MakeType { get; set; }

        [Required]
        [ForeignKey(nameof(Despatcher))]
        public int DespatcherId { get; set; }

        public Despatcher Despatcher { get; set; }

        public virtual ICollection<ClientTruck> ClientsTrucks { get; set; }

    }
}
