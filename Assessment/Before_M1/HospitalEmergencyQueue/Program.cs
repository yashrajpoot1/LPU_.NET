namespace HospitalEmergencyQueue;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║     Hospital Emergency Queue Management System       ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

        var emergency = new EmergencyManager(maxQueueSize: 5);

        try
        {
            // Add patients with different severity levels
            emergency.AddPatient(new Patient("P001", "John Doe", 30, 1, "Heart Attack"));
            emergency.AddPatient(new Patient("P002", "Jane Smith", 45, 3, "Broken Arm"));
            emergency.AddPatient(new Patient("P003", "Bob Wilson", 60, 1, "Stroke"));
            emergency.AddPatient(new Patient("P004", "Alice Brown", 25, 2, "Severe Bleeding"));
            emergency.AddPatient(new Patient("P005", "Charlie Davis", 35, 4, "Minor Cut"));
            emergency.AddPatient(new Patient("P006", "Diana Evans", 50, 2, "Chest Pain"));

            emergency.DisplayQueue();

            // Treat next patients
            Console.WriteLine("\n========== TREATING PATIENTS ==========");
            var patient1 = emergency.GetNextPatient();
            patient1?.Treat();

            var patient2 = emergency.GetNextPatient();
            patient2?.Treat();

            emergency.DisplayQueue();

            // Test invalid severity
            Console.WriteLine("\n========== TESTING INVALID SEVERITY ==========");
            try
            {
                emergency.AddPatient(new Patient("P007", "Invalid", 40, 6, "Test"));
            }
            catch (InvalidSeverityException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            // Test queue overflow
            Console.WriteLine("\n========== TESTING QUEUE OVERFLOW ==========");
            try
            {
                for (int i = 7; i <= 12; i++)
                {
                    emergency.AddPatient(new Patient($"P{i:D3}", $"Patient {i}", 20 + i, 5, "Test"));
                }
            }
            catch (QueueOverflowException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            // Remove patient
            Console.WriteLine("\n========== REMOVING PATIENT ==========");
            emergency.RemovePatient("P004");

            emergency.DisplayQueue();

            Console.WriteLine($"\n✅ Total Patients Waiting: {emergency.GetTotalWaiting()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
