# Flexible Inventory Management System

## 📋 Overview
A comprehensive inventory management system built with C# that demonstrates advanced OOP concepts, LINQ operations, and software design principles.

## 🎯 Features Implemented

### Core Functionality
- ✅ Abstract Product base class with inheritance
- ✅ Three product types: Electronics, Groceries, Clothing
- ✅ Complete CRUD operations (Create, Read, Update, Delete)
- ✅ Thread-safe inventory management
- ✅ Custom exception handling
- ✅ Input validation for all product types

### Product Types

#### Electronic Products
- Brand, Warranty, Voltage specifications
- Warranty expiry tracking
- Refurbished product support

#### Grocery Products
- Expiry date tracking
- Perishable/Non-perishable classification
- Weight and storage temperature
- Automatic discount for near-expiry items (20% off within 3 days)

#### Clothing Products
- Size validation (XS, S, M, L, XL, XXL)
- Color, Material, Gender, Season attributes
- Seasonal discount (15% off for off-season items)

### Reporting Features
1. **Complete Inventory Report** - Full product listing with values
2. **Category Summary** - Products grouped by category
3. **Value Report** - Statistical analysis (most/least valuable, average, median)
4. **Expiry Report** - Grocery products expiring soon

### Advanced Features
- ✅ LINQ queries for data filtering and analysis
- ✅ Custom search with predicates
- ✅ Category-based discount application
- ✅ Low stock alerts
- ✅ Thread-safe operations with locking
- ✅ Polymorphic value calculation

## 📁 Project Structure

```
FlexibleInventorySystem/
├── Models/
│   ├── Product.cs (Abstract base class)
│   ├── ElectronicProduct.cs
│   ├── GroceryProduct.cs
│   └── ClothingProduct.cs
├── Interfaces/
│   ├── IInventoryOperations.cs
│   └── IReportGenerator.cs
├── Services/
│   └── InventoryManager.cs (Main business logic)
├── Exceptions/
│   └── InventoryException.cs (Custom exception)
├── Utilities/
│   └── ProductValidator.cs (Validation logic)
├── Program.cs (Console UI)
├── FlexibleInventorySystem.csproj
└── README.md
```

## 🚀 How to Run

### Prerequisites
- .NET 8.0 SDK or later

### Running the Application
```bash
cd 18Feb/Assessment_2/FlexibleInventorySystem
dotnet run
```

### Building the Application
```bash
dotnet build
```

## 🎮 Usage Guide

### Main Menu Options
1. **Add Product** - Add new products (Electronics, Groceries, Clothing)
2. **Remove Product** - Remove product by ID
3. **Update Quantity** - Modify product quantity
4. **Find Product** - Search for specific product
5. **View All Products** - Display complete inventory
6. **Generate Reports** - Access various reports
7. **Check Low Stock** - View products below threshold
8. **View Products by Category** - Filter by category
9. **Exit** - Close application

### Sample Data
The system comes pre-loaded with sample data:
- 2 Electronic products (Laptop, Smartphone)
- 2 Grocery products (Milk, Bread)
- 2 Clothing products (T-Shirt, Jacket)

## 🧪 Testing Examples

### Adding a Product
```
Select Product Type: 1 (Electronic)
Enter Product ID: E003
Enter Product Name: Wireless Mouse
Enter Price: 29.99
Enter Quantity: 50
Enter Brand: Logitech
Enter Warranty (months): 12
Enter Voltage: 5V
Is Refurbished? (y/n): n
```

### Checking Low Stock
```
Enter threshold quantity: 20
Result: Shows all products with quantity < 20
```

### Generating Reports
```
Select report type:
1. Complete Inventory Report
2. Category Summary
3. Value Report
4. Expiry Report (for groceries)
```

## 🔧 Technical Implementation

### OOP Concepts
- **Abstraction**: Abstract Product class with abstract methods
- **Inheritance**: ElectronicProduct, GroceryProduct, ClothingProduct inherit from Product
- **Polymorphism**: Overridden CalculateValue() and GetProductDetails() methods
- **Encapsulation**: Private fields with public properties

### Design Patterns
- **Repository Pattern**: InventoryManager acts as repository
- **Strategy Pattern**: Different value calculation strategies per product type
- **Interface Segregation**: Separate interfaces for operations and reporting

### LINQ Operations Used
- `Where()` - Filtering products
- `Select()` - Projecting data
- `OrderBy()` / `OrderByDescending()` - Sorting
- `GroupBy()` - Grouping by category
- `Sum()` - Calculating totals
- `Average()` - Statistical calculations
- `FirstOrDefault()` - Finding single items
- `Any()` - Existence checks
- `OfType<T>()` - Type filtering

### Exception Handling
- Custom `InventoryException` with error codes
- Validation exceptions for invalid data
- Graceful error handling in UI

### Thread Safety
- Lock object for concurrent access protection
- Thread-safe collection operations

## 📊 Validation Rules

### All Products
- ID cannot be null or empty
- Name cannot be null or empty
- Price must be > 0
- Quantity must be >= 0
- Category cannot be null or empty

### Electronic Products
- Brand cannot be null or empty
- Warranty months cannot be negative
- Voltage specification required

### Grocery Products
- Expiry date must be set
- Weight must be > 0
- Storage temperature required

### Clothing Products
- Size must be valid (XS, S, M, L, XL, XXL)
- Color, Material, Gender, Season required

## 🎓 Learning Outcomes

This project demonstrates:
1. ✅ Object-Oriented Programming principles
2. ✅ Interface implementation
3. ✅ Abstract classes and inheritance
4. ✅ LINQ for data manipulation
5. ✅ Exception handling
6. ✅ Input validation
7. ✅ Collections and generics
8. ✅ Thread safety
9. ✅ Console application development
10. ✅ SOLID principles

## 📝 Assumptions Made
- Product IDs are unique and case-sensitive
- Category names are case-insensitive
- Dates are in dd-MM-yyyy format
- Prices are in local currency
- Thread safety is implemented for multi-threaded scenarios
- Sample data is loaded automatically on startup

## 🔮 Future Enhancements
- File I/O for data persistence (JSON/XML)
- Database integration
- Unit testing with xUnit/NUnit
- Web API interface
- Advanced search filters
- Barcode scanning support
- Multi-language support
- Export reports to PDF/Excel

## 👨‍💻 Author
Created as part of C# Programming Assessment

## 📅 Date
18-Feb-2026

---

**Note**: This is a complete implementation of all TODO items from the assignment specification.
