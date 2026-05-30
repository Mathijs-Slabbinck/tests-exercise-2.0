using System.Text.RegularExpressions;

namespace Howest.Cia.Services.Core.Services
{
    // Simplified subset of the IATA BCBP (Bar Coded Boarding Pass) "M" format.
    // Fixed-position layout used for this exercise:
    //   index 0      (1)   format code 'M'
    //   index 1      (1)   number of legs (digit)
    //   index 2-21   (20)  passenger name "LASTNAME/FIRSTNAME" (space padded)
    //   index 22     (1)   electronic ticket indicator 'E'
    //   index 23-29  (7)   booking reference (PNR)
    //   index 30-32  (3)   departure airport (IATA)
    //   index 33-35  (3)   arrival airport (IATA)
    //   index 36-38  (3)   operating carrier (space padded)
    //   index 39-43  (5)   flight number
    //   index 44-46  (3)   seat number
    //   index 47-50  (4)   check-in sequence number
    public class BoardingPassValidatorService
    {
        private const int MinimumLength = 51;

        public BoardingPassValidatorService() { }

        // Core validation
        public bool IsValid(string boardingPassData)
        {
            if (!IsValidFormat(boardingPassData))
                return false;

            if (!IsValidAirportCode(GetDepartureAirport(boardingPassData)))
                return false;

            if (!IsValidAirportCode(GetArrivalAirport(boardingPassData)))
                return false;

            return IsValidFlightNumber(GetFlightNumber(boardingPassData));
        }

        public bool IsValidFormat(string boardingPassData)
        {
            if (string.IsNullOrWhiteSpace(boardingPassData))
                return false;

            if (boardingPassData.Length < MinimumLength)
                return false;

            bool startsWithFormatCode = boardingPassData[0] == 'M';
            bool legCountIsDigit = char.IsDigit(boardingPassData[1]);

            return startsWithFormatCode && legCountIsDigit;
        }

        // Extracting fields
        public string GetPassengerName(string boardingPassData)
        {
            return ReadField(boardingPassData, 2, 20);
        }

        public string GetFlightNumber(string boardingPassData)
        {
            string carrier = ReadField(boardingPassData, 36, 3);
            string number = ReadField(boardingPassData, 39, 5);
            return carrier + number;
        }

        public string GetDepartureAirport(string boardingPassData)
        {
            return ReadField(boardingPassData, 30, 3);
        }

        public string GetArrivalAirport(string boardingPassData)
        {
            return ReadField(boardingPassData, 33, 3);
        }

        public string GetSeatNumber(string boardingPassData)
        {
            return ReadField(boardingPassData, 44, 3);
        }

        public string GetSequenceNumber(string boardingPassData)
        {
            return ReadField(boardingPassData, 47, 4);
        }

        // Field validation
        public bool IsValidAirportCode(string airportCode)
        {
            if (string.IsNullOrWhiteSpace(airportCode))
                return false;

            // IATA airport codes are exactly three uppercase letters.
            return Regex.IsMatch(airportCode, "^[A-Z]{3}$");
        }

        public bool IsValidFlightNumber(string flightNumber)
        {
            if (string.IsNullOrWhiteSpace(flightNumber))
                return false;

            // 2-3 char carrier code followed by 1-4 digits, e.g. "LH0815".
            return Regex.IsMatch(flightNumber, "^[A-Z0-9]{2,3}[0-9]{1,4}$");
        }

        // Reads a fixed-position field and trims the padding; returns empty when out of range.
        private string ReadField(string boardingPassData, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(boardingPassData))
                return string.Empty;

            if (startIndex + length > boardingPassData.Length)
                return string.Empty;

            return boardingPassData.Substring(startIndex, length).Trim();
        }
    }
}
