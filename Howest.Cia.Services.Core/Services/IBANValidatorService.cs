using System.Numerics;
using System.Text.RegularExpressions;

namespace Howest.Cia.Services.Core.Services
{
    public class IBANValidatorService
    {
        // Expected total IBAN length per country code (subset for the exercise).
        private readonly Dictionary<string, int> _lengthByCountry = new Dictionary<string, int>
        {
            { "BE", 16 },
            { "NL", 18 },
            { "FR", 27 },
            { "DE", 22 },
            { "ES", 24 },
            { "IT", 27 }
        };

        public IBANValidatorService() { }

        // Core validation
        public bool IsValid(string iban)
        {
            if (!IsValidFormat(iban))
                return false;

            if (!IsValidLength(iban))
                return false;

            return IsValidCheckDigits(iban);
        }

        public bool IsValidFormat(string iban)
        {
            if (string.IsNullOrWhiteSpace(iban))
                return false;

            string normalized = Normalize(iban);

            // Two letters, two check digits, then alphanumeric BBAN.
            return Regex.IsMatch(normalized, "^[A-Z]{2}[0-9]{2}[A-Z0-9]+$");
        }

        public bool IsValidLength(string iban)
        {
            if (string.IsNullOrWhiteSpace(iban))
                return false;

            string normalized = Normalize(iban);
            if (normalized.Length < 4)
                return false;

            string countryCode = normalized.Substring(0, 2);
            if (!IsValidCountryCode(countryCode))
                return false;

            int expectedLength = GetExpectedLength(countryCode);
            return normalized.Length == expectedLength;
        }

        public bool IsValidCheckDigits(string iban)
        {
            if (string.IsNullOrWhiteSpace(iban))
                return false;

            string normalized = Normalize(iban);

            // Mod-97 (ISO 7064): move first 4 chars to the end, convert letters to numbers, check remainder == 1.
            string rearranged = normalized.Substring(4) + normalized.Substring(0, 4);

            string digits = string.Empty;
            foreach (char character in rearranged)
            {
                if (char.IsLetter(character))
                    digits += (character - 'A' + 10).ToString();
                else
                    digits += character;
            }

            BigInteger number = BigInteger.Parse(digits);
            return number % 97 == 1;
        }

        // Country support
        public bool IsValidCountryCode(string countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                return false;

            return _lengthByCountry.ContainsKey(countryCode.ToUpper());
        }

        public int GetExpectedLength(string countryCode)
        {
            if (!IsValidCountryCode(countryCode))
                throw new ArgumentException("Unsupported country code.", nameof(countryCode));

            return _lengthByCountry[countryCode.ToUpper()];
        }

        // Extracting parts
        public string GetCountryCode(string iban)
        {
            if (string.IsNullOrWhiteSpace(iban))
                throw new ArgumentException("IBAN can't be null or empty.", nameof(iban));

            return Normalize(iban).Substring(0, 2);
        }

        public string GetCheckDigits(string iban)
        {
            if (string.IsNullOrWhiteSpace(iban))
                throw new ArgumentException("IBAN can't be null or empty.", nameof(iban));

            return Normalize(iban).Substring(2, 2);
        }

        public string GetBBban(string iban)
        {
            if (string.IsNullOrWhiteSpace(iban))
                throw new ArgumentException("IBAN can't be null or empty.", nameof(iban));

            // Everything after the country code and check digits.
            return Normalize(iban).Substring(4);
        }

        public bool IsValidBBan(string iban)
        {
            string bban = GetBBban(iban);
            return Regex.IsMatch(bban, "^[A-Z0-9]+$");
        }

        public string GetBankCode(string iban)
        {
            // Rough: first 3 characters of the BBAN (true for BE; differs per country).
            return GetBBban(iban).Substring(0, 3);
        }

        // Formatting / normalization
        public string Normalize(string iban)
        {
            if (iban == null)
                return string.Empty;

            return iban.Replace(" ", string.Empty).ToUpper();
        }
    }
}
