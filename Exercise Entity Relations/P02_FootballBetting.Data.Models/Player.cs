﻿namespace P02_FootballBetting.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Player
    {
        [Key]
        public int PlayerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }


        public int SquadNumber { get; set; }

        public bool IsInjured { get; set; }

        public int? TeamId { get; set; }

        public int PositionId { get; set; }
    }
}
