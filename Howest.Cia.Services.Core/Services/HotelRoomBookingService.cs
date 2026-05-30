using Howest.Cia.Services.Core.Models;

namespace Howest.Cia.Services.Core.Services
{
    public class HotelRoomBookingService
    {
        public HotelRoomBookingService() { }

        // Availability
        public bool IsRoomAvailable(int roomNumber, DateTime checkIn, DateTime checkOut)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> GetAvailableRooms(DateTime checkIn, DateTime checkOut)
        {
            throw new NotImplementedException();
        }

        // Booking management
        public Booking CreateBooking(int roomNumber, string guestName, DateTime checkIn, DateTime checkOut)
        {
            throw new NotImplementedException();
        }

        public bool CancelBooking(int bookingId)
        {
            throw new NotImplementedException();
        }

        public Booking GetBooking(int bookingId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> GetBookingsForGuest(string guestName)
        {
            throw new NotImplementedException();
        }
    }
}
