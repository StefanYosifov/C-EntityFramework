namespace FastFood.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Enums;
    using FastFood.Common;

    public class Order
    {

        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
            this.OrderItems = new HashSet<OrderItem>();
        }

        [Key]
        [MaxLength(ValidationConstants.GuidIdMaxLength)]
        public string Id { get; set; }

        [Required]
        public string Customer { get; set; } = null!;

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public OrderType Type { get; set; }

        [NotMapped]
        public decimal TotalPrice { get; set; }

        [ForeignKey(nameof(Employee))]
        [MaxLength(ValidationConstants.GuidIdMaxLength)]
        public string EmployeeId { get; set; }

        [Required]
        public Employee Employee { get; set; } = null!;

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}