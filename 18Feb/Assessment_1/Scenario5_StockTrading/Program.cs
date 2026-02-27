using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingPortfolio
{
    // Custom Exception
    public class InvalidTradeException : Exception
    {
        public InvalidTradeException(string message) : base(message) { }
    }

    // Events & Delegates
    public delegate void StockPriceChangedHandler(string stockSymbol, decimal oldPrice, decimal newPrice);

    // Risk Calculation Strategy Interface
    public interface IRiskCalculationStrategy
    {
        string CalculateRisk(decimal totalInvestment, decimal currentValue);
    }

    public class ConservativeRiskStrategy : IRiskCalculationStrategy
    {
        public string CalculateRisk(decimal totalInvestment, decimal currentValue)
        {
            decimal returnPercentage = ((currentValue - totalInvestment) / totalInvestment) * 100;
            if (returnPercentage < -5) return "High Risk";
            if (returnPercentage < 5) return "Medium Risk";
            return "Low Risk";
        }
    }

    public class AggressiveRiskStrategy : IRiskCalculationStrategy
    {
        public string CalculateRisk(decimal totalInvestment, decimal currentValue)
        {
            decimal returnPercentage = ((currentValue - totalInvestment) / totalInvestment) * 100;
            if (returnPercentage < -15) return "High Risk";
            if (returnPercentage < 10) return "Medium Risk";
            return "Low Risk";
        }
    }

    // Entities
    public class Stock
    {
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public decimal CurrentPrice { get; set; }
        public long VolumeTraded { get; set; }

        public event StockPriceChangedHandler PriceChanged;

        public Stock(string symbol, string companyName, decimal currentPrice)
        {
            Symbol = symbol;
            CompanyName = companyName;
            CurrentPrice = currentPrice;
            VolumeTraded = 0;
        }

        public void UpdatePrice(decimal newPrice)
        {
            decimal oldPrice = CurrentPrice;
            CurrentPrice = newPrice;
            PriceChanged?.Invoke(Symbol, oldPrice, newPrice);
        }
    }

    public enum TransactionType
    {
        Buy,
        Sell
    }

    public class Transaction
    {
        public int Id { get; set; }
        public Stock Stock { get; set; }
        public TransactionType Type { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerShare { get; set; }
        public DateTime TransactionDate { get; set; }

        public Transaction(int id, Stock stock, TransactionType type, int quantity, decimal pricePerShare, DateTime date)
        {
            Id = id;
            Stock = stock;
            Type = type;
            Quantity = quantity;
            PricePerShare = pricePerShare;
            TransactionDate = date;
        }

        public decimal TotalAmount => Quantity * PricePerShare;
    }

    public class Portfolio
    {
        public Investor Investor { get; set; }
        public Dictionary<string, int> Holdings { get; set; }
        public IRiskCalculationStrategy RiskStrategy { get; set; }

        public Portfolio(Investor investor, IRiskCalculationStrategy riskStrategy)
        {
            Investor = investor;
            Holdings = new Dictionary<string, int>();
            RiskStrategy = riskStrategy;
        }

        public void AddHolding(string stockSymbol, int quantity)
        {
            if (Holdings.ContainsKey(stockSymbol))
                Holdings[stockSymbol] += quantity;
            else
                Holdings[stockSymbol] = quantity;
        }

        public void RemoveHolding(string stockSymbol, int quantity)
        {
            if (!Holdings.ContainsKey(stockSymbol) || Holdings[stockSymbol] < quantity)
                throw new InvalidTradeException($"Insufficient shares of {stockSymbol}");

            Holdings[stockSymbol] -= quantity;
            if (Holdings[stockSymbol] == 0)
                Holdings.Remove(stockSymbol);
        }
    }

    public class Investor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Portfolio Portfolio { get; set; }

        public Investor(int id, string name, string email, IRiskCalculationStrategy riskStrategy)
        {
            Id = id;
            Name = name;
            Email = email;
            Portfolio = new Portfolio(this, riskStrategy);
        }
    }

    class Program
    {
        static List<Investor> investors = new List<Investor>();
        static List<Stock> stocks = new List<Stock>();
        static List<Transaction> transactions = new List<Transaction>();
        static Dictionary<string, List<Transaction>> transactionsByStock = new Dictionary<string, List<Transaction>>();
        static int transactionIdCounter = 1;

        static void Main(string[] args)
        {
            InitializeSampleData();

            while (true)
            {
                Console.WriteLine("\n╔════════════════════════════════════════╗");
                Console.WriteLine("║   STOCK TRADING PORTFOLIO SYSTEM       ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                Console.WriteLine("1. View Stocks");
                Console.WriteLine("2. View Investors");
                Console.WriteLine("3. Buy Stock");
                Console.WriteLine("4. Sell Stock");
                Console.WriteLine("5. Update Stock Price");
                Console.WriteLine("6. View Portfolio");
                Console.WriteLine("7. View Transactions");
                Console.WriteLine("8. Calculate Portfolio Risk");
                Console.WriteLine("9. LINQ Reports");
                Console.WriteLine("10. Exit");
                Console.Write("\nEnter choice: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": ViewStocks(); break;
                        case "2": ViewInvestors(); break;
                        case "3": BuyStock(); break;
                        case "4": SellStock(); break;
                        case "5": UpdateStockPrice(); break;
                        case "6": ViewPortfolio(); break;
                        case "7": ViewTransactions(); break;
                        case "8": CalculatePortfolioRisk(); break;
                        case "9": ShowLinqReports(); break;
                        case "10": return;
                        default: Console.WriteLine("Invalid choice!"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Error: {ex.Message}");
                }
            }
        }

        static void InitializeSampleData()
        {
            // Stocks
            var stock1 = new Stock("RELIANCE", "Reliance Industries", 2500);
            var stock2 = new Stock("TCS", "Tata Consultancy Services", 3200);
            var stock3 = new Stock("INFY", "Infosys", 1450);
            var stock4 = new Stock("HDFC", "HDFC Bank", 1600);

            // Subscribe to price change events
            stock1.PriceChanged += OnStockPriceChanged;
            stock2.PriceChanged += OnStockPriceChanged;
            stock3.PriceChanged += OnStockPriceChanged;
            stock4.PriceChanged += OnStockPriceChanged;

            stocks.AddRange(new[] { stock1, stock2, stock3, stock4 });

            // Investors
            investors.Add(new Investor(1, "Rajesh Kumar", "rajesh@investor.com", new ConservativeRiskStrategy()));
            investors.Add(new Investor(2, "Priya Sharma", "priya@investor.com", new AggressiveRiskStrategy()));
            investors.Add(new Investor(3, "Amit Patel", "amit@investor.com", new ConservativeRiskStrategy()));

            // Sample Transactions
            var transaction1 = new Transaction(transactionIdCounter++, stock1, TransactionType.Buy, 50, 2400, DateTime.Now.AddDays(-30));
            transactions.Add(transaction1);
            investors[0].Portfolio.AddHolding(stock1.Symbol, 50);
            stock1.VolumeTraded += 50;
            AddToTransactionDict(stock1.Symbol, transaction1);

            var transaction2 = new Transaction(transactionIdCounter++, stock2, TransactionType.Buy, 100, 3100, DateTime.Now.AddDays(-25));
            transactions.Add(transaction2);
            investors[0].Portfolio.AddHolding(stock2.Symbol, 100);
            stock2.VolumeTraded += 100;
            AddToTransactionDict(stock2.Symbol, transaction2);

            var transaction3 = new Transaction(transactionIdCounter++, stock3, TransactionType.Buy, 200, 1400, DateTime.Now.AddDays(-20));
            transactions.Add(transaction3);
            investors[1].Portfolio.AddHolding(stock3.Symbol, 200);
            stock3.VolumeTraded += 200;
            AddToTransactionDict(stock3.Symbol, transaction3);
        }

        static void OnStockPriceChanged(string stockSymbol, decimal oldPrice, decimal newPrice)
        {
            Console.WriteLine($"\n🔔 ALERT: {stockSymbol} price changed from ₹{oldPrice} to ₹{newPrice}");
            decimal changePercent = ((newPrice - oldPrice) / oldPrice) * 100;
            Console.WriteLine($"   Change: {changePercent:F2}%");
        }

        static void AddToTransactionDict(string stockSymbol, Transaction transaction)
        {
            if (!transactionsByStock.ContainsKey(stockSymbol))
                transactionsByStock[stockSymbol] = new List<Transaction>();
            transactionsByStock[stockSymbol].Add(transaction);
        }

        static void ViewStocks()
        {
            Console.WriteLine("\n--- Available Stocks ---");
            Console.WriteLine($"{"Symbol",-15} {"Company",-35} {"Price",-12} {"Volume",-15}");
            Console.WriteLine(new string('-', 80));
            foreach (var stock in stocks)
            {
                Console.WriteLine($"{stock.Symbol,-15} {stock.CompanyName,-35} ₹{stock.CurrentPrice,-10} {stock.VolumeTraded,-15}");
            }
        }

        static void ViewInvestors()
        {
            Console.WriteLine("\n--- Investors List ---");
            Console.WriteLine($"{"ID",-5} {"Name",-25} {"Email",-30} {"Risk Strategy",-20}");
            Console.WriteLine(new string('-', 85));
            foreach (var investor in investors)
            {
                string strategy = investor.Portfolio.RiskStrategy.GetType().Name;
                Console.WriteLine($"{investor.Id,-5} {investor.Name,-25} {investor.Email,-30} {strategy,-20}");
            }
        }

        static void BuyStock()
        {
            Console.Write("\nEnter Investor ID: ");
            int investorId = int.Parse(Console.ReadLine());

            var investor = investors.FirstOrDefault(i => i.Id == investorId);
            if (investor == null)
            {
                Console.WriteLine("❌ Investor not found!");
                return;
            }

            ViewStocks();
            Console.Write("\nEnter Stock Symbol: ");
            string symbol = Console.ReadLine().ToUpper();

            var stock = stocks.FirstOrDefault(s => s.Symbol == symbol);
            if (stock == null)
            {
                Console.WriteLine("❌ Stock not found!");
                return;
            }

            Console.Write("Enter Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("Enter Transaction Date (dd-MM-yyyy) or press Enter for today: ");
            string dateInput = Console.ReadLine();
            DateTime transactionDate = string.IsNullOrWhiteSpace(dateInput) 
                ? DateTime.Now 
                : DateTime.ParseExact(dateInput, "dd-MM-yyyy", null);

            if (transactionDate > DateTime.Now)
                throw new InvalidTradeException("Transaction date cannot be in the future");

            var transaction = new Transaction(transactionIdCounter++, stock, TransactionType.Buy, quantity, stock.CurrentPrice, transactionDate);
            transactions.Add(transaction);
            investor.Portfolio.AddHolding(stock.Symbol, quantity);
            stock.VolumeTraded += quantity;
            AddToTransactionDict(stock.Symbol, transaction);

            Console.WriteLine($"✅ Bought {quantity} shares of {stock.Symbol} at ₹{stock.CurrentPrice} per share");
            Console.WriteLine($"   Total Amount: ₹{transaction.TotalAmount:F2}");
        }

        static void SellStock()
        {
            Console.Write("\nEnter Investor ID: ");
            int investorId = int.Parse(Console.ReadLine());

            var investor = investors.FirstOrDefault(i => i.Id == investorId);
            if (investor == null)
            {
                Console.WriteLine("❌ Investor not found!");
                return;
            }

            if (investor.Portfolio.Holdings.Count == 0)
            {
                Console.WriteLine("❌ No holdings to sell!");
                return;
            }

            Console.WriteLine("\n--- Your Holdings ---");
            foreach (var holding in investor.Portfolio.Holdings)
            {
                Console.WriteLine($"{holding.Key}: {holding.Value} shares");
            }

            Console.Write("\nEnter Stock Symbol to sell: ");
            string symbol = Console.ReadLine().ToUpper();

            var stock = stocks.FirstOrDefault(s => s.Symbol == symbol);
            if (stock == null)
            {
                Console.WriteLine("❌ Stock not found!");
                return;
            }

            Console.Write("Enter Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("Enter Transaction Date (dd-MM-yyyy) or press Enter for today: ");
            string dateInput = Console.ReadLine();
            DateTime transactionDate = string.IsNullOrWhiteSpace(dateInput) 
                ? DateTime.Now 
                : DateTime.ParseExact(dateInput, "dd-MM-yyyy", null);

            if (transactionDate > DateTime.Now)
                throw new InvalidTradeException("Transaction date cannot be in the future");

            investor.Portfolio.RemoveHolding(stock.Symbol, quantity);

            var transaction = new Transaction(transactionIdCounter++, stock, TransactionType.Sell, quantity, stock.CurrentPrice, transactionDate);
            transactions.Add(transaction);
            stock.VolumeTraded += quantity;
            AddToTransactionDict(stock.Symbol, transaction);

            Console.WriteLine($"✅ Sold {quantity} shares of {stock.Symbol} at ₹{stock.CurrentPrice} per share");
            Console.WriteLine($"   Total Amount: ₹{transaction.TotalAmount:F2}");
        }

        static void UpdateStockPrice()
        {
            ViewStocks();
            Console.Write("\nEnter Stock Symbol: ");
            string symbol = Console.ReadLine().ToUpper();

            var stock = stocks.FirstOrDefault(s => s.Symbol == symbol);
            if (stock == null)
            {
                Console.WriteLine("❌ Stock not found!");
                return;
            }

            Console.Write($"Current Price: ₹{stock.CurrentPrice}. Enter New Price: ");
            decimal newPrice = decimal.Parse(Console.ReadLine());

            stock.UpdatePrice(newPrice);
            Console.WriteLine("✅ Stock price updated successfully!");
        }

        static void ViewPortfolio()
        {
            Console.Write("\nEnter Investor ID: ");
            int investorId = int.Parse(Console.ReadLine());

            var investor = investors.FirstOrDefault(i => i.Id == investorId);
            if (investor == null)
            {
                Console.WriteLine("❌ Investor not found!");
                return;
            }

            Console.WriteLine($"\n--- Portfolio for {investor.Name} ---");
            if (investor.Portfolio.Holdings.Count == 0)
            {
                Console.WriteLine("No holdings");
                return;
            }

            decimal totalInvestment = 0;
            decimal currentValue = 0;

            Console.WriteLine($"{"Stock",-15} {"Quantity",-12} {"Avg Buy Price",-15} {"Current Price",-15} {"P&L",-15}");
            Console.WriteLine(new string('-', 75));

            foreach (var holding in investor.Portfolio.Holdings)
            {
                var stock = stocks.First(s => s.Symbol == holding.Key);
                var buyTransactions = transactions.Where(t => 
                    t.Stock.Symbol == holding.Key && 
                    t.Type == TransactionType.Buy &&
                    investor.Portfolio.Holdings.ContainsKey(holding.Key));

                decimal avgBuyPrice = buyTransactions.Any() 
                    ? buyTransactions.Average(t => t.PricePerShare) 
                    : 0;

                decimal invested = avgBuyPrice * holding.Value;
                decimal current = stock.CurrentPrice * holding.Value;
                decimal profitLoss = current - invested;

                totalInvestment += invested;
                currentValue += current;

                Console.WriteLine($"{holding.Key,-15} {holding.Value,-12} ₹{avgBuyPrice,-13:F2} ₹{stock.CurrentPrice,-13:F2} ₹{profitLoss,-13:F2}");
            }

            Console.WriteLine(new string('-', 75));
            Console.WriteLine($"Total Investment: ₹{totalInvestment:F2}");
            Console.WriteLine($"Current Value: ₹{currentValue:F2}");
            Console.WriteLine($"Net P&L: ₹{(currentValue - totalInvestment):F2}");
        }

        static void ViewTransactions()
        {
            Console.WriteLine("\n--- All Transactions ---");
            Console.WriteLine($"{"ID",-5} {"Stock",-12} {"Type",-8} {"Qty",-8} {"Price",-12} {"Date",-15}");
            Console.WriteLine(new string('-', 65));
            
            foreach (var transaction in transactions.OrderByDescending(t => t.TransactionDate))
            {
                Console.WriteLine($"{transaction.Id,-5} {transaction.Stock.Symbol,-12} {transaction.Type,-8} {transaction.Quantity,-8} ₹{transaction.PricePerShare,-10:F2} {transaction.TransactionDate:dd-MMM-yyyy}");
            }
        }

        static void CalculatePortfolioRisk()
        {
            Console.Write("\nEnter Investor ID: ");
            int investorId = int.Parse(Console.ReadLine());

            var investor = investors.FirstOrDefault(i => i.Id == investorId);
            if (investor == null)
            {
                Console.WriteLine("❌ Investor not found!");
                return;
            }

            decimal totalInvestment = 0;
            decimal currentValue = 0;

            foreach (var holding in investor.Portfolio.Holdings)
            {
                var stock = stocks.First(s => s.Symbol == holding.Key);
                var buyTransactions = transactions.Where(t => 
                    t.Stock.Symbol == holding.Key && 
                    t.Type == TransactionType.Buy);

                decimal avgBuyPrice = buyTransactions.Any() 
                    ? buyTransactions.Average(t => t.PricePerShare) 
                    : 0;

                totalInvestment += avgBuyPrice * holding.Value;
                currentValue += stock.CurrentPrice * holding.Value;
            }

            string riskLevel = investor.Portfolio.RiskStrategy.CalculateRisk(totalInvestment, currentValue);
            decimal returnPercentage = totalInvestment > 0 
                ? ((currentValue - totalInvestment) / totalInvestment) * 100 
                : 0;

            Console.WriteLine($"\n--- Portfolio Risk Analysis ---");
            Console.WriteLine($"Investor: {investor.Name}");
            Console.WriteLine($"Strategy: {investor.Portfolio.RiskStrategy.GetType().Name}");
            Console.WriteLine($"Total Investment: ₹{totalInvestment:F2}");
            Console.WriteLine($"Current Value: ₹{currentValue:F2}");
            Console.WriteLine($"Return: {returnPercentage:F2}%");
            Console.WriteLine($"Risk Level: {riskLevel}");
        }

        static void ShowLinqReports()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║   LINQ REPORTS                         ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            // 1. Most profitable investor
            Console.WriteLine("\n1. Most Profitable Investor:");
            var investorProfits = investors.Select(inv => {
                decimal totalInvestment = 0;
                decimal currentValue = 0;

                foreach (var holding in inv.Portfolio.Holdings)
                {
                    var stock = stocks.First(s => s.Symbol == holding.Key);
                    var buyTransactions = transactions.Where(t => 
                        t.Stock.Symbol == holding.Key && 
                        t.Type == TransactionType.Buy);

                    decimal avgBuyPrice = buyTransactions.Any() 
                        ? buyTransactions.Average(t => t.PricePerShare) 
                        : 0;

                    totalInvestment += avgBuyPrice * holding.Value;
                    currentValue += stock.CurrentPrice * holding.Value;
                }

                return new { Investor = inv.Name, Profit = currentValue - totalInvestment };
            }).OrderByDescending(x => x.Profit).FirstOrDefault();

            if (investorProfits != null)
                Console.WriteLine($"   {investorProfits.Investor}: ₹{investorProfits.Profit:F2}");

            // 2. Stock with highest volume traded
            var highestVolume = stocks.OrderByDescending(s => s.VolumeTraded).FirstOrDefault();
            if (highestVolume != null)
                Console.WriteLine($"\n2. Stock with Highest Volume: {highestVolume.Symbol} ({highestVolume.VolumeTraded} shares)");

            // 3. Transactions grouped by stock
            Console.WriteLine("\n3. Transactions Grouped by Stock:");
            var groupedTransactions = transactions.GroupBy(t => t.Stock.Symbol);
            foreach (var group in groupedTransactions)
            {
                Console.WriteLine($"   {group.Key}: {group.Count()} transactions");
            }

            // 4. Net profit/loss using LINQ Aggregate
            decimal netProfitLoss = investors.Aggregate(0m, (total, inv) => {
                decimal investorPL = 0;
                foreach (var holding in inv.Portfolio.Holdings)
                {
                    var stock = stocks.First(s => s.Symbol == holding.Key);
                    var buyTransactions = transactions.Where(t => 
                        t.Stock.Symbol == holding.Key && 
                        t.Type == TransactionType.Buy);

                    decimal avgBuyPrice = buyTransactions.Any() 
                        ? buyTransactions.Average(t => t.PricePerShare) 
                        : 0;

                    investorPL += (stock.CurrentPrice - avgBuyPrice) * holding.Value;
                }
                return total + investorPL;
            });

            Console.WriteLine($"\n4. Total Net Profit/Loss (All Investors): ₹{netProfitLoss:F2}");

            // 5. Investors with negative returns
            Console.WriteLine("\n5. Investors with Negative Returns:");
            var negativeReturns = investors.Where(inv => {
                decimal totalInvestment = 0;
                decimal currentValue = 0;

                foreach (var holding in inv.Portfolio.Holdings)
                {
                    var stock = stocks.First(s => s.Symbol == holding.Key);
                    var buyTransactions = transactions.Where(t => 
                        t.Stock.Symbol == holding.Key && 
                        t.Type == TransactionType.Buy);

                    decimal avgBuyPrice = buyTransactions.Any() 
                        ? buyTransactions.Average(t => t.PricePerShare) 
                        : 0;

                    totalInvestment += avgBuyPrice * holding.Value;
                    currentValue += stock.CurrentPrice * holding.Value;
                }

                return currentValue < totalInvestment;
            });

            foreach (var investor in negativeReturns)
            {
                Console.WriteLine($"   {investor.Name}");
            }

            if (!negativeReturns.Any())
                Console.WriteLine("   None");
        }
    }
}
