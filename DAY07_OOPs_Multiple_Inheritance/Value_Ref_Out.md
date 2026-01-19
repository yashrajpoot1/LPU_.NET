# Parameter Passing in C#: Value, Reference, and Out

## Introduction

In C#, there are three primary ways to pass parameters to methods:
1. **Pass by Value** (default)
2. **Pass by Reference** (using `ref` keyword)
3. **Pass by Out** (using `out` keyword)

Understanding these mechanisms is fundamental to writing effective C# programs.

---

## 1. Pass by Value (Default Behavior)

### Concept

When a parameter is passed by value, a **copy** of the variable is created and passed to the method. Any modifications made to the parameter inside the method **do not affect** the original variable.

### Characteristics

- Default parameter passing mechanism
- Creates a copy of the argument
- Original variable remains unchanged
- Suitable for most scenarios

### Syntax

```csharp
public void MethodName(int parameter)
{
    // Modifications here do not affect the original
}
```

### Example

```csharp
using System;

class Program
{
    static void Main()
    {
        int number = 10;
        Console.WriteLine($"Before method call: {number}");
        
        ModifyValue(number);
        
        Console.WriteLine($"After method call: {number}");
    }
    
    static void ModifyValue(int value)
    {
        value = 20;  // Only modifies the copy
        Console.WriteLine($"Inside method: {value}");
    }
}
```

**Output:**
```
Before method call: 10
Inside method: 20
After method call: 10
```

**Explanation:** The original variable `number` remains 10 because only a copy was passed to the method.

---

## 2. Pass by Reference (`ref` keyword)

### Concept

When a parameter is passed by reference using the `ref` keyword, the method receives a **reference** to the original variable, not a copy. Any modifications made inside the method **directly affect** the original variable.

### Characteristics

- Uses the `ref` keyword in both method definition and method call
- Variable **must be initialized** before passing
- Original variable is modified
- Both caller and method share the same memory location

### Syntax

```csharp
// Method definition
public void MethodName(ref int parameter)
{
    // Modifications affect the original
}

// Method call
MethodName(ref variable);
```

### Example

```csharp
using System;

class Program
{
    static void Main()
    {
        int number = 10;
        Console.WriteLine($"Before method call: {number}");
        
        ModifyReference(ref number);
        
        Console.WriteLine($"After method call: {number}");
    }
    
    static void ModifyReference(ref int value)
    {
        value = 20;  // Modifies the original
        Console.WriteLine($"Inside method: {value}");
    }
}
```

**Output:**
```
Before method call: 10
Inside method: 20
After method call: 20
```

**Explanation:** The original variable `number` is changed to 20 because a reference to it was passed to the method.

---

## 3. Pass by Out (`out` keyword)

### Concept

The `out` keyword is similar to `ref`, but it is specifically designed for methods that need to **return multiple values**. A variable passed with `out` does **not need to be initialized** before passing, but it **must be assigned a value** inside the method before the method returns.

### Characteristics

- Uses the `out` keyword in both method definition and method call
- Variable does **not need** to be initialized before passing
- Variable **must be assigned** inside the method
- Typically used to return multiple values from a method

### Syntax

```csharp
// Method definition
public void MethodName(out int parameter)
{
    parameter = value;  // Must assign before returning
}

// Method call
MethodName(out variable);
```

### Example

```csharp
using System;

class Program
{
    static void Main()
    {
        int result;  // No initialization needed
        
        GetSquareAndCube(5, out int square, out int cube);
        
        Console.WriteLine($"Square: {square}");
        Console.WriteLine($"Cube: {cube}");
    }
    
    static void GetSquareAndCube(int number, out int square, out int cube)
    {
        square = number * number;      // Must assign
        cube = number * number * number;  // Must assign
    }
}
```

**Output:**
```
Square: 25
Cube: 125
```

**Explanation:** The method assigns values to both `square` and `cube`, effectively returning multiple values.

---

## Comparison Table

| Feature | Pass by Value | Pass by Reference (`ref`) | Pass by Out (`out`) |
|---------|---------------|---------------------------|---------------------|
| **Keyword** | None | `ref` | `out` |
| **Initialization Required** | Yes | Yes | No |
| **Must Assign in Method** | No | No | Yes |
| **Modifies Original** | No | Yes | Yes |
| **Primary Purpose** | Pass data to method | Modify existing data | Return multiple values |

---

## Comprehensive Example

The following example demonstrates all three parameter passing mechanisms:

```csharp
using System;

class Calculator
{
    // Pass by Value - original not affected
    public int AddByValue(int a, int b)
    {
        int sum = a + b;
        a = 0;  // Does not affect original
        return sum;
    }
    
    // Pass by Reference - original is modified
    public void AddByReference(ref int a, int b)
    {
        a = a + b;  // Modifies the original variable
    }
    
    // Pass by Out - returns multiple results
    public void CalculateOperations(int a, int b, out int sum, out int product, out int difference)
    {
        sum = a + b;
        product = a * b;
        difference = a - b;
    }
}

class Program
{
    static void Main()
    {
        Calculator calc = new Calculator();
        
        // Example 1: Pass by Value
        Console.WriteLine("=== Pass by Value ===");
        int x = 5, y = 3;
        int result1 = calc.AddByValue(x, y);
        Console.WriteLine($"x = {x}, y = {y}, result = {result1}");
        // Output: x = 5, y = 3, result = 8 (x unchanged)
        
        // Example 2: Pass by Reference
        Console.WriteLine("\n=== Pass by Reference ===");
        int a = 5;
        Console.WriteLine($"Before: a = {a}");
        calc.AddByReference(ref a, 3);
        Console.WriteLine($"After: a = {a}");
        // Output: Before: a = 5, After: a = 8 (a modified)
        
        // Example 3: Pass by Out
        Console.WriteLine("\n=== Pass by Out ===");
        int sum, product, difference;  // No initialization
        calc.CalculateOperations(10, 4, out sum, out product, out difference);
        Console.WriteLine($"Sum: {sum}, Product: {product}, Difference: {difference}");
        // Output: Sum: 14, Product: 40, Difference: 6
    }
}
```

