namespace BankingTransactionRisk;

public abstract class Transaction
{
    public string TransactionId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
    public string AccountId { get; set; }
    public int RiskScore { get; protected set; }

    protected Transaction(string transactionId, decimal amount, string accountId)
    {
        if (amount < 0)
            throw new NegativeAmountException(amount);

        TransactionId = transactionId;
        Amount = amount;
        AccountId = accountId;
        Timestamp = DateTime.Now;
    }

    public abstract void CalculateRiskScore();
    public abstract void DisplayInfo();
}

public class OnlineTransaction : Transaction
{
    public string IpAddress { get; set; }
    public string DeviceId { get; set; }
    public string Location { get; set; }

    public OnlineTransaction(string transactionId, decimal amount, string accountId,
                            string ipAddress, string deviceId, string location)
        : base(transactionId, amount, accountId)
    {
        IpAddress = ipAddress;
        DeviceId = deviceId;
        Location = location;
        CalculateRiskScore();
    }

    public override void CalculateRiskScore()
    {
        // Strategy: Higher amount + foreign location = higher risk
        int score = 0;

        if (Amount > 10000) score += 50;
        else if (Amount > 5000) score += 30;
        else if (Amount > 1000) score += 10;

        if (Location.Contains("Foreign")) score += 30;
        if (!DeviceId.StartsWith("TRUSTED")) score += 20;

        RiskScore = Math.Min(score, 100);

        if (RiskScore > 80)
            throw new FraudDetectedException(TransactionId, RiskScore);
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"[Online] Transaction {TransactionId}");
        Console.WriteLine($"  Amount: ${Amount:F2}, Account: {AccountId}");
        Console.WriteLine($"  IP: {IpAddress}, Device: {DeviceId}");
        Console.WriteLine($"  Location: {Location}, Risk Score: {RiskScore}");
    }
}

public class WireTransfer : Transaction
{
    public string BeneficiaryBank { get; set; }
    public string BeneficiaryCountry { get; set; }
    public bool IsInternational { get; set; }

    public WireTransfer(string transactionId, decimal amount, string accountId,
                       string beneficiaryBank, string beneficiaryCountry, bool isInternational)
        : base(transactionId, amount, accountId)
    {
        BeneficiaryBank = beneficiaryBank;
        BeneficiaryCountry = beneficiaryCountry;
        IsInternational = isInternational;
        CalculateRiskScore();
    }

    public override void CalculateRiskScore()
    {
        // Strategy: International + high amount = higher risk
        int score = 0;

        if (Amount > 50000) score += 60;
        else if (Amount > 20000) score += 40;
        else if (Amount > 10000) score += 20;

        if (IsInternational) score += 25;
        if (BeneficiaryCountry.Contains("High-Risk")) score += 30;

        RiskScore = Math.Min(score, 100);

        if (RiskScore > 80)
            throw new FraudDetectedException(TransactionId, RiskScore);
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"[Wire Transfer] Transaction {TransactionId}");
        Console.WriteLine($"  Amount: ${Amount:F2}, Account: {AccountId}");
        Console.WriteLine($"  Beneficiary: {BeneficiaryBank}, {BeneficiaryCountry}");
        Console.WriteLine($"  International: {IsInternational}, Risk Score: {RiskScore}");
    }
}
