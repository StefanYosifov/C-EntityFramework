namespace Boardgames.DataProcessor.ImportDto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GlobalConstants;

    public class ImportSellerDto
    {

        [Required]
        [MinLength(GlobalConstants.SellerNameMinLength)]
        [MaxLength(GlobalConstants.SellerNameMaxLength)]
        public string Name { get; set; } = null!;
        [Required]
        [MinLength(GlobalConstants.SellerAddressMinLength)]
        [MaxLength(GlobalConstants.SellerAddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        public string Country { get; set; } = null!;

        [Required]
        [RegularExpression(GlobalConstants.SellerWebsiteRegex)]
        public string Website { get; set; } = null!;

        public int[] Boardgames { get; set; } = null!;

    }
}
