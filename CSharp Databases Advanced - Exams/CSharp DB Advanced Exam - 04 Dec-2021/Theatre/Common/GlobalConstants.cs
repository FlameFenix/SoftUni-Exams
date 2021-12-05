using System;
using System.Collections.Generic;
using System.Text;

namespace Theatre.Common
{
    public static class GlobalConstants
    {
        // Theatre

        public const int NAME_MIN_LENGTH = 4;
        public const int NAME_MAX_LENGTH = 30;

        public const sbyte NUMBEROFHALLS_MIN_RANGE = 1;
        public const sbyte NUMBEROFHALLS_MAX_RANGE = 10;

        public const int DIRECTOR_MIN_LENGTH = 4;
        public const int DIRECTOR_MAX_LENGTH = 30;

        // Play

        public const int TITLE_MIN_LENGTH = 4;
        public const int TITLE_MAX_LENGTH = 50;

        public const float RATING_MIN_RANGE = 0;
        public const float RATING_MAX_RANGE = 10;

        public const int DESCRIPTION_MAX_LENGTH = 700;

        public const int SCREENWRITER_MIN_LENGTH = 4;
        public const int SCREENWRITER_MAX_LENGTH = 30;

        // Cast

        public const int CAST_FULLNAME_MIN_LENGTH = 4;
        public const int CAST_FULLNAME_MAX_LENGTH = 30;

        public const int CAST_PHONENUMBER_MAX_LENGTH = 15;
        public const string CAST_PHONENUMBER_REGEX = @"^\+44-\d{2}-\d{3}-\d{4}$";

        // Ticket

        public const double TICKET_PRICE_MIN_RANGE = 1.00;
        public const double TICKET_PRICE_MAX_RANGE = 100.00;

        public const sbyte TICKET_ROWNUMBER_MIN_RANGE = 1;
        public const sbyte TICKET_ROWNUMBER_MAX_RANGE = 10;
    }
}
