namespace Boardgames.GlobalConstants
{
    public class GlobalConstants
    {

        //Boardgame
        public const int BoardgameNameMinLength = 10;
        public const int BoardgameNameMaxLength = 20;

        public const int BoardRatingMinValue = 1;
        public const double BoardRatingMaxValue = 10;

        public const int BoardYearPublishedMin = 2018;
        public const int BoardYearPublishedMax = 2023;

        //Seller
        public const int SellerNameMinLength = 5;
        public const int SellerNameMaxLength = 20;

        public const int SellerAddressMinLength = 2;
        public const int SellerAddressMaxLength = 30;

        public const string SellerWebsiteRegex = @"^(www.[A-z\-\d]+.com)$";

        //Creator
        public const int CreatorFirstNameMinLength = 2;
        public const int CreatorFirstNameMaxLength = 7;

        public const int CreatorLastNameMinLength = 2;
        public const int CreatorLastNameMaxLength = 7;

    }
}
