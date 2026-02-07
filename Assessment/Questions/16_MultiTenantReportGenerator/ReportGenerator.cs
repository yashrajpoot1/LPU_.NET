namespace MultiTenantReportGenerator
{
    public class Transaction
    {
        public string TenantId { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class TenantReport
    {
        public string TenantId { get; set; } = string.Empty;
        public decimal TotalCredits { get; set; }
        public decimal TotalDebits { get; set; }
        public int PeakTransactionHour { get; set; }
        public bool IsSuspicious { get; set; }
    }

    public class ReportGenerator
    {
        public List<TenantReport> GenerateReports(List<Transaction> transactions)
        {
            return transactions
                .GroupBy(t => t.TenantId)
                .Select(g => GenerateTenantReport(g.Key, g.ToList()))
                .ToList();
        }

        private TenantReport GenerateTenantReport(string tenantId, List<Transaction> transactions)
        {
            var credits = transactions.Where(t => t.Type.Equals("Credit", StringComparison.OrdinalIgnoreCase))
                .Sum(t => t.Amount);

            var debits = transactions.Where(t => t.Type.Equals("Debit", StringComparison.OrdinalIgnoreCase))
                .Sum(t => t.Amount);

            var peakHour = transactions
                .GroupBy(t => t.Timestamp.Hour)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? 0;

            var isSuspicious = CheckSuspiciousActivity(transactions);

            return new TenantReport
            {
                TenantId = tenantId,
                TotalCredits = credits,
                TotalDebits = debits,
                PeakTransactionHour = peakHour,
                IsSuspicious = isSuspicious
            };
        }

        private bool CheckSuspiciousActivity(List<Transaction> transactions)
        {
            var debits = transactions
                .Where(t => t.Type.Equals("Debit", StringComparison.OrdinalIgnoreCase))
                .OrderBy(t => t.Timestamp)
                .ToList();

            for (int i = 0; i < debits.Count - 2; i++)
            {
                var window = debits.Skip(i).Take(3).ToList();
                if (window.Count == 3)
                {
                    var timeSpan = window.Last().Timestamp - window.First().Timestamp;
                    if (timeSpan <= TimeSpan.FromMinutes(5))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
