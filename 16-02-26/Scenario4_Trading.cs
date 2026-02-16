using System;
using System.Collections.Generic;
using System.Linq;

namespace Generics_Assessment.Scenario4
{
    public interface IFinancialInstrument
    {
        string Symbol { get; }
        decimal CurrentPrice { get; }
        InstrumentType Type { get; }
    }

    public enum InstrumentType { Stock, Bond, Option, Future }
    public enum Trend { Upward, Downward, Sideways }

    // 1. Generic portfolio
    public class Portfolio<T> where T : IFinancialInstrument
    {
        private Dictionary<T, int> _holdings = new(); // Instrument -> Quantity
        private Dictionary<T, decimal> _purchasePrices = new(); // Track purchase prices

        /// <summary>
        /// Buys instrument
        /// </summary>
        public void Buy(T instrument, int quantity, decimal price)
        {
            if (instrument == null)
                throw new ArgumentNullException(nameof(instrument));

            // Validate: quantity > 0, price > 0
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive", nameof(quantity));

            if (price <= 0)
                throw new ArgumentException("Price must be positive", nameof(price));

            // Add to holdings or update quantity
            if (_holdings.ContainsKey(instrument))
            {
                // Calculate weighted average purchase price
                int existingQty = _holdings[instrument];
                decimal existingPrice = _purchasePrices[instrument];
                decimal totalCost = (existingQty * existingPrice) + (quantity * price);
                int totalQty = existingQty + quantity;

                _holdings[instrument] = totalQty;
                _purchasePrices[instrument] = totalCost / totalQty;
            }
            else
            {
                _holdings[instrument] = quantity;
                _purchasePrices[instrument] = price;
            }

            Console.WriteLine($"✓ Bought {quantity} units of {instrument.Symbol} at ${price:F2}");
        }

        /// <summary>
        /// Sells instrument
        /// </summary>
        public decimal? Sell(T instrument, int quantity, decimal currentPrice)
        {
            if (instrument == null)
                throw new ArgumentNullException(nameof(instrument));

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive", nameof(quantity));

            if (currentPrice <= 0)
                throw new ArgumentException("Price must be positive", nameof(currentPrice));

            // Validate: enough quantity
            if (!_holdings.ContainsKey(instrument) || _holdings[instrument] < quantity)
            {
                Console.WriteLine($"✗ Insufficient holdings of {instrument.Symbol}");
                return null;
            }

            // Remove/update holdings
            _holdings[instrument] -= quantity;
            if (_holdings[instrument] == 0)
            {
                _holdings.Remove(instrument);
                _purchasePrices.Remove(instrument);
            }

            // Return proceeds (quantity * currentPrice)
            decimal proceeds = quantity * currentPrice;
            Console.WriteLine($"✓ Sold {quantity} units of {instrument.Symbol} at ${currentPrice:F2} " +
                            $"(Proceeds: ${proceeds:F2})");
            return proceeds;
        }

        /// <summary>
        /// Calculates total portfolio value
        /// </summary>
        public decimal CalculateTotalValue()
        {
            return _holdings.Sum(h => h.Value * h.Key.CurrentPrice);
        }

        /// <summary>
        /// Gets top performing instrument
        /// </summary>
        public (T instrument, decimal returnPercentage)? GetTopPerformer()
        {
            if (!_holdings.Any())
                return null;

            var performances = _holdings
                .Select(h => new
                {
                    Instrument = h.Key,
                    ReturnPercentage = ((h.Key.CurrentPrice - _purchasePrices[h.Key]) /
                                       _purchasePrices[h.Key]) * 100
                })
                .OrderByDescending(p => p.ReturnPercentage)
                .FirstOrDefault();

            return performances != null
                ? (performances.Instrument, performances.ReturnPercentage)
                : null;
        }

        /// <summary>
        /// Gets all holdings
        /// </summary>
        public IReadOnlyDictionary<T, int> GetHoldings()
        {
            return _holdings;
        }

