using System;

namespace HealthSyncBilling.Models
{
    /// <summary>
    /// Abstract base class for all consultants
    /// Implements abstraction - prevents creation of generic consultants
    /// </summary>
    public abstract class Consultant
    {
        /// <summary>
        /// Consultant ID (must be validated)
        /// </summary>
        public string ConsultantId { get; set; }

        /// <summary>
        /// Consultant name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Consultant specialization
        /// </summary>
        public string Specialization { get; set; }

        /// <summary>
        /// Abstract method to calculate gross payout
        /// Forces subclasses to implement their own financial logic
        /// </summary>
        /// <returns>Gross payout amount</returns>
        public abstract decimal CalculateGrossPayout();

        /// <summary>
        /// Virtual method for TDS calculation
        /// Default: Sliding scale (5% or 15%) based on earnings
        /// Can be overridden by subclasses
        /// </summary>
        /// <param name="grossPayout">Gross payout amount</param>
        /// <returns>TDS percentage</returns>
        public virtual decimal CalculateTDS(decimal grossPayout)
        {
            // Default sliding scale taxation
            if (grossPayout <= 5000)
            {
                return 5m; // 5%
            }
            else
            {
                return 15m; // 15%
            }
        }

        /// <summary>
        /// Calculate net payout after TDS deduction
        /// </summary>
        /// <returns>Net payout amount</returns>
        public decimal CalculateNetPayout()
        {
            decimal grossPayout = CalculateGrossPayout();
            decimal tdsPercentage = CalculateTDS(grossPayout);
            decimal tdsAmount = grossPayout * (tdsPercentage / 100);
            return grossPayout - tdsAmount;
        }

        /// <summary>
        /// Get TDS percentage for display
        /// </summary>
        /// <returns>TDS percentage</returns>
        public decimal GetTDSPercentage()
        {
            decimal grossPayout = CalculateGrossPayout();
            return CalculateTDS(grossPayout);
        }

        /// <summary>
        /// Validate consultant ID
        /// Rules:
        /// - Length: Exactly 6 characters
        /// - Prefix: Must start with "DR"
        /// - Format: Last 4 characters must be numeric
        /// </summary>
        /// <param name="consultantId">ID to validate</param>
        /// <returns>True if valid</returns>
        public static bool ValidateConsultantId(string consultantId)
        {
            // Check if null or empty
            if (string.IsNullOrWhiteSpace(consultantId))
            {
                return false;
            }

            // Check length (exactly 6 characters)
            if (consultantId.Length != 6)
            {
                return false;
            }

            // Check prefix (must start with "DR")
            if (!consultantId.StartsWith("DR", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            // Check last 4 characters are numeric
            string numericPart = consultantId.Substring(2);
            if (!int.TryParse(numericPart, out _))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Display consultant details
        /// </summary>
        public virtual void DisplayDetails()
        {
            decimal grossPayout = CalculateGrossPayout();
            decimal tdsPercentage = GetTDSPercentage();
            decimal netPayout = CalculateNetPayout();

            Console.WriteLine($"\n--- Consultant Details ---");
            Console.WriteLine($"ID: {ConsultantId}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Specialization: {Specialization}");
            Console.WriteLine($"Type: {GetType().Name}");
            Console.WriteLine($"Gross Payout: {grossPayout:C}");
            Console.WriteLine($"TDS Applied: {tdsPercentage}%");
            Console.WriteLine($"Net Payout: {netPayout:C}");
        }
    }
}
