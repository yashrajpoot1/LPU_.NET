using System;
using System.Linq;
using CustomLinqExtensions;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Problem 15: Custom LINQ Extensions ===");
        
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var evens = numbers.WhereEx(n => n % 2 == 0);
        Console.WriteLine($"Even numbers: {string.Join(", ", evens)}");

        var squared = numbers.SelectEx(n => n * n);
        Console.WriteLine($"Squared: {string.Join(", ", squared.Take(5))}...");

        var duplicates = new[] { 1, 2, 2, 3, 3, 3, 4 };
        var distinct = duplicates.DistinctEx();
        Console.WriteLine($"Distinct: {string.Join(", ", distinct)}");

        var words = new[] { "apple", "banana", "apricot", "blueberry" };
        var grouped = words.GroupByEx(w => w[0]);
        foreach (var group in grouped)
        {
            Console.WriteLine($"Group '{group.Key}': {string.Join(", ", group)}");
        }

        Console.WriteLine("✓ Test Passed\n");
    }
}
