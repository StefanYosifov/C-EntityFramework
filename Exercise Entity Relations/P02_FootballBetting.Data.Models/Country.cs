namespace P02_FootballBetting.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        //TODO Navigation collections
    }
}
