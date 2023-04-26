namespace SoftJail.Data.Models
{
    using SoftJail.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class Cell
    {
        public Cell()
        {
            this.Prisoners = new HashSet<Prisoner>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Range(ValidationConstants.CellCellNumberMinNumber, ValidationConstants.CellCellNumberMaxNumber)]
        public int CellNumber { get; set; }
        [Required]
        public bool HasWindow { get; set; }

        [Required]
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }

        [Required]
        public Department Department { get; set; }

        public virtual ICollection<Prisoner> Prisoners { get; set; }
    }
}
