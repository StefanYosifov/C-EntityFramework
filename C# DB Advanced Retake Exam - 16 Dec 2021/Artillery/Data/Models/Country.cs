namespace Artillery.Data.Models
{
    using Artillery.GlobalConstants;
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        public Country()
        {
            this.CountriesGuns = new HashSet<CountryGun>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.CountryNameMaxLength)]
        public string CountryName { get; set; } = null!;

        [Required]
        public int ArmySize  { get; set; }

        public virtual ICollection<CountryGun> CountriesGuns  { get; set; }

    }
}
