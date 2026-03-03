# Ecommerce Inventory - Simple Clothing Store

A simple ASP.NET Core MVC ecommerce website for clothing with product categories by gender (Men, Women, Kids).

## Features

- **Product Catalog**: Browse products by gender categories
- **Product Details**: View detailed product information with price
- **Shopping Cart**: Add products to cart with session-based storage
- **Wishlist**: Add products to wishlist (placeholder functionality)
- **Responsive Design**: Bootstrap-based responsive UI
- **In-Memory Data**: No database required - uses internal data storage

## Project Structure

- **Controllers**: ProductsController, CartController, HomeController
- **Models**: Product, CartItem, ErrorViewModel
- **Data**: ProductData (in-memory product storage)
- **Views**: Product listing, product details, shopping cart
- **Images**: SVG placeholder images in wwwroot/images/products/

## How to Run

1. Open the project in Visual Studio or your preferred IDE
2. Run the application:
   ```bash
   dotnet run
   ```
3. Navigate to `https://localhost:5001` or the URL shown in the console

## Navigation

- **Home**: Landing page with category cards
- **Clothing Dropdown**: Navigate to Men, Women, Kids, or All Products
- **Cart Icon**: View shopping cart
- **Product Details**: Click "View Details" on any product card

## Product Categories

- **Men's Clothing**: T-Shirts, Jeans, Jackets, Sneakers
- **Women's Clothing**: Dresses, Blouses, Jeans, Heels
- **Kids' Clothing**: T-Shirts, Shorts, Sneakers, Jackets

## Technologies Used

- ASP.NET Core 10.0
- MVC Pattern
- Bootstrap 5
- Session-based cart storage
- SVG placeholder images
