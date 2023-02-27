namespace P02_FootballBetting.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Team
    {
        [Key] // -> Unique, NOT NULL
        public int TeamId { get; set; }

        [Required] // NOT NULL
        [MaxLength(40)]
        public string Name { get; set; } = null!;

        [MaxLength(100)]
        public string LogoUrl { get; set; }

        [Required]
        [MaxLength(4)]
        public string Initials { get; set; }

        public decimal Budget { get; set; } // Not null by default

        public int PrimaryKitColorId { get; set; }

        public int SecondaryKitColorId { get; set; }

        public int TownId { get; set; }

    }
}