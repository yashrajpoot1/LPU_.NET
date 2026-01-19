# DAY 7 - Multiple Inheritance in C#

## Overview
C# does not support multiple inheritance with classes (a class cannot inherit from multiple classes). However, C# allows **multiple interface inheritance**, where a class can implement multiple interfaces. This provides the benefits of multiple inheritance while avoiding the "diamond problem."

## Multiple Inheritance through Interfaces

### Interface Definitions

First, we need to define the interfaces that our classes will implement:

```csharp
public interface IBirdFirst
{
    void SingBird();
    void FlyBird();
}

public interface IBirdSecond
{
    void DanceBird();
    void SwimBird();
}
```

### Implementation Classes

```csharp
using System;
using System.Collections.Generic;
using System.Text;

namespace DAY7_Learning
{
    /// <summary>
    /// Represents a bird that can perform basic actions such as singing and flying.
    /// </summary>
    public class BirdFirst : IBirdFirst
    {
        // Implementing SingBird method
        public void SingBird()
        {
            Console.WriteLine("Bird is singing");
        }

        // Implementing FlyBird method
        public void FlyBird()
        {
            Console.WriteLine("Bird is flying");
        }
    }

    /// <summary>
    /// Represents a bird that can perform actions such as dancing and swimming.
    /// </summary>
    public class BirdSecond : IBirdSecond
    {
        // Implementing DanceBird method
        public void DanceBird()
        {
            Console.WriteLine("Bird is dancing");
        }
        
        // Implementing SwimBird method
        public void SwimBird()
        {
            Console.WriteLine("Bird is swimming");
        }
    }

    /// <summary>
    /// Represents a bird that implements both the IBirdFirst and IBirdSecond interfaces, providing behaviors such as
    /// running, singing, flying, dancing, and swimming.
    /// </summary>
    /// <remarks>
    /// Use this class when you need a bird object that supports multiple actions defined by the
    /// IBirdFirst and IBirdSecond interfaces. Each method corresponds to a distinct bird behavior and can be called
    /// independently. This demonstrates multiple inheritance through interfaces.
    /// </remarks>
    public class MyBird : IBirdFirst, IBirdSecond
    {
        // Implementing MyBirdrun method
        public void MyBirdrun()
        {
            Console.WriteLine("MyBird is running");
        }

        // Implementing SingBird method from IBirdFirst
        public void SingBird()
        {
            Console.WriteLine("Bird is singing");
        }

        // Implementing FlyBird method from IBirdFirst
        public void FlyBird()
        {
            Console.WriteLine("Bird is flying");
        }

        // Implementing DanceBird method from IBirdSecond
        public void DanceBird()
        {
            Console.WriteLine("Bird is dancing");
        }

        // Implementing SwimBird method from IBirdSecond
        public void SwimBird()
        {
            Console.WriteLine("Bird is swimming");
        }
    }
}
```

## Key Points about Multiple Interface Inheritance

1. **A class can implement multiple interfaces** - The `MyBird` class implements both `IBirdFirst` and `IBirdSecond`
2. **All interface members must be implemented** - The class must provide implementation for all methods from all interfaces
3. **Avoids the Diamond Problem** - Since interfaces only define contracts (no implementation), there's no ambiguity
4. **Provides flexibility** - Different classes can implement different combinations of interfaces

---

## Usage Examples

### Example 1: Direct Class Instance

This example demonstrates creating an instance of `MyBird` and calling all its methods directly:

```csharp
namespace DAY7_Learning
{
    /// <summary>
    /// The main program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Serves as the entry point for the application.
        /// </summary>
        /// <param name="args">An array of command-line arguments supplied to the application.</param>
        public static void Main(string[] args)
        {
            // Create an instance of MyBird and demonstrate its capabilities
            Console.WriteLine("Here is My Bird's Capabilities: ");
            MyBird myBird = new MyBird();
            myBird.MyBirdrun();
            myBird.SingBird();
            myBird.FlyBird();
            myBird.DanceBird();
            myBird.SwimBird();
        }
    }
}
```

**Output:**
```
Here is My Bird's Capabilities: 
MyBird is running
Bird is singing
Bird is flying
Bird is dancing
Bird is swimming
```

### Example 2: Using Interface References (Interface Segregation Principle)

This example demonstrates using interface references to access only specific behaviors. This is useful when you want to restrict what methods can be called on an object:

```csharp
namespace DAY7_Learning
{
    /// <summary>
    /// The main program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Serves as the entry point for the application.
        /// </summary>
        /// <param name="args">An array of command-line arguments supplied to the application.</param>
        public static void Main(string[] args)
        {
            // Create an instance of MyBird and demonstrate its capabilities
            Console.WriteLine("Here is My Bird's Capabilities: ");
            
            // Using interface references to access methods
            // Demonstrating interface segregation principle
            IBirdFirst birdFirst = new MyBird();
            IBirdSecond birdSecond = new MyBird();
            
            // Can only access IBirdFirst methods through birdFirst reference
            birdFirst.FlyBird();
            birdFirst.SingBird();
            
            // Can only access IBirdSecond methods through birdSecond reference
            birdSecond.DanceBird();
            birdSecond.SwimBird();
        }
    }
}
```

**Output:**
```
Here is My Bird's Capabilities: 
Bird is flying
Bird is singing
Bird is dancing
Bird is swimming
```

**Benefits of Interface Segregation:**
- **Encapsulation** - Limits access to only relevant methods
- **Flexibility** - Different parts of code can work with different interfaces
- **Testability** - Easier to mock specific interfaces in unit tests
- **SOLID Principles** - Follows the Interface Segregation Principle (ISP)

---

## Summary

In this lesson, we covered:

1. **Multiple Inheritance through Interfaces** - How C# allows a class to implement multiple interfaces by implementing multiple interface contracts
2. **Interface Segregation Principle** - Using interface references to limit access to specific behaviors and follow SOLID principles
3. **Practical Implementation** - Real-world examples demonstrating how to use multiple interfaces effectively

These concepts are fundamental to writing flexible, maintainable C# code that follows SOLID principles.

---

## Related Topics

For more information on related C# concepts, see:
- [Reference Parameters](reference_parameter.md) - Learn about passing parameters by reference using `ref`, `out`, and `in` keywords

