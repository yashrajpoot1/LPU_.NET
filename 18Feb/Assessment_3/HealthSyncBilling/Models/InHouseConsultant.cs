using System;

namespace HealthSyncBilling.Models
{
    /// <summary>
    /// In-House Consultant class
    /// Uses formula: MonthlyStipend + Allowances + Bonuses
    /// </summary>
    public class InHouseConsultant : Consultant
    {
        /// <summary>
        /// Monthly stipend amount
        /// </summary>
        public decimal MonthlyStipend { get; set; }

        /// <summary>
        /// Additional allowances (housing, transport, etc.)
        /// </summary>
        public decimal Allowances { get; set; }

        /// <summary>
        /// Performance bonuses
        /// </summary>
        public decimal Bonuses { get; set; }

        /// <summary>
        /// Override CalculateGrossPayout for In-House consultants
        /// Formula: MonthlyStipend + Allowances + Bonuses
        /// </summary>
        /// <returns>Gross payout amount</returns>
        public override decimal CalculateGrossPayout()
        {
            return MonthlyStipend + Allowances + Bonuses;
        }

        /// <summary>
        /// Display In-House consultant specific details
        /// </summary>
        public override void DisplayDetails()
        {
            Console.WriteLine($"\n╔════════════════════════════════════════╗");
            Console.WriteLine($"║     IN-HOUSE CONSULTANT PAYOUT         ║");
            Console.WriteLine($"╚════════════════════════════════════════╝");
            Console.WriteLine($"ID: {ConsultantId}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Specialization: {Specialization}");
            Console.WriteLine($"\n--- Payout Breakdown ---");
            Console.WriteLine($"Monthly Stipend: {MonthlyStipend:C}");
            Console.WriteLine($"Allowances: {Allowances:C}");
            Console.WriteLine($"Bonuses: {Bonuses:C}");
            Console.WriteLine($"Gross Payout: {CalculateGrossPayout():C}");
            Console.WriteLine($"TDS Applied: {GetTDSPercentage()}%");
            Console.WriteLine($"Net Payout: {CalculateNetPayout():C}");
        }
    }
}
