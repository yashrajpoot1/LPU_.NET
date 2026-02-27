using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlexibleInventorySystem.Interfaces;
using FlexibleInventorySystem.Models;
using FlexibleInventorySystem.Exceptions;
using FlexibleInventorySystem.Utilities;

namespace FlexibleInventorySystem.Services
{
    /// <summary>
    /// Main inventory manager class
    /// Implements both IInventoryOperations and IReportGenerator
    /// </summary>
    public class InventoryManager : IInventoryOperations, IReportGenerator
    {
        private List<Product> _products;
        private readonly object _lock = new object();

        /// <summary>
        /// Constructor to initialize inventory
        /// </summary>
        public InventoryManager()
        {
            _products = new List<Product>();
        }

        // ============ IInventoryOperations Implementation ============

        /// <summary>
        /// Add a product to inventory
        /// </summary>
        public bool AddProduct(Product product)
        {
            lock (_lock)
            {
                // Validate product
                if (!ProductValidator.ValidateProduct(product, out string errorMessage))
                {
                    throw new InventoryException(errorMessage, "INVALID_PRODUCT");
                }

                // Check for duplicate ID
                if (_products.Any(p => p.Id == product.Id))
                {
                    throw new InventoryException($"Product with ID {product.Id} already exists", "DUPLICATE_ID");
                }

                // Validate specific product types
                if (product is ElectronicProduct electronic)
                {
                    if (!ProductValidator.ValidateElectronicProduct(electronic, out errorMessage))
                    {
                        throw new InventoryException(errorMessage, "INVALID_ELECTRONIC");
                    }
                }
                else if (product is GroceryProduct grocery)
                {
                    if (!ProductValidator.ValidateGroceryProduct(grocery, out errorMessage))
                    {
                        throw new InventoryException(errorMessage, "INVALID_GROCERY");
                    }
                }
                else if (product is ClothingProduct clothing)
                {
                    if (!ProductValidator.ValidateClothingProduct(clothing, out errorMessage))
                    {
                        throw new InventoryException(errorMessage, "INVALID_CLOTHING");
                    }
                }

                _products.Add(product);
                return true;
            }
        }

        /// <summary>
        /// Remove product by ID
        /// </summary>
        public bool RemoveProduct(string productId)
        {
            lock (_lock)
            {
                var product = _products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                {
                    return false;
                }

                _products.Remove(product);
                return true;
            }
        }

        /// <summary>
        /// Find product by ID
        /// </summary>
        public Product FindProduct(string productId)
        {
            lock (_lock)
            {
                return _products.FirstOrDefault(p => p.Id == productId);
            }
        }

