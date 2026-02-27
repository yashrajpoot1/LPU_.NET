using System;
using System.Collections.Generic;

namespace FlexibleInventorySystem.Models
{
    /// <summary>
    /// Clothing product class
    /// </summary>
    public class ClothingProduct : Product
    {
        /// <summary>
        /// Clothing size
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// Clothing color
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Material composition
        /// </summary>
        public string Material { get; set; }

        /// <summary>
        /// Target gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Season suitability
        /// </summary>
        public string Season { get; set; }

        private static readonly List<string> ValidSizes = new List<string> { "XS", "S", "M", "L", "XL", "XXL" };

        /// <summary>
        /// Override GetProductDetails for clothing items
        /// </summary>
        /// <returns>Clothing product details</returns>
        public override string GetProductDetails()
        {
            return $"Size: {Size}, Color: {Color}, Material: {Material}, Gender: {Gender}, Season: {Season}";
        }

        /// <summary>
        /// Check if size is available
        /// Valid sizes: XS, S, M, L, XL, XXL
        /// </summary>
        /// <returns>True if size is valid</returns>
        public bool IsValidSize()
        {
            return ValidSizes.Contains(Size?.ToUpper());
        }

        /// <summary>
        /// Override CalculateValue to apply seasonal discount
        /// Apply 15% discount for off-season items
        /// </summary>
        /// <returns>Calculated value with seasonal discount if applicable</returns>
        public override decimal CalculateValue()
        {
            decimal baseValue = base.CalculateValue();
            
            // Determine current season
            int currentMonth = DateTime.Now.Month;
            string currentSeason = GetCurrentSeason(currentMonth);
            
            // Apply 15% discount for off-season items
            if (Season != "All-season" && Season != currentSeason)
            {
                return baseValue * 0.85m; // 15% discount
            }
            
            return baseValue;
        }

        /// <summary>
        /// Helper method to determine current season
        /// </summary>
        private string GetCurrentSeason(int month)
        {
            if (month >= 3 && month <= 5) return "Spring";
            if (month >= 6 && month <= 8) return "Summer";
            if (month >= 9 && month <= 11) return "Fall";
            return "Winter";
        }
    }
}
