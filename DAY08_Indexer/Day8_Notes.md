#  C# Indexer

## 1️⃣ What is an Indexer?

An **Indexer** in C# allows an object to be accessed using **array-style syntax (`[]`)**.

Instead of calling methods like:
```csharp
obj.GetValue(0);
````

You can write:

```csharp
obj[0];
```

👉 Indexers make objects behave like **collections** while still following **OOP principles**.

---

## 2️⃣ Why Do We Need Indexers? (Architect Thinking)

### Problem Without Indexers

* Code becomes verbose
* Access feels unnatural
* No clean abstraction for collections

### Solution With Indexers

* Clean syntax
* Encapsulation is preserved
* Validation & rules can be enforced

📌 **Indexer = Controlled array-like access to object data**

---

## 3️⃣ Key Definition (Interview-Ready)

> An indexer is a special member that enables an object to be indexed like an array, providing controlled access to internal data.

---

## 4️⃣ Mental Model (Very Important)

* Indexer behaves like a **property**
* But:

  * Has **parameters**
  * Has **no name**
  * Uses `this`
  * Uses `[]`

👉 Think of indexers as **parameterized properties**

---

## 5️⃣ Basic Syntax of an Indexer

```csharp
public returnType this[indexType index]
{
    get
    {
        // return value
    }
    set
    {
        // assign value
    }
}
```

### Important Rules

* Must be inside a **class or struct**
* Uses `this`
* Cannot be static
* Can be overloaded

---

## 6️⃣ First Simple Example (Array-Based)

```csharp
class Marks
{
    private int[] _marks = new int[5];

    public int this[int index]
    {
        get
        {
            return _marks[index];
        }
        set
        {
            _marks[index] = value;
        }
    }
}
```

### Usage

```csharp
Marks m = new Marks();
m[0] = 85;
Console.WriteLine(m[0]);
```

---

## 7️⃣ Indexer vs Array (Architect Comparison)

| Feature        | Array  | Indexer    |
| -------------- | ------ | ---------- |
| Data exposure  | Direct | Controlled |
| Validation     | ❌      | ✅          |
| Encapsulation  | ❌      | ✅          |
| Business rules | ❌      | ✅          |

📌 **Indexer ≠ Data structure**

---

## 8️⃣ Adding Validation (Real-World Design)

```csharp
public int this[int index]
{
    get
    {
        if (index < 0 || index >= _marks.Length)
            throw new IndexOutOfRangeException();

        return _marks[index];
    }
    set
    {
        if (value < 0 || value > 100)
            throw new ArgumentException("Marks must be 0–100");

        _marks[index] = value;
    }
}
```

🧠 Architect Rule:

> Never expose internal data without validation.

---

## 9️⃣ Indexer with String Key (Dictionary Style)

```csharp
class Student
{
    private Dictionary<string, int> _subjects = new();

    public int this[string subject]
    {
        get
        {
            return _subjects.ContainsKey(subject) ? _subjects[subject] : 0;
        }
        set
        {
            _subjects[subject] = value;
        }
    }
}
```

### Usage

```csharp
Student s = new Student();
s["Math"] = 95;
Console.WriteLine(s["Math"]);
```

---

## 🔟 Multiple Indexers

A class can have **multiple indexers** with different parameter types.

```csharp
public int this[int index] { get; set; }
public string this[string key] { get; set; }
```

Used in:

* Configuration systems
* ORMs
* Caching layers

---

## 1️⃣1️⃣ Read-Only Indexer

```csharp
public int this[int index]
{
    get { return _marks[index]; }
}
```

✔ Used when modification is not allowed

---

## 1️⃣2️⃣ When to Use Indexers

✅ When your class:

* Represents a collection
* Needs array-like access
* Requires validation or rules
* Exposes data frequently

---

## 1️⃣3️⃣ When NOT to Use Indexers

❌ When:

* Only single value access is needed
* Method name gives better clarity
* Logic is complex

---

## 1️⃣4️⃣ Common Beginner Mistakes

* Treating indexer like a method
* No bounds checking
* Overusing indexers
* Using indexers for non-collection logic

---

## 1️⃣5️⃣ Interview One-Liner (Must Remember)

> “Indexers allow array-style access to objects while maintaining encapsulation and enforcing business rules.”

---

## 1️⃣6️⃣ Architect Summary

* Indexers improve API design
* They hide internal structure
* They promote clean, readable code
* They are widely used in framework-level code

---

## 1️⃣7️⃣ Practice Suggestions

1. Library book indexer
2. Employee salary indexer
3. Configuration key-value indexer
4. Read-only report indexer

📌 Mastery comes from **designing**, not memorizing.

---

## 1️⃣8️⃣ Quick Notes (General C#)

- `this` keyword: Refers to the current instance of the class.
- Remove `set` from a property: Makes it read-only from outside; assign value via constructor or internally.
- Auto-property with private setter: { get; private set; } allows internal mutation while protecting external code.

---

# Partial Classes — In-Depth

### What is a Partial Class?
A **partial class** lets you split the definition of a single class across **multiple files**. At compile time, all parts are combined into one class. This is useful for separating **generated code** from **hand-written code**, improving maintainability for large types.

### Why Use Partial Classes?
- Separation of concerns: Keep designer/generated code separate from business logic.
- Team collaboration: Multiple developers can work on different parts without merge noise.
- Code generation: Tools (e.g., WinForms/WPF designers, scaffolding) put output in a separate file.

### Syntax
```csharp
// File: Customer.Core.cs
namespace MyApp.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        partial void OnCreated();

        public Customer()
        {
            OnCreated(); // optional partial method hook
        }
    }
}
```

```csharp
// File: Customer.Logic.cs
namespace MyApp.Models
{
    public partial class Customer
    {
        public string GetDisplayName() => $"#{Id} — {Name}";

