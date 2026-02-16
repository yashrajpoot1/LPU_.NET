# Scenario-Based Generics & Collections Assessment
**Date: 16-02-26**

## 📋 Overview

This project contains comprehensive implementations of four real-world scenarios demonstrating advanced C# generics, collections, and design patterns. Each scenario is designed to test different levels of expertise from Junior to Senior/Architect level.

## 🎯 Scenarios Implemented

### Scenario 1: E-Commerce Inventory System (Junior Level)
**File:** `Scenario1_ECommerce.cs`

**Key Features:**
- Generic repository pattern with `ProductRepository<T>`
- Product hierarchy with interfaces
- Discounted product wrapper using generics
- Inventory management with bulk operations
- Validation and error handling

**Concepts Demonstrated:**
- `where T : class, IProduct` constraint
- List<T> and Dictionary<TKey, TValue>
- LINQ queries for filtering
- Lambda expressions for predicates
- Repository pattern

**Sample Output:**
```
✓ Added: [Electronics] iPhone 15 Pro by Apple - $999.99
✓ Added: [Electronics] Samsung 4K TV by Samsung - $799.99
Total Value: $4425.95
```

---

### Scenario 2: University Course Registration System (Mid-Level)
**File:** `Scenario2_University.cs`

**Key Features:**
- Multiple generic constraints (`TStudent`, `TCourse`)
- Enrollment system with capacity and prerequisite validation
- Generic gradebook with GPA calculation
- Grade distribution analysis
- Immutable return types

**Concepts Demonstrated:**
- Multiple `where` constraints
- Dictionary with tuple keys
- IReadOnlyList for immutability
- Complex validation logic
- Weighted calculations

**Sample Output:**
```
✓ Successfully enrolled Alice Johnson in Advanced Algorithms Lab
✗ Enrollment failed: Bob Smith (Semester 3) doesn't meet prerequisite (Semester 4)
Alice Johnson: GPA = 3.75/4.0
```

---

### Scenario 3: Hospital Patient Management System (Mid-Level)
**File:** `Scenario3_Hospital.cs`

**Key Features:**
- Priority queue with SortedDictionary
- Generic medical records
- Type-specific medication validation
- Drug interaction checking
- Age-based patient categorization

**Concepts Demonstrated:**
- SortedDictionary<int, Queue<T>>
- Type-specific validation with pattern matching
- Func<T, bool> delegates
- DateTime handling
- Priority-based processing

**Sample Output:**
```
✓ Enqueued Robert Davis with priority 1
✓ Prescribed Amoxicillin (250mg twice daily) to Emma Wilson
⚠ WARNING: Warfarin interacts with: Aspirin
```

---

### Scenario 4: Financial Trading Platform (Senior/Architect Level)
**File:** `Scenario4_Trading.cs`

**Key Features:**
- Generic portfolio management
- Trading strategy pattern
- Price history tracking with moving averages
- Trend detection algorithms
- Risk metrics calculation
- Profit/loss tracking

**Concepts Demonstrated:**
- Strategy pattern with generics
- Complex calculations (weighted averages, volatility)
- Trend analysis algorithms
- Performance optimization
- Financial domain modeling

**Sample Output:**
```
✓ Bought 50 units of AAPL at $170.00
Top Performer: AAPL
Return: 3.24%
Trend: Upward
```

---

## 🏗️ Architecture & Design Patterns

### Design Patterns Used

1. **Repository Pattern**
   - `ProductRepository<T>` in Scenario 1
   - Encapsulates data access logic
   - Provides clean API for CRUD operations

2. **Strategy Pattern**
   - `TradingStrategy<T>` in Scenario 4
   - Allows runtime algorithm selection
   - Uses Func<T, bool> for conditions

3. **Wrapper Pattern**
   - `DiscountedProduct<T>` in Scenario 1
   - Adds functionality without modifying original

4. **Factory Pattern**
   - Patient type creation in Scenario 3
   - Instrument creation in Scenario 4

### Generic Constraints Used

```csharp
// Class constraint
where T : class

// Interface constraint
where T : IProduct
where T : IStudent
where T : IPatient
where T : IFinancialInstrument

// Multiple constraints
where TStudent : IStudent
where TCourse : ICourse

// New constraint
where T : class, new()
```

### Collection Choices

