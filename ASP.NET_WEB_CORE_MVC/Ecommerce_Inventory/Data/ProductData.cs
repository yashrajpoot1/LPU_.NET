using Ecommerce_Inventory.Models;

namespace Ecommerce_Inventory.Data
{
    public static class ProductData
    {
        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
                // Men's Clothing
                new Product { Id = 1, Name = "Men's T-Shirt", Description = "Comfortable cotton t-shirt", Price = 29.99m, ImageUrl = "/images/products/men-tshirt.jpg", Category = "Clothing", Gender = "Men" },
                new Product { Id = 2, Name = "Men's Jeans", Description = "Classic blue denim jeans", Price = 59.99m, ImageUrl = "/images/products/men-jeans.jpg", Category = "Clothing", Gender = "Men" },
                new Product { Id = 3, Name = "Men's Jacket", Description = "Stylish leather jacket", Price = 129.99m, ImageUrl = "/images/products/men-jacket.jpg", Category = "Clothing", Gender = "Men" },
                new Product { Id = 4, Name = "Men's Sneakers", Description = "Comfortable running shoes", Price = 79.99m, ImageUrl = "/images/products/men-sneakers.jpg", Category = "Clothing", Gender = "Men" },

                // Women's Clothing
                new Product { Id = 5, Name = "Women's Dress", Description = "Elegant summer dress", Price = 69.99m, ImageUrl = "/images/products/women-dress.jpg", Category = "Clothing", Gender = "Women" },
                new Product { Id = 6, Name = "Women's Blouse", Description = "Silk blouse for office wear", Price = 49.99m, ImageUrl = "/images/products/women-blouse.jpg", Category = "Clothing", Gender = "Women" },
                new Product { Id = 7, Name = "Women's Jeans", Description = "Skinny fit jeans", Price = 54.99m, ImageUrl = "/images/products/women-jeans.jpg", Category = "Clothing", Gender = "Women" },
                new Product { Id = 8, Name = "Women's Heels", Description = "Classic high heels", Price = 89.99m, ImageUrl = "/images/products/women-heels.jpg", Category = "Clothing", Gender = "Women" },

                // Kids' Clothing
                new Product { Id = 9, Name = "Kids T-Shirt", Description = "Colorful cotton t-shirt", Price = 19.99m, ImageUrl = "/images/products/kids-tshirt.jpg", Category = "Clothing", Gender = "Kids" },
                new Product { Id = 10, Name = "Kids Shorts", Description = "Comfortable summer shorts", Price = 24.99m, ImageUrl = "/images/products/kids-shorts.jpg", Category = "Clothing", Gender = "Kids" },
                new Product { Id = 11, Name = "Kids Sneakers", Description = "Fun and colorful sneakers", Price = 39.99m, ImageUrl = "/images/products/kids-sneakers.jpg", Category = "Clothing", Gender = "Kids" },
                new Product { Id = 12, Name = "Kids Jacket", Description = "Warm winter jacket", Price = 59.99m, ImageUrl = "/images/products/kids-jacket.jpg", Category = "Clothing", Gender = "Kids" }
            };
        }
    }
}