**Output:**
```
=== Pass by Value ===
x = 5, y = 3, result = 8

=== Pass by Reference ===
Before: a = 5
After: a = 8

=== Pass by Out ===
Sum: 14, Product: 40, Difference: 6
```

---

## When to Use Each Method

### Use Pass by Value When:
- You want to **protect** the original variable from modification
- You are passing **simple data** that the method only needs to read
- This is the **default and safest** approach

### Use Pass by Reference (`ref`) When:
- You need to **modify** the original variable
- You want to **avoid copying** large structures (performance optimization)
- The variable must be **initialized** before passing

### Use Pass by Out (`out`) When:
- You need to **return multiple values** from a method
- The variable does **not have a meaningful initial value**
- You want to follow the **TryParse pattern** (returning success/failure and a result)

---

## Real-World Example: TryParse Pattern

The `out` parameter is commonly used in the TryParse pattern, which safely attempts to convert a string to another type:

```csharp
using System;

class Program
{
    static void Main()
    {
        string input = "123";
        
        // The out parameter receives the parsed value
        if (int.TryParse(input, out int number))
        {
            Console.WriteLine($"Successfully parsed: {number}");
        }
        else
        {
            Console.WriteLine("Failed to parse");
        }
        
        // Invalid input example
        string invalidInput = "abc";
        if (int.TryParse(invalidInput, out int invalidNumber))
        {
            Console.WriteLine($"Successfully parsed: {invalidNumber}");
        }
        else
        {
            Console.WriteLine("Failed to parse invalid input");
        }
    }
}
```

**Output:**
```
Successfully parsed: 123
Failed to parse invalid input
```

---

## Important Rules

### For `ref` Parameters:
1. Variable **must be initialized** before passing
2. `ref` keyword required in **both** method definition and call
3. Method **can read and modify** the variable

### For `out` Parameters:
1. Variable **does not need** initialization before passing
2. `out` keyword required in **both** method definition and call
3. Method **must assign** a value before returning
4. Method **cannot read** the variable before assigning it

---

## Common Mistakes

### Mistake 1: Forgetting `ref` in Method Call
```csharp
// ❌ Wrong
int value = 10;
ModifyValue(value);  // Missing 'ref'

// ✅ Correct
int value = 10;
ModifyValue(ref value);
```

### Mistake 2: Not Initializing Before `ref`
```csharp
// ❌ Wrong
int value;  // Not initialized
ModifyValue(ref value);  // Compiler error

// ✅ Correct
int value = 10;  // Initialized
ModifyValue(ref value);
```

### Mistake 3: Not Assigning `out` Parameter
```csharp
// ❌ Wrong
void GetValue(out int value)
{
    // Not assigning value - Compiler error
}

// ✅ Correct
void GetValue(out int value)
{
    value = 42;  // Must assign
}
```

---

## Summary

Understanding parameter passing mechanisms is crucial for effective C# programming:

1. **Pass by Value** (default)
   - Creates a copy
   - Original unchanged
   - Safe and simple

2. **Pass by Reference** (`ref`)
   - Passes reference
   - Modifies original
   - Requires initialization

3. **Pass by Out** (`out`)
   - Returns multiple values
   - No initialization needed
   - Must assign in method

Choose the appropriate method based on your requirements, keeping in mind that pass by value is the default and safest option for most scenarios.

---

## Practice Exercises

### Exercise 1: Swap Two Numbers
Write a method that swaps two integer values using `ref` parameters.

**Solution:**
```csharp
static void Swap(ref int a, ref int b)
{
    int temp = a;
    a = b;
    b = temp;
}

// Usage
int x = 5, y = 10;
Swap(ref x, ref y);
Console.WriteLine($"x = {x}, y = {y}");  // x = 10, y = 5
```

### Exercise 2: Division with Remainder
Write a method that returns both quotient and remainder using `out` parameters.

**Solution:**
```csharp
static void Divide(int dividend, int divisor, out int quotient, out int remainder)
{
    quotient = dividend / divisor;
    remainder = dividend % divisor;
}

// Usage
Divide(17, 5, out int q, out int r);
Console.WriteLine($"Quotient: {q}, Remainder: {r}");  // Quotient: 3, Remainder: 2
```

### Exercise 3: Increment Counter
Write a method that increments a counter passed by reference.

**Solution:**
```csharp
static void IncrementCounter(ref int counter)
{
    counter++;
}

// Usage
int count = 0;
IncrementCounter(ref count);
IncrementCounter(ref count);
Console.WriteLine($"Count: {count}");  // Count: 2
```

---

## Conclusion

Mastering parameter passing techniques in C# allows you to write more efficient and flexible code. Remember:
- Use **value** parameters by default for safety
- Use **ref** when you need to modify existing data
- Use **out** when returning multiple results

These fundamentals form the foundation for more advanced C# programming concepts.
