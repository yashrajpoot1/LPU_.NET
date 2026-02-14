namespace AirlineBookingFare;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║    Airline Booking Fare Classification System        ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

        var bookingSystem = new BookingManager();

        try
        {
            bookingSystem.AddTicket(new Economy("TKT001", "NYC", "LAX", 250m, "12A", 1));
            bookingSystem.AddTicket(new Business("TKT002", "NYC", "LAX", 1200m, "3B", true, 2));
            bookingSystem.AddTicket(new FirstClass("TKT003", "NYC", "LAX", 3500m, "1A", true, true));
            bookingSystem.AddTicket(new Economy("TKT004", "NYC", "LAX", 280m, "15C", 2));
            bookingSystem.AddTicket(new Business("TKT005", "NYC", "LAX", 1150m, "4A", false, 1));

            bookingSystem.DisplayTickets();

            // Test duplicate seat
            Console.WriteLine("\n========== TESTING DUPLICATE SEAT ==========");
            try
            {
                bookingSystem.AddTicket(new Economy("TKT006", "NYC", "LAX", 260m, "12A", 1));
            }
            catch (SeatAlreadyBookedException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            // Test invalid fare
            Console.WriteLine("\n========== TESTING INVALID FARE ==========");
            try
            {
                bookingSystem.AddTicket(new Economy("TKT007", "NYC", "LAX", -100m, "20A", 1));
            }
            catch (InvalidFareException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            Console.WriteLine($"\n✅ Total Tickets Booked: {bookingSystem.GetTotalTickets()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
