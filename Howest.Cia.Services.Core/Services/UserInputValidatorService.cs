namespace Howest.Cia.Services.Core.Services
{
    public class UserInputValidatorService
    {
        public UserInputValidatorService() { }

        // Email
        public bool IsValidEmail(string email)
        {
            throw new NotImplementedException();
        }

        // Phone number (localised per country)
        public bool IsValidPhoneNumber(string phoneNumber, string countryCode)
        {
            throw new NotImplementedException();
        }

        // Postal code (localised per country)
        public bool IsValidPostalCode(string postalCode, string countryCode)
        {
            throw new NotImplementedException();
        }
    }
}
