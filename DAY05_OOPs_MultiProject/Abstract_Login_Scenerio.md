# Abstract Class – Login Scenario (C#)

## Scenario

We are developing an application that supports multiple modules (Science, Maths, etc.).
Each module requires **Login** and **Logout** functionality, but the authentication logic
may differ for each module.

To ensure:
- A **common contract** for login operations
- **Mandatory implementation** of Login and Logout
- **Flexibility** to implement module-specific logic

we use an **abstract class**.

---

## Implementation Approach

1. Create an **abstract class** `LoginAbs` in a common library (`CommonLib`)
   - Defines abstract methods `Login()` and `Logout()`
   - Acts as a contract for all login implementations

2. Create a **concrete class** `SciLogin` in `ScienceLib`
   - Inherits from `LoginAbs`
   - Implements Science-specific login logic

3. Use the concrete implementation in the **main application**
   - The main program depends on the implementation
   - Authentication logic remains encapsulated

---

## Code Implementation

### CommonLib – Abstract Login Contract

```csharp
namespace CommonLib
{
    /// <summary>
    /// Abstract base class defining login and logout operations.
    /// </summary>
    public abstract class LoginAbs
    {
        public abstract void Login(string username, string password);
        public abstract void Logout();
    }
}
````

---

### ScienceLib – Concrete Implementation

```csharp
using CommonLib;
using System;

namespace ScienceLib
{
    /// <summary>
    /// Science module specific login implementation.
    /// </summary>
    public class SciLogin : LoginAbs
    {
        public override void Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Username and password cannot be null or empty.");
            }

            if (username == "testUser" && password == "password123")
            {
                Console.WriteLine($"User {username} logged in.");
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }
        }

        public override void Logout()
        {
            Console.WriteLine("User logged out.");
        }
    }
}
```

---

### Main Application Usage

```csharp
using ScienceLib;

namespace CalcAppMain
{
    public class Program
    {
        static void Main(string[] args)
        {
            SciLogin sciLogin = new SciLogin();
            sciLogin.Login("testUser", "password123");
        }
    }
}
```

---

## Key Takeaways

* **Abstract class** enforces implementation of login behavior
* **Different modules** can have different authentication logic
* Promotes **loose coupling**, **scalability**, and **clean architecture**
* Base class cannot be instantiated directly
* Ideal for defining **mandatory behavior with flexible implementation**

---

## Practice Example: Payment System

### Question

**How would you design a payment system that supports multiple payment methods (UPI, Cash, Card) where each payment method has its own processing logic, but all must implement a common payment interface?**

### Short Explanation

This example demonstrates using an abstract class to enforce a common payment contract while allowing different payment methods to implement their specific processing logic. The `Payment` abstract class defines:
- A shared property (`Amount`) and concrete method (`PrintReceipt()`)
- An abstract method (`Pay()`) that each payment type must implement

This design ensures all payment methods follow the same structure while maintaining flexibility for method-specific implementations.

---

### Code Implementation

#### Payment Classes

```csharp
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcAppMain
{
    /// <summary>
    /// Abstract base class for payment processing.
    /// </summary>
    abstract class Payment
    {
        public decimal Amount { get; }
        
        protected Payment(decimal amount)
        {
            Amount = amount;
        }
        
        public void PrintReceipt()
        {
            Console.WriteLine($"Amount is Received: {Amount}");
        }

        public abstract void Pay();
    }

    /// <summary>
    /// UPI payment implementation.
    /// </summary>
    class UpiPayment : Payment
    {
        public string UpiId { get; }

        public UpiPayment(decimal amount, string upiid) : base(amount) => UpiId = upiid;
        
        public override void Pay()
        {
            Console.WriteLine($"Processing UPI payment of {Amount} using UPI ID: {UpiId}");
            // Add UPI payment processing logic here
        }
    }

    /// <summary>
    /// Cash payment implementation.
    /// </summary>
    class CashPayment : Payment
    {
        public CashPayment(decimal amount) : base(amount) { }
        
        public override void Pay()
        {
            Console.WriteLine($"Processing cash payment of {Amount}");
        }
    }

    /// <summary>
    /// Card payment implementation.
    /// </summary>
    class CardPayment : Payment
    {
        public string CardNumber { get; }
        
        public CardPayment(decimal amount, string cardNumber) : base(amount) => CardNumber = cardNumber;
        
        public override void Pay()
        {
            Console.WriteLine($"Processing card payment of {Amount} using Card Number: {CardNumber}");
            // Add card payment processing logic here
        }
    }
}
```

#### Program.cs

```csharp
public class Program
{
    static void Main(string[] args)
    {
        Payment p = new UpiPayment(499.00m, "ittechgenie@upi");
        p.Pay();
        p.PrintReceipt();
    }
}
```

---

### Key Concepts Demonstrated

* **Abstract class** with both abstract and concrete methods
* **Polymorphism** - different payment types through a common base reference
* **Encapsulation** - each payment type maintains its own specific properties
* **Code reusability** - shared `PrintReceipt()` method across all payment types
* **Extensibility** - easy to add new payment methods (Bitcoin, Wallet, etc.)