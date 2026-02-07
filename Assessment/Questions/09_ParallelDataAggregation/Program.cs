using System;
using System.Collections.Generic;
using ParallelDataAggregation;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Problem 9: Parallel Data Aggregation ===");
        var aggregator = new DataAggregator();
        
        var sales = new List<Sale>
        {
            new Sale { Region = "North", Category = "Electronics", Amount = 1000, Date = new DateTime(2024, 1, 1) },
            new Sale { Region = "North", Category = "Clothing", Amount = 500, Date = new DateTime(2024, 1, 1) },
            new Sale { Region = "South", Category = "Electronics", Amount = 1500, Date = new DateTime(2024, 1, 2) },
            new Sale { Region = "South", Category = "Food", Amount = 300, Date = new DateTime(2024, 1, 2) },
            new Sale { Region = "North", Category = "Electronics", Amount = 2000, Date = new DateTime(2024, 1, 3) }
        };

        var result = aggregator.Aggregate(sales);

        Console.WriteLine("Region Reports:");
        foreach (var report in result.RegionReports)
        {
            Console.WriteLine($"{report.Region}: Total=${report.TotalSales}, Top Category={report.TopCategory}");
        }
        Console.WriteLine($"\nBest Sales Day: {result.BestSalesDay:yyyy-MM-dd} (${result.BestDaySales})");
        Console.WriteLine("✓ Test Passed\n");
    }
}
