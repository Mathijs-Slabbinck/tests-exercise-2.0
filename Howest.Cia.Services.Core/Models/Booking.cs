namespace Howest.Cia.Services.Core.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public string GuestName { get; set; } = string.Empty;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool IsCancelled { get; set; }
    }
}
