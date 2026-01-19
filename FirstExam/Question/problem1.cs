using System;

namespace QuickMartTraders
{
    public class SaleTransaction
    {
        public string InvoiceNo { get; set; }
        public string CustomerName { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal SellingAmount { get; set; }
        public string ProfitOrLossStatus { get; set; }
        public decimal ProfitOrLossAmount { get; set; }
        public decimal ProfitMarginPercent { get; set; }

        public static SaleTransaction LastTransaction;
        public static bool HasLastTransaction = false;

        public static void CreateNewTransaction()
        {
            Console.WriteLine("Enter Invoice No:");
            string invoiceNo = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(invoiceNo))
            {
                Console.WriteLine("Invoice No cannot be empty. Please try again.");
                return;
            }

            Console.WriteLine("Enter Customer Name:");
            string customerName = Console.ReadLine();

            Console.WriteLine("Enter Item Name:");
            string itemName = Console.ReadLine();

            Console.WriteLine("Enter Quantity:");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Quantity must be greater than 0. Please try again.");
                return;
            }

            Console.WriteLine("Enter Purchase Amount (total):");
            if (!decimal.TryParse(Console.ReadLine(), out decimal purchaseAmount) || purchaseAmount <= 0)
            {
                Console.WriteLine("Purchase Amount must be greater than 0. Please try again.");
                return;
            }

            Console.WriteLine("Enter Selling Amount (total):");
            if (!decimal.TryParse(Console.ReadLine(), out decimal sellingAmount) || sellingAmount < 0)
            {
                Console.WriteLine("Selling Amount must be 0 or greater. Please try again.");
                return;
            }

            // Calculate profit/loss values
            string profitOrLossStatus;
            decimal profitOrLossAmount;
            decimal profitMarginPercent;

            if (sellingAmount > purchaseAmount)
            {
                profitOrLossStatus = "PROFIT";
                profitOrLossAmount = sellingAmount - purchaseAmount;
            }
            else if (sellingAmount < purchaseAmount)
            {
                profitOrLossStatus = "LOSS";
                profitOrLossAmount = purchaseAmount - sellingAmount;
            }
            else
            {
                profitOrLossStatus = "BREAK-EVEN";
                profitOrLossAmount = 0;
            }

            profitMarginPercent = (profitOrLossAmount / purchaseAmount) * 100;

            // Create and store the transaction
            LastTransaction = new SaleTransaction
            {
                InvoiceNo = invoiceNo,
                CustomerName = customerName,
                ItemName = itemName,
                Quantity = quantity,
                PurchaseAmount = purchaseAmount,
                SellingAmount = sellingAmount,
                ProfitOrLossStatus = profitOrLossStatus,
                ProfitOrLossAmount = profitOrLossAmount,
                ProfitMarginPercent = profitMarginPercent
            };

            HasLastTransaction = true;

            Console.WriteLine("Transaction saved successfully.");
            Console.WriteLine($"Status: {profitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {profitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {profitMarginPercent:F2}");
        }

        public static void ViewLastTransaction()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            Console.WriteLine("-------------- Last Transaction --------------");
            Console.WriteLine($"InvoiceNo: {LastTransaction.InvoiceNo}");
            Console.WriteLine($"Customer: {LastTransaction.CustomerName}");
            Console.WriteLine($"Item: {LastTransaction.ItemName}");
            Console.WriteLine($"Quantity: {LastTransaction.Quantity}");
            Console.WriteLine($"Purchase Amount: {LastTransaction.PurchaseAmount:F2}");
            Console.WriteLine($"Selling Amount: {LastTransaction.SellingAmount:F2}");
            Console.WriteLine($"Status: {LastTransaction.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {LastTransaction.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {LastTransaction.ProfitMarginPercent:F2}");
            Console.WriteLine("--------------------------------------------");
        }

        public static void CalculateProfitLoss()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            // Recompute profit/loss values
            string profitOrLossStatus;
            decimal profitOrLossAmount;
            decimal profitMarginPercent;

            if (LastTransaction.SellingAmount > LastTransaction.PurchaseAmount)
            {
                profitOrLossStatus = "PROFIT";
                profitOrLossAmount = LastTransaction.SellingAmount - LastTransaction.PurchaseAmount;
            }
            else if (LastTransaction.SellingAmount < LastTransaction.PurchaseAmount)
            {
                profitOrLossStatus = "LOSS";
                profitOrLossAmount = LastTransaction.PurchaseAmount - LastTransaction.SellingAmount;
            }
            else
            {
                profitOrLossStatus = "BREAK-EVEN";
                profitOrLossAmount = 0;
            }

            profitMarginPercent = (profitOrLossAmount / LastTransaction.PurchaseAmount) * 100;

            // Update the stored transaction
            LastTransaction.ProfitOrLossStatus = profitOrLossStatus;
            LastTransaction.ProfitOrLossAmount = profitOrLossAmount;
            LastTransaction.ProfitMarginPercent = profitMarginPercent;

            // Print the computed output
            Console.WriteLine("Profit/Loss Calculation Results:");
            Console.WriteLine($"Status: {profitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {profitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {profitMarginPercent:F2}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool exitProgram = false;

            while (!exitProgram)
            {
                // Display menu
                Console.WriteLine("================== QuickMart Traders ==================");
                Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
                Console.WriteLine("2. View Last Transaction");
                Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your option:");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        SaleTransaction.CreateNewTransaction();
                        break;
                    case "2":
                        SaleTransaction.ViewLastTransaction();
                        break;
                    case "3":
                        SaleTransaction.CalculateProfitLoss();
                        break;
                    case "4":
                        Console.WriteLine("Thank you. Application closed normally.");
                        exitProgram = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please select a valid menu option (1-4).");
                        break;
                }

                if (!exitProgram)
                {
                    Console.WriteLine("------------------------------------------------------");
                }
            }
        }
    }
}