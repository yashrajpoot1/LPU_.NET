using System;
using FlexibleInventorySystem.Models;

namespace FlexibleInventorySystem.Utilities
{
    /// <summary>
    /// Validation helper class for products
    /// </summary>
    public static class ProductValidator
    {
        /// <summary>
        /// Validate product data
        /// </summary>
        /// <param name="product">Product to validate</param>
        /// <param name="errorMessage">Error message if validation fails</param>
        /// <returns>True if valid</returns>
        public static bool ValidateProduct(Product product, out string errorMessage)
        {
            errorMessage = null;

            if (product == null)
            {
                errorMessage = "Product cannot be null";
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.Id))
            {
                errorMessage = "Product ID cannot be null or empty";
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.Name))
            {
                errorMessage = "Product name cannot be null or empty";
                return false;
            }

            if (product.Price <= 0)
            {
                errorMessage = "Product price must be greater than zero";
                return false;
            }

            if (product.Quantity < 0)
            {
                errorMessage = "Product quantity cannot be negative";
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.Category))
            {
                errorMessage = "Product category cannot be null or empty";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate electronic product specific rules
        /// </summary>
        /// <param name="product">Electronic product to validate</param>
        /// <param name="errorMessage">Error message if validation fails</param>
        /// <returns>True if valid</returns>
        public static bool ValidateElectronicProduct(ElectronicProduct product, out string errorMessage)
        {
            errorMessage = null;

            // First validate base product properties
            if (!ValidateProduct(product, out errorMessage))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.Brand))
            {
                errorMessage = "Electronic product brand cannot be null or empty";
                return false;
            }

            if (product.WarrantyMonths < 0)
            {
                errorMessage = "Warranty months cannot be negative";
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.Voltage))
            {
                errorMessage = "Voltage specification cannot be null or empty";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate grocery product specific rules
        /// </summary>
        /// <param name="product">Grocery product to validate</param>
        /// <param name="errorMessage">Error message if validation fails</param>
        /// <returns>True if valid</returns>
        public static bool ValidateGroceryProduct(GroceryProduct product, out string errorMessage)
        {
            errorMessage = null;

            // First validate base product properties
            if (!ValidateProduct(product, out errorMessage))
            {
                return false;
            }

            if (product.ExpiryDate == default(DateTime))
            {
                errorMessage = "Expiry date must be set";
                return false;
            }

            if (product.Weight <= 0)
            {
                errorMessage = "Weight must be greater than zero";
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.StorageTemperature))
            {
                errorMessage = "Storage temperature cannot be null or empty";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate clothing product specific rules
        /// </summary>
        /// <param name="product">Clothing product to validate</param>
        /// <param name="errorMessage">Error message if validation fails</param>
        /// <returns>True if valid</returns>
        public static bool ValidateClothingProduct(ClothingProduct product, out string errorMessage)
        {
            errorMessage = null;

            // First validate base product properties
            if (!ValidateProduct(product, out errorMessage))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.Size))
            {
                errorMessage = "Size cannot be null or empty";
                return false;
            }

            if (!product.IsValidSize())
            {
                errorMessage = "Invalid size. Valid sizes are: XS, S, M, L, XL, XXL";
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.Color))
            {
                errorMessage = "Color cannot be null or empty";
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.Material))
            {
                errorMessage = "Material cannot be null or empty";
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.Gender))
            {
                errorMessage = "Gender cannot be null or empty";
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.Season))
            {
                errorMessage = "Season cannot be null or empty";
                return false;
            }

            return true;
        }
    }
}
