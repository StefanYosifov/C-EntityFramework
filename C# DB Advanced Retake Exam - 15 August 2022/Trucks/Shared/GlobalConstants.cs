namespace Trucks.Shared
{
    public static class GlobalConstants
    {
        //Truck
        public const int TruckRegistrationNumberMaxLength = 8;
        public const string TruckRegistrationNumberRegex = @"^([A-Z]{2,2}[0-9]{4,4}[A-Z]{2,2})$";

        public const int TruckVinNumberLength = 17;

        public const int TruckTankCapacityMinRange = 950;
        public const int TruckTankCapacityMaxRange = 1420;

        public const int TruckCargoCapacityMinRange = 5000;
        public const int TruckCargoCapacityMaxRange = 29000;



        //Client
        public const int ClientNameMinLength = 3;
        public const int ClientNameMaxLength = 40;

        public const int ClientNationalityMinLength = 2;
        public const int ClientNationalityMaxLength = 40;

        //Despatcher
        public const int DespatcherNameMinLength = 2;
        public const int DespatcherNameMaxLength = 40;

        //enums
        public const int CategoryTypeRangeMin = 0;
        public const int CategoryTypeRangeMax = 3;

        public const int MakeTypeRangeMin = 0;
        public const int MakeTypeRangeMax = 4;

    }
}
