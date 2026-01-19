# Ecommerce Application – Payment Processing with Custom Exception

## Problem Statement

Develop a simple **Ecommerce Application** to process user payments using object-oriented principles and **custom exception handling** in C#.

The application should:

* Store user and purchase details in a class.
* Process a payment using a method.
* Throw and handle a user-defined exception when the wallet balance is insufficient.

---

## Class Design Requirements

### EcommerceShop Class

The `EcommerceShop` class must include the following **public properties**:

| Property Name       | Data Type |
| ------------------- | --------- |
| UserName            | string    |
| WalletBalance       | double    |
| TotalPurchaseAmount | double    |

---

## Method Specification

### MakePayment Method

**Method Signature:**

```csharp
public EcommerceShop MakePayment(string name, double balance, double amount)
```

**Description:**

* Accepts purchase details such as user name, wallet balance, and total purchase amount.
* Creates and returns an `EcommerceShop` object using the provided details.
* If the wallet balance is **less than** the purchase amount, it must throw a **user-defined exception** named `InsufficientWalletBalanceException` with the message:

```
"Insufficient balance in your digital wallet"
```

---

## Exception Handling Requirements

* Create a custom exception class `InsufficientWalletBalanceException` that **inherits from `Exception`**.
* The exception object itself must display the required error message.
* In the `Main` method:

  * Call `MakePayment` inside a `try` block.
  * If payment succeeds, display:

    ```
    Payment successful
    ```
  * If an exception occurs, catch it and display the exception message.
* Output is **case-sensitive**.

---

## C# Implementation

> ⚠️ **Note:**
> The following code is preserved **exactly as provided**.
> No logic, indentation, structure, or statements have been modified.

```csharp
namespace TecStack_Practice
{
    public class Program
    {
        // Entry point that attempts a payment and reports success or wallet-balance failures.
        public static void Main(string[] args)
        {
            try
            {
                EcommerceShop shop = MakePayment("Emily", 1000, 900);
                Console.WriteLine("Payment successful");
            }
            catch (InsufficientWalletBalanceException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Processes a payment by constructing the purchase details and throwing when funds are insufficient.
        public static EcommerceShop MakePayment(string name, double balance, double amount)
        {
            EcommerceShop ecommerceShop = new EcommerceShop
            {
                UserName = name,
                WalletBalance = balance,
                TotalPurchaseAmount = amount
            };

            if (balance < amount)
            {
                throw new InsufficientWalletBalanceException();
            }

            return ecommerceShop;
        }
    }

    public class EcommerceShop
    {
        public string? UserName { get; set; }
        public double WalletBalance { get; set; }
        public double TotalPurchaseAmount { get; set; }
    }

    public class InsufficientWalletBalanceException : Exception
    {
        public override string Message =>
            "Insufficient balance in your digital wallet";
    }

}
```

---

## Explanation (Academic)

* The `EcommerceShop` class acts as a **data model** for storing user and purchase details.
* The `MakePayment` method encapsulates **business logic** for validating wallet balance.
* A **custom exception** (`InsufficientWalletBalanceException`) is used to clearly represent business-rule violations.
* The `try-catch` mechanism in `Main` ensures **graceful error handling** without crashing the program.
* Overriding the `Message` property ensures the exception itself controls the output message.