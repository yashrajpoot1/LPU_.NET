# DAY 5: OOPs - Multi-Project Solution

## Project 1: CalcAppMain Multi-Project Solution

### Project Setup

I have created a CalcAppMain project with solution name CalcAppMain.

In the same solution, I clicked on "Add Project" and then created two class libraries: **MathsLib** and **ScienceLib**. These should be created where the main project is available, not inside the main project folder.

Then I clicked on the main project folder and added project references to MathsLib and ScienceLib.

### Code Implementation

#### Main Project - Program.cs

```csharp
using MathsLib;
using ScienceLib;

namespace CalcAppMain
{
    public class Program
    {
        static void Main(string[] args)
        {
            Algebra algebra = new Algebra();
            AeroScience aeroScience = new AeroScience();
            Console.WriteLine(aeroScience.CalculateLift(10.0, 1.2, 1.225, 50.0));
            Console.WriteLine(algebra.Add(5, 10));
            Console.WriteLine("Hello, World!");
        }
    }
}
```

#### MathsLib - Algebra.cs

```csharp
namespace MathsLib
{
    /// <summary>
    /// Provides basic algebraic operations.
    /// </summary>
    public class Algebra
    {
        /// <summary>
        /// Calculates the sum of two 32-bit signed integers.
        /// </summary>
        /// <param name="a">The first number to add.</param>
        /// <param name="b">The second number to add.</param>
        /// <returns>The sum of the two specified integers.</returns>
        public int Add(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// This method subtracts two integers and returns the result.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int Subtract(int a, int b)
        {
            return a - b;
        }

        /// <summary>
        /// Calculates the product of two integers.
        /// </summary>
        /// <param name="a">The first integer to multiply.</param>
        /// <param name="b">The second integer to multiply.</param>
        /// <returns>The product of the two specified integers.</returns>
        public int Multiply(int a, int b)
        {
            return a * b;
        }

        /// <summary>
        /// Divides one integer by another and returns the result as an integer quotient.
        /// </summary>
        /// <param name="a">The dividend. This is the number to be divided.</param>
        /// <param name="b">The divisor. This is the number by which to divide. Cannot be zero.</param>
        /// <returns>The integer result of dividing <paramref name="a"/> by <paramref name="b"/>.</returns>
        /// <exception cref="DivideByZeroException">Thrown when <paramref name="b"/> is zero.</exception>
        public int Divide(int a, int b) {
            if (b == 0) {
                throw new DivideByZeroException("Denominator cannot be zero.");
            }
            return a / b;
        }
    }
}
```

#### ScienceLib - AeroScience.cs

```csharp
namespace ScienceLib
{
    public class AeroScience
    {
        /// <summary>
        /// Calculates the aerodynamic lift force based on the specified area, lift coefficient, air density, and
        /// velocity.
        /// </summary>
        /// <remarks>This method uses the standard lift equation: Lift = 0.5 × area × coefficient ×
        /// density × velocity². Ensure that all input values use consistent units to obtain a correct result.</remarks>
        /// <param name="area">The reference area, in square meters, over which the lift is generated. Must be greater than zero.</param>
        /// <param name="coefficient">The lift coefficient, a dimensionless number representing the lift characteristics of the object.</param>
        /// <param name="density">The air density, in kilograms per cubic meter, at which the lift is calculated. Must be greater than zero.</param>
        /// <param name="velocity">The velocity of the object relative to the air, in meters per second. Must be greater than or equal to zero.</param>
        /// <returns>The calculated lift force, in newtons.</returns>
        public double CalculateLift(double area, double coefficient, double density, double velocity)
        {
            return 0.5 * area * coefficient * density * velocity * velocity;
        }
    }
}
```

---

## Project 2: Abstract Class - Employee Tax Calculation

Here's a **short, clean scenario** plus a **brief implementation description** for abstract vs virtual methods using tax calculation.

---

### Scenario (Short & Clear)

