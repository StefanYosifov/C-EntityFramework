namespace Artillery.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using Artillery.GlobalConstants;
    public class ImportGunsDto
    {

        [Required]
        public int ManufacturerId { get; set; }

        [Required]
        [Range(GlobalConstants.GunWeightMinRange,GlobalConstants.GunWeightMaxRange)]
        public int GunWeight { get; set; }

        [Required]
        [Range(GlobalConstants.GunBarrelLengthMin,GlobalConstants.GunBarrelLengthMax)]
        public double BarrelLength { get; set; }

        public int? NumberBuild { get; set; }

        [Required]
        [Range(GlobalConstants.GunRangeMin,GlobalConstants.GunRangeMax)]
        public int Range { get; set; }

        [Required]
        public string GunType { get; set; } = null!;

        [Required]
        public int ShellId { get; set; }

        public ImportGunCountryDto[] Countries { get; set; } = null!;

    }
}
