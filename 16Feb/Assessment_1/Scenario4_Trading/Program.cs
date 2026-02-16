using System;
using System.Collections.Generic;
using System.Linq;

namespace Scenario4_Trading
{
    public interface IFinancialInstrument
    {
        string Symbol { get; }
        decimal CurrentPrice { get; set; }
        InstrumentType Type { get; }
    }

    public enum InstrumentType { Stock, Bond, Option, Future }
    public enum Trend { Upward, Downward, Sideways }

    // 1. Generic portfolio
    public class Portfolio<T> where T : IFinancialInstrument
    {
        private Dictionary<T, int> _holdings = new();
        private Dictionary<T, decimal> _purchasePrices = new();

        public void Buy(T instrument, int quantity, decimal price)
        {
            if (instrument == null)
                throw new ArgumentNullException(nameof(instrument));
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive");
            if (price <= 0)
                throw new ArgumentException("Price must be positive");

            if (_holdings.ContainsKey(instrument))
            {
                // Update average purchase price
                int currentQty = _holdings[instrument];
                decimal currentAvgPrice = _purchasePrices[instrument];
                decimal newAvgPrice = ((currentAvgPrice * currentQty) + (price * quantity)) / (currentQty + quantity);

                _holdings[instrument] += quantity;
                _purchasePrices[instrument] = newAvgPrice;
            }
            else
            {
                _holdings[instrument] = quantity;
                _purchasePrices[instrument] = price;
            }

            Console.WriteLine($"Bought {quantity} units of {instrument.Symbol} at ${price:F2}");
        }

        public decimal? Sell(T instrument, int quantity, decimal currentPrice)
        {
            if (instrument == null)
                throw new ArgumentNullException(nameof(instrument));
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive");
            if (currentPrice <= 0)
                throw new ArgumentException("Price must be positive");

            if (!_holdings.ContainsKey(instrument) || _holdings[instrument] < quantity)
            {
                Console.WriteLine($"Insufficient holdings to sell {quantity} units of {instrument.Symbol}");
                return null;
            }

            _holdings[instrument] -= quantity;
            if (_holdings[instrument] == 0)
            {
                _holdings.Remove(instrument);
                _purchasePrices.Remove(instrument);
            }

            decimal proceeds = quantity * currentPrice;
            Console.WriteLine($"Sold {quantity} units of {instrument.Symbol} at ${currentPrice:F2}, Proceeds: ${proceeds:F2}");
            return proceeds;
        }

        public decimal CalculateTotalValue()
        {
            return _holdings.Sum(kvp => kvp.Key.CurrentPrice * kvp.Value);
        }

        public (T instrument, decimal returnPercentage)? GetTopPerformer()
        {
            if (!_holdings.Any())
                return null;

            var performers = _holdings.Select(kvp =>
            {
                var instrument = kvp.Key;
                var purchasePrice = _purchasePrices[instrument];
                var returnPct = ((instrument.CurrentPrice - purchasePrice) / purchasePrice) * 100;
                return (instrument, returnPct);
            }).OrderByDescending(p => p.returnPct);

            return performers.FirstOrDefault();
        }

        public IEnumerable<(T instrument, int quantity, decimal purchasePrice)> GetHoldings()
        {
            return _holdings.Select(kvp => (kvp.Key, kvp.Value, _purchasePrices[kvp.Key]));
        }
    }

    // 2. Specialized instruments
    public class Stock : IFinancialInstrument
    {
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
        public InstrumentType Type => InstrumentType.Stock;
        public string CompanyName { get; set; }
        public decimal DividendYield { get; set; }
    }

    public class Bond : IFinancialInstrument
    {
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
        public InstrumentType Type => InstrumentType.Bond;
        public DateTime MaturityDate { get; set; }
        public decimal CouponRate { get; set; }
    }

