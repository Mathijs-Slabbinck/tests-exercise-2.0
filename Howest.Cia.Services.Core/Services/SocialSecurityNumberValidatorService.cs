using Howest.Cia.Services.Core.Enums;

namespace Howest.Cia.Services.Core.Services
{
    public class SocialSecurityNumberValidatorService
    {
        public SocialSecurityNumberValidatorService() { }

        // Core validation
        public bool IsValid(string ssn, string countryCode)
        {
            throw new NotImplementedException();
        }

        public bool IsValidFormat(string ssn, string countryCode)
        {
            throw new NotImplementedException();
        }

        public bool IsValidLength(string ssn, string countryCode)
        {
            throw new NotImplementedException();
        }

        public bool IsValidCheckDigits(string ssn, string countryCode)
        {
            throw new NotImplementedException();
        }

        // Country support
        public bool IsSupportedCountry(string countryCode)
        {
            throw new NotImplementedException();
        }

        // Extracting info
        public DateTime GetDateOfBirth(string ssn, string countryCode)
        {
            throw new NotImplementedException();
        }

        public Gender GetGender(string ssn, string countryCode)
        {
            throw new NotImplementedException();
        }

        // Formatting / normalization
        public string Normalize(string ssn)
        {
            throw new NotImplementedException();
        }
    }
}
