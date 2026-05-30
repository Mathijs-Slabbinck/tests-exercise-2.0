namespace Howest.Cia.Services.Core.Services
{
    public class BoardingPassValidatorService
    {
        public BoardingPassValidatorService() { }

        // Core validation
        public bool IsValid(string boardingPassData)
        {
            throw new NotImplementedException();
        }

        public bool IsValidFormat(string boardingPassData)
        {
            throw new NotImplementedException();
        }

        // Extracting fields
        public string GetPassengerName(string boardingPassData)
        {
            throw new NotImplementedException();
        }

        public string GetFlightNumber(string boardingPassData)
        {
            throw new NotImplementedException();
        }

        public string GetDepartureAirport(string boardingPassData)
        {
            throw new NotImplementedException();
        }

        public string GetArrivalAirport(string boardingPassData)
        {
            throw new NotImplementedException();
        }

        public string GetSeatNumber(string boardingPassData)
        {
            throw new NotImplementedException();
        }

        public string GetSequenceNumber(string boardingPassData)
        {
            throw new NotImplementedException();
        }

        // Field validation
        public bool IsValidAirportCode(string airportCode)
        {
            throw new NotImplementedException();
        }

        public bool IsValidFlightNumber(string flightNumber)
        {
            throw new NotImplementedException();
        }
    }
}
