## ➕ Method Overloading

### Problem Statement

Method Overloading is the common way of implementing **polymorphism**.
C# can distinguish the methods with **different method signatures**, i.e., the methods can have the same name but with different parameters list (i.e., the number of parameters, order of the parameters, and data types of the parameters) within the same class.

Your task here is to implement a **C# code** based on the following specifications.
Note that your code should match the specifications in a **precise manner**.
Consider **default visibility** of classes, data fields, and methods unless mentioned otherwise.

---

### Specifications

#### Class Definitions

##### Class `Source`

**Method Definitions:**

 - `Add(int a, int b, int c)`

     - Method to add three integer values
     - Visibility: `public`
     - Return Type: `int`

 - `Add(double a, double b, double c)`

     - Method to add three double values
     - Visibility: `public`
     - Return Type: `double`

---

### Task

 - Create a class **`Source`**.
 - Demonstrate **method overloading** by implementing the following methods:

  1. `Add(int a, int b, int c)` – method to add three integer values.
  2. `Add(double a, double b, double c)` – method to add three double values.

---

### Important Notes

 - If you want to test your program, you can implement a `Main()` method in the stub.
 - Use the **RUN CODE** option to test your program.
 - Ensure valid function calls with valid data.

---

### Source.cs
```csharp
using System;
using System.Collections.Generic;
using System.Text;

namespace Practice
{
    // Source class with overloaded Add methods
    public class Source
    {
        // Method to add three integers
        /// <summary>
        /// Adds three integer values and returns the sum.
        /// </summary>
        public int Add(int a, int b, int c)
        {
            return a + b + c;
        }

        // Method to add three doubles
        /// <summary>
        /// Adds three double values and returns the sum.
        /// </summary>
        public double Add(double a, double b, double c)
        {
            return a + b + c;
        }
    }
}

```

### Program.cs

```csharp
namespace Practice
{
    // Main program class
    public class Program
    {
        // Entry point of the program
        static void Main(string[] args)
        {
           Source source = new Source(); // Create an instance of Source class
            Console.WriteLine("Addition of Integers: " + source.Add(1, 2, 3)); // Call Add method with integer arguments
            Console.WriteLine("Addition of Doubles: " + source.Add(1.1, 2.2, 3.3)); // Call Add method with double arguments
        }
    }
}

```