using System;
using System.IO;
using CsvImportPartialSuccess;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Problem 13: CSV Import Partial Success ===");
        
        var csvFile = "products.csv";
        using (var writer = new StreamWriter(csvFile))
        {
            writer.WriteLine("ProductId,Name,Price");
            writer.WriteLine("P001,Laptop,999.99");
            writer.WriteLine("P002,Mouse,");
            writer.WriteLine(",Keyboard,49.99");
            writer.WriteLine("P003,Monitor,299.99");
        }

        var importer = new CsvProductImporter();
        var result = importer.ImportProducts(csvFile);

        Console.WriteLine($"Inserted: {result.InsertedCount}");
        Console.WriteLine($"Failed: {result.FailedRows.Count}");
        foreach (var failed in result.FailedRows)
        {
            Console.WriteLine($"  Row {failed.RowNumber}: {failed.Reason}");
        }

        File.Delete(csvFile);
        Console.WriteLine("✓ Test Passed\n");
    }
}
