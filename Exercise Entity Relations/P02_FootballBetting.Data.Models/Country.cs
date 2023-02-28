namespace P02_FootballBetting.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        public Country()
        {
            this.Towns = new HashSet<Town>();
        }

        [Key]
        public int CountryId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        //TODO Navigation collections
        public virtual ICollection<Town> Towns { get; set; }
    }
}
