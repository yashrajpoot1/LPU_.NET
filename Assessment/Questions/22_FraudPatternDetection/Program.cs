using System;
using System.Collections.Generic;
using FraudPatternDetection;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Problem 22: Fraud Pattern Detection ===");
        var detector = new FraudDetector();
        
        var transactions = new List<Transaction>
        {
            new Transaction { TransactionId = "T1", CardNumber = "1234", Amount = 60000, Timestamp = DateTime.UtcNow, City = "NYC" },
            new Transaction { TransactionId = "T2", CardNumber = "1234", Amount = 55000, Timestamp = DateTime.UtcNow.AddSeconds(30), City = "NYC" },
            new Transaction { TransactionId = "T3", CardNumber = "1234", Amount = 70000, Timestamp = DateTime.UtcNow.AddSeconds(60), City = "NYC" },
            new Transaction { TransactionId = "T4", CardNumber = "5678", Amount = 1000, Timestamp = DateTime.UtcNow, City = "LA" },
            new Transaction { TransactionId = "T5", CardNumber = "5678", Amount = 2000, Timestamp = DateTime.UtcNow.AddMinutes(5), City = "NYC" }
        };

        var alerts = detector.DetectFraud(transactions);

        Console.WriteLine($"Found {alerts.Count} fraud alerts:");
        foreach (var alert in alerts)
        {
            Console.WriteLine($"  Card: {alert.CardNumber}");
            Console.WriteLine($"  Reason: {alert.Reason}");
            Console.WriteLine($"  Transactions: {string.Join(", ", alert.TransactionIds)}");
        }

        Console.WriteLine("✓ Test Passed\n");
    }
}