        /// <summary>
        /// Get all products in a specific category
        /// </summary>
        public List<Product> GetProductsByCategory(string category)
        {
            lock (_lock)
            {
                return _products
                    .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }

        /// <summary>
        /// Update product quantity
        /// </summary>
        public bool UpdateQuantity(string productId, int newQuantity)
        {
            lock (_lock)
            {
                if (newQuantity < 0)
                {
                    throw new InventoryException("Quantity cannot be negative", "INVALID_QUANTITY");
                }

                var product = _products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                {
                    return false;
                }

                product.Quantity = newQuantity;
                return true;
            }
        }

        /// <summary>
        /// Calculate total value of all products
        /// </summary>
        public decimal GetTotalInventoryValue()
        {
            lock (_lock)
            {
                return _products.Sum(p => p.CalculateValue());
            }
        }

        /// <summary>
        /// Get products with quantity below threshold
        /// </summary>
        public List<Product> GetLowStockProducts(int threshold)
        {
            lock (_lock)
            {
                return _products.Where(p => p.Quantity < threshold).ToList();
            }
        }

        // ============ IReportGenerator Implementation ============

        /// <summary>
        /// Generate complete inventory report
        /// </summary>
        public string GenerateInventoryReport()
        {
            lock (_lock)
            {
                var sb = new StringBuilder();
                sb.AppendLine("================================");
                sb.AppendLine("       INVENTORY REPORT");
                sb.AppendLine("================================");
                sb.AppendLine($"Total Products: {_products.Count}");
                sb.AppendLine($"Total Value: {GetTotalInventoryValue():C}");
                sb.AppendLine();
                sb.AppendLine("Product List:");
                sb.AppendLine(new string('-', 80));

                foreach (var product in _products)
                {
                    sb.AppendLine($"{product.Id,-10} {product.Name,-25} {product.Category,-15} Qty: {product.Quantity,-5} Value: {product.CalculateValue():C}");
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Generate category-wise summary
        /// </summary>
        public string GenerateCategorySummary()
        {
            lock (_lock)
            {
                var sb = new StringBuilder();
                sb.AppendLine("================================");
                sb.AppendLine("      CATEGORY SUMMARY");
                sb.AppendLine("================================");

                var categoryGroups = _products.GroupBy(p => p.Category);

                foreach (var group in categoryGroups)
                {
                    int count = group.Count();
                    decimal totalValue = group.Sum(p => p.CalculateValue());
                    sb.AppendLine($"{group.Key}: {count} items - Total Value: {totalValue:C}");
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Generate value analysis report
        /// </summary>
        public string GenerateValueReport()
        {
            lock (_lock)
            {
                var sb = new StringBuilder();
                sb.AppendLine("================================");
                sb.AppendLine("       VALUE REPORT");
                sb.AppendLine("================================");

                if (_products.Count == 0)
                {
                    sb.AppendLine("No products in inventory");
                    return sb.ToString();
                }

                var mostValuable = _products.OrderByDescending(p => p.CalculateValue()).FirstOrDefault();
                var leastValuable = _products.OrderBy(p => p.CalculateValue()).FirstOrDefault();
                decimal averagePrice = _products.Average(p => p.Price);
                
                // Calculate median price
                var sortedPrices = _products.Select(p => p.Price).OrderBy(p => p).ToList();
                decimal medianPrice = sortedPrices.Count % 2 == 0
                    ? (sortedPrices[sortedPrices.Count / 2 - 1] + sortedPrices[sortedPrices.Count / 2]) / 2
                    : sortedPrices[sortedPrices.Count / 2];

                sb.AppendLine($"Most Valuable Product: {mostValuable.Name} - {mostValuable.CalculateValue():C}");
                sb.AppendLine($"Least Valuable Product: {leastValuable.Name} - {leastValuable.CalculateValue():C}");
                sb.AppendLine($"Average Price: {averagePrice:C}");
                sb.AppendLine($"Median Price: {medianPrice:C}");
                sb.AppendLine();
                sb.AppendLine("Products Above Average Price:");

                var aboveAverage = _products.Where(p => p.Price > averagePrice);
                foreach (var product in aboveAverage)
                {
                    sb.AppendLine($"  - {product.Name}: {product.Price:C}");
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Generate report of expiring grocery products
        /// </summary>
        public string GenerateExpiryReport(int daysThreshold)
        {
            lock (_lock)
            {
                var sb = new StringBuilder();
                sb.AppendLine("================================");
                sb.AppendLine("      EXPIRY REPORT");
                sb.AppendLine("================================");
                sb.AppendLine($"Products expiring within {daysThreshold} days:");
                sb.AppendLine();

                var expiringProducts = _products
                    .OfType<GroceryProduct>()
                    .Where(g => g.DaysUntilExpiry() <= daysThreshold && g.DaysUntilExpiry() >= 0)
                    .OrderBy(g => g.DaysUntilExpiry());

                if (!expiringProducts.Any())
                {
                    sb.AppendLine("No products expiring soon");
                }
                else
                {
                    foreach (var product in expiringProducts)
                    {
                        sb.AppendLine($"{product.Name} - Expires in {product.DaysUntilExpiry()} days ({product.ExpiryDate:dd-MMM-yyyy})");
                    }
                }

                return sb.ToString();
            }
        }

        // ============ Additional Methods ============

        /// <summary>
        /// Search products with custom criteria
        /// </summary>
        public IEnumerable<Product> SearchProducts(Func<Product, bool> predicate)
        {
            lock (_lock)
            {
                return _products.Where(predicate).ToList();
            }
        }

        /// <summary>
        /// Apply discount to products in category
        /// </summary>
        public void ApplyCategoryDiscount(string category, decimal discountPercentage)
        {
            lock (_lock)
            {
                var productsInCategory = _products
                    .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

                foreach (var product in productsInCategory)
                {
                    product.Price = product.Price * (1 - discountPercentage / 100);
                }
            }
        }

        /// <summary>
        /// Get total count of products
        /// </summary>
        public int GetTotalProductCount()
        {
            lock (_lock)
            {
                return _products.Count;
            }
        }

        /// <summary>
        /// Get unique categories
        /// </summary>
        public IEnumerable<string> GetCategories()
        {
            lock (_lock)
            {
                return _products.Select(p => p.Category).Distinct().ToList();
            }
        }
    }
}
