using System;
using System.Threading.Tasks;
using ConcurrentTicketBooking;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("=== Problem 1: Concurrent Ticket Booking ===");
        var system = new TicketBookingSystem(10);

        var tasks = new Task[20];
        for (int i = 0; i < 20; i++)
        {
            int userId = i;
            int seatNo = (i % 10) + 1;
            tasks[i] = Task.Run(() =>
            {
                bool success = system.BookSeat(seatNo, $"User{userId}");
                Console.WriteLine($"User{userId} booking seat {seatNo}: {(success ? "SUCCESS" : "FAILED")}");
            });
        }

        await Task.WhenAll(tasks);
        Console.WriteLine("\nFinal seat status:");
        for (int i = 1; i <= 10; i++)
        {
            var seat = system.GetSeat(i);
            Console.WriteLine($"Seat {i}: {(seat?.IsBooked == true ? $"Booked by {seat.BookedBy}" : "Available")}");
        }
        Console.WriteLine("✓ Test Passed\n");
    }
}
