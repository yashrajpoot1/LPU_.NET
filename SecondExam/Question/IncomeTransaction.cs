using System;

namespace SecondExam.Question
{
    /// <summary>
    /// Represents an income transaction in the petty cash system
    /// Demonstrates inheritance from Transaction base class
    /// </summary>
    public class IncomeTransaction : Transaction
    {
        /// <summary>
        /// Source of the income (e.g., Main Cash, Bank Transfer)
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Constructor for IncomeTransaction
        /// </summary>
        /// <param name="id">Transaction ID</param>
        /// <param name="date">Transaction date</param>
        /// <param name="amount">Income amount</param>
        /// <param name="description">Income description</param>
        /// <param name="source">Income source</param>
        public IncomeTransaction(int id, DateTime date, decimal amount, string description, string source)
            : base(id, date, amount, description)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentException("Source cannot be empty", nameof(source));

            Source = source;
        }
        /// <summary>
        /// Implementation of abstract GetSummary method
        /// Provides income-specific summary format
        /// </summary>
        /// <returns>Formatted income summary</returns>
        public override string GetSummary()
        {
            return $"INCOME - {Date:yyyy-MM-dd} | Source: {Source} | Amount: +${Amount:F2} | {Description}";
        }
        /// <summary>
        /// Override ToString for income-specific display
        /// </summary>
        /// <returns>String representation of income transaction</returns>
        public override string ToString()
        {
            return $"{base.ToString()}, Source: {Source}";
        }
    }
}