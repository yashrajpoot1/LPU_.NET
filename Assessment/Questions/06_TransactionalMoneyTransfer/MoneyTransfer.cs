namespace TransactionalMoneyTransfer
{
    public class Account
    {
        public string AccountId { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }

    public class TransferResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string AuditLogId { get; set; } = string.Empty;
    }

    public class AuditLog
    {
        public string LogId { get; set; } = Guid.NewGuid().ToString();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string FromAccount { get; set; } = string.Empty;
        public string ToAccount { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class MoneyTransferService
    {
        private readonly Dictionary<string, Account> _accounts = new();
        private readonly List<AuditLog> _auditLogs = new();
        private readonly object _lock = new();

        public void AddAccount(Account account)
        {
            _accounts[account.AccountId] = account;
        }

        public TransferResult Transfer(string fromAcc, string toAcc, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive", nameof(amount));

            if (fromAcc == toAcc)
                throw new ArgumentException("Cannot transfer to same account");

            lock (_lock)
            {
                if (!_accounts.ContainsKey(fromAcc))
                    throw new ArgumentException($"Account {fromAcc} not found");

                if (!_accounts.ContainsKey(toAcc))
                    throw new ArgumentException($"Account {toAcc} not found");

                var fromAccount = _accounts[fromAcc];
                var toAccount = _accounts[toAcc];

                if (fromAccount.Balance < amount)
                {
                    var auditLog = CreateAuditLog(fromAcc, toAcc, amount, false, "Insufficient balance");
                    return new TransferResult 
                    { 
                        Success = false, 
                        Message = "Insufficient balance",
                        AuditLogId = auditLog.LogId
                    };
                }

                try
                {
                    fromAccount.Balance -= amount;
                    
                    try
                    {
                        toAccount.Balance += amount;
                        var auditLog = CreateAuditLog(fromAcc, toAcc, amount, true, "Transfer successful");
                        return new TransferResult 
                        { 
                            Success = true, 
                            Message = "Transfer successful",
                            AuditLogId = auditLog.LogId
                        };
                    }
                    catch
                    {
                        fromAccount.Balance += amount;
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    var auditLog = CreateAuditLog(fromAcc, toAcc, amount, false, $"Transfer failed: {ex.Message}");
                    return new TransferResult 
                    { 
                        Success = false, 
                        Message = $"Transfer failed: {ex.Message}",
                        AuditLogId = auditLog.LogId
                    };
                }
            }
        }

        private AuditLog CreateAuditLog(string fromAcc, string toAcc, decimal amount, bool success, string message)
        {
            var log = new AuditLog
            {
                FromAccount = fromAcc,
                ToAccount = toAcc,
                Amount = amount,
                Success = success,
                Message = message
            };
            _auditLogs.Add(log);
            return log;
        }

        public List<AuditLog> GetAuditLogs() => _auditLogs;
    }
}
