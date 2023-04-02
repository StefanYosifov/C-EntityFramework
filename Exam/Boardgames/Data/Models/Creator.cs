namespace Boardgames.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Boardgames.GlobalConstants;

    public class Creator
    {
        public Creator()
        {
            this.Boardgames = new HashSet<Boardgame>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.CreatorFirstNameMaxLength)]
        public string FirstName  { get; set; } = null!;

        [Required]
        [MaxLength(GlobalConstants.CreatorLastNameMaxLength)]
        public string LastName  { get; set; } = null!;

        public virtual ICollection<Boardgame>  Boardgames { get; set; }
    }
}
