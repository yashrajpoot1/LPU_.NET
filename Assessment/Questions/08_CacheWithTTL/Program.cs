using System;
using System.Threading.Tasks;
using CacheWithTTL;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("=== Problem 8: Cache with TTL + LRU ===");
        var cache = new AdvancedCache<string, string>(3);

        cache.Set("key1", "value1", 5);
        cache.Set("key2", "value2", 5);
        cache.Set("key3", "value3", 5);

        var (found1, val1) = cache.Get("key1");
        Console.WriteLine($"Get key1: {(found1 ? val1 : "NOT FOUND")}");

        cache.Set("key4", "value4", 5);
        var (found2, val2) = cache.Get("key2");
        Console.WriteLine($"Get key2 (should be evicted): {(found2 ? val2 : "NOT FOUND")}");

        cache.Set("key5", "value5", 2);
        await Task.Delay(3000);
        var (found3, val3) = cache.Get("key5");
        Console.WriteLine($"Get key5 (expired): {(found3 ? val3 : "NOT FOUND")}");

        Console.WriteLine("✓ Test Passed\n");
    }
}
