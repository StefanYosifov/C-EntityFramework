using Trucks.Common;

namespace Trucks.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Client
    {
        public Client()
        {
            this.ClientsTrucks = new HashSet<ClientTruck>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.ClientNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.ClientNationalityMaxLength)]
        public string Nationality { get; set; }

        public string Type { get; set; }

        public virtual ICollection<ClientTruck> ClientsTrucks  { get;set; }
    }
}
