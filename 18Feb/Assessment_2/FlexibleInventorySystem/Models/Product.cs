using System;

namespace FlexibleInventorySystem.Models
{
    /// <summary>
    /// Abstract base class for all products
    /// </summary>
    public abstract class Product
    {
        /// <summary>
        /// Unique product identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Available quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Product category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Date when product was added to inventory
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// Constructor to initialize DateAdded
        /// </summary>
        public Product()
        {
            DateAdded = DateTime.Now;
        }

        /// <summary>
        /// Abstract method to get product-specific details
        /// </summary>
        /// <returns>Product-specific information</returns>
        public abstract string GetProductDetails();

        /// <summary>
        /// Virtual method to calculate inventory value
        /// Default: Price * Quantity
        /// </summary>
        /// <returns>Total value of product inventory</returns>
        public virtual decimal CalculateValue()
        {
            return Price * Quantity;
        }

        /// <summary>
        /// Override ToString() to return product summary
        /// </summary>
        /// <returns>Formatted product summary</returns>
        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Price: {Price:C}, Quantity: {Quantity}";
        }
    }
}
