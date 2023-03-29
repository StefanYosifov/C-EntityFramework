﻿namespace Artillery.Data.Models
{
    using Artillery.Data.Models.Enums;
    using Artillery.GlobalConstants;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public class Gun
    {
        public Gun()
        {
            this.CountriesGuns = new HashSet<CountryGun>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerId  { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        [Required]
        [MaxLength(GlobalConstants.GunWeightMaxRange)]
        public int GunWeight { get; set; }

        [Required]
        [MaxLength(GlobalConstants.GunBarrelLengthMax)]
        public double BarrelLength  { get; set; }

        public int? NumberBuild  { get; set; }

        [Required]
        [MaxLength(GlobalConstants.GunRangeMax)]
        public int Range  { get; set; }

        [Required]
        public GunType GunType  { get; set; }

        [Required]
        [ForeignKey(nameof(ShellId))]
        public int ShellId   { get; set; }

        public virtual Shell Shell { get; set; } = null!;

        public virtual ICollection<CountryGun> CountriesGuns  { get; set; }



    }
}
