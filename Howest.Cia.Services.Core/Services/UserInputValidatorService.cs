using System.Text.RegularExpressions;

namespace Howest.Cia.Services.Core.Services
{
    public class UserInputValidatorService
    {
        // Localised patterns per country code (subset for the exercise).
        private readonly Dictionary<string, string> _phonePatterns = new Dictionary<string, string>
        {
            { "BE", "^(\\+32|0)[0-9]{8,9}$" },
            { "NL", "^(\\+31|0)[0-9]{9}$" },
            { "FR", "^(\\+33|0)[0-9]{9}$" }
        };

        private readonly Dictionary<string, string> _postalPatterns = new Dictionary<string, string>
        {
            { "BE", "^[0-9]{4}$" },
            { "NL", "^[0-9]{4}\\s?[A-Z]{2}$" },
            { "FR", "^[0-9]{5}$" }
        };

        public UserInputValidatorService() { }

        // Email
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Something, an @, something, a dot, something — no spaces.
            return Regex.IsMatch(email, "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$");
        }

        // Phone number (localised per country)
        public bool IsValidPhoneNumber(string phoneNumber, string countryCode)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(countryCode))
                return false;

            string country = countryCode.ToUpper();
            if (!_phonePatterns.ContainsKey(country))
                return false;

            // Ignore spaces in the entered number before matching.
            string normalized = phoneNumber.Replace(" ", string.Empty);
            return Regex.IsMatch(normalized, _phonePatterns[country]);
        }

        // Postal code (localised per country)
        public bool IsValidPostalCode(string postalCode, string countryCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode) || string.IsNullOrWhiteSpace(countryCode))
                return false;

            string country = countryCode.ToUpper();
            if (!_postalPatterns.ContainsKey(country))
                return false;

            return Regex.IsMatch(postalCode.ToUpper(), _postalPatterns[country]);
        }
    }
}
