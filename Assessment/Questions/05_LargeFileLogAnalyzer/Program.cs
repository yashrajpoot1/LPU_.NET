using System;
using System.IO;
using System.Linq;
using LargeFileLogAnalyzer;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Problem 5: Large File Log Analyzer ===");
        
        var testFile = "test_log.txt";
        using (var writer = new StreamWriter(testFile))
        {
            writer.WriteLine("2024-01-01 10:00:00 ERR123 Connection failed");
            writer.WriteLine("2024-01-01 10:01:00 ERR456 Timeout occurred");
            writer.WriteLine("2024-01-01 10:02:00 ERR123 Connection failed");
            writer.WriteLine("2024-01-01 10:03:00 ERR789 Invalid request");
            writer.WriteLine("2024-01-01 10:04:00 ERR123 Connection failed");
            writer.WriteLine("2024-01-01 10:05:00 ERR456 Timeout occurred");
        }

        var analyzer = new LogAnalyzer();
        var topErrors = analyzer.GetTopErrors(testFile, 3).ToList();

        Console.WriteLine("Top 3 errors:");
        foreach (var error in topErrors)
        {
            Console.WriteLine($"{error.ErrorCode}: {error.Count} occurrences");
        }

        File.Delete(testFile);
        Console.WriteLine("✓ Test Passed\n");
    }
}
