namespace DeadlockFreeBankLocks
{
    public class Account
    {
        public string AccountId { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public object Lock { get; } = new object();
    }

    public class BankTransferService
    {
        public void SafeTransfer(Account a, Account b, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive");

            var first = string.Compare(a.AccountId, b.AccountId, StringComparison.Ordinal) < 0 ? a : b;
            var second = first == a ? b : a;

            lock (first.Lock)
            {
                lock (second.Lock)
                {
                    if (a.Balance < amount)
                        throw new InvalidOperationException("Insufficient balance");

                    a.Balance -= amount;
                    b.Balance += amount;
                }
            }
        }
    }
}
