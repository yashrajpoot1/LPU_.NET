using System;

namespace FlexibleInventorySystem.Models
{
    /// <summary>
    /// Electronic product class
    /// </summary>
    public class ElectronicProduct : Product
    {
        /// <summary>
        /// Product brand
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Warranty duration in months
        /// </summary>
        public int WarrantyMonths { get; set; }

        /// <summary>
        /// Voltage specification
        /// </summary>
        public string Voltage { get; set; }

        /// <summary>
        /// Indicates if product is refurbished
        /// </summary>
        public bool IsRefurbished { get; set; }

        /// <summary>
        /// Override GetProductDetails to include electronic specifics
        /// </summary>
        /// <returns>Electronic product details</returns>
        public override string GetProductDetails()
        {
            string refurbishedStatus = IsRefurbished ? " (Refurbished)" : "";
            return $"Brand: {Brand}, Model: {Name}, Warranty: {WarrantyMonths} months, Voltage: {Voltage}{refurbishedStatus}";
        }

        /// <summary>
        /// Calculate warranty expiration date
        /// </summary>
        /// <returns>Warranty expiry date</returns>
        public DateTime GetWarrantyExpiryDate()
        {
            return DateAdded.AddMonths(WarrantyMonths);
        }

        /// <summary>
        /// Check if warranty is still valid
        /// </summary>
        /// <returns>True if warranty is valid</returns>
        public bool IsWarrantyValid()
        {
            return DateTime.Now < GetWarrantyExpiryDate();
        }
    }
}
