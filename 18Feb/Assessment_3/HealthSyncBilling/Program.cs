using System;
using HealthSyncBilling.Models;
using HealthSyncBilling.Services;

namespace HealthSyncBilling
{
    /// <summary>
    /// Main program for HealthSync Advanced Billing System
    /// </summary>
    class Program
    {
        private static BillingService _billingService = new BillingService();

        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║  HEALTHSYNC ADVANCED BILLING SYSTEM    ║");
            Console.WriteLine("║        Machine Masters Edition         ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            // Load sample scenarios
            LoadSampleScenarios();

            while (true)
            {
                try
                {
                    DisplayMenu();
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddInHouseConsultant();
                            break;
                        case "2":
                            AddVisitingConsultant();
                            break;
                        case "3":
                            ViewConsultantDetails();
                            break;
                        case "4":
                            ViewAllConsultants();
                            break;
                        case "5":
                            _billingService.GenerateSummaryReport();
                            break;
                        case "6":
                            _billingService.GenerateDetailedReport();
                            break;
                        case "7":
                            RunSampleScenarios();
                            break;
                        case "8":
                            Console.WriteLine("\nThank you for using HealthSync Billing System!");
                            return;
                        default:
                            Console.WriteLine("\n❌ Invalid option. Please try again.");
                            break;
                    }

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\n❌ Validation Error: {ex.Message}");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Error: {ex.Message}");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║           MAIN MENU                    ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine("1. Add In-House Consultant");
            Console.WriteLine("2. Add Visiting Consultant");
            Console.WriteLine("3. View Consultant Details");
            Console.WriteLine("4. View All Consultants");
            Console.WriteLine("5. Generate Summary Report");
            Console.WriteLine("6. Generate Detailed Report");
            Console.WriteLine("7. Run Sample Scenarios");
            Console.WriteLine("8. Exit");
            Console.Write("\nEnter your choice: ");
        }

        static void AddInHouseConsultant()
        {
            Console.WriteLine("\n--- Add In-House Consultant ---");

            Console.Write("Enter Consultant ID (Format: DRxxxx): ");
            string id = Console.ReadLine();

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Specialization: ");
            string specialization = Console.ReadLine();

            Console.Write("Enter Monthly Stipend: ");
            decimal stipend = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Allowances: ");
            decimal allowances = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Bonuses: ");
            decimal bonuses = decimal.Parse(Console.ReadLine());

            var consultant = new InHouseConsultant
            {
                ConsultantId = id,
                Name = name,
                Specialization = specialization,
                MonthlyStipend = stipend,
                Allowances = allowances,
                Bonuses = bonuses
            };

            if (_billingService.AddConsultant(consultant))
            {
                Console.WriteLine("\n✅ In-House consultant added successfully!");
                consultant.DisplayDetails();
            }
        }

        static void AddVisitingConsultant()
        {
            Console.WriteLine("\n--- Add Visiting Consultant ---");

            Console.Write("Enter Consultant ID (Format: DRxxxx): ");
            string id = Console.ReadLine();

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Specialization: ");
            string specialization = Console.ReadLine();

            Console.Write("Enter Number of Consultations: ");
            int consultations = int.Parse(Console.ReadLine());

            Console.Write("Enter Rate Per Visit: ");
            decimal rate = decimal.Parse(Console.ReadLine());

            var consultant = new VisitingConsultant
            {
                ConsultantId = id,
                Name = name,
                Specialization = specialization,
                ConsultationsCount = consultations,
                RatePerVisit = rate
            };

            if (_billingService.AddConsultant(consultant))
            {
                Console.WriteLine("\n✅ Visiting consultant added successfully!");
                consultant.DisplayDetails();
            }
        }

        static void ViewConsultantDetails()
        {
            Console.Write("\nEnter Consultant ID: ");
            string id = Console.ReadLine();

            var consultant = _billingService.GetConsultant(id);
            if (consultant != null)
            {
                consultant.DisplayDetails();
            }
            else
            {
                Console.WriteLine("\n❌ Consultant not found!");
            }
        }

        static void ViewAllConsultants()
        {
            var consultants = _billingService.GetAllConsultants();

            if (consultants.Count == 0)
            {
                Console.WriteLine("\n❌ No consultants in the system!");
                return;
            }

            Console.WriteLine("\n--- All Consultants ---");
            Console.WriteLine($"{"ID",-10} {"Name",-20} {"Type",-15} {"Specialization",-20}");
            Console.WriteLine(new string('-', 70));

            foreach (var consultant in consultants)
            {
                string type = consultant is InHouseConsultant ? "In-House" : "Visiting";
                Console.WriteLine($"{consultant.ConsultantId,-10} {consultant.Name,-20} {type,-15} {consultant.Specialization,-20}");
            }
        }

