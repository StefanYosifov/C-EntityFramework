namespace SoftJail.Data.Models
{
    using SoftJail.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Prisoner
    {
        public Prisoner()
        {
            this.PrisonerOfficers = new HashSet<OfficerPrisoner>();
            this.Mails = new HashSet<Mail>();
        }


        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.PrisonerFullNameMaxLength)]
        public string FullName { get; set; }

        [Required]
        public string Nickname  { get; set; }

        [Required]
        [Range(ValidationConstants.PrisonerAgeMin,ValidationConstants.PrisonerAgeMax)]
        public int Age { get; set; }

        public DateTime IncarcerationDate  { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public decimal? Bail { get; set; }

        [ForeignKey(nameof(Cell))]
        public int? CellId { get; set; }
       
        public Cell Cell { get; set; }
       
        public ICollection<Mail> Mails{ get; set; }
      
        public ICollection<OfficerPrisoner> PrisonerOfficers  { get; set; }
    }
}