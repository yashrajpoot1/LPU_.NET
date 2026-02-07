namespace CsvImportPartialSuccess
{
    public class Product
    {
        public string ProductId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    public class FailedRow
    {
        public int RowNumber { get; set; }
        public string Reason { get; set; } = string.Empty;
    }

    public class ImportResult
    {
        public int InsertedCount { get; set; }
        public List<FailedRow> FailedRows { get; set; } = new();
    }

    public class CsvProductImporter
    {
        private readonly List<Product> _products = new();

        public ImportResult ImportProducts(string csvPath)
        {
            var result = new ImportResult();
            int rowNumber = 0;

            using (var reader = new StreamReader(csvPath))
            {
                string? line = reader.ReadLine();
                rowNumber++;

                while ((line = reader.ReadLine()) != null)
                {
                    rowNumber++;
                    var parts = line.Split(',');

                    if (parts.Length != 3)
                    {
                        result.FailedRows.Add(new FailedRow
                        {
                            RowNumber = rowNumber,
                            Reason = "Invalid column count"
                        });
                        continue;
                    }

                    try
                    {
                        var product = new Product
                        {
                            ProductId = parts[0].Trim(),
                            Name = parts[1].Trim(),
                            Price = decimal.Parse(parts[2].Trim())
                        };

                        if (string.IsNullOrWhiteSpace(product.ProductId))
                        {
                            result.FailedRows.Add(new FailedRow
                            {
                                RowNumber = rowNumber,
                                Reason = "ProductId is required"
                            });
                            continue;
                        }

                        _products.Add(product);
                        result.InsertedCount++;
                    }
                    catch (Exception ex)
                    {
                        result.FailedRows.Add(new FailedRow
                        {
                            RowNumber = rowNumber,
                            Reason = ex.Message
                        });
                    }
                }
            }

            return result;
        }
    }
}
