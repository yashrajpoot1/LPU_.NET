namespace ParallelDataAggregation
{
    public class Sale
    {
        public string Region { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }

    public class RegionReport
    {
        public string Region { get; set; } = string.Empty;
        public decimal TotalSales { get; set; }
        public string TopCategory { get; set; } = string.Empty;
    }

    public class AggregationResult
    {
        public List<RegionReport> RegionReports { get; set; } = new();
        public DateTime BestSalesDay { get; set; }
        public decimal BestDaySales { get; set; }
    }

    public class DataAggregator
    {
        public AggregationResult Aggregate(List<Sale> sales)
        {
            var regionReports = sales
                .AsParallel()
                .GroupBy(s => s.Region)
                .Select(g => new RegionReport
                {
                    Region = g.Key,
                    TotalSales = g.Sum(s => s.Amount),
                    TopCategory = g.GroupBy(s => s.Category)
                        .OrderByDescending(cg => cg.Sum(s => s.Amount))
                        .First()
                        .Key
                })
                .OrderBy(r => r.Region)
                .ToList();

            var bestDay = sales
                .AsParallel()
                .GroupBy(s => s.Date.Date)
                .Select(g => new { Date = g.Key, Total = g.Sum(s => s.Amount) })
                .OrderByDescending(x => x.Total)
                .First();

            return new AggregationResult
            {
                RegionReports = regionReports,
                BestSalesDay = bestDay.Date,
                BestDaySales = bestDay.Total
            };
        }
    }
}
