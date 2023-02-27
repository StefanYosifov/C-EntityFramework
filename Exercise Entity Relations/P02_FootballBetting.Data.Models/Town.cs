namespace P02_FootballBetting.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Town
    {
        [Key]
        public int TownId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public int CountryId { get; set; }
    }
}
