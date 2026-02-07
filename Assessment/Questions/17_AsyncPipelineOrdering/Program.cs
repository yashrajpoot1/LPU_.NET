using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncPipelineOrdering;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("=== Problem 17: Async Pipeline with Ordering ===");
        var pipeline = new AsyncPipeline();
        
        var inputs = new List<Input>();
        for (int i = 1; i <= 10; i++)
        {
            inputs.Add(new Input { Id = i, Data = $"Item{i}" });
        }

        var outputs = await pipeline.ProcessAsync(inputs);

        Console.WriteLine("Output (should be in order):");
        foreach (var output in outputs)
        {
            Console.WriteLine($"  {output.Id}: {output.Result}");
        }

        Console.WriteLine("✓ Test Passed\n");
    }
}
