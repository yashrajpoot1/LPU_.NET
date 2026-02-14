namespace BankingTransactionRisk;

public class TransactionException : Exception
{
    public TransactionException(string message) : base(message) { }
}

public class FraudDetectedException : TransactionException
{
    public string TransactionId { get; }
    public int RiskScore { get; }

    public FraudDetectedException(string transactionId, int riskScore)
        : base($"Fraud detected! Transaction {transactionId} has risk score {riskScore}")
    {
        TransactionId = transactionId;
        RiskScore = riskScore;
    }
}

public class NegativeAmountException : TransactionException
{
    public decimal Amount { get; }

    public NegativeAmountException(decimal amount)
        : base($"Transaction amount cannot be negative: ${amount}")
    {
        Amount = amount;
    }
}

public class TransactionLimitExceededException : TransactionException
{
    public decimal Amount { get; }
    public decimal Limit { get; }

    public TransactionLimitExceededException(decimal amount, decimal limit)
        : base($"Transaction amount ${amount:F2} exceeds limit of ${limit:F2}")
    {
        Amount = amount;
        Limit = limit;
    }
}
