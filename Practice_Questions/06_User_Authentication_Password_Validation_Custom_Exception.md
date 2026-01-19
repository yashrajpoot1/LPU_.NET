# User Authentication – Password Validation with Custom Exception

## Problem Statement

Design a **User Authentication module** that validates user registration details using object-oriented programming and **custom exception handling** in C#.

The system should:

* Store user credentials in a class.
* Validate password and confirmation password.
* Throw and handle a custom exception when passwords do not match.

---

## Class Design Requirements

### User Class

The `User` class must include the following **public properties**:

| Property Name        | Data Type |
| -------------------- | --------- |
| Name                 | string    |
| Password             | string    |
| ConfirmationPassword | string    |

---

## Method Specification

### ValidatePassword Method

**Method Signature:**

```csharp
public User ValidatePassword(string name, string password, string confirmationPassword)
```

**Description:**

* Accepts user name, password, and confirmation password.
* Creates and returns a `User` object using the provided details.
* If the password and confirmation password are **different (case-sensitive)**, it must throw a **user-defined exception** named `PasswordMismatchException` with the message:

```
"Password entered does not match"
```

---

## Exception Handling Requirements

* Create a custom exception class `PasswordMismatchException` that **inherits from `Exception`**.
* The exception object itself must display the specified error message.
* In the `Main` method:

  * Call `ValidatePassword` inside a `try` block.
  * If validation is successful, display:

    ```
    Registered Succesfully
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
    public class Program
    {
        // Entry point that demonstrates registration success and handles password mismatches.
        public static void Main(string[] args)
        {
            try{
                User user = ValidatePassword("Asad", "asad123", "asad123"); // Match Password
                // User user = ValidatePassword("Asad", "asad123", "Asad123"); // Mismatch Password
                System.Console.WriteLine("Registered Succesfully");
            }catch(PasswordMismatchException ex){
                System.Console.WriteLine(ex.Message);
            }
        }
        
        // Validates the provided credentials, returning a populated user or throwing when passwords differ.
        public static User ValidatePassword(string name, string password, string confrimpassword){
            User user = new User
            {
                Name = name,
                Password = password,
                ConfirmPassword = confrimpassword,
            };

            if (password != confrimpassword)
            {
                throw new PasswordMismatchException();
            }

            return user;
        }

    }

    public class User
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
    
    public class PasswordMismatchException : Exception
    {
        public override string Message =>
            "Password entered does not match";
    }

}
```

---

## Explanation (Academic)

* The `User` class represents a **data model** for user registration details.
* The `ValidatePassword` method encapsulates **business validation logic** for password matching.
* A **custom exception** (`PasswordMismatchException`) is used to explicitly indicate validation failure.
* The `try-catch` block ensures **controlled error handling** without terminating the program.
* Case-sensitive comparison ensures stricter authentication validation.