        // Partial method implementation (optional)
        partial void OnCreated()
        {
            // Initialization logic injected here
        }
    }
}
```

### Rules & Characteristics
- All parts must use the same namespace, class name, and access modifier.
- Mark every part with `partial`.
- Members declared in any part are members of the single combined type.
- Attributes applied to any part are combined on the final type.
- Supports partial methods:
  - Must return `void` and are implicitly `private`.
  - No access modifiers or `virtual`.
  - If not implemented, calls are removed at compile time (no IL emitted).

### When to Use
- Designer-generated or tool-generated code lives in a separate file.
- Very large classes where logical grouping improves maintainability.
- Providing extension points via partial methods for generated code.

### When Not to Use
- Small, cohesive classes (splitting adds unnecessary indirection).
- When fragmentation harms discoverability and comprehension.

### Common Pitfalls
- Forgetting to mark all parts `partial` → compilation errors.
- Scattering logic across too many files → difficult to navigate.
- Assuming partial classes affect runtime behavior (they don’t; it’s a compile-time feature).

### Interview One-Liner
> “Partial classes split a type’s definition across files to separate concerns and integrate generated code, all combined by the compiler into one class.”

---

# Static Classes — In-Depth

### What is a Static Class?
A **static class** cannot be instantiated and can contain only **static members**. It’s ideal for **utility functions**, **constants**, and **extension methods**.

### Why Use Static Classes?
- Centralized utilities: Group stateless helper functions in one place.
- Extension methods: Add methods to existing types without inheritance.
- Global constants/config: Shared values accessible app-wide.

### Syntax
```csharp
public static class MathUtils
{
    public const double Pi = 3.141592653589793;

    public static int Add(int a, int b) => a + b;

    static MathUtils()
    {
        // Static constructor runs once per AppDomain before first use
    }
}
```

### Extension Methods (Must be in a static class)
```csharp
public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? value)
        => string.IsNullOrEmpty(value);

    public static string ToTitleCase(this string value)
        => System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
}

// Usage
// "hello world".ToTitleCase() => "Hello World"
```

### Rules & Characteristics
- `static class` is implicitly `sealed`; cannot be inherited or instantiated.
- Only static members allowed (fields, properties, methods, events).
- May have a static constructor; no instance constructors.
- Cannot implement interfaces or be used as a generic type argument.
- Lifetime is application-wide; consider thread safety and initialization order.

### When to Use
- Pure, stateless helpers (e.g., math, parsing, formatting).
- Extension methods that enhance framework or domain types.
- Shared constants/configuration values.

### When Not to Use
- Logic needing state, polymorphism, or test-friendly design.
- Services that benefit from DI (prefer interfaces and concrete classes).
- Code that relies on global mutable state.

### Common Pitfalls
- Overusing static state → hidden dependencies, hard-to-test code.
- Static constructor exceptions → type unusable for the lifetime of the AppDomain.
- Non-thread-safe static members → race conditions.

### Interview One-Liner
> “Static classes hold only static members, ideal for utilities and extension methods, but avoid global mutable state due to testability and thread-safety concerns.”

---

## 2️⃣1️⃣ Practice Suggestions
1. Convert a helper set into a `static class` with extension methods.
2. Split a large UI model into **partial** designer and logic files.
3. Add a **partial method** hook to generated code and implement it in a hand-written file.
4. Refactor global constants into a dedicated `static class`.
