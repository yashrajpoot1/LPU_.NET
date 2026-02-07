namespace FraudPatternDetection
{
    public class Transaction
    {
        public string TransactionId { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string City { get; set; } = string.Empty;
    }

    public class FraudAlert
    {
        public string CardNumber { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public List<string> TransactionIds { get; set; } = new();
    }

    public class FraudDetector
    {
        public List<FraudAlert> DetectFraud(List<Transaction> transactions)
        {
            var alerts = new List<FraudAlert>();

            var byCard = transactions.GroupBy(t => t.CardNumber);

            foreach (var cardGroup in byCard)
            {
                var cardTxns = cardGroup.OrderBy(t => t.Timestamp).ToList();

                var highValueAlert = DetectHighValuePattern(cardTxns);
                if (highValueAlert != null)
                    alerts.Add(highValueAlert);

                var locationAlert = DetectLocationPattern(cardTxns);
                if (locationAlert != null)
                    alerts.Add(locationAlert);
            }

            return alerts;
        }

        private FraudAlert? DetectHighValuePattern(List<Transaction> transactions)
        {
            for (int i = 0; i < transactions.Count - 2; i++)
            {
                var window = transactions.Skip(i).Take(3).ToList();
                if (window.Count == 3)
                {
                    var timeSpan = window.Last().Timestamp - window.First().Timestamp;
                    var highValueCount = window.Count(t => t.Amount > 50000);

                    if (timeSpan <= TimeSpan.FromMinutes(2) && highValueCount >= 3)
                    {
                        return new FraudAlert
                        {
                            CardNumber = transactions[0].CardNumber,
                            Reason = "3+ transactions > 50,000 within 2 minutes",
                            TransactionIds = window.Select(t => t.TransactionId).ToList()
                        };
                    }
                }
            }

            return null;
        }

        private FraudAlert? DetectLocationPattern(List<Transaction> transactions)
        {
            for (int i = 0; i < transactions.Count - 1; i++)
            {
                for (int j = i + 1; j < transactions.Count; j++)
                {
                    var timeSpan = transactions[j].Timestamp - transactions[i].Timestamp;
                    
                    if (timeSpan <= TimeSpan.FromMinutes(10) && 
                        transactions[i].City != transactions[j].City)
                    {
                        return new FraudAlert
                        {
                            CardNumber = transactions[0].CardNumber,
                            Reason = "Same card used in 2 cities within 10 minutes",
                            TransactionIds = new List<string> 
                            { 
                                transactions[i].TransactionId, 
                                transactions[j].TransactionId 
                            }
                        };
                    }
                }
            }

            return null;
        }
    }
}
