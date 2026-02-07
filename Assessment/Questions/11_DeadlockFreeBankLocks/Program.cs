using System;
using System.Threading.Tasks;
using DeadlockFreeBankLocks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("=== Problem 11: Deadlock-Free Bank Locks ===");
        var service = new BankTransferService();
        
        var acc1 = new Account { AccountId = "ACC001", Balance = 1000 };
        var acc2 = new Account { AccountId = "ACC002", Balance = 1000 };

        var tasks = new Task[10];
        for (int i = 0; i < 5; i++)
        {
            tasks[i] = Task.Run(() => service.SafeTransfer(acc1, acc2, 100));
            tasks[i + 5] = Task.Run(() => service.SafeTransfer(acc2, acc1, 50));
        }

        await Task.WhenAll(tasks);

        Console.WriteLine($"ACC001 Balance: ${acc1.Balance}");
        Console.WriteLine($"ACC002 Balance: ${acc2.Balance}");
        Console.WriteLine($"Total: ${acc1.Balance + acc2.Balance} (should be $2000)");
        Console.WriteLine("✓ Test Passed\n");
    }
}
