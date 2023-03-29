namespace Artillery.Data.Models
{
    using Artillery.GlobalConstants;
    using System.ComponentModel.DataAnnotations;

    public class Shell
    {
        public Shell()
        {
            this.Guns = new HashSet<Gun>();
        }


        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.ShellWeightMaxRange)]
        public double ShellWeight  { get; set; }

        [Required]
        [MaxLength(GlobalConstants.ShellCaliberMaxLength)]
        public string Caliber { get; set; } = null!;

        public virtual ICollection<Gun> Guns { get; set; }


    }
}
