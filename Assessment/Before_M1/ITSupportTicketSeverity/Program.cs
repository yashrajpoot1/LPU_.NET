namespace ITSupportTicketSeverity;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║        IT Support Ticket Severity System             ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

        var ticketSystem = new TicketManager(maxQueueSize: 5);

        try
        {
            ticketSystem.AddTicket(new SupportTicket("T001", new Employee("E001", "Alice", 30), 1, "Server Down"));
            ticketSystem.AddTicket(new SupportTicket("T002", new Employee("E002", "Bob", 35), 3, "Printer Issue"));
            ticketSystem.AddTicket(new SupportTicket("T003", new Admin("A001", "Charlie", 40), 1, "Security Breach"));
            ticketSystem.AddTicket(new SupportTicket("T004", new Employee("E003", "Diana", 28), 2, "Email Not Working"));

            ticketSystem.DisplayTickets();

            // Process tickets
            Console.WriteLine("\n========== PROCESSING TICKETS ==========");
            ticketSystem.ProcessNextTicket();
            ticketSystem.ProcessNextTicket();

            // Test invalid priority
            Console.WriteLine("\n========== TESTING INVALID PRIORITY ==========");
            try
            {
                ticketSystem.AddTicket(new SupportTicket("T005", new Employee("E004", "Eve", 25), 6, "Test"));
            }
            catch (InvalidPriorityException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            ticketSystem.DisplayTickets();

            Console.WriteLine($"\n✅ Total Tickets: {ticketSystem.GetTotalTickets()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
