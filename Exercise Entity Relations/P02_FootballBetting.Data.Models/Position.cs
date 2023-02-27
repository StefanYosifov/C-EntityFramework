namespace P02_FootballBetting.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Position
    {
        [Key]
        public int PositionId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

    }
}
