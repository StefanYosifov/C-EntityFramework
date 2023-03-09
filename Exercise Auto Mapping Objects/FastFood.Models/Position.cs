namespace FastFood.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Position
    {

        public Position()
        {
            this.Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public ICollection<Employee> Employees { get; set; }
    }
}