A company operates in multiple countries and employs people under different tax regulations.
Although every employee must pay tax, the **tax calculation rules vary by country** (e.g., India and the USA).
To ensure a common structure while allowing country-specific tax logic, the system is designed using **abstraction**.

---

### Why Abstract Is Used (Scenario Logic)

* Every employee **must** have a tax calculation
* The company **cannot define a single tax rule**
* Each country decides its own tax logic
* Hence, tax calculation is **forced** on child classes

---

### Short Implementation Description

* An **abstract class `Employee`** is created to represent a generic employee.
* The class contains an **abstract method `CalcTax()`**, which has no implementation.
* Country-specific classes like `IndianEmployee` and `USEmployee` **inherit** from `Employee`.
* Each child class **overrides `CalcTax()`** and implements tax logic based on its country.
* Objects are accessed using the **base class reference**, enabling **runtime polymorphism**.

---

### One-Line Summary (Interview Ready)

> Abstraction ensures that tax calculation is mandatory for all employees, while allowing different implementations based on country-specific rules.

---

### Code Implementation

#### Employee.cs

```csharp
using System;

/// <summary>
/// Represents an employee with a name and salary, and provides a base for calculating tax obligations.
/// </summary>
/// <remarks>This is an abstract base class intended to be inherited by specific employee types that implement tax
/// calculation logic. Derived classes must provide an implementation for the CalcTax method to determine the tax owed
/// based on the employee's details.</remarks>
abstract class Employee
{
    public string Name { get; set; }
    public decimal Salary { get; set; }

    /// <summary>
    /// Initializes a new instance of the Employee class with the specified name and salary.
    /// </summary>
    /// <param name="name">The name of the employee. Cannot be null or empty.</param>
    /// <param name="salary">The initial salary assigned to the employee. Must be a non-negative value.</param>
    public Employee(string name, decimal salary)
    {
        Name = name;
        Salary = salary;
    }

    // Abstract method (no body)
    public abstract decimal CalcTax(); // Each derived class must implement this method
}

/// <summary>
/// Represents an employee based in India with tax calculation logic specific to Indian tax regulations.
/// </summary>
/// <remarks>This class extends the Employee type to provide tax calculation behavior that reflects typical Indian
/// income tax brackets. Use this class when managing employees whose tax liabilities should be computed according to
/// Indian standards.</remarks>
class IndianEmployee : Employee
{
    public IndianEmployee(string name, decimal salary)
        : base(name, salary)
    {
    }

    public override decimal CalcTax()
    {
        if (Salary <= 500000)
            return Salary * 0.10m;
        else
            return Salary * 0.20m;
    }
}

/// <summary>
/// Represents an employee based in the United States, providing tax calculation specific to U.S. tax rates.
/// </summary>
/// <remarks>This class extends the Employee type to implement U.S.-specific tax logic. Use this class when
/// working with employees whose tax calculations should follow U.S. rules.</remarks>

class USEmployee : Employee
{
    public USEmployee(string name, decimal salary)
        : base(name, salary)
    {
    }

    public override decimal CalcTax()
    {
        return Salary * 0.25m;
    }
}
```

#### Program.cs

```csharp
/// <summary>
/// Provides the entry point for the application.
/// </summary>
/// <remarks>This class contains the Main method, which demonstrates tax calculation for employees in different
/// regions by creating instances of employee types and displaying their calculated taxes.</remarks>
class Program
{
    /// <summary>
    /// Serves as the entry point for the application.
    /// </summary>
    /// <remarks>This method demonstrates the calculation of tax for employees in different countries by
    /// creating instances of employee types and displaying their respective tax amounts. The method is intended to be
    /// called by the runtime and should not be invoked directly.</remarks>
    static void Main()
    {
        Employee emp1 = new IndianEmployee("Rahul", 600000);
        Employee emp2 = new USEmployee("John", 80000);

        Console.WriteLine($"Indian Employee Tax: {emp1.CalcTax()}");
        Console.WriteLine($"US Employee Tax: {emp2.CalcTax()}");
    }
}
```


