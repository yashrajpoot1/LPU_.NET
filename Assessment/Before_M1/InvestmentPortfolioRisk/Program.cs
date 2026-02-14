namespace InvestmentPortfolioRisk;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║  Investment Portfolio Risk Categorization System     ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

        var portfolio = new PortfolioManager(maxInvestments: 10, totalLimit: 100000m);

        try
        {
            portfolio.AddInvestment(new Stock("INV001", "Apple Inc", 5000m, 3, 120m));
            portfolio.AddInvestment(new Bond("INV002", "US Treasury", 10000m, 1, 3.5));
            portfolio.AddInvestment(new MutualFund("INV003", "Vanguard 500", 7500m, 2, 15));
            portfolio.AddInvestment(new Stock("INV004", "Tesla", 8000m, 4, 250m));
            portfolio.AddInvestment(new Bond("INV005", "Corporate Bond", 6000m, 2, 5.0));

            portfolio.DisplayPortfolio();

            // Test invalid risk rating
            Console.WriteLine("\n========== TESTING INVALID RISK RATING ==========");
            try
            {
                portfolio.AddInvestment(new Stock("INV006", "Invalid", 1000m, 6, 50m));
            }
            catch (InvalidRiskRatingException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            // Test investment limit
            Console.WriteLine("\n========== TESTING INVESTMENT LIMIT ==========");
            try
            {
                portfolio.AddInvestment(new Stock("INV007", "Huge Investment", 80000m, 5, 1000m));
            }
            catch (InvestmentLimitExceededException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            Console.WriteLine($"\n✅ Total Investments: {portfolio.GetTotalInvestments()}");
            Console.WriteLine($"✅ Portfolio Value: ${portfolio.GetTotalValue():F2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
