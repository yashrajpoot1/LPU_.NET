using System;
using System.Collections.Generic;
using SecondExam.Question;
namespace SecondExam
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digital Petty Cash Ledger System");
            Console.WriteLine("Use Case UC-FIN-01: Record and Balance Petty Cash");
            try
            {
                // Step 1: Create a Ledger<IncomeTransaction> to track funds received
                var incomeLedger = new Ledger<IncomeTransaction>();
                // Step 2: Record a $500 replenishment from "Main Cash"
                var income = new IncomeTransaction(1, DateTime.Now, 500m, "Monthly petty cash replenishment", "Main Cash");
                incomeLedger.AddEntry(income);
                // Step 3: Create a Ledger<ExpenseTransaction> to track daily spends
                var expenseLedger = new Ledger<ExpenseTransaction>();
                // Step 4: Record expenses - $20 for Stationery and $15 for Team Snacks
                var expense1 = new ExpenseTransaction(2, DateTime.Now, 20m, "Office stationery", "Office");
                var expense2 = new ExpenseTransaction(3, DateTime.Now, 15m, "Team snacks", "Food");
                expenseLedger.AddEntry(expense1);
                expenseLedger.AddEntry(expense2);
                // Step 5: Display totals using generic methods
                Console.WriteLine($"Total Income: ${incomeLedger.CalculateTotal():F2}");
                Console.WriteLine($"Total Expenses: ${expenseLedger.CalculateTotal():F2}");
                // Step 6: Calculate Net Balance (Total Income - Total Expenses)
                var netBalance = incomeLedger.CalculateTotal() - expenseLedger.CalculateTotal();
                Console.WriteLine($"Net Balance: ${netBalance:F2}");
                Console.WriteLine("Polymorphic Transaction Display:");
                // Demonstrate polymorphism - List<Transaction> calling GetSummary()
                var allTransactions = new List<Transaction>();
                allTransactions.AddRange(incomeLedger.GetAllTransactions());
                allTransactions.AddRange(expenseLedger.GetAllTransactions());
                foreach (var transaction in allTransactions)
                {
                    Console.WriteLine(transaction.GetSummary());
                }
                Console.WriteLine("System demonstrates:");
                Console.WriteLine("- Inheritance: ExpenseTransaction and IncomeTransaction inherit from Transaction");
                Console.WriteLine("- Polymorphism: GetSummary() method calls work on Transaction base type");
                Console.WriteLine("- Generics: Ledger<T> provides type safety with constraints");
                Console.WriteLine("- Collections: List<T> used for storing and filtering transactions");
                Console.WriteLine("- Interface: IReportable implemented by all transaction types");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}