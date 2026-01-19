# DAY 9: Regular Expressions (RegEx) & Garbage Collection - Practice Code

## 📚 Table of Contents
1. [Overview](#overview)
2. [Regular Expressions Examples](#regular-expressions-examples)
3. [Garbage Collection Example](#garbage-collection-example)
4. [Pattern Explanations](#pattern-explanations)
5. [How to Run](#how-to-run)
6. [Expected Output](#expected-output)
7. [Best Practices](#best-practices)

---

## Overview

This file demonstrates practical implementations of:
- **Regular Expressions (RegEx)**: Pattern matching for string validation
- **Garbage Collection (GC)**: Memory management in .NET

**Namespace:** `RegEx`  
**Technologies:** C# .NET, System.Text.RegularExpressions, Garbage Collector

---

## Regular Expressions Examples

### Example 1: Basic Pattern Matching with Timeout

**Purpose:** Search for a specific pattern in a string with timeout protection

```csharp
using System.Text.RegularExpressions;

namespace RegEx
{
    /// <summary>
    /// Demonstrates basic pattern matching with timeout protection.
    /// </summary>
    /// <remarks>
    /// This example shows how to use Regex to find a specific word in a string.
    /// The timeout prevents potential RegEx DOS attacks from complex patterns.
    /// </remarks>
    public class Program
    {
        /// <summary>
        /// Serves as the entry point for the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            string input = "Error: TIMEOUT while calling API";
            string pattern = "TIMEOUT";

            // Create Regex with case-insensitive matching and timeout
            var rx = new Regex(pattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(200));

            Console.WriteLine(rx.IsMatch(input) ? "Found" : "Not Found");
        }
    }
}
```

**Key Features:**
- ✅ Case-insensitive matching with `RegexOptions.IgnoreCase`
- ✅ Timeout protection: `TimeSpan.FromMilliseconds(200)`
- ✅ Simple boolean result using `IsMatch()`

**Expected Output:**
```
Found
```

---

### Example 2: Username Validation

**Purpose:** Validate username format (alphanumeric with underscore, 3-12 characters)

```csharp
using System.Text.RegularExpressions;

namespace RegEx
{
    /// <summary>
    /// Demonstrates username validation using regular expressions.
    /// </summary>
    /// <remarks>
    /// This example validates that a username contains only letters, numbers, and underscores,
    /// and is between 3 and 12 characters in length.
    /// </remarks>
    public class Program
    {
        /// <summary>
        /// Serves as the entry point for the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            string username = "gopi_1";
            
            // Pattern: Start with letter/number/underscore, 3-12 characters
            string pattern = @"^[A-Za-z0-9_]{3,12}$";

            bool ok = Regex.IsMatch(username, pattern);
            Console.WriteLine(ok ? "Valid" : "Invalid");
        }
    }
}
```

**Pattern Breakdown:**
- `^` - Start of string
- `[A-Za-z0-9_]` - Letters (upper/lower), digits, or underscore
- `{3,12}` - Between 3 and 12 characters
- `$` - End of string

**Test Cases:**
```csharp
"gopi_1"     => Valid   ✅
"ab"         => Invalid ❌ (too short)
"user@name"  => Invalid ❌ (contains @)
"valid_user" => Valid   ✅
```

**Expected Output:**
```
Valid
```

---

### Example 3: Phone Number Validation (Indian Format)

**Purpose:** Validate Indian phone numbers with various formats

```csharp
using System.Text.RegularExpressions;

namespace RegEx
{
    /// <summary>
    /// Demonstrates phone number validation for Indian mobile numbers.
    /// </summary>
    /// <remarks>
    /// This example validates phone numbers in multiple formats:
    /// - With country code: +91 9876543210
    /// - Without country code: 9876543210
    /// - With separator: +91-9876543210
    /// </remarks>
    public class Program
    {
        /// <summary>
        /// Serves as the entry point for the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main()
        {
            string[] phones = { 
                "+91 9876543210",      // Valid with space
                "9876543210",          // Valid without code
                "+91-12345",           // Invalid (too short)
                "0091 9876543210"      // Invalid (wrong code format)
            };
            
            // Pattern for Indian phone numbers
            string pattern = @"^(?:\+91[\s-]?)?\d{10}$";

            foreach (var p in phones)
                Console.WriteLine($"{p} => {Regex.IsMatch(p, pattern)}");
        }
    }
}
```

**Pattern Breakdown:**
- `^` - Start of string
- `(?:\+91[\s-]?)?` - Optional country code (+91) with optional space/dash
  - `(?:...)` - Non-capturing group
  - `\+91` - Literal +91
  - `[\s-]?` - Optional space or dash
  - `?` - Makes entire group optional
- `\d{10}` - Exactly 10 digits
- `$` - End of string

**Expected Output:**
```
+91 9876543210 => True
9876543210 => True
+91-12345 => False
0091 9876543210 => False
```

---

## Garbage Collection Example

### Example 4: Memory Management with GC

**Purpose:** Demonstrate garbage collection and memory monitoring

```csharp
using System;
using System.Collections.Generic;

namespace RegEx
{
    /// <summary>
    /// Demonstrates .NET Garbage Collection behavior and memory management.
    /// </summary>
    /// <remarks>
    /// This example shows:
    /// 1. How to allocate memory in managed heap
    /// 2. How to monitor total memory usage
    /// 3. How to manually trigger garbage collection
    /// 4. Memory difference before and after collection
    /// </remarks>
    public class Program
    {
        /// <summary>
        /// Serves as the entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            // Create a strongly-typed list to hold byte arrays
            var list = new List<byte[]>();

            // Allocate memory: 2000 x 1 KB = ~2 MB
            for (int i = 0; i < 2000; i++)
            {
                // Allocate 1 KB blocks (1024 bytes)
                list.Add(new byte[1024]);
            }

            Console.WriteLine("✅ Memory Allocated");
            Console.WriteLine("───────────────────────────────");

            // Check memory before garbage collection
            long memoryBefore = GC.GetTotalMemory(forceFullCollection: false);
            Console.WriteLine($"Total memory BEFORE GC: {memoryBefore:N0} bytes ({memoryBefore / 1024.0:F2} KB)");

            // Manually trigger garbage collection
            Console.WriteLine("\n🗑️  Running Garbage Collection...\n");
            GC.Collect();
            GC.WaitForPendingFinalizers(); // Wait for finalizers to complete
            GC.Collect(); // Second collection to clean up

            // Check memory after garbage collection
            long memoryAfter = GC.GetTotalMemory(forceFullCollection: false);
            Console.WriteLine($"Total memory AFTER GC:  {memoryAfter:N0} bytes ({memoryAfter / 1024.0:F2} KB)");

            // Calculate memory freed (if any)
            long freed = memoryBefore - memoryAfter;
            Console.WriteLine($"\n💾 Memory freed: {freed:N0} bytes ({freed / 1024.0:F2} KB)");

            Console.WriteLine("\n📊 GC Information:");
            Console.WriteLine($"   Generation 0 collections: {GC.CollectionCount(0)}");
            Console.WriteLine($"   Generation 1 collections: {GC.CollectionCount(1)}");
            Console.WriteLine($"   Generation 2 collections: {GC.CollectionCount(2)}");
        }
    }
}
```

**Key Concepts:**

| Concept | Description |
|---------|-------------|
| **Managed Heap** | Memory area where .NET objects are allocated |
| **GC.GetTotalMemory()** | Returns total bytes allocated in managed heap |
| **GC.Collect()** | Manually triggers garbage collection |
| **Generation 0** | Recently allocated objects |
| **Generation 1** | Objects that survived one collection |
| **Generation 2** | Long-lived objects |

**Expected Output:**
```
✅ Memory Allocated
───────────────────────────────
Total memory BEFORE GC: 2,097,152 bytes (2,048.00 KB)

🗑️  Running Garbage Collection...

Total memory AFTER GC:  2,097,152 bytes (2,048.00 KB)

💾 Memory freed: 0 bytes (0.00 KB)

📊 GC Information:
   Generation 0 collections: 2
   Generation 1 collections: 1
   Generation 2 collections: 1
```

**Note:** Memory may not be freed because the `list` variable still holds references to the byte arrays.

---

## Pattern Explanations

### Common RegEx Symbols

| Symbol | Meaning | Example |
|--------|---------|---------|
| `^` | Start of string | `^Hello` matches "Hello world" |
| `$` | End of string | `world$` matches "Hello world" |
| `.` | Any character | `a.c` matches "abc", "a1c" |
| `*` | Zero or more | `ab*c` matches "ac", "abc", "abbc" |
| `+` | One or more | `ab+c` matches "abc", "abbc" (not "ac") |
| `?` | Zero or one | `ab?c` matches "ac", "abc" |
| `{n}` | Exactly n times | `\d{3}` matches "123" |
| `{n,m}` | Between n and m | `\d{2,4}` matches "12", "123", "1234" |
| `[abc]` | Any of a, b, or c | `[aeiou]` matches vowels |
| `[^abc]` | Not a, b, or c | `[^0-9]` matches non-digits |
| `\d` | Digit (0-9) | `\d+` matches "123" |
| `\w` | Word character | `\w+` matches "Hello_123" |
| `\s` | Whitespace | `\s+` matches spaces, tabs |
| `\|` | OR operator | `cat\|dog` matches "cat" or "dog" |

### Pattern Examples

```csharp
// Email validation (simple)
@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"

// URL validation
@"^(https?:\/\/)?([\w\-]+\.)+[\w\-]+(\/[\w\-._~:/?#[\]@!$&'()*+,;=]*)?$"

// Password (8+ chars, uppercase, lowercase, digit)
@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"

// Date (MM/DD/YYYY)
@"^(0[1-9]|1[0-2])\/(0[1-9]|[12]\d|3[01])\/\d{4}$"

// IPv4 Address
@"^(\d{1,3}\.){3}\d{1,3}$"
```

---

## How to Run

### Option 1: Run Individual Examples
Each example is standalone. Copy the code and run separately:

```bash
# Create a new console project
dotnet new console -n RegExDemo

# Copy code to Program.cs
# Run the project
dotnet run
```

### Option 2: Combine All Examples
Create separate methods for each example:

```csharp
static void Main(string[] args)
{
    Console.WriteLine("Choose an example:");
    Console.WriteLine("1. Pattern Matching");
    Console.WriteLine("2. Username Validation");
    Console.WriteLine("3. Phone Validation");
    Console.WriteLine("4. Garbage Collection");
    
    int choice = int.Parse(Console.ReadLine());
    
    switch (choice)
    {
        case 1: PatternMatchingExample(); break;
        case 2: UsernameValidationExample(); break;
        case 3: PhoneValidationExample(); break;
        case 4: GarbageCollectionExample(); break;
    }
}
```

---

## Expected Output

### Combined Output (Running All Examples)

```
==================================
Example 1: Pattern Matching
==================================
Found

==================================
Example 2: Username Validation
==================================
Valid

==================================
Example 3: Phone Validation
==================================
+91 9876543210 => True
9876543210 => True
+91-12345 => False
0091 9876543210 => False

==================================
Example 4: Garbage Collection
==================================
✅ Memory Allocated
───────────────────────────────
Total memory BEFORE GC: 2,097,152 bytes (2,048.00 KB)

🗑️  Running Garbage Collection...

Total memory AFTER GC:  2,097,152 bytes (2,048.00 KB)

💾 Memory freed: 0 bytes (0.00 KB)

📊 GC Information:
   Generation 0 collections: 2
   Generation 1 collections: 1
   Generation 2 collections: 1
```

---

## Best Practices

### Regular Expressions

✅ **DO:**
- Use `@` verbatim strings for patterns: `@"\d{3}"`
- Add timeout for complex patterns to prevent DOS attacks
- Use `RegexOptions.Compiled` for frequently used patterns
- Test patterns thoroughly with edge cases
- Use named groups for clarity: `(?<area>\d{3})`

❌ **DON'T:**
- Don't use RegEx for simple string operations (use `Contains()`, `StartsWith()`)
- Avoid overly complex patterns - break them down
- Don't forget to escape special characters: `\.` for literal dot
- Don't use RegEx for parsing HTML/XML (use proper parsers)

### Garbage Collection

✅ **DO:**
- Let GC run automatically in most cases
- Use `using` statements for disposable objects
- Implement `IDisposable` for unmanaged resources
- Monitor memory in production with performance counters

❌ **DON'T:**
- Don't call `GC.Collect()` frequently (performance impact)
- Don't rely on finalizers for critical cleanup
- Avoid creating unnecessary objects in loops
- Don't ignore memory leaks from event handlers

### Code Examples

```csharp
// ✅ GOOD: Using 'using' for disposable resources
using (var reader = new StreamReader("file.txt"))
{
    string content = reader.ReadToEnd();
}

// ❌ BAD: Manual GC calls in loops
for (int i = 0; i < 1000; i++)
{
    DoWork();
    GC.Collect(); // Don't do this!
}

// ✅ GOOD: Compiled RegEx for repeated use
private static readonly Regex EmailPattern = 
    new Regex(@"^[\w\.-]+@[\w\.-]+\.\w+$", RegexOptions.Compiled);

// ❌ BAD: Creating new Regex in a loop
foreach (var email in emails)
{
    var regex = new Regex(@"^[\w\.-]+@[\w\.-]+\.\w+$"); // Inefficient
    if (regex.IsMatch(email)) { }
}
```

---

## Advanced Example: IDisposable Pattern

### Example 5: Proper Resource Management with IDisposable

**Purpose:** Demonstrate proper implementation of IDisposable for resource cleanup

```csharp
using System;
using System.Collections;

namespace RegEx
{
    /// <summary>
    /// Demonstrates the proper implementation of IDisposable pattern for resource management.
    /// </summary>
    /// <remarks>
    /// This class shows how to properly manage resources and implement the dispose pattern.
    /// The IDisposable interface ensures proper cleanup of managed and unmanaged resources.
    /// </remarks>
    public class BigMan : IDisposable
    {
        private bool _disposed = false; // Track disposal state
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BigMan"/> class.
        /// </summary>
        public BigMan()
        {
            // Initialize the Names collection
            Names = new ArrayList();
        }

        /// <summary>
        /// Gets or sets the list of names.
        /// </summary>
        public ArrayList Names { get; set; }

        /// <summary>
        /// Releases all resources used by the <see cref="BigMan"/> instance.
        /// </summary>
        /// <param name="disposing">
        /// True if called from Dispose(); false if called from finalizer.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    if (Names != null)
                    {
                        Names.Clear();
                        Names = null;
                    }
                }

                // Dispose unmanaged resources here (if any)
                
                _disposed = true;
            }
        }

        /// <summary>
        /// Releases all resources used by the <see cref="BigMan"/> instance.
        /// </summary>
        public void Dispose()
        {
            // Call Dispose with true (called from user code)
            Dispose(true);
            
            // Suppress finalization since we've already cleaned up
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizer (destructor) for cleanup in case Dispose wasn't called.
        /// </summary>
        ~BigMan()
        {
            // Call Dispose with false (called from finalizer)
            Dispose(false);
        }
    }
}
```

**Usage Example:**

```csharp
// ✅ RECOMMENDED: Using 'using' statement for automatic disposal
using (var bigMan = new BigMan())
{
    bigMan.Names.Add("Alice");
    bigMan.Names.Add("Bob");
    bigMan.Names.Add("Charlie");
    
    Console.WriteLine($"Total names: {bigMan.Names.Count}");
} // Dispose is automatically called here

// ✅ ALTERNATIVE: Manual disposal
var bigMan2 = new BigMan();
try
{
    bigMan2.Names.Add("David");
    Console.WriteLine($"Total names: {bigMan2.Names.Count}");
}
finally
{
    bigMan2.Dispose(); // Explicit disposal
}
```

**Key Concepts:**

| Component | Purpose |
|-----------|---------|
| **`_disposed` flag** | Prevents multiple disposal calls |
| **`Dispose(bool)`** | Protected method for actual cleanup |
| **`Dispose()`** | Public method called by users |
| **Finalizer `~BigMan()`** | Backup cleanup if Dispose() not called |
| **`GC.SuppressFinalize()`** | Prevents finalizer from running twice |

**IDisposable Pattern Flow:**

```
User calls Dispose()
    ↓
Dispose(disposing: true)
    ↓
Clean up managed resources
    ↓
Set _disposed = true
    ↓
GC.SuppressFinalize(this)
    ↓
Finalizer won't run
```

**Best Practices Demonstrated:**
- ✅ Implements full dispose pattern
- ✅ Prevents multiple disposal with `_disposed` flag
- ✅ Separates managed and unmanaged resource cleanup
- ✅ Uses finalizer as safety net
- ✅ Suppresses finalization when disposed properly
- ✅ Virtual `Dispose(bool)` allows inheritance

---

## Additional Practice Exercises

### Exercise 1: Email Validation
Create a regex pattern to validate email addresses.

**Requirements:**
- Must have @ symbol
- Must have domain extension (.com, .org, etc.)
- Can contain letters, numbers, dots, underscores

### Exercise 2: Credit Card Validation
Validate credit card format (16 digits, optional spaces/dashes).

### Exercise 3: Strong Password Validation
Create a pattern that requires:
- Minimum 8 characters
- At least one uppercase letter
- At least one lowercase letter
- At least one digit
- At least one special character

---

