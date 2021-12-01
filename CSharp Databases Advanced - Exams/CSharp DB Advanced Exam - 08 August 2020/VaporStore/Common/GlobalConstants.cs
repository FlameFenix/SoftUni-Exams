using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.Common
{
    public static class GlobalConstants
    {
        // User
        public const int USERNAME_MINLENGTH = 3;
        public const int USERNAME_MAXLENGTH = 20;

        public const string FULLNAME_REGEX = @"^[A-Z]{1}[a-z]+\s[A-Z]{1}[a-z]+$";

        public const int USER_MAX_AGE_RANGE = 103;

        // Purchase

        public const int PRODUCT_KEY_MAX_LENGTH = 14;
        public const string PRODUCT_KEY_REGEX = @"^[A-Z0-9]{4}\-[A-Z0-9]{4}\-[A-Z0-9]{4}$";

        // Card

        public const int CARD_NUMBER_MAX_LENGTH = 19;
        public const int CARD_CVC_MAX_LENGTH = 3;

        public const string CARD_NUMBER_REGEX = @"^[0-9]{4}\s[0-9]{4}\s[0-9]{4}\s[0-9]{4}$";
        public const string CARD_CVC_REGER = @"^[0-9]{3}$";

        // Game

        public const int GAME_MIN_PRICE_VALUE = 0;

        // Output 

        public const string ErrorMessage = "Invalid Data";
        public const string SuccessfullyAddedGames = "Added {0} ({1}) with {2} tags";
        public const string SuccessfullyAddedPurchases = "Imported {0} for {1}";
        public const string SuccessfullyAddedUsersWithCards = "Imported {0} with {1} cards";
    }
}