        /// <summary>
        /// Gets purchase price for instrument
        /// </summary>
        public decimal? GetPurchasePrice(T instrument)
        {
            return _purchasePrices.ContainsKey(instrument)
                ? _purchasePrices[instrument]
                : null;
        }

        /// <summary>
        /// Gets profit/loss for specific instrument
        /// </summary>
        public decimal GetProfitLoss(T instrument)
        {
            if (!_holdings.ContainsKey(instrument))
                return 0;

            int quantity = _holdings[instrument];
            decimal purchasePrice = _purchasePrices[instrument];
            decimal currentValue = quantity * instrument.CurrentPrice;
            decimal costBasis = quantity * purchasePrice;

            return currentValue - costBasis;
        }
    }

    // 2. Specialized instruments
    public class Stock : IFinancialInstrument
    {
        public string Symbol { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
        public InstrumentType Type => InstrumentType.Stock;
        public string CompanyName { get; set; } = string.Empty;
        public decimal DividendYield { get; set; }

        public override string ToString()
        {
            return $"{Symbol} ({CompanyName}) - ${CurrentPrice:F2} [Div: {DividendYield:F2}%]";
        }
    }

    public class Bond : IFinancialInstrument
    {
        public string Symbol { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
        public InstrumentType Type => InstrumentType.Bond;
        public DateTime MaturityDate { get; set; }
        public decimal CouponRate { get; set; }

        public override string ToString()
        {
            return $"{Symbol} - ${CurrentPrice:F2} [Coupon: {CouponRate:F2}%, " +
                   $"Maturity: {MaturityDate:yyyy-MM-dd}]";
        }
    }

    public class Option : IFinancialInstrument
    {
        public string Symbol { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
        public InstrumentType Type => InstrumentType.Option;
        public string UnderlyingSymbol { get; set; } = string.Empty;
        public decimal StrikePrice { get; set; }
        public DateTime ExpirationDate { get; set; }

        public override string ToString()
        {
            return $"{Symbol} (Underlying: {UnderlyingSymbol}) - ${CurrentPrice:F2} " +
                   $"[Strike: ${StrikePrice:F2}]";
        }
    }

    // 3. Generic trading strategy
    public class TradingStrategy<T> where T : IFinancialInstrument
    {
        /// <summary>
        /// Executes strategy on portfolio
        /// </summary>
        public void Execute(Portfolio<T> portfolio, IEnumerable<T> marketData,
            Func<T, bool> buyCondition, Func<T, bool> sellCondition)
        {
            if (portfolio == null)
                throw new ArgumentNullException(nameof(portfolio));

            if (marketData == null)
                throw new ArgumentNullException(nameof(marketData));

            Console.WriteLine("\n--- Executing Trading Strategy ---");

            // Apply buy conditions
            foreach (var instrument in marketData.Where(buyCondition))
            {
                portfolio.Buy(instrument, 10, instrument.CurrentPrice);
            }

            // Apply sell conditions
            var holdings = portfolio.GetHoldings().Keys.ToList();
            foreach (var instrument in holdings.Where(sellCondition))
            {
                int quantity = portfolio.GetHoldings()[instrument];
                portfolio.Sell(instrument, quantity, instrument.CurrentPrice);
            }
        }

        /// <summary>
        /// Calculates risk metrics
        /// </summary>
        public Dictionary<string, decimal> CalculateRiskMetrics(IEnumerable<T> instruments)
        {
            if (instruments == null || !instruments.Any())
                return new Dictionary<string, decimal>();

            var prices = instruments.Select(i => i.CurrentPrice).ToList();

            // Calculate volatility (standard deviation)
            decimal mean = prices.Average();
            decimal sumSquaredDiff = prices.Sum(p => (p - mean) * (p - mean));
            decimal volatility = (decimal)Math.Sqrt((double)(sumSquaredDiff / prices.Count));

            // Simplified metrics
            return new Dictionary<string, decimal>
            {
                ["Volatility"] = volatility,
                ["AveragePrice"] = mean,
                ["PriceRange"] = prices.Max() - prices.Min(),
                ["Beta"] = 1.0m, // Simplified - would need market data
                ["SharpeRatio"] = 1.5m // Simplified - would need risk-free rate
            };
        }
    }

