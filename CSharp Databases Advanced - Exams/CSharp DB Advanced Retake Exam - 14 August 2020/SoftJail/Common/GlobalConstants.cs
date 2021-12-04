using System;
using System.Collections.Generic;
using System.Text;

namespace SoftJail.Common
{
    public static class GlobalConstants
    {
        // Prisoner

        public const int PRISONER_FULL_NAME_MIN_LENGTH = 3;
        public const int PRISONER_FULLNAME_MAX_LENGTH = 20;
        public const int PRISONER_AGE_RANGE_MIN = 18;
        public const int PRISONER_AGE_RANGE_MAX = 65;
        public const string PRISONER_NICKNAME_REGEX = @"^(The )[A-Z]{1}[a-z]+$";
        public const int PRISONER_BAIL_MIN_RANGE = 0;

        // Officer 

        public const int OFFICER_FULLNAME_MIN_LENGTH = 3;
        public const int OFFICER_FULLNAME_MAX_LENGTH = 30;

        // Cell

        public const int CELL_RANGE_MIN = 1;
        public const int CELL_RANGE_MAX = 1000;

        // Department
        public const int DEPARTMENT_NAME_MIN_LENGTH = 3;
        public const int DEPARTMENT_NAME_MAX_LENGTH = 25;

        // Mail

        public const string MAIL_ADDRESS_REGEX = @"^[A-z0-9\s]+(str\.)$";

        // Output
        public const string ERROR_MESSAGE = "Invalid Data";
        public const string DEPARTMENT_SUCCESS_MESSAGE = "Imported {0} with {1} cells";

        public const string PRISONER_SUCCESS_MESSAGE = "Imported {0} {1} years old";

        public const string OFFICERS_SUCCESS_MESSAGE = "Imported {0} ({1} prisoners)"; // Imported {officer name} ({prisoners count} prisoners)
    }
}
