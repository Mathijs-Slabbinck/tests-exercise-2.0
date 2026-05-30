using Howest.Cia.Services.Core.Enums;

namespace Howest.Cia.Services.Core.Services
{
    public class ShippingCostCalculatorService
    {
        private const double MaxWeightInKg = 1000.0;

        // Country zones for the exercise. "BE" is domestic, the rest of the EU set is zone 2, everything else is world.
        private readonly List<string> _domesticCountries = new List<string> { "BE" };
        private readonly List<string> _euCountries = new List<string> { "NL", "FR", "DE", "LU" };

        public ShippingCostCalculatorService() { }

        // Cost calculation
        public decimal CalculateCost(double weightInKg, string destinationCountryCode)
        {
            return CalculateCost(weightInKg, destinationCountryCode, ShippingMethod.Standard);
        }

        public decimal CalculateCost(double weightInKg, string destinationCountryCode, ShippingMethod method)
        {
            if (!IsValidWeight(weightInKg))
                throw new ArgumentOutOfRangeException(nameof(weightInKg), "Weight must be between 0 and 1000 kg.");

            if (!IsValidDestination(destinationCountryCode))
                throw new ArgumentException("Unsupported destination.", nameof(destinationCountryCode));

            decimal baseRate = GetBaseRate(destinationCountryCode);
            decimal ratePerKg = GetRatePerKg(destinationCountryCode);
            decimal methodMultiplier = GetMethodMultiplier(method);

            decimal cost = baseRate + (ratePerKg * (decimal)weightInKg);
            return cost * methodMultiplier;
        }

        // Destination support
        public bool IsValidDestination(string destinationCountryCode)
        {
            if (string.IsNullOrWhiteSpace(destinationCountryCode))
                return false;

            // Anything that isn't blank is shippable; the zone decides the price.
            return destinationCountryCode.Length == 2;
        }

        // Input validation
        public bool IsValidWeight(double weightInKg)
        {
            return weightInKg > 0 && weightInKg <= MaxWeightInKg;
        }

        // Pricing helpers
        private decimal GetBaseRate(string destinationCountryCode)
        {
            string country = destinationCountryCode.ToUpper();

            if (_domesticCountries.Contains(country))
                return 4.00m;

            if (_euCountries.Contains(country))
                return 8.00m;

            return 15.00m;
        }

        private decimal GetRatePerKg(string destinationCountryCode)
        {
            string country = destinationCountryCode.ToUpper();

            if (_domesticCountries.Contains(country))
                return 1.00m;

            if (_euCountries.Contains(country))
                return 2.00m;

            return 4.00m;
        }

        private decimal GetMethodMultiplier(ShippingMethod method)
        {
            if (method == ShippingMethod.Express)
                return 1.5m;

            if (method == ShippingMethod.NextDay)
                return 2.0m;

            return 1.0m;
        }
    }
}