        static void LoadSampleScenarios()
        {
            try
            {
                // Scenario 1: In-House Consultant (High Earner)
                var inHouse1 = new InHouseConsultant
                {
                    ConsultantId = "DR2001",
                    Name = "Dr. Rajesh Kumar",
                    Specialization = "Cardiology",
                    MonthlyStipend = 10000,
                    Allowances = 2000,
                    Bonuses = 1000
                };
                _billingService.AddConsultant(inHouse1);

                // Scenario 2: Visiting Consultant
                var visiting1 = new VisitingConsultant
                {
                    ConsultantId = "DR8005",
                    Name = "Dr. Priya Sharma",
                    Specialization = "Neurology",
                    ConsultationsCount = 10,
                    RatePerVisit = 600
                };
                _billingService.AddConsultant(visiting1);

                // Additional In-House (Low Earner for 5% TDS)
                var inHouse2 = new InHouseConsultant
                {
                    ConsultantId = "DR3002",
                    Name = "Dr. Amit Patel",
                    Specialization = "General Medicine",
                    MonthlyStipend = 3000,
                    Allowances = 500,
                    Bonuses = 200
                };
                _billingService.AddConsultant(inHouse2);

                Console.WriteLine("✅ Sample scenarios loaded successfully!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading sample data: {ex.Message}");
            }
        }

        static void RunSampleScenarios()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║     SAMPLE SCENARIOS DEMONSTRATION     ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            // Scenario 1: In-House Consultant (High Earner)
            Console.WriteLine("\n--- Scenario 1: In-House Consultant (High Earner) ---");
            Console.WriteLine("Input: ID: DR2001, Stipend: 10000, Allowances: 2000, Bonuses: 1000");
            var consultant1 = _billingService.GetConsultant("DR2001");
            if (consultant1 != null)
            {
                decimal gross1 = consultant1.CalculateGrossPayout();
                decimal tds1 = consultant1.GetTDSPercentage();
                decimal net1 = consultant1.CalculateNetPayout();
                Console.WriteLine($"Process: Gross = 10000 + 2000 + 1000 = {gross1}. Tax = {tds1}%");
                Console.WriteLine($"Output: Gross: {gross1:F2} | TDS Applied: {tds1}% | Net Payout: {net1:F2}");
            }

            // Scenario 2: Visiting Consultant
            Console.WriteLine("\n--- Scenario 2: Visiting Consultant ---");
            Console.WriteLine("Input: ID: DR8005, 10 Visits @ 600");
            var consultant2 = _billingService.GetConsultant("DR8005");
            if (consultant2 != null)
            {
                decimal gross2 = consultant2.CalculateGrossPayout();
                decimal tds2 = consultant2.GetTDSPercentage();
                decimal net2 = consultant2.CalculateNetPayout();
                Console.WriteLine($"Process: Gross = 10 × 600 = {gross2}. Tax = {tds2}% (Flat)");
                Console.WriteLine($"Output: Gross: {gross2:F2} | TDS Applied: {tds2}% | Net Payout: {net2:F2}");
            }

            // Scenario 3: Validation Failure
            Console.WriteLine("\n--- Scenario 3: Validation Failure ---");
            Console.WriteLine("Input: ID: MD1001");
            try
            {
                var invalidConsultant = new InHouseConsultant
                {
                    ConsultantId = "MD1001",
                    Name = "Invalid Doctor",
                    Specialization = "Test",
                    MonthlyStipend = 5000,
                    Allowances = 0,
                    Bonuses = 0
                };
                _billingService.AddConsultant(invalidConsultant);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Output: {ex.Message} (Process terminates)");
            }

            // Additional Scenario: Low Earner (5% TDS)
            Console.WriteLine("\n--- Additional Scenario: In-House Consultant (Low Earner) ---");
            Console.WriteLine("Input: ID: DR3002, Stipend: 3000, Allowances: 500, Bonuses: 200");
            var consultant3 = _billingService.GetConsultant("DR3002");
            if (consultant3 != null)
            {
                decimal gross3 = consultant3.CalculateGrossPayout();
                decimal tds3 = consultant3.GetTDSPercentage();
                decimal net3 = consultant3.CalculateNetPayout();
                Console.WriteLine($"Process: Gross = 3000 + 500 + 200 = {gross3}. Tax = {tds3}%");
                Console.WriteLine($"Output: Gross: {gross3:F2} | TDS Applied: {tds3}% | Net Payout: {net3:F2}");
            }
        }
    }
}
