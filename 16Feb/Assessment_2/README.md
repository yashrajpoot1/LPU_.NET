# Assessment 2: C# Collections & Generics Challenges

This folder contains implementations of various C# collections and generics challenges, demonstrating advanced programming concepts.

## 📁 Project Structure

### Basic Challenges (Completed)

#### Challenge 1: Hospital Patient Management System
**Location:** `Challenge1_HospitalPatientManagement/`

**Concepts Covered:**
- Dictionary<int, Patient> for patient storage
- Queue<Patient> for appointment scheduling
- List<string> for medical history
- LINQ queries for filtering patients
- Proper encapsulation and data management

**Key Features:**
- Patient registration with unique IDs
- Appointment queue management (FIFO)
- Medical history tracking
- Condition-based patient search
- Age range queries

**Run:**
```bash
cd Challenge1_HospitalPatientManagement
dotnet run
```

---

#### Challenge 2: Library Book Management System
**Location:** `Challenge2_LibraryBookManagement/`

**Concepts Covered:**
- Generic Catalog<T> class with constraints
- HashSet<string> for ISBN uniqueness
- SortedDictionary<string, List<T>> for genre indexing
- Custom indexers for genre-based access
- LINQ with lambda expressions

**Key Features:**
- Generic catalog supporting any book type
- Automatic ISBN duplicate detection
- Genre-based book organization
- Advanced search with predicates
- Availability tracking

**Run:**
```bash
cd Challenge2_LibraryBookManagement
dotnet run
```

---

#### Challenge 3: E-commerce Shopping Cart
**Location:** `Challenge3_EcommerceShoppingCart/`

**Concepts Covered:**
- Generic ShoppingCart<T> with product constraints
- Dictionary<T, int> for cart items
- Func<T, double, double> delegates for discount strategies
- LINQ for analytics and sorting
- Abstract base classes

**Key Features:**
- Type-safe cart for different product categories
- Flexible discount calculation using delegates
- Multiple discount strategies (percentage, tiered, bulk)
- Top expensive items retrieval
- Cart analytics and summaries

**Run:**
```bash
cd Challenge3_EcommerceShoppingCart
dotnet run
```

---

#### Challenge 4: Tournament Ranking System
**Location:** `Challenge4_TournamentRanking/`

**Concepts Covered:**
- IComparable<T> for custom sorting
- LinkedList<Match> for match scheduling
- Stack<Match> for undo functionality
- SortedList concepts for rankings
- Complex comparison logic

**Key Features:**
- Automatic ranking based on points and goal difference
- Match scheduling with LinkedList
- Undo functionality for match results
- Comprehensive team statistics
- Tournament standings display

**Run:**
```bash
cd Challenge4_TournamentRanking
dotnet run
```

---

## 🎯 Learning Objectives

### Collections Mastery
- **List<T>**: Dynamic arrays with LINQ support
- **Dictionary<TKey, TValue>**: Fast key-based lookups
- **Queue<T>**: FIFO data structure
- **Stack<T>**: LIFO data structure with undo patterns
- **HashSet<T>**: Unique element collections
- **LinkedList<T>**: Efficient insertion/deletion
- **SortedDictionary<TKey, TValue>**: Ordered key-value pairs

### Generics Concepts
- Generic classes with type constraints
- Generic methods and delegates
- Covariance and contravariance basics
- Type safety and reusability

### LINQ Techniques
- Where, Select, OrderBy, Take
- Lambda expressions
- Method chaining
- Aggregate operations
- Custom predicates

### Design Patterns
- Repository pattern (Challenge 1)
- Strategy pattern with delegates (Challenge 3)
- Command pattern with undo (Challenge 4)
- Generic catalog pattern (Challenge 2)

---

## 🚀 Running All Projects

To build and run all challenges:

```bash
# From Assessment_2 directory
dotnet build Challenge1_HospitalPatientManagement
dotnet build Challenge2_LibraryBookManagement
dotnet build Challenge3_EcommerceShoppingCart
dotnet build Challenge4_TournamentRanking

# Run each
dotnet run --project Challenge1_HospitalPatientManagement
dotnet run --project Challenge2_LibraryBookManagement
dotnet run --project Challenge3_EcommerceShoppingCart
dotnet run --project Challenge4_TournamentRanking
```

---

## 📊 Difficulty Progression

| Level | Focus Area | Key Concepts |
|-------|-----------|--------------|
| **Beginner** | Basic collections | List<T>, Dictionary<TKey,TValue>, foreach loops |
| **Intermediate** | LINQ & Delegates | Where, Select, OrderBy, lambda expressions |
| **Advanced** | Custom Collections | Sorted collections, custom comparers, generics constraints |
| **Expert** | Performance & Thread Safety | Concurrent collections, IEnumerable vs IQueryable |

---

## 💡 Key Takeaways

1. **Choose the Right Collection:**
   - Use `List<T>` for ordered, indexed access
   - Use `Dictionary<TKey, TValue>` for fast key lookups
   - Use `HashSet<T>` for uniqueness checks
   - Use `Queue<T>` for FIFO operations
   - Use `Stack<T>` for LIFO operations

2. **Generics Benefits:**
   - Type safety at compile time
   - Code reusability
   - Better performance (no boxing/unboxing)
   - Cleaner, more maintainable code

3. **LINQ Power:**
   - Declarative query syntax
   - Lazy evaluation for performance
   - Composable operations
   - Works with any IEnumerable<T>

4. **Delegates for Flexibility:**
   - Func<T, TResult> for transformations
   - Action<T> for operations
   - Predicate<T> for filtering
   - Custom delegates for specific scenarios

---

## 🎓 Next Steps

After mastering these challenges, explore:

1. **Thread-Safe Collections:**
   - ConcurrentDictionary<TKey, TValue>
   - ConcurrentQueue<T>
   - BlockingCollection<T>

2. **Immutable Collections:**
   - ImmutableList<T>
   - ImmutableDictionary<TKey, TValue>
   - Functional programming patterns

3. **Custom Collections:**
   - Implementing IEnumerable<T>
   - Custom iterators with yield
   - Collection initializers

4. **Performance Optimization:**
   - Span<T> and Memory<T>
   - ArrayPool<T>
   - ValueTask<T>

---

## 📝 Notes

- All projects target .NET 8.0
- Each project is self-contained and runnable independently
- Comprehensive test cases included in Main() methods
- Code follows C# naming conventions and best practices
- Extensive comments for learning purposes

---

## 🏆 Challenge Completion Checklist

- [x] Challenge 1: Hospital Patient Management
- [x] Challenge 2: Library Book Management
- [x] Challenge 3: E-commerce Shopping Cart
- [x] Challenge 4: Tournament Ranking System

---

**Created:** February 16, 2026  
**Framework:** .NET 8.0  
**Language:** C# 12.0
