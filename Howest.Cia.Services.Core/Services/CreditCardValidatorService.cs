using System.Text.RegularExpressions;
using Howest.Cia.Services.Core.Enums;

namespace Howest.Cia.Services.Core.Services
{
    public class CreditCardValidatorService
    {
        public CreditCardValidatorService() { }

        // Core validation
        public bool IsValid(string cardNumber)
        {
            if (!IsValidLength(cardNumber))
                return false;

            if (!IsValidCardType(cardNumber))
                return false;

            return IsValidLuhn(cardNumber);
        }

        public bool IsValidLuhn(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return false;

            string normalized = Normalize(cardNumber);
            if (!Regex.IsMatch(normalized, "^[0-9]+$"))
                return false;

            int sum = 0;
            bool doubleDigit = false;

            // Walk the digits right-to-left, doubling every second one.
            for (int index = normalized.Length - 1; index >= 0; index--)
            {
                int digit = normalized[index] - '0';

                if (doubleDigit)
                {
                    digit = digit * 2;
                    if (digit > 9)
                        digit = digit - 9;
                }

                sum = sum + digit;
                doubleDigit = !doubleDigit;
            }

            return sum % 10 == 0;
        }

        public bool IsValidLength(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return false;

            string normalized = Normalize(cardNumber);
            return normalized.Length >= 13 && normalized.Length <= 19;
        }

        // Card type / issuer
        public CreditCardType GetCardType(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return CreditCardType.Unknown;

            string normalized = Normalize(cardNumber);

            if (Regex.IsMatch(normalized, "^4"))
                return CreditCardType.Visa;

            if (Regex.IsMatch(normalized, "^5[1-5]"))
                return CreditCardType.Mastercard;

            if (Regex.IsMatch(normalized, "^3[47]"))
                return CreditCardType.AmericanExpress;

            if (Regex.IsMatch(normalized, "^6(?:011|5)"))
                return CreditCardType.Discover;

            return CreditCardType.Unknown;
        }

        public bool IsValidCardType(string cardNumber)
        {
            return GetCardType(cardNumber) != CreditCardType.Unknown;
        }

        // Expiry date
        public bool IsValidExpiryDate(int month, int year)
        {
            if (month < 1 || month > 12)
                return false;

            // Reject obviously wrong years; the actual expiry check is done by IsExpired.
            if (year < 2000 || year > 2100)
                return false;

            return !IsExpired(month, year);
        }

        public bool IsExpired(int month, int year)
        {
            // A card is valid through the last day of its expiry month.
            int lastDay = DateTime.DaysInMonth(year, month);
            DateTime lastDayOfExpiryMonth = new DateTime(year, month, lastDay);
            return DateTime.Now.Date > lastDayOfExpiryMonth;
        }

        // Security code (CVC / CVV)
        public bool IsValidCvc(string cvc, CreditCardType cardType)
        {
            if (string.IsNullOrWhiteSpace(cvc))
                return false;

            if (!Regex.IsMatch(cvc, "^[0-9]+$"))
                return false;

            // Amex uses a 4-digit code, everyone else uses 3.
            if (cardType == CreditCardType.AmericanExpress)
                return cvc.Length == 4;

            return cvc.Length == 3;
        }

        // Formatting / normalization
        public string Normalize(string cardNumber)
        {
            if (cardNumber == null)
                return string.Empty;

            return cardNumber.Replace(" ", string.Empty).Replace("-", string.Empty);
        }
    }
}
