# HealthSync Advanced Billing System

## 📋 Executive Summary
The HealthSync Advanced Billing System is a C# application designed for Machine Masters to automate medical consultant payouts. The system utilizes Object-Oriented Programming (OOP) to handle complex payroll logic, multi-tier taxation (TDS), and strict data validation for two distinct employee categories: In-House and Visiting Consultants.

## 🏗️ System Architecture

### A. Abstraction (The Consultant Class)
- **Abstract base class** acts as a blueprint
- Prevents creation of "generic" consultants
- Every consultant must be either In-House or Visiting
- **Key Feature**: `CalculateGrossPayout()` method is abstract, forcing subclasses to define their own financial math

### B. Polymorphism (Method Overriding)
The system uses polymorphism to handle specialized payout formulas:
- **In-House**: Uses formula involving `MonthlyStipend + Allowances + Bonuses`
- **Visiting**: Uses formula based on `ConsultationsCount × RatePerVisit`

### C. Virtual Logic (Dynamic Taxation)
Virtual method for TDS (Tax Deducted at Source) calculation:
- **Default Logic**: Sliding scale (5% or 15%) based on earnings
- **Overridden Logic**: Visiting consultants use flat 10% rate

## 📊 Functional Specifications

### Validation Rules
All IDs must pass the `ValidateConsultantId()` check:
- **Length**: Exactly 6 characters
- **Prefix**: Must start with "DR"
- **Format**: Last 4 characters must be numeric

### Taxation Table

| Consultant Type | Condition | Applied Rate |
|----------------|-----------|--------------|
| In-House | Payout ≤ 5000 | 5% |
| In-House | Payout > 5000 | 15% |
| Visiting | Any Amount | 10% (Flat) |

## 🎯 Sample Output Scenarios

### Scenario 1: In-House Consultant (High Earner)
```
Input: ID: DR2001, Stipend: 10000, Allowances: 2000, Bonuses: 1000
Process: Gross = 10000 + 2000 + 1000 = 13000. Tax = 15%
Output: Gross: 13000.00 | TDS Applied: 15% | Net Payout: 11050.00
```

### Scenario 2: Visiting Consultant
```
Input: ID: DR8005, 10 Visits @ 600
Process: Gross = 10 × 600 = 6000. Tax = 10% (Flat)
Output: Gross: 6000.00 | TDS Applied: 10% | Net Payout: 5400.00
```

### Scenario 3: Validation Failure
```
Input: ID: MD1001
Output: Invalid doctor id (Process terminates)
```

### Additional Scenario: In-House Consultant (Low Earner)
```
Input: ID: DR3002, Stipend: 3000, Allowances: 500, Bonuses: 200
Process: Gross = 3000 + 500 + 200 = 3700. Tax = 5%
Output: Gross: 3700.00 | TDS Applied: 5% | Net Payout: 3515.00
```

## 📁 Project Structure

```
HealthSyncBilling/
├── Models/
│   ├── Consultant.cs (Abstract base class)
│   ├── InHouseConsultant.cs (Derived class)
│   └── VisitingConsultant.cs (Derived class)
├── Services/
│   └── BillingService.cs (Business logic)
├── Program.cs (Console UI)
├── HealthSyncBilling.csproj
└── README.md
```

## 🚀 How to Run

### Prerequisites
- .NET 8.0 SDK or later

### Running the Application
```bash
cd 18Feb/Assessment_3/HealthSyncBilling
dotnet run
```

### Building the Application
```bash
dotnet build
```

## 🎮 Features

### Main Menu Options
1. **Add In-House Consultant** - Register new in-house consultant
2. **Add Visiting Consultant** - Register new visiting consultant
3. **View Consultant Details** - View specific consultant information
4. **View All Consultants** - List all registered consultants
5. **Generate Summary Report** - Overall billing summary
6. **Generate Detailed Report** - Detailed payout breakdown
7. **Run Sample Scenarios** - Demonstrate all test scenarios
8. **Exit** - Close application

### Key Features
- ✅ Abstract base class implementation
- ✅ Polymorphic method overriding
- ✅ Virtual method for dynamic taxation
- ✅ Strict ID validation (DR + 4 digits)
- ✅ Multi-tier TDS calculation
- ✅ Comprehensive reporting
- ✅ Sample scenarios pre-loaded

## 🔧 Technical Implementation