| Collection | Use Case | Scenario |
|------------|----------|----------|
| `List<T>` | Ordered storage, indexed access | All scenarios |
| `Dictionary<TKey, TValue>` | Fast lookups by key | All scenarios |
| `SortedDictionary<TKey, TValue>` | Priority queue | Scenario 3 |
| `Queue<T>` | FIFO processing | Scenario 3 |
| `HashSet<T>` | Unique items | Potential optimization |

---

## 🚀 Running the Project

### Prerequisites
- .NET 8.0 SDK or later
- Visual Studio 2022, VS Code, or any C# IDE

### Build and Run

```bash
# Navigate to project directory
cd 16-02-26

# Build the project
dotnet build

# Run the project
dotnet run
```

### Interactive Menu

The program provides an interactive menu:

```
1. Scenario 1: E-Commerce Inventory System
2. Scenario 2: University Course Registration
3. Scenario 3: Hospital Patient Management
4. Scenario 4: Financial Trading Platform
5. Run All Scenarios
6. Show Assessment Criteria
0. Exit
```

---

## 📊 Assessment Criteria

### Evaluation Weights

| Criterion | Weight | What's Evaluated |
|-----------|--------|------------------|
| Type Safety | 25% | Proper constraints, no unsafe casts |
| Collection Choice | 20% | Appropriate data structures |
| Error Handling | 15% | Validation, meaningful exceptions |
| Design Patterns | 15% | Factory, Strategy, Repository |
| Performance | 10% | Time/space complexity |
| Code Clarity | 10% | Readable, well-documented |
| Testing | 5% | Edge cases covered |

### Key Features Implemented

✅ Type-safe generic implementations  
✅ Comprehensive error handling  
✅ Input validation at all levels  
✅ XML documentation comments  
✅ LINQ queries for data manipulation  
✅ Lambda expressions for delegates  
✅ Proper encapsulation  
✅ Immutable return types (IReadOnlyList)  
✅ Design patterns (Repository, Strategy, Factory)  
✅ Performance-optimized collection choices  

---

## 🧪 Testing & Validation

### Validation Implemented

1. **Null Checks**
   - All public methods validate null parameters
   - Throws `ArgumentNullException` with descriptive messages

2. **Range Validation**
   - Prices must be positive
   - Quantities must be positive
   - Grades must be 0-100
   - Priority must be 1-5

3. **Business Rules**
   - Unique IDs in repositories
   - Course capacity limits
   - Prerequisite semester requirements
   - Drug interaction checks

4. **Edge Cases**
   - Empty collections
   - Division by zero prevention
   - Date validation (no future dates)
   - Duplicate enrollment prevention

### Error Handling Examples

```csharp
// Validation with meaningful exceptions
if (product.Price <= 0)
    throw new ArgumentException("Price must be positive", nameof(product));

// Business rule validation
if (_enrollments[course].Count >= course.MaxCapacity)
{
    Console.WriteLine($"✗ Enrollment failed: {course.Title} is at full capacity");
    return false;
}

// Try-catch for graceful degradation
try
{
    gradeBook.AddGrade(student, course, 105);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"✗ {ex.Message}");
}
```

---

## 💡 Advanced Concepts Demonstrated

### 1. Covariance & Contravariance
```csharp
// Demonstrated in interface design
public interface IProcessor<in TInput, out TOutput>
{
    TOutput Process(TInput input);
}
```

### 2. Generic Caching
```csharp
// Potential extension
public class Cache<TKey, TValue> where TValue : class
{
    private ConcurrentDictionary<TKey, TValue> _cache = new();
    // Implementation with expiration logic
}
```

### 3. Event Aggregator Pattern
```csharp
// Potential extension
public class EventAggregator
{
    private Dictionary<Type, List<object>> _handlers = new();
    public void Subscribe<TEvent>(Action<TEvent> handler) { }
    public void Publish<TEvent>(TEvent event) { }
}
```

---

## 📈 Performance Considerations

### Collection Performance

| Operation | List<T> | Dictionary<TKey, TValue> | SortedDictionary |
|-----------|---------|--------------------------|------------------|
| Add | O(1) | O(1) | O(log n) |
| Remove | O(n) | O(1) | O(log n) |
| Search | O(n) | O(1) | O(log n) |
| Iterate | O(n) | O(n) | O(n) |

