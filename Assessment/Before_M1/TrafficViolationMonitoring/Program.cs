namespace TrafficViolationMonitoring;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║   Smart Traffic Violation Monitoring System          ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

        var trafficSystem = new TrafficMonitor(maxPenalty: 5000m);

        try
        {
            trafficSystem.AddViolation(new Violation("V001", new Car("ABC123", "Toyota", "John"), 150m, "Speeding"));
            trafficSystem.AddViolation(new Violation("V002", new Truck("XYZ789", "Volvo", "Bob"), 300m, "Red Light"));
            trafficSystem.AddViolation(new Violation("V003", new Bike("BIKE01", "Honda", "Alice"), 75m, "No Helmet"));
            trafficSystem.AddViolation(new Violation("V004", new Car("DEF456", "Honda", "Charlie"), 200m, "Illegal Parking"));

            trafficSystem.DisplayViolations();

            // Test invalid vehicle
            Console.WriteLine("\n========== TESTING INVALID VEHICLE ==========");
            try
            {
                trafficSystem.AddViolation(new Violation("V005", new Car("", "Tesla", "Diana"), 100m, "Speeding"));
            }
            catch (InvalidVehicleException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            // Test penalty exceeds limit
            Console.WriteLine("\n========== TESTING PENALTY EXCEEDS LIMIT ==========");
            try
            {
                trafficSystem.AddViolation(new Violation("V006", new Truck("TRUCK99", "Mack", "Eve"), 6000m, "Overload"));
            }
            catch (PenaltyExceedsLimitException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            Console.WriteLine($"\n✅ Total Violations: {trafficSystem.GetTotalViolations()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
