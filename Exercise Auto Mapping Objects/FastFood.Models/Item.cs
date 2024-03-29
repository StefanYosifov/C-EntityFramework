﻿namespace FastFood.Models
{
    using FastFood.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Item
    {
        public Item()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }

        [Key]
        [MaxLength(ValidationConstants.GuidIdMaxLength)]
        public string Id { get; set; }

        [StringLength(30, MinimumLength = 3)]
        public string? Name { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; } = null!;

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } 
    }
}