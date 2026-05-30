using Howest.Cia.Services.Core.Models;

namespace Howest.Cia.Services.Core.Services
{
    public class HotelRoomBookingService
    {
        // In-memory storage for the exercise (no database).
        private readonly List<Booking> _bookings = new List<Booking>();
        private readonly List<int> _rooms = new List<int> { 101, 102, 103, 104, 105 };
        private int _nextId = 1;

        public HotelRoomBookingService() { }

        // Availability
        public bool IsRoomAvailable(int roomNumber, DateTime checkIn, DateTime checkOut)
        {
            if (!_rooms.Contains(roomNumber))
                return false;

            if (checkOut <= checkIn)
                return false;

            foreach (Booking booking in _bookings)
            {
                if (booking.IsCancelled)
                    continue;

                if (booking.RoomNumber != roomNumber)
                    continue;

                // Two date ranges overlap when each starts before the other ends.
                // The check-out day is free for the next guest.
                bool overlaps = checkIn < booking.CheckOut && booking.CheckIn < checkOut;
                if (overlaps)
                    return false;
            }

            return true;
        }

        public IEnumerable<int> GetAvailableRooms(DateTime checkIn, DateTime checkOut)
        {
            List<int> availableRooms = new List<int>();

            foreach (int roomNumber in _rooms)
            {
                if (IsRoomAvailable(roomNumber, checkIn, checkOut))
                    availableRooms.Add(roomNumber);
            }

            return availableRooms;
        }

        // Booking management
        public Booking CreateBooking(int roomNumber, string guestName, DateTime checkIn, DateTime checkOut)
        {
            if (string.IsNullOrWhiteSpace(guestName))
                throw new ArgumentException("Guest name is required.", nameof(guestName));

            if (!IsRoomAvailable(roomNumber, checkIn, checkOut))
                throw new InvalidOperationException("Room is not available for the requested dates.");

            Booking booking = new Booking
            {
                Id = _nextId,
                RoomNumber = roomNumber,
                GuestName = guestName,
                CheckIn = checkIn,
                CheckOut = checkOut,
                IsCancelled = false
            };

            _nextId = _nextId + 1;
            _bookings.Add(booking);

            return booking;
        }

        public bool CancelBooking(int bookingId)
        {
            Booking? booking = _bookings.FirstOrDefault(b => b.Id == bookingId);

            if (booking == null)
                return false;

            booking.IsCancelled = true;
            return true;
        }

        public Booking GetBooking(int bookingId)
        {
            Booking? booking = _bookings.FirstOrDefault(b => b.Id == bookingId);

            if (booking == null)
                throw new ArgumentException("No booking found with the given id.", nameof(bookingId));

            return booking;
        }

        public IEnumerable<Booking> GetBookingsForGuest(string guestName)
        {
            if (string.IsNullOrWhiteSpace(guestName))
                return new List<Booking>();

            return _bookings
                .Where(b => b.GuestName.ToUpper() == guestName.ToUpper())
                .ToList();
        }
    }
}
