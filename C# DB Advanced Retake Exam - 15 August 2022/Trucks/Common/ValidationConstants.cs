namespace Trucks.Common
{
   

    internal class ValidationConstants
    {
        public const int TruckRegistrationNumberMaxLength = 8;
        public const int TruckVinNumberMaxLength = 17;
        public const int TankMinCapacity = 950;
        public const int TankMaxCapacity = 1420;
        public const int CargoMinCapacity = 5000;
        public const int CargoMaxCapacity = 29000;
        public const string RegistrationNumberValidation = @"/([A-Z]{2}[\d]{4}[A-Z]{2})/g";

        public const int ClientNameMinLength = 3;
        public const int ClientNameMaxLength = 40;
        public const int ClientNationalityMinLength = 2;
        public const int ClientNationalityMaxLength = 40;

        public const int DespatcherNameMinLength = 2;
        public const int DespatcherNameMaxLength = 40;


    }
}
