using System.Collections.Concurrent;

namespace ConcurrentTicketBooking
{
    public class TicketBookingSystem
    {
        private readonly ConcurrentDictionary<int, Seat> _seats = new();
        private readonly ConcurrentDictionary<int, object> _seatLocks = new();

        public TicketBookingSystem(int totalSeats)
        {
            for (int i = 1; i <= totalSeats; i++)
            {
                _seats[i] = new Seat { SeatNo = i, IsBooked = false };
                _seatLocks[i] = new object();
            }
        }

        public bool BookSeat(int seatNo, string userId)
        {
            if (!_seats.ContainsKey(seatNo))
                return false;

            lock (_seatLocks[seatNo])
            {
                var seat = _seats[seatNo];
                if (seat.IsBooked)
                    return false;

                seat.IsBooked = true;
                seat.BookedBy = userId;
                return true;
            }
        }

        public Seat? GetSeat(int seatNo)
        {
            return _seats.TryGetValue(seatNo, out var seat) ? seat : null;
        }
    }
}
