namespace ECommerceFlashSale;

public class AuctionEngine
{
    private readonly SortedDictionary<decimal, List<Bid>> _bids = new(Comparer<decimal>.Create((a, b) => b.CompareTo(a)));
    private readonly decimal _minBid;
    private bool _isClosed = false;

    public AuctionEngine(decimal minBid)
    {
        _minBid = minBid;
    }

    public void PlaceBid(Bid bid)
    {
        if (_isClosed)
            throw new AuctionClosedException();

        if (!bid.Validate(_minBid))
            throw new BidTooLowException(bid.Amount, _minBid);

        if (!_bids.ContainsKey(bid.Amount))
            _bids[bid.Amount] = new List<Bid>();

        _bids[bid.Amount].Add(bid);
        Console.WriteLine($"✓ Bid placed: {bid.Bidder.Name} - ${bid.Amount:F2}");
    }

    public Bid? DetermineWinner()
    {
        foreach (var kvp in _bids)
        {
            if (kvp.Value.Count > 0)
                return kvp.Value[0];
        }
        return null;
    }

    public void DisplayBids()
    {
        Console.WriteLine("\n========== BIDS (Highest First) ==========");
        foreach (var kvp in _bids)
        {
            Console.WriteLine($"\n--- Bid Amount: ${kvp.Key:F2} ---");
            foreach (var bid in kvp.Value)
            {
                bid.DisplayInfo();
            }
        }
    }

    public void CloseAuction()
    {
        _isClosed = true;
        Console.WriteLine("✓ Auction closed");
    }

    public int GetTotalBids() => _bids.Values.Sum(list => list.Count);
}

public abstract class User
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }

    protected User(string userId, string name, int age)
    {
        UserId = userId;
        Name = name;
        Age = age;
    }
}

public class Buyer : User
{
    public Buyer(string userId, string name, int age) : base(userId, name, age) { }
}

public class Bid
{
    public string BidId { get; set; }
    public Buyer Bidder { get; set; }
    public decimal Amount { get; set; }
    public string ProductName { get; set; }
    public DateTime Timestamp { get; set; }

    public Bid(string bidId, Buyer bidder, decimal amount, string productName)
    {
        BidId = bidId;
        Bidder = bidder;
        Amount = amount;
        ProductName = productName;
        Timestamp = DateTime.Now;
    }

    public bool Validate(decimal minBid)
    {
        return Amount >= minBid;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Bid {BidId}: {Bidder.Name} - ${Amount:F2}");
        Console.WriteLine($"  Product: {ProductName}, Time: {Timestamp:HH:mm:ss}");
    }
}

public class AuctionException : Exception
{
    public AuctionException(string message) : base(message) { }
}

public class BidTooLowException : AuctionException
{
    public BidTooLowException(decimal bidAmount, decimal minBid)
        : base($"Bid ${bidAmount:F2} is below minimum ${minBid:F2}") { }
}

public class AuctionClosedException : AuctionException
{
    public AuctionClosedException() : base("Auction is closed. No more bids accepted") { }
}
