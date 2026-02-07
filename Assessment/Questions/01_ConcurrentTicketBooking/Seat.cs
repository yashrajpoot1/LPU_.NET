namespace ConcurrentTicketBooking
{
    public class Seat
    {
        public int SeatNo { get; set; }
        public bool IsBooked { get; set; }
        public string? BookedBy { get; set; }
    }
}
