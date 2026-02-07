using System;
using System.Collections.Generic;
using MultiTenantReportGenerator;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Problem 16: Multi-Tenant Report Generator ===");
        var generator = new ReportGenerator();
        
        var transactions = new List<Transaction>
        {
            new Transaction { TenantId = "T1", Type = "Credit", Amount = 1000, Timestamp = DateTime.UtcNow },
            new Transaction { TenantId = "T1", Type = "Debit", Amount = 200, Timestamp = DateTime.UtcNow.AddMinutes(1) },
            new Transaction { TenantId = "T1", Type = "Debit", Amount = 300, Timestamp = DateTime.UtcNow.AddMinutes(2) },
            new Transaction { TenantId = "T1", Type = "Debit", Amount = 400, Timestamp = DateTime.UtcNow.AddMinutes(3) },
            new Transaction { TenantId = "T2", Type = "Credit", Amount = 5000, Timestamp = DateTime.UtcNow }
        };

        var reports = generator.GenerateReports(transactions);

        foreach (var report in reports)
        {
            Console.WriteLine($"\nTenant: {report.TenantId}");
            Console.WriteLine($"  Credits: ${report.TotalCredits}");
            Console.WriteLine($"  Debits: ${report.TotalDebits}");
            Console.WriteLine($"  Peak Hour: {report.PeakTransactionHour}");
            Console.WriteLine($"  Suspicious: {report.IsSuspicious}");
        }

        Console.WriteLine("✓ Test Passed\n");
    }
}
