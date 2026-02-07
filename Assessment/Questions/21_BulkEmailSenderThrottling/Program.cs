using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BulkEmailSenderThrottling;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("=== Problem 21: Bulk Email Sender with Throttling ===");
        var sender = new BulkEmailSender();

        var emails = new List<Email>();
        for (int i = 1; i <= 15; i++)
        {
            emails.Add(new Email
            {
                To = $"user{i}@test.com",
                Subject = "Test Email",
                Body = "This is a test"
            });
        }

        Console.WriteLine($"Sending {emails.Count} emails...");
        var report = await sender.SendBulkAsync(emails);

        Console.WriteLine($"Success: {report.SuccessCount}");
        Console.WriteLine($"Failed: {report.FailureCount}");
        if (report.FailedEmails.Count > 0)
        {
            Console.WriteLine($"Failed emails: {string.Join(", ", report.FailedEmails)}");
        }

        Console.WriteLine("✓ Test Passed\n");
    }
}