    // 3. Generic trading strategy
    public class TradingStrategy<T> where T : IFinancialInstrument
    {
        public void Execute(Portfolio<T> portfolio, List<T> marketInstruments,
            Func<T, bool> buyCondition, Func<T, bool> sellCondition)
        {
            if (portfolio == null)
                throw new ArgumentNullException(nameof(portfolio));
            if (marketInstruments == null)
                throw new ArgumentNullException(nameof(marketInstruments));
            if (buyCondition == null)
                throw new ArgumentNullException(nameof(buyCondition));
            if (sellCondition == null)
                throw new ArgumentNullException(nameof(sellCondition));

            Console.WriteLine("\n=== Executing Trading Strategy ===");

            // Check buy conditions
            foreach (var instrument in marketInstruments)
            {
                if (buyCondition(instrument))
                {
                    portfolio.Buy(instrument, 10, instrument.CurrentPrice);
                }
            }

            // Check sell conditions
            var holdings = portfolio.GetHoldings().ToList();
            foreach (var holding in holdings)
            {
                if (sellCondition(holding.instrument))
                {
                    portfolio.Sell(holding.instrument, holding.quantity, holding.instrument.CurrentPrice);
                }
            }
        }

        public Dictionary<string, decimal> CalculateRiskMetrics(IEnumerable<T> instruments)
        {
            if (instruments == null || !instruments.Any())
                return new Dictionary<string, decimal>();

            var prices = instruments.Select(i => i.CurrentPrice).ToList();
            var avgPrice = prices.Average();
            var variance = prices.Sum(p => (p - avgPrice) * (p - avgPrice)) / prices.Count;
            var volatility = (decimal)Math.Sqrt((double)variance);

            return new Dictionary<string, decimal>
            {
                { "Volatility", volatility },
                { "AveragePrice", avgPrice },
                { "PriceRange", prices.Max() - prices.Min() }
            };
        }
    }

    // 4. Price history with generics
    public class PriceHistory<T> where T : IFinancialInstrument
    {
        private Dictionary<T, List<(DateTime, decimal)>> _history = new();

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
            _history[instrument] = _history[instrument].OrderBy(h => h.Item1).ToList();
        }

        public decimal? GetMovingAverage(T instrument, int days)
        {
            if (instrument == null)
                throw new ArgumentNullException(nameof(instrument));
            if (days <= 0)
                throw new ArgumentException("Days must be positive");

            if (!_history.ContainsKey(instrument))
                return null;

            var recentPrices = _history[instrument]
                .OrderByDescending(h => h.Item1)
                .Take(days)
                .Select(h => h.Item2)
                .ToList();

            if (!recentPrices.Any())
                return null;

            return recentPrices.Average();
        }

        public Trend DetectTrend(T instrument, int period)
        {
            if (instrument == null)
                throw new ArgumentNullException(nameof(instrument));
            if (period <= 0)
                throw new ArgumentException("Period must be positive");

            if (!_history.ContainsKey(instrument))
                return Trend.Sideways;

            var recentPrices = _history[instrument]
                .OrderByDescending(h => h.Item1)
                .Take(period)
                .Select(h => h.Item2)
                .ToList();

            if (recentPrices.Count < 2)
                return Trend.Sideways;

            var firstHalf = recentPrices.Take(recentPrices.Count / 2).Average();
            var secondHalf = recentPrices.Skip(recentPrices.Count / 2).Average();

            var changePercent = ((secondHalf - firstHalf) / firstHalf) * 100;

            if (changePercent > 2)
                return Trend.Upward;
            else if (changePercent < -2)
                return Trend.Downward;
            else
                return Trend.Sideways;
        }

        public IEnumerable<(DateTime date, decimal price)> GetHistory(T instrument)
        {
            if (instrument == null)
                throw new ArgumentNullException(nameof(instrument));

            return _history.ContainsKey(instrument)
                ? _history[instrument].OrderBy(h => h.Item1)
                : Enumerable.Empty<(DateTime, decimal)>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Financial Trading Platform Demo ===\n");

            try
            {
                // Create instruments
                var apple = new Stock
                {
                    Symbol = "AAPL",
                    CurrentPrice = 175.50m,
                    CompanyName = "Apple Inc.",
                    DividendYield = 0.52m
                };

                var microsoft = new Stock
                {
                    Symbol = "MSFT",
                    CurrentPrice = 380.25m,
                    CompanyName = "Microsoft Corporation",
                    Div