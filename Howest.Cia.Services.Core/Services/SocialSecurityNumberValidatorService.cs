using System.Text.RegularExpressions;
using Howest.Cia.Services.Core.Enums;

namespace Howest.Cia.Services.Core.Services
{
    // Implemented for the Belgian "rijksregisternummer" (country code "BE"): 11 digits,
    // layout YYMMDD + 3-digit serial + 2 check digits. Other countries are not supported here.
    public class SocialSecurityNumberValidatorService
    {
        private const string BelgiumCountryCode = "BE";
        private const int BelgianLength = 11;

        public SocialSecurityNumberValidatorService() { }

        // Core validation
        public bool IsValid(string ssn, string countryCode)
        {
            if (!IsSupportedCountry(countryCode))
                return false;

            if (!IsValidFormat(ssn, countryCode))
                return false;

            return IsValidCheckDigits(ssn, countryCode);
        }

        public bool IsValidFormat(string ssn, string countryCode)
        {
            if (!IsValidLength(ssn, countryCode))
                return false;

            // After normalization it must be digits only, and the date part must be a real date.
            string normalized = Normalize(ssn);
            if (!Regex.IsMatch(normalized, "^[0-9]+$"))
                return false;

            return HasValidBirthDate(normalized);
        }

        public bool IsValidLength(string ssn, string countryCode)
        {
            if (!IsSupportedCountry(countryCode))
                return false;

            return Normalize(ssn).Length == BelgianLength;
        }

        public bool IsValidCheckDigits(string ssn, string countryCode)
        {
            if (!IsValidLength(ssn, countryCode))
                return false;

            string normalized = Normalize(ssn);
            int providedCheck = int.Parse(normalized.Substring(9, 2));

            // Births before 2000 use the first 9 digits; from 2000 on, a leading "2" is prepended.
            int checkFor1900 = CalculateCheck(normalized.Substring(0, 9));
            int checkFor2000 = CalculateCheck("2" + normalized.Substring(0, 9));

            return providedCheck == checkFor1900 || providedCheck == checkFor2000;
        }

        // Country support
        public bool IsSupportedCountry(string countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                return false;

            return countryCode.ToUpper() == BelgiumCountryCode;
        }

        // Extracting info
        public DateTime GetDateOfBirth(string ssn, string countryCode)
        {
            if (!IsValid(ssn, countryCode))
                throw new ArgumentException("Invalid social security number.", nameof(ssn));

            string normalized = Normalize(ssn);
            int year = int.Parse(normalized.Substring(0, 2));
            int month = int.Parse(normalized.Substring(2, 2));
            int day = int.Parse(normalized.Substring(4, 2));

            int century = GetCentury(normalized);
            return new DateTime(century + year, month, day);
        }

        public Gender GetGender(string ssn, string countryCode)
        {
            if (!IsValid(ssn, countryCode))
                throw new ArgumentException("Invalid social security number.", nameof(ssn));

            string normalized = Normalize(ssn);
            int serial = int.Parse(normalized.Substring(6, 3));

            // Odd serial = male, even serial = female.
            if (serial % 2 == 0)
                return Gender.Female;

            return Gender.Male;
        }

        // Formatting / normalization
        public string Normalize(string ssn)
        {
            if (ssn == null)
                return string.Empty;

            // Strip dots, dashes, spaces — keep digits only.
            return Regex.Replace(ssn, "[^0-9]", string.Empty);
        }

        // Helpers
        private int CalculateCheck(string nineOrTenDigits)
        {
            long number = long.Parse(nineOrTenDigits);
            return 97 - (int)(number % 97);
        }

        private int GetCentury(string normalized)
        {
            int providedCheck = int.Parse(normalized.Substring(9, 2));
            int checkFor2000 = CalculateCheck("2" + normalized.Substring(0, 9));

            if (providedCheck == checkFor2000)
                return 2000;

            return 1900;
        }

        private bool HasValidBirthDate(string normalized)
        {
            int year = int.Parse(normalized.Substring(0, 2));
            int month = int.Parse(normalized.Substring(2, 2));
            int day = int.Parse(normalized.Substring(4, 2));

            if (month < 1 || month > 12)
                return false;

            int century = GetCentury(normalized);
            int daysInMonth = DateTime.DaysInMonth(century + year, month);

            return day >= 1 && day <= daysInMonth;
        }
    }
}