### OOP Concepts Demonstrated

#### 1. Abstraction
```csharp
public abstract class Consultant
{
    public abstract decimal CalculateGrossPayout();
}
```
- Forces subclasses to implement their own payout logic
- Prevents instantiation of generic consultants

#### 2. Polymorphism
```csharp
// In-House Implementation
public override decimal CalculateGrossPayout()
{
    return MonthlyStipend + Allowances + Bonuses;
}

// Visiting Implementation
public override decimal CalculateGrossPayout()
{
    return ConsultationsCount * RatePerVisit;
}
```

#### 3. Virtual Methods
```csharp
// Base class default behavior
public virtual decimal CalculateTDS(decimal grossPayout)
{
    return grossPayout <= 5000 ? 5m : 15m;
}

// Visiting consultant overrides
public override decimal CalculateTDS(decimal grossPayout)
{
    return 10m; // Flat rate
}
```

### Validation Logic
```csharp
public static bool ValidateConsultantId(string consultantId)
{
    // Length check: exactly 6 characters
    if (consultantId.Length != 6) return false;
    
    // Prefix check: must start with "DR"
    if (!consultantId.StartsWith("DR")) return false;
    
    // Numeric check: last 4 characters must be digits
    string numericPart = consultantId.Substring(2);
    return int.TryParse(numericPart, out _);
}
```

## 📊 Calculation Examples

### In-House Consultant (High Earner)
```
Monthly Stipend: $10,000
Allowances: $2,000
Bonuses: $1,000
─────────────────────────
Gross Payout: $13,000
TDS (15%): $1,950
─────────────────────────
Net Payout: $11,050
```

### Visiting Consultant
```
Consultations: 10
Rate Per Visit: $600
─────────────────────────
Gross Payout: $6,000
TDS (10%): $600
─────────────────────────
Net Payout: $5,400
```

### In-House Consultant (Low Earner)
```
Monthly Stipend: $3,000
Allowances: $500
Bonuses: $200
─────────────────────────
Gross Payout: $3,700
TDS (5%): $185
─────────────────────────
Net Payout: $3,515
```

## 🧪 Testing

### Valid IDs
- ✅ DR2001
- ✅ DR8005
- ✅ DR3002
- ✅ DR0001
- ✅ DR9999

### Invalid IDs
- ❌ MD1001 (wrong prefix)
- ❌ DR001 (too short)
- ❌ DR20011 (too long)
- ❌ DRABC1 (non-numeric)
- ❌ 2001DR (wrong format)

## 📈 Reports Available

### 1. Summary Report
- Total consultants count
- Breakdown by type (In-House vs Visiting)
- Total gross payout
- Total TDS collected
- Total net payout

### 2. Detailed Report
- Individual consultant breakdown
- ID, Name, Type, Gross, TDS%, Net
- Formatted table view

## 🎓 Learning Outcomes

This project demonstrates:
1. ✅ **Abstraction** - Abstract base classes
2. ✅ **Polymorphism** - Method overriding
3. ✅ **Virtual Methods** - Default behavior with override capability
4. ✅ **Encapsulation** - Properties and methods
5. ✅ **Validation** - Input validation logic
6. ✅ **Business Logic** - Complex financial calculations
7. ✅ **Exception Handling** - ArgumentException for validation
8. ✅ **LINQ** - Data querying and filtering
9. ✅ **Collections** - List management
10. ✅ **Console UI** - User-friendly interface

## 💡 Key Design Decisions

### Why Abstract Class?
- Prevents creation of generic consultants
- Forces implementation of payout logic
- Provides common functionality (validation, net calculation)

### Why Virtual TDS Method?
- Allows default behavior (sliding scale)
- Enables subclasses to override (flat rate for visiting)
- Demonstrates flexibility in OOP design

### Why Separate Service Class?
- Separates business logic from data models
- Enables easier testing and maintenance
- Follows Single Responsibility Principle

## 🔮 Future Enhancements
- Database integration for persistence
- Export reports to PDF/Excel
- Historical payout tracking
- Multi-month billing cycles
- Email notifications
- Web API interface
- Unit testing suite

## 👨‍💻 Author
Created for Machine Masters - HealthSync Billing System

## 📅 Date
18-Feb-2026

---

**Note**: This implementation follows all specifications from the technical project report and demonstrates advanced OOP concepts in C#.