    // 4. Price history with generics
    public class PriceHistory<T> where T : IFinancialInstrument
    {
        private Dictionary<T, List<(DateTime timestamp, decimal price)>> _history = new();

        /// <summary>
        /// Adds price point
        /// </summary>
        public void AddPrice(T instrument, DateTime timestamp, decimal price)
        {
            if (instrument == null)
                throw new ArgumentNullException(nameof(instrument));

            if (price <= 0)
                throw new ArgumentException("Price must be positive");

            if (!_history.ContainsKey(instrument))
            {
                _history[instrument] = new List<(DateTime, decimal)>();
            }

            _history[instrument].Add((timestamp, price));
            _history[instrument] = _history[instrument].OrderBy(h => h.timestamp).ToList();
        }

        /// <summary>
        /// Gets moving average
        /// </summary>
        public decimal? GetMovingAverage(T instrument, int days)
        {
            if (instrument == null)
                throw new ArgumentNullException(nameof(instrument));

            if (days <= 0)
                throw new ArgumentException("Days must be positive");

            if (!_history.ContainsKey(instrument))
                return null;

            var recentPrices = _history[instrument]
                .OrderByDescending(h => h.timestamp)
                .Take(days)
                .Select(h => h.price)
                .ToList();

            return recentPrices.Any() ? recentPrices.Average() : null;
        }

        /// <summary>
        /// Detects trend
        /// </summary>
        public Trend DetectTrend(T instrument, int period)
        {
            if (instrument == null)
                throw new ArgumentNullException(nameof(instrument));

            if (!_history.ContainsKey(instrument) || _history[instrument].Count < period)
                return Trend.Sideways;

            var recentPrices = _history[instrument]
                .OrderByDescending(h => h.timestamp)
                .Take(period)
                .Select(h => h.price)
                .ToList();

            if (recentPrices.Count < 2)
                return Trend.Sideways;

            // Simple trend detection: compare first and last prices
            decimal firstPrice = recentPrices.Last();
            decimal lastPrice = recentPrices.First();
            decimal changePercent = ((lastPrice - firstPrice) / firstPrice) * 100;

            if (changePercent > 5)
                return Trend.Upward;
            else if (changePercent < -5)
                return Trend.Downward;
            else
                return Trend.Sideways;
        }

        /// <summary>
        /// Gets price history for instrument
        /// </summary>
        public IReadOnlyList<(DateTime timestamp, decimal price)> GetHistory(T instrument)
        {
            if (instrument == null)
                throw new ArgumentNullException(nameof(instrument));

            return _history.ContainsKey(instrument)
                ? _history[instrument].AsReadOnly()
                : new List<(DateTime, decimal)>().AsReadOnly();
        }

        /// <summary>
        /// Gets highest price in period
        /// </summary>
        public decimal? GetHighestPrice(T instrument, int days)
        {
            if (!_history.ContainsKey(instrument))
                return null;

            return _history[instrument]
                .OrderByDescending(h => h.timestamp)
                .Take(days)
                .Max(h => h.price);
        }

        /// <summary>
        /// Gets lowest price in period
        /// </summary>
        public decimal? GetLowestPrice(T instrument, int days)
        {
            if (!_history.ContainsKey(instrument))
                return null;

            return _history[instrument]
                .OrderByDescending(h => h.timestamp)
                .Take(days)
                .Min(h => h.price);
        }
    }

    // Demo class for Scenario 4
    public static class Scenario4Demo
    {
        public static void Run()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   SCENARIO 4: Financial Trading Platform                  ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");

