using System;

namespace HealthSyncBilling.Models
{
    /// <summary>
    /// Visiting Consultant class
    /// Uses formula: ConsultationsCount * RatePerVisit
    /// Overrides TDS calculation to use flat 10% rate
    /// </summary>
    public class VisitingConsultant : Consultant
    {
        /// <summary>
        /// Number of consultations performed
        /// </summary>
        public int ConsultationsCount { get; set; }

        /// <summary>
        /// Rate per consultation visit
        /// </summary>
        public decimal RatePerVisit { get; set; }

        /// <summary>
        /// Override CalculateGrossPayout for Visiting consultants
        /// Formula: ConsultationsCount * RatePerVisit
        /// </summary>
        /// <returns>Gross payout amount</returns>
        public override decimal CalculateGrossPayout()
        {
            return ConsultationsCount * RatePerVisit;
        }

        /// <summary>
        /// Override CalculateTDS to use flat 10% rate
        /// Demonstrates polymorphism - subclass opts out of default behavior
        /// </summary>
        /// <param name="grossPayout">Gross payout amount</param>
        /// <returns>TDS percentage (always 10%)</returns>
        public override decimal CalculateTDS(decimal grossPayout)
        {
            // Flat 10% rate for all visiting consultants
            return 10m;
        }

        /// <summary>
        /// Display Visiting consultant specific details
        /// </summary>
        public override void DisplayDetails()
        {
            Console.WriteLine($"\n╔════════════════════════════════════════╗");
            Console.WriteLine($"║     VISITING CONSULTANT PAYOUT         ║");
            Console.WriteLine($"╚════════════════════════════════════════╝");
            Console.WriteLine($"ID: {ConsultantId}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Specialization: {Specialization}");
            Console.WriteLine($"\n--- Payout Breakdown ---");
            Console.WriteLine($"Consultations Count: {ConsultationsCount}");
            Console.WriteLine($"Rate Per Visit: {RatePerVisit:C}");
            Console.WriteLine($"Gross Payout: {CalculateGrossPayout():C}");
            Console.WriteLine($"TDS Applied: {GetTDSPercentage()}% (Flat Rate)");
            Console.WriteLine($"Net Payout: {CalculateNetPayout():C}");
        }
    }
}
