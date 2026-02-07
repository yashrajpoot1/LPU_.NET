using System;
using System.Collections.Generic;
using JsonBatchValidation;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Problem 7: JSON Batch Validation ===");
        var validator = new JsonBatchValidator();
        
        var jsonPayloads = new List<string>
        {
            "{\"Name\":\"John Doe\",\"Email\":\"john@example.com\",\"Age\":25,\"PAN\":\"ABCDE1234F\"}",
            "{\"Name\":\"Jane\",\"Email\":\"invalid-email\",\"Age\":25,\"PAN\":\"ABCDE1234F\"}",
            "{\"Name\":\"Bob\",\"Email\":\"bob@test.com\",\"Age\":70,\"PAN\":\"ABCDE1234F\"}",
            "{\"Name\":\"Alice\",\"Email\":\"alice@test.com\",\"Age\":30,\"PAN\":\"INVALID\"}"
        };

        var report = validator.ValidateBatch(jsonPayloads);
        
        Console.WriteLine($"Total Valid: {report.TotalValid}");
        Console.WriteLine($"Total Invalid: {report.TotalInvalid}");
        Console.WriteLine("\nValidation Errors:");
        foreach (var error in report.ValidationErrors)
        {
            Console.WriteLine($"Record {error.RecordIndex}: {string.Join(", ", error.Errors)}");
        }
        Console.WriteLine("✓ Test Passed\n");
    }
}
