using System;

namespace FlexibleInventorySystem.Models
{
    /// <summary>
    /// Grocery product class
    /// </summary>
    public class GroceryProduct : Product
    {
        /// <summary>
        /// Product expiry date
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Indicates if product is perishable
        /// </summary>
        public bool IsPerishable { get; set; }

        /// <summary>
        /// Product weight in kg
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Storage temperature requirement
        /// </summary>
        public string StorageTemperature { get; set; }

        /// <summary>
        /// Override GetProductDetails for grocery items
        /// </summary>
        /// <returns>Grocery product details</returns>
        public override string GetProductDetails()
        {
            string perishableStatus = IsPerishable ? "Perishable" : "Non-Perishable";
            string expiryInfo = $"Expires: {ExpiryDate:dd-MMM-yyyy} ({DaysUntilExpiry()} days)";
            return $"{Name}, Weight: {Weight}kg, {perishableStatus}, {expiryInfo}, Storage: {StorageTemperature}";
        }

        /// <summary>
        /// Check if product is expired
        /// </summary>
        /// <returns>True if expired</returns>
        public bool IsExpired()
        {
            return DateTime.Now > ExpiryDate;
        }

        /// <summary>
        /// Calculate days until expiry
        /// Return negative if expired
        /// </summary>
        /// <returns>Days until expiry</returns>
        public int DaysUntilExpiry()
        {
            TimeSpan difference = ExpiryDate - DateTime.Now;
            return (int)difference.TotalDays;
        }

        /// <summary>
        /// Override CalculateValue to apply discount for near-expiry items
        /// Apply 20% discount if within 3 days of expiry
        /// </summary>
        /// <returns>Calculated value with discount if applicable</returns>
        public override decimal CalculateValue()
        {
            decimal baseValue = base.CalculateValue();
            
            // Apply 20% discount if within 3 days of expiry
            if (DaysUntilExpiry() <= 3 && DaysUntilExpiry() >= 0)
            {
                return baseValue * 0.80m; // 20% discount
            }
            
            return baseValue;
        }
    }
}
