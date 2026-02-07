using System.Text.RegularExpressions;

namespace LargeFileLogAnalyzer
{
    public class ErrorSummary
    {
        public string ErrorCode { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class LogAnalyzer
    {
        public IEnumerable<ErrorSummary> GetTopErrors(string filePath, int topN)
        {
            var errorCounts = new Dictionary<string, int>();
            var errorPattern = new Regex(@"ERR\d+", RegexOptions.Compiled);

            using (var reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var matches = errorPattern.Matches(line);
                    foreach (Match match in matches)
                    {
                        var errorCode = match.Value;
                        errorCounts[errorCode] = errorCounts.GetValueOrDefault(errorCode, 0) + 1;
                    }
                }
            }

            return errorCounts
                .OrderByDescending(kvp => kvp.Value)
                .Take(topN)
                .Select(kvp => new ErrorSummary { ErrorCode = kvp.Key, Count = kvp.Value });
        }
    }
}
