using Howest.Cia.Services.Core.Enums;

namespace Howest.Cia.Services.Core.Services
{
    public class ShippingCostCalculatorService
    {
        public ShippingCostCalculatorService() { }

        // Cost calculation
        public decimal CalculateCost(double weightInKg, string destinationCountryCode)
        {
            throw new NotImplementedException();
        }

        public decimal CalculateCost(double weightInKg, string destinationCountryCode, ShippingMethod method)
        {
            throw new NotImplementedException();
        }

        // Destination support
        public bool IsValidDestination(string destinationCountryCode)
        {
            throw new NotImplementedException();
        }

        // Input validation
        public bool IsValidWeight(double weightInKg)
        {
            throw new NotImplementedException();
        }
    }
}
