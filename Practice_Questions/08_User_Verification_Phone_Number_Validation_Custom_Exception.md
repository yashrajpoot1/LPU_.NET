# User Verification – Phone Number Validation with Custom Exception

## Problem Statement

Design a **User Verification module** to validate a user’s phone number using object-oriented programming and **custom exception handling** in C#.

The system should:

* Store user details.
* Validate the phone number based on length.
* Throw and handle a custom exception when the phone number is invalid.

---

## Class Design Requirements

### User Class

The `User` class must include the following **public properties**:

| Property Name | Data Type |
| ------------- | --------- |
| Name          | string    |
| PhoneNumber   | string    |

---

## Method Specification

### ValidatePhoneNumber Method

**Method Signature:**

```csharp
public User ValidatePhoneNumber(string name, string phoneNumber)
```

**Description:**

* Checks whether the **length of the phone number is exactly 10**.
* If the length is valid:

  * Copies the name and phone number into a `User` object.
  * Returns the `User` object.
* If the phone number length is **not equal to 10**:

  * Throws a **user-defined exception** named `InvalidPhoneNumberException` with the message:

```
"Invalid Phone Number"
```

---

## Exception Handling Requirements

* Create a custom exception class `InvalidPhoneNumberException` that **inherits from `Exception`**.
* The exception object itself must display the specified message.
* In the `Main` method:

  * Invoke `ValidatePhoneNumber` inside a `try` block.
  * If validation succeeds, display:

    ```
    Valid
    ```
  * If an exception occurs, catch it and display the exception message.
* Output is **case-sensitive**.

---

## C# Implementation

> ⚠️ **Important:**
> The following code is preserved **exactly as provided**.
> No logic, naming, indentation, or structure has been modified.

```csharp
namespace TecStack_Practice
{
    // Main program class
    public class Program
    {
        // Entry point of the program
        public static void Main(string[] args)
        {
            try
            {
                // User user = ValidatePhoneNumber("Asad", "8252620527"); //Valid
                User user = ValidatePhoneNumber("Asad", "8528624"); //Not Valid
                System.Console.WriteLine("Valid");
            }
            catch (InvalidPhoneNumberException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        // Method to validate phone number
        public static User ValidatePhoneNumber(string name, string phone)
        {
            if (phone.Length == 10)
            {
                User user = new User
                {
                    Name = name,
                    PhoneNumber = phone,
                };
                return user;
            }
            else
            {
                throw new InvalidPhoneNumberException();
            }
        }

    }

    // User class to hold user information
    public class User
    {
        public string? Name{ get; set; }
        public string? PhoneNumber { get; set; }
    }
    
    // Custom exception for invalid phone numbers
    public class InvalidPhoneNumberException : Exception
    {
        public override string Message =>
            "Invalid Phone Number";
    }

}
```

---

## Explanation (Academic)

* The `User` class stores basic user identification details.
* The `ValidatePhoneNumber` method enforces the business rule that a phone number must contain **exactly 10 characters**.
* A **custom exception** (`InvalidPhoneNumberException`) is used to clearly represent validation failure.
* The `try-catch` mechanism ensures **controlled exception handling** and meaningful user feedback.
* This design improves **clarity, modularity, and robustness** of the verification logic.
