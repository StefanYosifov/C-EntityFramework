namespace SoftJail.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class ValidationConstants
    {
        public const int PrisonerFullNameMinLength = 3;
        public const int PrisonerFullNameMaxLength = 20;
        public const int PrisonerAgeMin = 18;
        public const int PrisonerAgeMax = 65;
        public const string PrisonerNickRegex = @"^(The) ([A-Z][a-z]*)$";


        //Officer
        public const int OfficerFullNameMinLength = 3;
        public const int OfficerFullNameMaxLength = 30;

        //Cell
        public const int CellCellNumberMinNumber = 1;
        public const int CellCellNumberMaxNumber = 1000;

        //Department
        public const int DepartmentNameMinLength = 3;
        public const int DepartmentNameMaxLength = 25;

        //Mail
        public const string MailValidateAdressRegex = @"^([\w\d\s]+)(\sstr.)$";

    }
}
