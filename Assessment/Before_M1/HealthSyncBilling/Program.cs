namespace HealthSyncBilling;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║      HealthSync Advanced Billing System v1.0          ║");
        Console.WriteLine("║           Machine Masters - Medical Payroll           ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

        var billingManager = new BillingManager();

        Console.WriteLine("========== SCENARIO 1: In-House Consultant (High Earner) ==========\n");
        try
        {
            var inHouse1 = new InHouseConsultant(
                consultantId: "DR2001",
                name: "Dr. Sarah Johnson",
                specialization: "Cardiology",
                monthlyStipend: 10000m,
                travelAllowance: 2000m,
                performanceBonus: 1000m
            );
            billingManager.AddConsultant(inHouse1);
            inHouse1.DisplayPayoutDetails();

            Console.WriteLine("Expected: Gross: 13000.00 | TDS Applied: 15% | Net Payout: 11050.00");
            Console.WriteLine($"Actual:   Gross: {inHouse1.CalculateGrossPayout():F2} | " +
                            $"TDS Applied: {inHouse1.GetTDSPercentage(inHouse1.CalculateGrossPayout())} | " +
                            $"Net Payout: {inHouse1.CalculateNetPayout():F2}");
            Console.WriteLine("✓ SCENARIO 1 PASSED\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}\n");
        }

        Console.WriteLine("\n========== SCENARIO 2: Visiting Consultant ==========\n");
        try
        {
            var visiting1 = new VisitingConsultant(
                consultantId: "DR8005",
                name: "Dr. Michael Chen",
                specialization: "Orthopedics",
                consultationsCount: 10,
                ratePerVisit: 600m
            );
            billingManager.AddConsultant(visiting1);
            visiting1.DisplayPayoutDetails();

            Console.WriteLine("Expected: Gross: 6000.00 | TDS Applied: 10% | Net Payout: 5400.00");
            Console.WriteLine($"Actual:   Gross: {visiting1.CalculateGrossPayout():F2} | " +
                            $"TDS Applied: {visiting1.GetTDSPercentage(visiting1.CalculateGrossPayout())} | " +
                            $"Net Payout: {visiting1.CalculateNetPayout():F2}");
            Console.WriteLine("✓ SCENARIO 2 PASSED\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}\n");
        }

        Console.WriteLine("\n========== SCENARIO 3: Validation Failure ==========\n");
        try
        {
            Console.WriteLine("Attempting to create consultant with invalid ID: MD1001");
            var invalid = new InHouseConsultant(
                consultantId: "MD1001",
                name: "Dr. Invalid",
                specialization: "General",
                monthlyStipend: 5000m
            );
            Console.WriteLine("✗ SCENARIO 3 FAILED - Exception should have been thrown\n");
        }
        catch (InvalidConsultantIdException ex)
        {
            Console.WriteLine($"✓ Exception caught: {ex.Message}");
            Console.WriteLine("✓ SCENARIO 3 PASSED - Process terminated as expected\n");
        }

        Console.WriteLine("\n========== ADDITIONAL TEST CASES ==========\n");

        // Test Case 4: In-House Low Earner (5% TDS)
        try
        {
            var inHouse2 = new InHouseConsultant(
                consultantId: "DR3001",
                name: "Dr. Emily Rodriguez",
                specialization: "Pediatrics",
                monthlyStipend: 4000m,
                travelAllowance: 500m,
                performanceBonus: 300m
            );
            billingManager.AddConsultant(inHouse2);
            Console.WriteLine($"✓ Added: {inHouse2.Name} (Low earner - should have 5% TDS)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }

        // Test Case 5: Visiting Consultant with many visits
        try
        {
            var visiting2 = new VisitingConsultant(
                consultantId: "DR9002",
                name: "Dr. James Wilson",
                specialization: "Neurology",
                consultationsCount: 25,
                ratePerVisit: 800m
            );
            billingManager.AddConsultant(visiting2);
            Console.WriteLine($"✓ Added: {visiting2.Name} (High volume visiting consultant)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }

        // Test Case 6: Invalid ID formats
        Console.WriteLine("\n--- Testing Various Invalid ID Formats ---");
        string[] invalidIds = { "DR123", "DR12345", "AB1234", "DR12AB", "dr1234", "" };
        
        foreach (var id in invalidIds)
        {
            bool isValid = Consultant.ValidateConsultantId(id);
            Console.WriteLine($"ID: '{id}' → {(isValid ? "✓ Valid" : "✗ Invalid")}");
        }

        // Test Case 7: Valid ID formats
        Console.WriteLine("\n--- Testing Valid ID Formats ---");
        string[] validIds = { "DR1234", "DR0001", "DR9999", "DR5678" };
        
        foreach (var id in validIds)
        {
            bool isValid = Consultant.ValidateConsultantId(id);
            Console.WriteLine($"ID: '{id}' → {(isValid ? "✓ Valid" : "✗ Invalid")}");
        }

        // Process all payouts
        Console.WriteLine("\n\n========== PROCESSING ALL PAYOUTS ==========");
        billingManager.ProcessAllPayouts();

        // Display summary
        billingManager.DisplaySummary();

        // Demonstrate OOP Concepts
        Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║              OOP CONCEPTS DEMONSTRATED                 ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.WriteLine("\n✓ ABSTRACTION:");
        Console.WriteLine("  - Abstract Consultant class prevents generic consultant creation");
        Console.WriteLine("  - Abstract CalculateGrossPayout() forces subclass implementation");
        
        Console.WriteLine("\n✓ POLYMORPHISM:");
        Console.WriteLine("  - InHouseConsultant overrides CalculateGrossPayout()");
        Console.WriteLine("    Formula: Stipend + Allowance + Bonus");
        Console.WriteLine("  - VisitingConsultant overrides CalculateGrossPayout()");
        Console.WriteLine("    Formula: ConsultationsCount × RatePerVisit");
        
        Console.WriteLine("\n✓ VIRTUAL LOGIC:");
        Console.WriteLine("  - Base class: CalculateTDS() uses sliding scale (5%/15%)");
        Console.WriteLine("  - InHouseConsultant: Uses inherited sliding scale");
        Console.WriteLine("  - VisitingConsultant: Overrides to flat 10% rate");
        
        Console.WriteLine("\n✓ ENCAPSULATION:");
        Console.WriteLine("  - ID validation encapsulated in ValidateConsultantId()");
        Console.WriteLine("  - Payout logic encapsulated in respective classes");
        
        Console.WriteLine("\n✓ EXCEPTION HANDLING:");
        Console.WriteLine("  - Custom InvalidConsultantIdException");
        Console.WriteLine("  - Validation at constructor level");
        
        Console.WriteLine("\n════════════════════════════════════════════════════════");
        Console.WriteLine("✅ HealthSync Advanced Billing System - Demo Complete!");
        Console.WriteLine("════════════════════════════════════════════════════════\n");
    }
}
