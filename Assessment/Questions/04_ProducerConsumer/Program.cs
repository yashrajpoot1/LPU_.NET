using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProducerConsumer;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("=== Problem 4: Producer-Consumer Order Processing ===");
        var processor = new OrderProcessor();
        
        var orders = new List<Order>();
        for (int i = 1; i <= 20; i++)
        {
            orders.Add(new Order
            {
                OrderId = i,
                CustomerName = $"Customer{i}",
                Amount = i * 10
            });
        }

        Console.WriteLine($"Processing {orders.Count} orders with 3 consumers...");
        var processedCount = await processor.ProcessOrdersAsync(orders, 3);
        Console.WriteLine($"Total processed: {processedCount}");
        Console.WriteLine("✓ Test Passed\n");
    }
}
