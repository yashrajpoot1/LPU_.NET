# Assessment 1 - Advanced C# OOP Scenarios

This assessment contains 5 comprehensive scenarios demonstrating advanced C# concepts including OOP, LINQ, Collections, Custom Exceptions, Design Patterns, Events & Delegates.

## 📁 Project Structure

### Scenario 1: Smart Banking System
**Location:** `Scenario1_SmartBanking/`

**Features:**
- Abstract class `BankAccount` with derived classes (SavingsAccount, CurrentAccount, LoanAccount)
- Custom Exceptions (InsufficientBalanceException, MinimumBalanceException, InvalidTransactionException)
- Polymorphism for interest calculation
- Transaction history tracking
- Money transfer between accounts
- LINQ queries for reporting

**Run:** `dotnet run --project Scenario1_SmartBanking`

---

### Scenario 2: E-Commerce Order Management
**Location:** `Scenario2_ECommerce/`

**Features:**
- Product, Customer, Order, OrderItem entities
- Custom Exceptions (OutOfStockException, OrderAlreadyShippedException, CustomerBlacklistedException)
- Strategy Pattern for discount calculation (Percentage, Flat, Festival)
- Collections (List, Dictionary)
- LINQ queries for sales analytics
- Order status management

**Run:** `dotnet run --project Scenario2_ECommerce`

---

### Scenario 3: Hospital Management System
**Location:** `Scenario3_Hospital/`

**Features:**
- Inheritance (Person → Doctor, Patient)
- Appointment scheduling with overlap prevention
- IBillable interface for polymorphic billing
- Medical records with Dictionary storage
- Custom Exceptions (DoctorNotAvailableException, InvalidAppointmentException, etc.)
- LINQ reports for hospital analytics

**Run:** `dotnet run --project Scenario3_Hospital`

---

### Scenario 4: Online Learning Platform
**Location:** `Scenario4_OnlineLearning/`

**Features:**
- Generic Repository Pattern (IRepository<T>)
- IComparable implementation for custom sorting
- Course enrollment with capacity management
- Assignment submission with deadline validation
- LINQ Join operations
- Custom Exceptions (DuplicateEnrollmentException, CourseCapacityExceededException, LateSubmissionException)

**Run:** `dotnet run --project Scenario4_OnlineLearning`

---

### Scenario 5: Stock Trading Portfolio System
**Location:** `Scenario5_StockTrading/`

**Features:**
- Events & Delegates for stock price change notifications
- Strategy Pattern for risk calculation (Conservative, Aggressive)
- Portfolio management with P&L tracking
- LINQ Aggregate for complex calculations
- Custom Exception (InvalidTradeException)
- Transaction history by stock (Dictionary<string, List<Transaction>>)

**Run:** `dotnet run --project Scenario5_StockTrading`

---

## 🎯 Key Concepts Covered

### OOP Concepts
- ✅ Abstraction (Abstract classes and interfaces)
- ✅ Inheritance (Base and derived classes)
- ✅ Polymorphism (Method overriding, interface implementation)
- ✅ Encapsulation (Properties, access modifiers)

### Advanced Features
- ✅ Custom Exceptions
- ✅ Events & Delegates
- ✅ Design Patterns (Strategy, Repository)
- ✅ Generic Programming
- ✅ IComparable interface

### Collections
- ✅ List<T>
- ✅ Dictionary<TKey, TValue>
- ✅ IEnumerable<T>

### LINQ Operations
- ✅ Where, Select, OrderBy, GroupBy
- ✅ Sum, Average, Count, Take
- ✅ Join operations
- ✅ Aggregate functions
- ✅ Anonymous types

## 🚀 How to Run

### Run Individual Scenario
```bash
cd 18Feb/Assessment_1
dotnet run --project Scenario1_SmartBanking
dotnet run --project Scenario2_ECommerce
dotnet run --project Scenario3_Hospital
dotnet run --project Scenario4_OnlineLearning
dotnet run --project Scenario5_StockTrading
```

### Build All Projects
```bash
cd 18Feb/Assessment_1
dotnet build Scenario1_SmartBanking
dotnet build Scenario2_ECommerce
dotnet build Scenario3_Hospital
dotnet build Scenario4_OnlineLearning
dotnet build Scenario5_StockTrading
```

## 📊 Assessment Marking Scheme

| Criteria | Points |
|----------|--------|
| OOP Implementation (Abstraction, Inheritance, Polymorphism) | 20 |
| Custom Exceptions & Error Handling | 15 |
| Collections Usage (List, Dictionary) | 10 |
| LINQ Queries (Minimum 6 per scenario) | 20 |
| Design Patterns Implementation | 15 |
| Code Quality & Best Practices | 10 |
| Menu-Driven UI & User Experience | 10 |
| **Total** | **100** |

## 🔥 Super Advanced Add-Ons (Optional)

For corporate-level implementation, consider adding:
- ✅ Layered Architecture (Presentation, Business, Data layers)
- ✅ Logging (using ILogger or Serilog)
- ✅ JSON Serialization for file persistence
- ✅ Async/Await for async operations
- ✅ Unit Testing (NUnit/xUnit)
- ✅ SOLID Principles
- ✅ Dependency Injection

## 📝 Notes

- All scenarios include comprehensive error handling
- Menu-driven console UI for easy interaction
- Sample data pre-loaded for testing
- LINQ reports demonstrate various query operations
- Code follows C# naming conventions and best practices

---

**Created:** 18-Feb-2026  
**Target Framework:** .NET 8.0
