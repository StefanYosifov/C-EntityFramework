namespace CarDealer.DTOs.Import
{


    public class ImportCarsDto
    {
        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public long TravelledDistance { get; set; }

        public int SupplierId { get; set; }

    }
}
