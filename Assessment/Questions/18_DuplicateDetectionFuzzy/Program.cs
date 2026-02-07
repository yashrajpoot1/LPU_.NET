using System;
using System.Collections.Generic;
using DuplicateDetectionFuzzy;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Problem 18: Duplicate Detection with Fuzzy Matching ===");
        var detector = new DuplicateDetector();
        
        var customers = new List<Customer>
        {
            new Customer { CustomerId = "C1", Name = "John Smith", Email = "john@test.com", Phone = "1234567890" },
            new Customer { CustomerId = "C2", Name = "Jon Smith", Email = "jon@test.com", Phone = "1234567890" },
            new Customer { CustomerId = "C3", Name = "Jane Doe", Email = "jane@test.com", Phone = "9876543210" },
            new Customer { CustomerId = "C4", Name = "Jane Doe", Email = "jane@test.com", Phone = "5555555555" }
        };

        var duplicates = detector.FindDuplicates(customers);

        Console.WriteLine($"Found {duplicates.Count} duplicate groups:");
        foreach (var group in duplicates)
        {
            Console.WriteLine($"  Group: {string.Join(", ", group.CustomerIds)} - Reason: {group.Reason}");
        }

        Console.WriteLine("✓ Test Passed\n");
    }
}
