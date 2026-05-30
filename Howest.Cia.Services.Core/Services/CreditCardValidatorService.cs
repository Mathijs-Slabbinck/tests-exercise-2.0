using Howest.Cia.Services.Core.Enums;

namespace Howest.Cia.Services.Core.Services
{
    public class CreditCardValidatorService
    {
        public CreditCardValidatorService() { }

        // Core validation
        public bool IsValid(string cardNumber)
        {
            throw new NotImplementedException();
        }

        public bool IsValidLuhn(string cardNumber)
        {
            throw new NotImplementedException();
        }

        public bool IsValidLength(string cardNumber)
        {
            throw new NotImplementedException();
        }

        // Card type / issuer
        public CreditCardType GetCardType(string cardNumber)
        {
            throw new NotImplementedException();
        }

        public bool IsValidCardType(string cardNumber)
        {
            throw new NotImplementedException();
        }

        // Expiry date
        public bool IsValidExpiryDate(int month, int year)
        {
            throw new NotImplementedException();
        }

        public bool IsExpired(int month, int year)
        {
            throw new NotImplementedException();
        }

        // Security code (CVC / CVV)
        public bool IsValidCvc(string cvc, CreditCardType cardType)
        {
            throw new NotImplementedException();
        }

        // Formatting / normalization
        public string Normalize(string cardNumber)
        {
            throw new NotImplementedException();
        }
    }
}