            try
            {
                // a) Create portfolio with mixed instruments
                Console.WriteLine("\n--- Creating Financial Instruments ---");

                var stocks = new List<Stock>
                {
                    new Stock
                    {
                        Symbol = "AAPL",
                        CompanyName = "Apple Inc.",
                        CurrentPrice = 175.50m,
                        DividendYield = 0.52m
                    },
                    new Stock
                    {
                        Symbol = "MSFT",
                        CompanyName = "Microsoft Corp.",
                        CurrentPrice = 380.25m,
                        DividendYield = 0.78m
                    },
                    new Stock
                    {
                        Symbol = "GOOGL",
                        CompanyName = "Alphabet Inc.",
                        CurrentPrice = 142.80m,
                        DividendYield = 0.00m
                    }
                };

                var bonds = new List<Bond>
                {
                    new Bond
                    {
                        Symbol = "US10Y",
                        CurrentPrice = 98.50m,
                        CouponRate = 4.25m,
                        MaturityDate = DateTime.Now.AddYears(10)
                    },
                    new Bond
                    {
                        Symbol = "CORP5Y",
                        CurrentPrice = 102.30m,
                        CouponRate = 5.50m,
                        MaturityDate = DateTime.Now.AddYears(5)
                    }
                };

                foreach (var stock in stocks)
                {
                    Console.WriteLine($"  {stock}");
                }
                foreach (var bond in bonds)
                {
                    Console.WriteLine($"  {bond}");
                }

                // b) Implement buy/sell logic
                Console.WriteLine("\n--- Building Portfolio ---");
                var stockPortfolio = new Portfolio<Stock>();
                var bondPortfolio = new Portfolio<Bond>();

                // Buy stocks
                stockPortfolio.Buy(stocks[0], 50, 170.00m);  // AAPL at $170
                stockPortfolio.Buy(stocks[1], 25, 375.00m);  // MSFT at $375
                stockPortfolio.Buy(stocks[2], 100, 140.00m); // GOOGL at $140

                // Buy bonds
                bondPortfolio.Buy(bonds[0], 10, 100.00m);    // US10Y at $100
                bondPortfolio.Buy(bonds[1], 5, 101.00m);     // CORP5Y at $101

                // Update current prices (simulate market movement)
                stocks[0].CurrentPrice = 175.50m; // AAPL up
                stocks[1].CurrentPrice = 380.25m; // MSFT up
                stocks[2].CurrentPrice = 142.80m; // GOOGL up
                bonds[0].CurrentPrice = 98.50m;   // US10Y down
                bonds[1].CurrentPrice = 102.30m;  // CORP5Y up

                // Display portfolio
                Console.WriteLine("\n--- Current Portfolio Holdings ---");
                Console.WriteLine("\nStocks:");
                foreach (var holding in stockPortfolio.GetHoldings())
                {
                    var stock = holding.Key;
                    var quantity = holding.Value;
                    var purchasePrice = stockPortfolio.GetPurchasePrice(stock);
                    var profitLoss = stockPortfolio.GetProfitLoss(stock);

                    Console.WriteLine($"  {stock.Symbol}: {quantity} shares @ ${stock.CurrentPrice:F2}");
                    Console.WriteLine($"    Purchase Price: ${purchasePrice:F2}");
                    Console.WriteLine($"    P/L: ${profitLoss:F2} ({(profitLoss >= 0 ? "+" : "")}" +
                                    $"{(profitLoss / (purchasePrice.Value * quantity)) * 100:F2}%)");
                }

                Console.WriteLine("\nBonds:");
                foreach (var holding in bondPortfolio.GetHoldings())
                {
                    var bond = holding.Key;
                    var quantity = holding.Value;
                    var purchasePrice = bondPortfolio.GetPurchasePrice(bond);
                    var profitLoss = bondPortfolio.GetProfitLoss(bond);

                    Console.WriteLine($"  {bond.Symbol}: {quantity} units @ ${bond.CurrentPrice:F2}");
                    Console.WriteLine($"    Purchase Price: ${purchasePrice:F2}");
                    Console.WriteLine($"    P/L: ${profitLoss:F2}");
                }

                decimal totalStockValue = stockPortfolio.CalculateTotalValue();
                decimal totalBondValue = bondPortfolio.CalculateTotalValue();
                Console.WriteLine($"\nTotal Stock Portfolio Value: ${totalStockValue:F2}");
                Console.WriteLine($"Total Bond Portfolio Value: ${totalBondValue:F2}");
                Console.WriteLine($"Total Portfolio Value: ${totalStockValue + totalBondValue:F2}");

                // c) Create trading strategy with lambda conditions
                Console.WriteLine("\n--- Trading Strategy Execution ---");
                var strategy = new TradingStrategy<Stock>();

                // Buy condition: Price < $150 and has dividend
                Func<Stock, bool> buyCondition = s => s.CurrentPrice < 150 && s.DividendYield > 0;

                // Sell condition: Profit > 5%
                Func<Stock, bool> sellCondition = s =>
                {
                    var purchasePrice = stockPortfolio.GetPurchasePrice(s);
                    if (!purchasePrice.HasValue) return false;
                    return ((s.CurrentPrice - purchasePrice.Value) / purchasePrice.Value) > 0.05m;
                };

                // Note: In real scenario, would pass market data
                // strategy.Execute(stockPortfolio, stocks, buyCondition, sellCondition);

                // d) Track price history
                Console.WriteLine("\n--- Price History Tracking ---");
                var priceHistory = new PriceHistory<Stock>();

                // Add historical prices for AAPL
                var baseDate = DateTime.Now.AddDays(-30);
                decimal[] aaplPrices = { 165m, 168m, 170m, 172m, 175m, 173m, 176m, 175.50m };

                for (int i = 0; i < aaplPrices.Length; i++)
                {
                    priceHistory.AddPrice(stocks[0], baseDate.AddDays(i * 4), aaplPrices[i]);
                }

                // Calculate moving averages
                var ma5 = priceHistory.GetMovingAverage(stocks[0], 5);
                var ma10 = priceHistory.GetMovingAverage(stocks[0], 8);

                Console.WriteLine($"\n{stocks[0].Symbol} Price Analysis:");
                Console.WriteLine($"  Current Price: ${stocks[0].CurrentPrice:F2}");
                Console.WriteLine($"  5-Period MA: ${ma5?.ToString("F2") ?? "N/A"}");
                Console.WriteLine($"  8-Period MA: ${ma10?.ToString("F2") ?? "N/A"}");
                Console.WriteLine($"  30-Day High: ${priceHistory.GetHighestPrice(stocks[0], 30)?.ToString("F2") ?? "N/A"}");
                Console.WriteLine($"  30-Day Low: ${priceHistory.GetLowestPrice(stocks[0], 30)?.ToString("F2") ?? "N/A"}");

                // e) Trend detection
                var trend = priceHistory.DetectTrend(stocks[0], 8);
                Console.WriteLine($"  Trend: {trend}");

                // f) Risk calculation
                Console.WriteLine("\n--- Risk Metrics ---");
                var riskMetrics = strategy.CalculateRiskMetrics(stocks);

                foreach (var metric in riskMetrics)
                {
                    Console.WriteLine($"  {metric.Key}: {metric.Value:F2}");
                }

                // g) Performance comparison
                Console.WriteLine("\n--- Performance Comparison ---");
                var topPerformer = stockPortfolio.GetTopPerformer();

                if (topPerformer.HasValue)
                {
                    Console.WriteLine($"Top Performer: {topPerformer.Value.instrument.Symbol}");
                    Console.WriteLine($"Return: {topPerformer.Value.returnPercentage:F2}%");
                }

                // h) Portfolio rebalancing
                Console.WriteLine("\n--- Portfolio Rebalancing ---");
                Console.WriteLine("Selling underperforming assets...");

                // Sell some GOOGL to rebalance
                stockPortfolio.Sell(stocks[2], 50, stocks[2].CurrentPrice);

                Console.WriteLine($"\nNew Total Portfolio Value: ${stockPortfolio.CalculateTotalValue():F2}");

                Console.WriteLine("\n✓ Scenario 4 completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error in Scenario 4: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
