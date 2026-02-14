namespace LibraryFineCalculation;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║   Library Fine Calculation & Penalty System          ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

        var library = new LibraryManager();

        try
        {
            library.AddMember(new StudentMember("M001", "Alice", 20, 15));
            library.AddMember(new FacultyMember("M002", "Dr. Smith", 45, 5));
            library.AddMember(new StudentMember("M003", "Bob", 22, 30));

            library.DisplayMembers();

            // Pay fine
            Console.WriteLine("\n========== PAYING FINES ==========");
            library.PayFine("M001", 10m);

            // Test invalid payment
            Console.WriteLine("\n========== TESTING INVALID PAYMENT ==========");
            try
            {
                library.PayFine("M002", -5m);
            }
            catch (InvalidPaymentException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            // Test fine not found
            Console.WriteLine("\n========== TESTING FINE NOT FOUND ==========");
            try
            {
                library.PayFine("M999", 10m);
            }
            catch (FineNotFoundException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            library.DisplayMembers();

            Console.WriteLine($"\n✅ Total Members: {library.GetTotalMembers()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
