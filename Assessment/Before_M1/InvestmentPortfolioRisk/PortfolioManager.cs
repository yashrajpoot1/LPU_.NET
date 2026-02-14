namespace InvestmentPortfolioRisk;

public abstract class Asset
{
    public string InvestmentId { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    private int _riskRating;
    public int RiskRating
    {
        get => _riskRating;
        set
        {
            if (value < 1 || value > 5)
                throw new InvalidRiskRatingException(value);
            _riskRating = value;
        }
    }

    protected Asset(string investmentId, string name, decimal amount, int riskRating)
    {
        InvestmentId = investmentId;
        Name = name;
        Amount = amount;
        RiskRating = riskRating;
    }

    public abstract decimal CalculateReturn();
    public abstract string GetAssetType();
    public abstract void DisplayInfo();
}

public class Stock : Asset
{
    public decimal SharePrice { get; set; }

    public Stock(string investmentId, string name, decimal amount, int riskRating, decimal sharePrice)
        : base(investmentId, name, amount, riskRating)
    {
        SharePrice = sharePrice;
    }

    public override decimal CalculateReturn()
    {
        return Amount * 0.12m; // 12% expected return
    }

    public override string GetAssetType() => "Stock";

    public override void DisplayInfo()
    {
        Console.WriteLine($"[{GetAssetType()}] {Name} (ID: {InvestmentId})");
        Console.WriteLine($"  Amount: ${Amount:F2}, Share Price: ${SharePrice:F2}");
        Console.WriteLine($"  Risk: {RiskRating}/5, Expected Return: ${CalculateReturn():F2}");
    }
}

public class Bond : Asset
{
    public double InterestRate { get; set; }

    public Bond(string investmentId, string name, decimal amount, int riskRating, double interestRate)
        : base(investmentId, name, amount, riskRating)
    {
        InterestRate = interestRate;
    }

    public override decimal CalculateReturn()
    {
        return Amount * (decimal)InterestRate / 100;
    }

    public override string GetAssetType() => "Bond";

    public override void DisplayInfo()
    {
        Console.WriteLine($"[{GetAssetType()}] {Name} (ID: {InvestmentId})");
        Console.WriteLine($"  Amount: ${Amount:F2}, Interest Rate: {InterestRate}%");
        Console.WriteLine($"  Risk: {RiskRating}/5, Expected Return: ${CalculateReturn():F2}");
    }
}

public class MutualFund : Asset
{
    public int NumberOfFunds { get; set; }

    public MutualFund(string investmentId, string name, decimal amount, int riskRating, int numberOfFunds)
        : base(investmentId, name, amount, riskRating)
    {
        NumberOfFunds = numberOfFunds;
    }

    public override decimal CalculateReturn()
    {
        return Amount * 0.08m; // 8% expected return
    }

    public override string GetAssetType() => "Mutual Fund";

    public override void DisplayInfo()
    {
        Console.WriteLine($"[{GetAssetType()}] {Name} (ID: {InvestmentId})");
        Console.WriteLine($"  Amount: ${Amount:F2}, Funds: {NumberOfFunds}");
        Console.WriteLine($"  Risk: {RiskRating}/5, Expected Return: ${CalculateReturn():F2}");
    }
}

public class PortfolioManager
{
    private readonly SortedDictionary<int, List<Asset>> _investments = new();
    private readonly int _maxInvestments;
    private readonly decimal _totalLimit;
    private decimal _currentTotal = 0;

    public PortfolioManager(int maxInvestments, decimal totalLimit)
    {
        _maxInvestments = maxInvestments;
        _totalLimit = totalLimit;
    }

    public void AddInvestment(Asset asset)
    {
        if (_currentTotal + asset.Amount > _totalLimit)
            throw new InvestmentLimitExceededException(_currentTotal + asset.Amount, _totalLimit);

        if (!_investments.ContainsKey(asset.RiskRating))
            _investments[asset.RiskRating] = new List<Asset>();

        _investments[asset.RiskRating].Add(asset);
        _currentTotal += asset.Amount;

        Console.WriteLine($"✓ Added: {asset.Name} (Risk: {asset.RiskRating})");
    }

    public void DisplayPortfolio()
    {
        Console.WriteLine("\n========== PORTFOLIO (By Risk Rating) ==========");
        foreach (var kvp in _investments)
        {
            Console.WriteLine($"\n--- Risk Level {kvp.Key} ({kvp.Value.Count} investments) ---");
            foreach (var asset in kvp.Value)
            {
                asset.DisplayInfo();
            }
        }
    }

    public int GetTotalInvestments() => _investments.Values.Sum(list => list.Count);
    public decimal GetTotalValue() => _currentTotal;
}

public class InvestmentException : Exception
{
    public InvestmentException(string message) : base(message) { }
}

public class InvalidRiskRatingException : InvestmentException
{
    public InvalidRiskRatingException(int rating)
        : base($"Invalid risk rating: {rating}. Must be between 1 and 5") { }
}

public class InvestmentLimitExceededException : InvestmentException
{
    public InvestmentLimitExceededException(decimal total, decimal limit)
        : base($"Investment total ${total:F2} exceeds limit of ${limit:F2}") { }
}
