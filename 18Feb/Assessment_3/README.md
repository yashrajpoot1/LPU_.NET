# Assessment 3 - HealthSync Advanced Billing System

## 📋 Overview
This assessment contains the HealthSync Advanced Billing System, a comprehensive C# application demonstrating advanced OOP concepts including abstraction, polymorphism, and virtual methods.

## 🎯 Project: HealthSync Advanced Billing

**Location:** `HealthSyncBilling/`

### Key Features
- ✅ Abstract base class (Consultant)
- ✅ Two derived classes (InHouseConsultant, VisitingConsultant)
- ✅ Polymorphic method overriding
- ✅ Virtual method for dynamic taxation
- ✅ Strict ID validation (DR + 4 digits)
- ✅ Multi-tier TDS calculation
- ✅ Comprehensive reporting system

### OOP Concepts Demonstrated

#### 1. Abstraction
```csharp
public abstract class Consultant
{
    public abstract decimal CalculateGrossPayout();
}
```
- Prevents creation of generic consultants
- Forces subclasses to implement payout logic

#### 2. Polymorphism
- In-House: `MonthlyStipend + Allowances + Bonuses`
- Visiting: `ConsultationsCount × RatePerVisit`

#### 3. Virtual Methods
- Default TDS: Sliding scale (5% or 15%)
- Visiting TDS: Flat 10% (overridden)

### Taxation Rules

| Consultant Type | Condition | TDS Rate |
|----------------|-----------|----------|
| In-House | Payout ≤ $5,000 | 5% |
| In-House | Payout > $5,000 | 15% |
| Visiting | Any Amount | 10% (Flat) |

### Sample Scenarios

#### Scenario 1: In-House (High Earner)
```
Input: DR2001, Stipend: $10,000, Allowances: $2,000, Bonuses: $1,000
Gross: $13,000 | TDS: 15% | Net: $11,050
```

#### Scenario 2: Visiting Consultant
```
Input: DR8005, 10 Visits @ $600
Gross: $6,000 | TDS: 10% | Net: $5,400
```

#### Scenario 3: Validation Failure
```
Input: MD1001
Output: Invalid doctor id (Process terminates)
```

## 🚀 How to Run

```bash
cd 18Feb/Assessment_3/HealthSyncBilling
dotnet run
```

## 📁 Project Structure

```
HealthSyncBilling/
├── Models/
│   ├── Consultant.cs (Abstract base)
│   ├── InHouseConsultant.cs
│   └── VisitingConsultant.cs
├── Services/
│   └── BillingService.cs
├── Program.cs
├── HealthSyncBilling.csproj
└── README.md
```

## 🎮 Menu Options

1. Add In-House Consultant
2. Add Visiting Consultant
3. View Consultant Details
4. View All Consultants
5. Generate Summary Report
6. Generate Detailed Report
7. Run Sample Scenarios
8. Exit

## 🧪 Validation Rules

### Valid IDs
- ✅ DR2001
- ✅ DR8005
- ✅ DR0001

### Invalid IDs
- ❌ MD1001 (wrong prefix)
- ❌ DR001 (too short)
- ❌ DRABC1 (non-numeric)

## 📊 Reports

### Summary Report
- Total consultants
- Breakdown by type
- Total gross/TDS/net payout

### Detailed Report
- Individual consultant breakdown
- Formatted table with all details

## 🎓 Learning Objectives

1. ✅ Implement abstraction with abstract classes
2. ✅ Use polymorphism for method overriding
3. ✅ Apply virtual methods for flexible behavior
4. ✅ Implement strict validation rules
5. ✅ Handle complex business logic
6. ✅ Create comprehensive reporting
7. ✅ Use LINQ for data operations
8. ✅ Build user-friendly console UI

## 💡 Technical Highlights

- **Abstraction**: Forces implementation of payout logic
- **Polymorphism**: Different formulas per consultant type
- **Virtual Methods**: Default behavior with override capability
- **Validation**: Regex-like ID validation
- **Business Logic**: Multi-tier taxation system
- **Reporting**: Multiple report types with LINQ

## 📝 Notes

- Sample data is pre-loaded for testing
- All scenarios from the technical report are implemented
- ID validation follows strict format: DR + 4 digits
- TDS calculation uses sliding scale for In-House, flat rate for Visiting
- Net payout automatically calculated after TDS deduction

---

**Created:** 18-Feb-2026  
**Target Framework:** .NET 8.0  
**Purpose:** Demonstrate advanced OOP concepts in C#
