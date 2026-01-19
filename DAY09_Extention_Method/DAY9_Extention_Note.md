# DAY 9: Extension Methods in C#

## 📚 Table of Contents
1. [Introduction](#introduction)
2. [What are Extension Methods?](#what-are-extension-methods)
3. [Why Use Extension Methods?](#why-use-extension-methods)
4. [Syntax and Rules](#syntax-and-rules)
5. [Instance Methods vs Extension Methods](#instance-methods-vs-extension-methods)
6. [Practical Examples](#practical-examples)
7. [Best Practices](#best-practices)
8. [Common Use Cases](#common-use-cases)
9. [Limitations](#limitations)
10. [Summary](#summary)

---

## Introduction

Extension Methods are a powerful feature in C# that allows you to add new methods to existing types without modifying the original type, creating a new derived type, or recompiling the code.

**Simple Definition:** Extension methods let you "extend" or add functionality to classes that you don't own or can't modify.

---

## What are Extension Methods?

### 📖 Definition
An **Extension Method** is a special type of static method that appears to belong to an existing type, even though it's defined in a separate static class.

### Key Characteristics:
- Defined in a **static class**
- Must be a **static method**
- First parameter uses the **`this`** keyword
- Can be called as if they were instance methods
- Available throughout your application once the namespace is imported

### Simple Analogy:
Think of extension methods like "accessories" for your phone. You can't modify the phone's internal design, but you can add a case, screen protector, or pop socket to enhance its functionality.

---

## Why Use Extension Methods?

### Benefits:
1. **Extend Sealed Classes**: Add methods to classes you cannot inherit from (like `string`, `int`, etc.)
2. **No Source Code Needed**: Extend third-party libraries without modifying their code
3. **Clean Code**: Make code more readable by using method chaining
4. **Backward Compatibility**: Add functionality without breaking existing code
5. **Reusability**: Create utility methods that can be used across projects

### Real-World Scenario:
Imagine you're using a library where the `string` class doesn't have a method to count words. You can't modify the `string` class, but you can create an extension method to add this functionality!

---

## Syntax and Rules

### Basic Syntax:
```csharp
public static class ExtensionClassName
{
    public static ReturnType MethodName(this TypeToExtend objectName, parameters)
    {
        // Method implementation
    }
}
```

### Essential Rules:
1. ✅ Extension method class **must be static**
2. ✅ Extension method **must be static**
3. ✅ First parameter **must have `this` keyword** before the type
4. ✅ The class should be in a **namespace**
5. ✅ Import the namespace where the extension method is defined using `using` directive

### Example Structure:
```csharp
namespace MyExtensions
{
    public static class StringExtensions
    {
        public static int WordCount(this string str)
        {
            return str.Split(' ').Length;
        }
    }
}
```

---

## Instance Methods vs Extension Methods

### 📊 Comparison Table

| Feature | Instance Method | Extension Method |
|---------|----------------|------------------|
| **Definition Location** | Inside the class | In a separate static class |
| **Access Modifiers** | Can be public, private, protected | Must be public static |
| **`this` Keyword** | Not required in parameters | Required for first parameter |
| **Accessing Private Members** | ✅ Yes | ❌ No (only public members) |
| **Priority** | Higher (called first) | Lower (called if no instance method exists) |
| **Modify Original Class** | ✅ Yes | ❌ No |

### Example Comparison:

#### Instance Method:
```csharp
public class Person
{
    public string Name { get; set; }
    
    // Instance method - defined inside the class
    public string GetGreeting()
    {
        return $"Hello, I'm {Name}";
    }
}

// Usage
Person person = new Person { Name = "John" };
string greeting = person.GetGreeting();
```

#### Extension Method:
```csharp
public class Person
{
    public string Name { get; set; }
}

// Extension method - defined in a separate static class
public static class PersonExtensions
{
    public static string GetGreeting(this Person person)
    {
        return $"Hello, I'm {person.Name}";
    }
}

// Usage - looks the same!
Person person = new Person { Name = "John" };
string greeting = person.GetGreeting();
```

---

## Practical Examples

### Example 1: Extending String Class
**Scenario:** Add a method to capitalize the first letter of each word

```csharp
using System;
using System.Linq;

namespace MyExtensions
{
    public static class StringExtensions
    {
        // Extension method to capitalize first letter of each word
        public static string ToTitleCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            
            return string.Join(" ", str.Split(' ')
                .Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));
        }
        
        // Extension method to count words
        public static int WordCount(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;
                
            return str.Split(new[] { ' ', '\t', '\n' }, 
                StringSplitOptions.RemoveEmptyEntries).Length;
        }
        
        // Extension method to check if string is a valid email
        public static bool IsValidEmail(this string str)
        {
            return str.Contains("@") && str.Contains(".");
        }
    }
}

// Usage:
class Program
{
    static void Main()
    {
        string text = "hello world from C#";
        
        Console.WriteLine(text.ToTitleCase());      // Output: Hello World From C#
        Console.WriteLine(text.WordCount());        // Output: 4
        
        string email = "test@example.com";
        Console.WriteLine(email.IsValidEmail());    // Output: True
    }
}
```

### Example 2: Extending Integer Class
**Scenario:** Add utility methods for integers

```csharp
using System;

namespace MyExtensions
{
    public static class IntExtensions
    {
        // Check if number is even
        public static bool IsEven(this int number)
        {
            return number % 2 == 0;
        }
        
        // Check if number is odd
        public static bool IsOdd(this int number)
        {
            return number % 2 != 0;
        }
        
        // Calculate factorial
        public static long Factorial(this int number)
        {
            if (number < 0)
                throw new ArgumentException("Number must be non-negative");
            
            long result = 1;
            for (int i = 2; i <= number; i++)
            {
                result *= i;
            }
            return result;
        }
        
        // Check if number is prime
        public static bool IsPrime(this int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            
            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}

// Usage:
class Program
{
    static void Main()
    {
        int num = 7;
        
        Console.WriteLine(num.IsEven());        // Output: False
        Console.WriteLine(num.IsOdd());         // Output: True
        Console.WriteLine(num.IsPrime());       // Output: True
        Console.WriteLine(5.Factorial());       // Output: 120
    }
}
```

### Example 3: Extending Custom Classes
**Scenario:** Add functionality to an Employee class

```csharp
using System;

namespace MyApplication
{
    // Original class (could be from a third-party library)
    public class Employee
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
    }
    
    // Extension methods for Employee
    public static class EmployeeExtensions
    {
        // Calculate years of service
        public static int GetYearsOfService(this Employee emp)
        {
            return DateTime.Now.Year - emp.JoinDate.Year;
        }
        
        // Calculate annual bonus (10% of salary)
        public static decimal GetAnnualBonus(this Employee emp)
        {
            return emp.Salary * 0.10m;
        }
        
        // Get employee summary
        public static string GetSummary(this Employee emp)
        {
            return $"{emp.Name} has been with us for {emp.GetYearsOfService()} years " +
                   $"and earns ${emp.Salary:N2} per year.";
        }
        
        // Check if eligible for promotion (5+ years)
        public static bool IsEligibleForPromotion(this Employee emp)
        {
            return emp.GetYearsOfService() >= 5;
        }
    }
}

// Usage:
class Program
{
    static void Main()
    {
        Employee emp = new Employee
        {
            Name = "Alice Johnson",
            Salary = 75000,
            JoinDate = new DateTime(2018, 3, 15)
        };
        
        Console.WriteLine(emp.GetSummary());
        Console.WriteLine($"Bonus: ${emp.GetAnnualBonus():N2}");
        Console.WriteLine($"Eligible for Promotion: {emp.IsEligibleForPromotion()}");
        
        // Output:
        // Alice Johnson has been with us for 7 years and earns $75,000.00 per year.
        // Bonus: $7,500.00
        // Eligible for Promotion: True
    }
}
```

### Example 4: Extending Collections
**Scenario:** Add utility methods for Lists

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyExtensions
{
    public static class ListExtensions
    {
        // Shuffle the list randomly
        public static void Shuffle<T>(this List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        
        // Print all elements
        public static void PrintAll<T>(this List<T> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
        
        // Get second largest element
        public static T SecondMax<T>(this List<T> list) where T : IComparable<T>
        {
            if (list.Count < 2)
                throw new InvalidOperationException("List must contain at least 2 elements");
            
            return list.OrderByDescending(x => x).Skip(1).First();
        }
    }
}

// Usage:
class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 5, 2, 8, 1, 9, 3 };
        
        Console.WriteLine("Original list:");
        numbers.PrintAll();
        
        numbers.Shuffle();
        Console.WriteLine("\nShuffled list:");
        numbers.PrintAll();
        
        Console.WriteLine($"\nSecond largest: {numbers.SecondMax()}");
    }
}
```

---

## Best Practices

### ✅ DO:
1. **Use Descriptive Names**: Make method names clear and self-explanatory
   ```csharp
   // Good
   public static bool IsValidEmail(this string str)
   
   // Bad
   public static bool Check(this string str)
   ```

2. **Keep Methods Simple**: Extension methods should do one thing well
   ```csharp
   // Good - Single responsibility
   public static int WordCount(this string str)
   
   // Bad - Too many responsibilities
   public static string ProcessAndValidateAndFormat(this string str)
   ```

3. **Use Meaningful Namespaces**: Group related extensions together
   ```csharp
   namespace MyCompany.StringExtensions
   namespace MyCompany.DateTimeExtensions
   ```

4. **Null Checking**: Always validate input parameters
   ```csharp
   public static int WordCount(this string str)
   {
       if (string.IsNullOrEmpty(str))
           return 0;
       // ... rest of code
   }
   ```

### ❌ DON'T:
1. **Don't Overuse**: Only create extension methods when they add real value
2. **Don't Modify State**: Extension methods shouldn't change the object's state (except for collections)
3. **Don't Replace Instance Methods**: If you own the class, add instance methods instead
4. **Don't Create Confusing Names**: Avoid names that conflict with existing methods

---

## Common Use Cases

### 1. **String Manipulation**
```csharp
public static string Truncate(this string str, int maxLength)
{
    return str.Length <= maxLength ? str : str.Substring(0, maxLength) + "...";
}
```

### 2. **Validation**
```csharp
public static bool IsInRange(this int value, int min, int max)
{
    return value >= min && value <= max;
}
```

### 3. **Formatting**
```csharp
public static string ToFriendlyDate(this DateTime date)
{
    return date.ToString("MMMM dd, yyyy");
}
```

### 4. **LINQ-Style Operations**
```csharp
public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> source)
{
    return source.Where(item => item != null);
}
```

---

## Limitations

### 🚫 What Extension Methods CANNOT Do:

1. **Cannot Access Private Members**: Only public members are accessible
   ```csharp
   public class MyClass
   {
       private int secret = 10;
   }
   
   public static class Extensions
   {
       public static void ShowSecret(this MyClass obj)
       {
           // ERROR: Cannot access 'secret' - it's private
           // Console.WriteLine(obj.secret);
       }
   }
   ```

2. **Cannot Override Existing Methods**: Instance methods always take priority
   ```csharp
   public class Person
   {
       public string GetName() => "Instance Method";
   }
   
   public static class Extensions
   {
       // This will NOT override the instance method
       public static string GetName(this Person p) => "Extension Method";
   }
   
   // Will always call instance method
   Person p = new Person();
   Console.WriteLine(p.GetName()); // Output: Instance Method
   ```

3. **Cannot Be Used with Static Classes**: Extension methods only work with instances

4. **Cannot Define Properties or Events**: Only methods are supported

5. **Cannot Be Virtual**: Extension methods cannot be overridden in derived classes

---

## Summary

### Key Takeaways:

📌 **Extension Methods** allow you to add functionality to existing types without modifying them

📌 They are defined in **static classes** as **static methods** with the **`this`** keyword

📌 They provide a cleaner syntax and improve code readability

📌 Useful for extending sealed classes, third-party libraries, and built-in types

📌 They can only access **public members** of the extended type

📌 **Instance methods** have priority over extension methods

### Quick Reference:

```csharp
// Template for creating extension methods
namespace YourNamespace
{
    public static class TypeExtensions
    {
        public static ReturnType MethodName(this TypeToExtend obj, parameters)
        {
            // Implementation
            return result;
        }
    }
}

// Usage
using YourNamespace;

TypeToExtend instance = new TypeToExtend();
instance.MethodName(arguments);
```

---

## 🎯 Practice Exercise

**Challenge:** Create extension methods for the following scenarios:

1. Add a `Reverse()` method to the `string` class that reverses the string
2. Add an `IsWeekend()` method to the `DateTime` class
3. Add a `Square()` method to the `int` class that returns the square of the number
4. Add a `RemoveDuplicates()` method to `List<T>` that removes duplicate items

**Solution hints available in DAY9_Code.md**

---

## 📚 Additional Resources

- Microsoft Documentation: [Extension Methods (C# Programming Guide)](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods)
- LINQ uses extension methods extensively
- Explore System.Linq namespace for real-world examples

---

**End of DAY 9 Notes**

*Created by: Senior Architecture Team*  
*Last Updated: December 30, 2025*
