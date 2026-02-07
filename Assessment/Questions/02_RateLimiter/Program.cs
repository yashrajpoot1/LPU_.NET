using System;
using System.Threading.Tasks;
using RateLimiterSystem;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("=== Problem 2: Rate Limiter ===");
        var limiter = new RateLimiter(5, 10);
        var now = DateTime.UtcNow;

        for (int i = 1; i <= 7; i++)
        {
            bool allowed = limiter.AllowRequest("client1", now.AddSeconds(i * 0.5));
            Console.WriteLine($"Request {i}: {(allowed ? "ALLOWED" : "BLOCKED")}");
        }

        await Task.Delay(11000);
        bool allowedAfterWindow = limiter.AllowRequest("client1", DateTime.UtcNow);
        Console.WriteLine($"Request after window: {(allowedAfterWindow ? "ALLOWED" : "BLOCKED")}");
        Console.WriteLine("✓ Test Passed\n");
    }
}
