namespace AirlineBookingFare;

public class BookingManager
{
    private readonly SortedDictionary<decimal, List<Ticket>> _tickets = new();
    private readonly HashSet<string> _bookedSeats = new();

    public void AddTicket(Ticket ticket)
    {
        if (_bookedSeats.Contains(ticket.SeatNumber))
            throw new SeatAlreadyBookedException(ticket.SeatNumber);

        if (!_tickets.ContainsKey(ticket.TotalFare))
            _tickets[ticket.TotalFare] = new List<Ticket>();

        _tickets[ticket.TotalFare].Add(ticket);
        _bookedSeats.Add(ticket.SeatNumber);

        Console.WriteLine($"✓ Booked: {ticket.GetClass()} - Seat {ticket.SeatNumber} (${ticket.TotalFare:F2})");
    }

    public void DisplayTickets()
    {
        Console.WriteLine("\n========== TICKETS (Sorted by Fare) ==========");
        foreach (var kvp in _tickets)
        {
            Console.WriteLine($"\n--- Fare: ${kvp.Key:F2} ({kvp.Value.Count} tickets) ---");
            foreach (var ticket in kvp.Value)
            {
                ticket.DisplayInfo();
            }
        }
    }

    public int GetTotalTickets() => _bookedSeats.Count;
}
