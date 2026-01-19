using System;
namespace SecondExam.Question
{
    /// <summary>
    /// Represents an expense transaction in the petty cash system
    /// Demonstrates inheritance from Transaction base class
    /// </summary>
    public class ExpenseTransaction : Transaction
    {
        /// <summary>
        /// Category of the expense (e.g., Office, Travel, Food)
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Constructor for ExpenseTransaction
        /// </summary>
        /// <param name="id">Transaction ID</param>
        /// <param name="date">Transaction date</param>
        /// <param name="amount">Expense amount</param>
        /// <param name="description">Expense description</param>
        /// <param name="category">Expense category</param>
        public ExpenseTransaction(int id, DateTime date, decimal amount, string description, string category)
            : base(id, date, amount, description)
        {
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Category cannot be empty", nameof(category));
            Category = category;
        }
        /// <summary>
        /// Implementation of abstract GetSummary method
        /// Provides expense-specific summary format
        /// </summary>
        /// <returns>Formatted expense summary</returns>
        public override string GetSummary()
        {
            return $"EXPENSE - {Date:yyyy-MM-dd} | Category: {Category} | Amount: -${Amount:F2} | {Description}";
        }

        /// <summary>
        /// Override ToString for expense-specific display
        /// </summary>
        /// <returns>String representation of expense transaction</returns>
        public override string ToString()
        {
            return $"{base.ToString()}, Category: {Category}";
        }
    }
}