namespace FastFood.Core.ViewModels.Positions
{
    using System.ComponentModel.DataAnnotations;

    public class CreatePositionInputModel
    {
        [StringLength(30, MinimumLength = 3)]
        public string PositionName { get; set; }
    }
}