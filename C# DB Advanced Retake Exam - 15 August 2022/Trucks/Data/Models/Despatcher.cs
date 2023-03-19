namespace Trucks.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Trucks.Common;

    public class Despatcher
    {
        public Despatcher()
        {
            this.Trucks = new HashSet<Truck>();
        }

        [Key()]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.DespatcherNameMaxLength)]
        public string Name { get; set; }

        public string Position { get; set; }

        public virtual ICollection<Truck> Trucks { get; set; }
    }
}
