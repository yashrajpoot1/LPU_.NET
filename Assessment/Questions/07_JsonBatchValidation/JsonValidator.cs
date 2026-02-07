using System.Text.Json;
using System.Text.RegularExpressions;

namespace JsonBatchValidation
{
    public class CustomerApplication
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }
        public string? PAN { get; set; }
    }

    public class ValidationError
    {
        public int RecordIndex { get; set; }
        public List<string> Errors { get; set; } = new();
    }

    public class ValidationReport
    {
        public int TotalValid { get; set; }
        public int TotalInvalid { get; set; }
        public List<ValidationError> ValidationErrors { get; set; } = new();
    }

    public class JsonBatchValidator
    {
        private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
        private static readonly Regex PanRegex = new(@"^[A-Z]{5}[0-9]{4}[A-Z]$", RegexOptions.Compiled);

        public ValidationReport ValidateBatch(List<string> jsonPayloads)
        {
            var report = new ValidationReport();

            for (int i = 0; i < jsonPayloads.Count; i++)
            {
                var errors = new List<string>();

                try
                {
                    var customer = JsonSerializer.Deserialize<CustomerApplication>(jsonPayloads[i]);

                    if (customer == null)
                    {
                        errors.Add("Invalid JSON format");
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(customer.Name))
                            errors.Add("Name is mandatory");

                        if (string.IsNullOrWhiteSpace(customer.Email))
                            errors.Add("Email is mandatory");
                        else if (!EmailRegex.IsMatch(customer.Email))
                            errors.Add("Invalid email format");

                        if (customer.Age < 18 || customer.Age > 60)
                            errors.Add("Age must be between 18 and 60");

                        if (string.IsNullOrWhiteSpace(customer.PAN))
                            errors.Add("PAN is mandatory");
                        else if (!PanRegex.IsMatch(customer.PAN))
                            errors.Add("Invalid PAN format (expected: AAAAA9999A)");
                    }
                }
                catch (JsonException)
                {
                    errors.Add("Invalid JSON format");
                }

                if (errors.Any())
                {
                    report.TotalInvalid++;
                    report.ValidationErrors.Add(new ValidationError
                    {
                        RecordIndex = i,
                        Errors = errors
                    });
                }
                else
                {
                    report.TotalValid++;
                }
            }

            return report;
        }
    }
}
