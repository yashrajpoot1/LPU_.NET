namespace BankingTransactionRisk;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║   Banking Transaction Risk Monitoring System         ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

        var riskMonitor = new RiskMonitor(transactionLimit: 100000);

        try
        {
            // Add Online Transactions
            riskMonitor.AddTransaction(new OnlineTransaction(
                "TXN001", 500, "ACC001", "192.168.1.1", "TRUSTED-DEV-001", "Domestic"));

            riskMonitor.AddTransaction(new OnlineTransaction(
                "TXN002", 8000, "ACC002", "45.67.89.10", "DEV-002", "Foreign-UK"));

            riskMonitor.AddTransaction(new OnlineTransaction(
                "TXN003", 1500, "ACC003", "192.168.1.5", "TRUSTED-DEV-003", "Domestic"));

            // Add Wire Transfers
            riskMonitor.AddTransaction(new WireTransfer(
                "TXN004", 15000, "ACC004", "Chase Bank", "USA", false));

            riskMonitor.AddTransaction(new WireTransfer(
                "TXN005", 25000, "ACC005", "HSBC", "UK", true));

            riskMonitor.DisplayTransactions();

            // Test negative amount
            Console.WriteLine("\n========== TESTING NEGATIVE AMOUNT ==========");
            try
            {
                riskMonitor.AddTransaction(new OnlineTransaction(
                    "TXN006", -100, "ACC006", "1.2.3.4", "DEV-006", "Domestic"));
            }
            catch (NegativeAmountException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            // Test transaction limit
            Console.WriteLine("\n========== TESTING TRANSACTION LIMIT ==========");
            try
            {
                riskMonitor.AddTransaction(new WireTransfer(
                    "TXN007", 150000, "ACC007", "Bank", "Country", false));
            }
            catch (TransactionLimitExceededException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            // Test fraud detection
            Console.WriteLine("\n========== TESTING FRAUD DETECTION ==========");
            try
            {
                riskMonitor.AddTransaction(new OnlineTransaction(
                    "TXN008", 15000, "ACC008", "1.2.3.4", "UNKNOWN-DEV", "Foreign-Nigeria"));
            }
            catch (FraudDetectedException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
                Console.WriteLine($"  Action: Transaction blocked for review");
            }

            // Get high-risk transactions
            Console.WriteLine("\n========== HIGH-RISK TRANSACTIONS (Score > 30) ==========");
            var highRisk = riskMonitor.GetHighRiskTransactions(30);
            foreach (var txn in highRisk)
            {
                txn.DisplayInfo();
            }

            Console.WriteLine($"\n✅ Total Transactions Monitored: {riskMonitor.GetTotalTransactions()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
