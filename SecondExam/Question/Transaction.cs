using System;

namespace SecondExam.Question
{
    /// <summary>
    /// Abstract base class for all financial transactions
    /// Demonstrates abstraction and encapsulation principles
    /// </summary>
    public abstract class Transaction : IReportable
    {
        /// <summary>
        /// Unique identifier for the transaction
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Date when the transaction occurred
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Amount of the transaction (always positive)
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Description of the transaction
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Constructor for Transaction
        /// </summary>
        /// <param name="id">Transaction ID</param>
        /// <param name="date">Transaction date</param>
        /// <param name="amount">Transaction amount</param>
        /// <param name="description">Transaction description</param>
        protected Transaction(int id, DateTime date, decimal amount, string description)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive", nameof(amount));
            
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty", nameof(description));

            Id = id;
            Date = date;
            Amount = amount;
            Description = description;
        }

        /// <summary>
        /// Abstract method to be implemented by derived classes
        /// </summary>
        /// <returns>Transaction summary</returns>
        public abstract string GetSummary();

        /// <summary>
        /// Override ToString for better display
        /// </summary>
        /// <returns>String representation of transaction</returns>
        public override string ToString()
        {
            return $"ID: {Id}, Date: {Date:yyyy-MM-dd}, Amount: ${Amount:F2}, Description: {Description}";
        }
    }
}