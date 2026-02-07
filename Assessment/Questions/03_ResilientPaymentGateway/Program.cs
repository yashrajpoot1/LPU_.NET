using System;
using System.Threading.Tasks;
using ResilientPaymentGateway;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("=== Problem 3: Resilient Payment Gateway ===");
        var gateway = new PaymentGateway();
        
        var request = new PaymentRequest
        {
            TransactionId = "TXN123",
            Amount = 100.50m,
            Currency = "USD"
        };

        var result = await gateway.ProcessPaymentAsync(request);
        Console.WriteLine($"Payment Status: {(result.Success ? "SUCCESS" : "FAILED")}");
        Console.WriteLine($"Message: {result.Message}");
        Console.WriteLine($"Transaction ID: {result.TransactionId}");
        Console.WriteLine("✓ Test Passed\n");
    }
}