### Optimizations Implemented

1. **Dictionary for Fast Lookups**
   - Used for enrollments, medications, holdings
   - O(1) average case for key-based access

2. **LINQ Deferred Execution**
   - `IEnumerable<T>` for lazy evaluation
   - Reduces memory footprint

3. **Capacity Pre-allocation**
   - Could be added: `new List<T>(capacity)`
   - Reduces array resizing

4. **Immutable Returns**
   - `IReadOnlyList<T>` prevents external modification
   - Maintains data integrity

---

## 🎓 Learning Outcomes

After completing this assessment, you will understand:

1. **Generic Constraints**
   - When and how to use different constraint types
   - Multiple constraint combinations
   - Constraint inheritance

2. **Collection Selection**
   - Choosing the right collection for the use case
   - Performance implications
   - Thread-safety considerations

3. **Design Patterns**
   - Repository pattern for data access
   - Strategy pattern for algorithms
   - Wrapper pattern for extending functionality

4. **Error Handling**
   - Validation strategies
   - Exception types and usage
   - Graceful degradation

5. **Real-World Modeling**
   - Domain-driven design
   - Business rule implementation
   - Type safety in complex systems

---

## 📝 Code Quality

### Documentation
- XML comments on all public members
- Clear parameter descriptions
- Return value documentation
- Exception documentation

### Naming Conventions
- PascalCase for classes, methods, properties
- camelCase for local variables
- Descriptive names (no abbreviations)
- Consistent prefixes (_field, parameter)

### Code Organization
- One class per file (except related types)
- Logical grouping with regions
- Consistent formatting
- Clear separation of concerns

---

## 🔍 Sample Interview Questions

1. **How would you modify Scenario 1 to support batch operations?**
   - Add `AddProducts(IEnumerable<T> products)` method
   - Use transactions for atomicity
   - Implement rollback on validation failure

2. **What collection would you choose for real-time price updates and why?**
   - `ConcurrentDictionary<TKey, TValue>` for thread-safety
   - Lock-free reads for performance
   - Atomic updates for consistency

3. **How would you make the medical system thread-safe?**
   - Use `ConcurrentDictionary` for shared state
   - Implement locking for critical sections
   - Consider async/await for I/O operations

4. **Implement a generic extension method for these scenarios**
   ```csharp
   public static class EnumerableExtensions
   {
       public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source)
           where T : class
       {
           return source.Where(item => item != null)!;
       }
   }
   ```

5. **How would you serialize/deserialize these generic collections?**
   - Use `System.Text.Json` for JSON serialization
   - Implement custom converters for complex types
   - Handle circular references

---

## 🚧 Future Enhancements

1. **Unit Tests**
   - Add xUnit/NUnit test projects
   - Test edge cases and validation
   - Mock dependencies

2. **Async/Await**
   - Convert I/O operations to async
   - Implement async repository methods
   - Use `Task<T>` for long-running operations

3. **Dependency Injection**
   - Use IServiceCollection
   - Register repositories as services
   - Implement interfaces for testability

4. **Database Integration**
   - Add Entity Framework Core
   - Implement actual persistence
   - Use migrations for schema management

5. **API Layer**
   - Create ASP.NET Core Web API
   - Expose scenarios as REST endpoints
   - Add Swagger documentation

---

## 📚 References

- [Microsoft Docs: Generics](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/)
- [Microsoft Docs: Collections](https://docs.microsoft.com/en-us/dotnet/standard/collections/)
- [Design Patterns in C#](https://refactoring.guru/design-patterns/csharp)
- [LINQ Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)

---

## ✅ Completion Checklist

- [x] Scenario 1: E-Commerce Inventory System
- [x] Scenario 2: University Course Registration
- [x] Scenario 3: Hospital Patient Management
- [x] Scenario 4: Financial Trading Platform
- [x] Interactive menu system
- [x] Comprehensive error handling
- [x] XML documentation
- [x] Design patterns implementation
- [x] Performance optimization
- [x] Assessment criteria display
- [x] README documentation

---

## 👨‍💻 Author

**Assessment Date:** 16-02-26  
**Framework:** .NET 8.0  
**Language:** C# 12  

---

## 📄 License

This project is for educational purposes as part of the LPU Capgemini training program.
