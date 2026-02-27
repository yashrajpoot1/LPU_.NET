using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartBankingSystem
{
    // Custom Exceptions
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(string message) : base(message) { }
    }

    public class MinimumBalanceException : Exception
    {
        public MinimumBalanceException(string message) : base(message) { }
    }

    public class InvalidTransactionException : Exception
    {
        public InvalidTransactionException(string message) : base(message) { }
    }

    // Abstract Base Class
    public abstract class BankAccount
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal Balance { get; protected set; }
        public List<string> TransactionHistory { get; set; }

        public BankAccount(string accountNumber, string customerName, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            CustomerName = customerName;
            Balance = initialBalance;
            TransactionHistory = new List<string>();
            TransactionHistory.Add($"Account created with balance: {initialBalance:C}");
        }

        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidTransactionException("Deposit amount must be positive");
            
            Balance += amount;
            TransactionHistory.Add($"Deposited: {amount:C} | Balance: {Balance:C}");
        }

        public abstract void Withdraw(decimal amount);
        public abstract decimal CalculateInterest();

        public void DisplayTransactionHistory()
        {
            Console.WriteLine($"\n--- Transaction History for {AccountNumber} ---");
            foreach (var transaction in TransactionHistory)
            {
                Console.WriteLine(transaction);
            }
        }
    }

    // Savings Account
    public class SavingsAccount : BankAccount
    {
        private const decimal MinimumBalance = 1000;
        private const decimal InterestRate = 0.04m; // 4%

        public SavingsAccount(string accountNumber, string customerName, decimal initialBalance)
            : base(accountNumber, customerName, initialBalance)
        {
            if (initialBalance < MinimumBalance)
                throw new MinimumBalanceException($"Savings account requires minimum balance of {MinimumBalance:C}");
        }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidTransactionException("Withdrawal amount must be positive");

            if (Balance - amount < MinimumBalance)
                throw new MinimumBalanceException($"Cannot withdraw. Minimum balance of {MinimumBalance:C} must be maintained");

            Balance -= amount;
            TransactionHistory.Add($"Withdrawn: {amount:C} | Balance: {Balance:C}");
        }

        public override decimal CalculateInterest()
        {
            return Balance * InterestRate;
        }
    }

    // Current Account
    public class CurrentAccount : BankAccount
    {
        private const decimal OverdraftLimit = 10000;
        private const decimal InterestRate = 0.02m; // 2%

        public CurrentAccount(string accountNumber, string customerName, decimal initialBalance)
            : base(accountNumber, customerName, initialBalance) { }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidTransactionException("Withdrawal amount must be positive");

            if (Balance - amount < -OverdraftLimit)
                throw new InsufficientBalanceException($"Overdraft limit of {OverdraftLimit:C} exceeded");

            Balance -= amount;
            TransactionHistory.Add($"Withdrawn: {amount:C} | Balance: {Balance:C}");
        }

        public override decimal CalculateInterest()
        {
            return Balance > 0 ? Balance * InterestRate : 0;
        }
    }

    // Loan Account
    public class LoanAccount : BankAccount
    {
        private const decimal InterestRate = 0.08m; // 8%

        public LoanAccount(string accountNumber, string customerName, decimal loanAmount)
            : base(accountNumber, customerName, -loanAmount) { }

        public override void Deposit(decimal amount)
        {
            throw new InvalidTransactionException("Cannot deposit to loan account. Use repayment method");
        }

        public void Repay(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidTransactionException("Repayment amount must be positive");

            Balance += amount;
            TransactionHistory.Add($"Loan Repayment: {amount:C} | Outstanding: {Math.Abs(Balance):C}");
        }

        public override void Withdraw(decimal amount)
        {
            throw new InvalidTransactionException("Cannot withdraw from loan account");
        }

        public override decimal CalculateInterest()
        {
            return Math.Abs(Balance) * InterestRate;
        }
    }

    class Program
    {
        static List<BankAccount> accounts = new List<BankAccount>();

        static void Main(string[] args)
        {
            InitializeSampleData();

            while (true)
            {
                Console.WriteLine("\n╔════════════════════════════════════════╗");
                Console.WriteLine("║   SMART BANKING SYSTEM                 ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Withdraw Money");
                Console.WriteLine("4. Transfer Money");
                Console.WriteLine("5. View Account Details");
                Console.WriteLine("6. Calculate Interest");
                Console.WriteLine("7. View Transaction History");
                Console.WriteLine("8. LINQ Reports");
                Console.WriteLine("9. Exit");
                Console.Write("\nEnter choice: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": CreateAccount(); break;
                        case "2": DepositMoney(); break;
                        case "3": WithdrawMoney(); break;
                        case "4": TransferMoney(); break;
                        case "5": ViewAccountDetails(); break;
                        case "6": CalculateInterest(); break;
                        case "7": ViewTransactionHistory(); break;
                        case "8": ShowLinqReports(); break;
                        case "9": return;
                        default: Console.WriteLine("Invalid choice!"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Error: {ex.Message}");
                }
            }
        }

        static void InitializeSampleData()
        {
            accounts.Add(new SavingsAccount("SA001", "Rajesh Kumar", 75000));
            accounts.Add(new SavingsAccount("SA002", "Priya Sharma", 120000));
            accounts.Add(new CurrentAccount("CA001", "Ramesh Patel", 85000));
            accounts.Add(new CurrentAccount("CA002", "Amit Singh", 45000));
            accounts.Add(new LoanAccount("LA001", "Ravi Verma", 200000));
        }

        static void CreateAccount()
        {
            Console.WriteLine("\n--- Create New Account ---");
            Console.WriteLine("1. Savings Account");
            Console.WriteLine("2. Current Account");
            Console.WriteLine("3. Loan Account");
            Console.Write("Select account type: ");
            string type = Console.ReadLine();

            Console.Write("Enter Account Number: ");
            string accNo = Console.ReadLine();

            Console.Write("Enter Customer Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Initial Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            switch (type)
            {
                case "1":
                    accounts.Add(new SavingsAccount(accNo, name, amount));
                    break;
                case "2":
                    accounts.Add(new CurrentAccount(accNo, name, amount));
                    break;
                case "3":
                    accounts.Add(new LoanAccount(accNo, name, amount));
                    break;
            }

            Console.WriteLine("✅ Account created successfully!");
        }

        static void DepositMoney()
        {
            Console.Write("\nEnter Account Number: ");
            string accNo = Console.ReadLine();

            var account = accounts.FirstOrDefault(a => a.AccountNumber == accNo);
            if (account == null)
            {
                Console.WriteLine("❌ Account not found!");
                return;
            }

            Console.Write("Enter amount to deposit: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            account.Deposit(amount);
            Console.WriteLine($"✅ Deposited {amount:C}. New Balance: {account.Balance:C}");
        }

        static void WithdrawMoney()
        {
            Console.Write("\nEnter Account Number: ");
            string accNo = Console.ReadLine();

            var account = accounts.FirstOrDefault(a => a.AccountNumber == accNo);
            if (account == null)
            {
                Console.WriteLine("❌ Account not found!");
                return;
            }

            Console.Write("Enter amount to withdraw: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            account.Withdraw(amount);
            Console.WriteLine($"✅ Withdrawn {amount:C}. New Balance: {account.Balance:C}");
        }

        static void TransferMoney()
        {
            Console.Write("\nEnter Source Account Number: ");
            string fromAccNo = Console.ReadLine();

            Console.Write("Enter Destination Account Number: ");
            string toAccNo = Console.ReadLine();

            var fromAccount = accounts.FirstOrDefault(a => a.AccountNumber == fromAccNo);
            var toAccount = accounts.FirstOrDefault(a => a.AccountNumber == toAccNo);

            if (fromAccount == null || toAccount == null)
            {
                Console.WriteLine("❌ One or both accounts not found!");
                return;
            }

            Console.Write("Enter amount to transfer: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            fromAccount.Withdraw(amount);
            toAccount.Deposit(amount);

            Console.WriteLine($"✅ Transferred {amount:C} from {fromAccNo} to {toAccNo}");
        }

        static void ViewAccountDetails()
        {
            Console.Write("\nEnter Account Number: ");
            string accNo = Console.ReadLine();

            var account = accounts.FirstOrDefault(a => a.AccountNumber == accNo);
            if (account == null)
            {
                Console.WriteLine("❌ Account not found!");
                return;
            }

            Console.WriteLine($"\n--- Account Details ---");
            Console.WriteLine($"Account Number: {account.AccountNumber}");
            Console.WriteLine($"Customer Name: {account.CustomerName}");
            Console.WriteLine($"Account Type: {account.GetType().Name}");
            Console.WriteLine($"Balance: {account.Balance:C}");
        }

        static void CalculateInterest()
        {
            Console.WriteLine("\n--- Interest Calculation ---");
            foreach (var account in accounts)
            {
                decimal interest = account.CalculateInterest();
                Console.WriteLine($"{account.AccountNumber} ({account.GetType().Name}): Interest = {interest:C}");
            }
        }

        static void ViewTransactionHistory()
        {
            Console.Write("\nEnter Account Number: ");
            string accNo = Console.ReadLine();

            var account = accounts.FirstOrDefault(a => a.AccountNumber == accNo);
            if (account == null)
            {
                Console.WriteLine("❌ Account not found!");
                return;
            }

            account.DisplayTransactionHistory();
        }

        static void ShowLinqReports()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║   LINQ REPORTS                         ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            // 1. Accounts with balance > 50,000
            Console.WriteLine("\n1. Accounts with balance > 50,000:");
            var highBalanceAccounts = accounts.Where(a => a.Balance > 50000);
            foreach (var acc in highBalanceAccounts)
            {
                Console.WriteLine($"   {acc.AccountNumber} - {acc.CustomerName}: {acc.Balance:C}");
            }

            // 2. Total bank balance
            decimal totalBalance = accounts.Sum(a => a.Balance);
            Console.WriteLine($"\n2. Total Bank Balance: {totalBalance:C}");

            // 3. Top 3 highest balance accounts
            Console.WriteLine("\n3. Top 3 Highest Balance Accounts:");
            var top3 = accounts.OrderByDescending(a => a.Balance).Take(3);
            foreach (var acc in top3)
            {
                Console.WriteLine($"   {acc.AccountNumber} - {acc.CustomerName}: {acc.Balance:C}");
            }

            // 4. Group accounts by type
            Console.WriteLine("\n4. Accounts Grouped by Type:");
            var groupedAccounts = accounts.GroupBy(a => a.GetType().Name);
            foreach (var group in groupedAccounts)
            {
                Console.WriteLine($"   {group.Key}: {group.Count()} accounts");
            }

            // 5. Customers whose name starts with "R"
            Console.WriteLine("\n5. Customers whose name starts with 'R':");
            var customersWithR = accounts.Where(a => a.CustomerName.StartsWith("R"));
            foreach (var acc in customersWithR)
            {
                Console.WriteLine($"   {acc.AccountNumber} - {acc.CustomerName}");
            }
        }
    }
}
