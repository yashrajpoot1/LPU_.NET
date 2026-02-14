namespace BankingTransactionRisk;

public class RiskMonitor
{
    // Sorted by risk score (descending) - higher risk first
    private readonly SortedDictionary<int, List<Transaction>> _transactions = new(Comparer<int>.Create((a, b) => b.CompareTo(a)));
    private readonly decimal _transactionLimit;

    public RiskMonitor(decimal transactionLimit)
    {
        _transactionLimit = transactionLimit;
    }

    public void AddTransaction(Transaction transaction)
    {
        if (transaction.Amount > _transactionLimit)
            throw new TransactionLimitExceededException(transaction.Amount, _transactionLimit);

        if (!_transactions.ContainsKey(transaction.RiskScore))
            _transactions[transaction.RiskScore] = new List<Transaction>();

        _transactions[transaction.RiskScore].Add(transaction);
        Console.WriteLine($"✓ Transaction {transaction.TransactionId} added (Risk: {transaction.RiskScore})");
    }

    public List<Transaction> GetHighRiskTransactions(int minRiskScore)
    {
        var result = new List<Transaction>();
        foreach (var kvp in _transactions)
        {
            if (kvp.Key >= minRiskScore)
                result.AddRange(kvp.Value);
        }
        return result;
    }

    public void DisplayTransactions()
    {
        Console.WriteLine("\n========== TRANSACTIONS (Sorted by Risk - Descending) ==========");
        foreach (var kvp in _transactions)
        {
            Console.WriteLine($"\n--- Risk Score: {kvp.Key} ({kvp.Value.Count} transactions) ---");
            foreach (var txn in kvp.Value)
            {
                txn.DisplayInfo();
            }
        }
    }

    public int GetTotalTransactions()
    {
        return _transactions.Values.Sum(list => list.Count);
    }
}
