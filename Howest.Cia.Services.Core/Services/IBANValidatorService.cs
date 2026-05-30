namespace Howest.Cia.Services.Core.Services
{
    public class IBANValidatorService
    {
        public IBANValidatorService() { }

        // Core validation
        public bool IsValid(string iban)
        {
            throw new NotImplementedException();
        }

        public bool IsValidFormat(string iban)
        {
            throw new NotImplementedException();
        }

        public bool IsValidLength(string iban)
        {
            throw new NotImplementedException();
        }

        public bool IsValidCheckDigits(string iban)
        {
            throw new NotImplementedException();
        }

        // Country support
        public bool IsValidCountryCode(string countryCode)
        {
            throw new NotImplementedException();
        }

        public int GetExpectedLength(string countryCode)
        {
            throw new NotImplementedException();
        }

        // Extracting parts
        public string GetCountryCode(string iban)
        {
            throw new NotImplementedException();
        }

        public string GetCheckDigits(string iban)
        {
            throw new NotImplementedException();
        }

        public string GetBBban(string iban)
        {
            throw new NotImplementedException();
        }

        public bool IsValidBBan(string iban)
        {
            throw new NotImplementedException();
        }

        public string GetBankCode(string iban)
        {
            throw new NotImplementedException();
        }

        // Formatting / normalization
        public string Normalize(string iban)
        {
            throw new NotImplementedException();
        }
    }
}
