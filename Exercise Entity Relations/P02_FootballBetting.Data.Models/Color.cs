namespace P02_FootballBetting.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Color
    {
        [Key]
        public int ColorId { get; set; }

        [Required]  
        [MaxLength(10)]
        public string Name { get; set; }

    }
}
