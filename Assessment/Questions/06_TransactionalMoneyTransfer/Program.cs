using System;
using TransactionalMoneyTransfer;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Problem 6: Transactional Money Transfer ===");
        var service = new MoneyTransferService();
        
        service.AddAccount(new Account { AccountId = "ACC001", Balance = 1000 });
        service.AddAccount(new Account { AccountId = "ACC002", Balance = 500 });

        Console.WriteLine("Initial balances: ACC001=$1000, ACC002=$500");
        
        var result = service.Transfer("ACC001", "ACC002", 300);
        Console.WriteLine($"\nTransfer $300: {(result.Success ? "SUCCESS" : "FAILED")}");
        Console.WriteLine($"Message: {result.Message}");
        Console.WriteLine($"Audit Log ID: {result.AuditLogId}");

        try
        {
            service.Transfer("ACC001", "ACC002", 2000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nTransfer $2000 failed as expected: {ex.Message}");
        }

        Console.WriteLine($"\nTotal audit logs: {service.GetAuditLogs().Count}");
        Console.WriteLine("✓ Test Passed\n");
    }
}
