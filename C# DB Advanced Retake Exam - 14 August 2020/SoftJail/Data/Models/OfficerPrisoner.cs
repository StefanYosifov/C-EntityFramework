namespace SoftJail.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class OfficerPrisoner
    {
       
        public int PrisonerId { get; set; }

        [Required]
        public virtual Prisoner Prisoner { get; set; }

       
        public int OfficerId { get; set; }

        [Required]
        public virtual Officer Officer { get; set; }

    }
}
