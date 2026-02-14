namespace ECommerceFlashSale;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║       E-Commerce Flash Sale Bidding Engine           ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

        var auction = new AuctionEngine(minBid: 100m);

        try
        {
            auction.PlaceBid(new Bid("BID001", new Buyer("B001", "Alice", 30), 500m, "iPhone 15"));
            auction.PlaceBid(new Bid("BID002", new Buyer("B002", "Bob", 35), 750m, "iPhone 15"));
            auction.PlaceBid(new Bid("BID003", new Buyer("B003", "Charlie", 28), 600m, "iPhone 15"));
            auction.PlaceBid(new Bid("BID004", new Buyer("B004", "Diana", 32), 800m, "iPhone 15"));

            auction.DisplayBids();

            // Test bid too low
            Console.WriteLine("\n========== TESTING BID TOO LOW ==========");
            try
            {
                auction.PlaceBid(new Bid("BID005", new Buyer("B005", "Eve", 25), 50m, "iPhone 15"));
            }
            catch (BidTooLowException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            // Determine winner
            Console.WriteLine("\n========== DETERMINING WINNER ==========");
            var winner = auction.DetermineWinner();
            if (winner != null)
            {
                Console.WriteLine($"🏆 Winner: {winner.Bidder.Name} with bid of ${winner.Amount:F2}");
            }

            // Test auction closed
            Console.WriteLine("\n========== TESTING AUCTION CLOSED ==========");
            auction.CloseAuction();
            try
            {
                auction.PlaceBid(new Bid("BID006", new Buyer("B006", "Frank", 40), 900m, "iPhone 15"));
            }
            catch (AuctionClosedException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            Console.WriteLine($"\n✅ Total Bids Placed: {auction.GetTotalBids()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
