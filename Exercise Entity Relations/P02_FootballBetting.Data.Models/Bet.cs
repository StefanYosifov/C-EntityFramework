namespace P02_FootballBetting.Data.Models
{
    using P02_FootballBetting.Data.Models.NewFolder;
    using System.ComponentModel.DataAnnotations;

    public class Bet
    {
        [Key]
        public int BetId { get; set; }

        public decimal Amount { get; set; }

        public Prediction Predictions { get; set; }

        public DateTime DateTime { get; set; }

        public int UserId { get; set; }

        public int GameId { get; set; }
    }
}
