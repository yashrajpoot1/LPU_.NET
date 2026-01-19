using System;
using System.Collections.Generic;
using System.Linq;

namespace SecondExam.Question
{
    /// <summary>
    /// Generic ledger class for managing transactions
    /// Demonstrates generics, collections, and type safety
    /// </summary>
    /// <typeparam name="T">Transaction type that must inherit from Transaction</typeparam>
    public class Ledger<T> where T : Transaction
    {
        /// <summary>
        /// Internal storage for transactions using List<T>
        /// Demonstrates use of collections
        /// </summary>
        private readonly List<T> _transactions;

        /// <summary>
        /// Read-only property to get transaction count
        /// </summary>
        public int TransactionCount => _transactions.Count;

        /// <summary>
        /// Constructor initializes the internal transaction list
        /// </summary>
        public Ledger()
        {
            _transactions = new List<T>();
        }

        /// <summary>
        /// Adds a transaction entry to the ledger
        /// Demonstrates encapsulation and type safety
        /// </summary>
        /// <param name="entry">Transaction to add</param>
        /// <exception cref="ArgumentNullException">Thrown when entry is null</exception>
        public void AddEntry(T entry)
        {
            if (entry is null)
                throw new ArgumentNullException(nameof(entry), "Transaction entry cannot be null");

            _transactions.Add(entry);
        }

        /// <summary>
        /// Retrieves transactions filtered by date
        /// Demonstrates LINQ usage and filtering
        /// </summary>
        /// <param name="date">Date to filter by</param>
        /// <returns>List of transactions for the specified date</returns>
        public List<T> GetTransactionsByDate(DateTime date)
        {
            return _transactions.Where(t => t.Date.Date == date.Date).ToList();
        }

        /// <summary>
        /// Calculates the total amount of all transactions
        /// Uses LINQ for efficient calculation
        /// </summary>
        /// <returns>Total amount of all transactions</returns>
        public decimal CalculateTotal()
        {
            return _transactions.Sum(t => t.Amount);
        }

        /// <summary>
        /// Gets all transactions in the ledger
        /// Returns a copy to maintain encapsulation
        /// </summary>
        /// <returns>List of all transactions</returns>
        public List<T> GetAllTransactions()
        {
            return new List<T>(_transactions);
        }

        /// <summary>
        /// Gets transactions within a date range
        /// Demonstrates advanced filtering capabilities
        /// </summary>
        /// <param name="startDate">Start date (inclusive)</param>
        /// <param name="endDate">End date (inclusive)</param>
        /// <returns>List of transactions within the date range</returns>
        public List<T> GetTransactionsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _transactions.Where(t => t.Date.Date >= startDate.Date && t.Date.Date <= endDate.Date).ToList();
        }
    }
}