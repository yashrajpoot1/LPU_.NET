# Construction Estimate – Validation with Custom Exception

## Problem Statement

Design a **Construction Estimate validation system** using object-oriented programming and **custom exception handling** in C#.

The application should:

* Store construction and site area details.
* Validate whether the construction area is permitted based on site area.
* Throw and handle a custom exception if the construction estimate is not approved.

---

## Class Design Requirements

### EstimateDetails Class

The `EstimateDetails` class must include the following **public properties**:

| Property Name    | Data Type |
| ---------------- | --------- |
| ConstructionArea | float     |
| SiteArea         | float     |

---

## Method Specification

### ValidateConstructionEstimate Method

**Method Signature:**

```csharp
public EstimateDetails ValidateConstructionEstimate(float ConstructionArea, float SiteArea)
```

**Description:**

* Checks whether the **Construction Area is less than or equal to the Site Area**.
* If the condition is satisfied:

  * Creates an `EstimateDetails` object.
  * Copies the Construction Area and Site Area values.
  * Returns the object.
* If the Construction Area is **greater than** the Site Area:

  * Throws a **user-defined exception** named `ConstructionEstimateException` with the message:

```
"Sorry Your Construction Estimate is not approved."
```

---

## Exception Handling Requirements

* Create a custom exception class `ConstructionEstimateException` that **inherits from `Exception`**.
* The exception object itself must display the specified message.
* In the `Main` method:

  * Call `ValidateConstructionEstimate` inside a `try` block.
  * If validation succeeds, display:

    ```
    Approved
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
        // Entry point of the program
        // Here we are validating construction estimate details
        public static void Main(string[] args)
        {
            try
            {
                EstimateDetails estimateDetails = ValidateConstructionEstimte(452, 852); // Approved
                // EstimateDetails estimateDetails = ValidateConstructionEstimte(952, 852); //Not Approved

                System.Console.WriteLine("Approved");
            }
            catch (ConstructionEstimateException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        // Method to validate construction estimate
        public static EstimateDetails ValidateConstructionEstimte(float ConstructionArea, float SiteArea)
        {
            if (ConstructionArea <= SiteArea)
            {
                EstimateDetails estimateDetails = new EstimateDetails
                {
                    ConstructionArea = ConstructionArea,
                    SiteArea = SiteArea,
                };
                return estimateDetails;
            }
            else
            {
                throw new ConstructionEstimateException();
            }
        }

    }

    // Class to hold estimate details
    public class EstimateDetails
    {
        public float ConstructionArea { get; set; }
        public float SiteArea { get; set; }
    }
    
    // Custom exception for construction estimate validation
    public class ConstructionEstimateException : Exception
    {
        public override string Message =>
            "Sorry Your Construction Estimate is not approved.";
    }

}
```

---

## Explanation (Academic)

* The `EstimateDetails` class acts as a **data container** for construction-related measurements.
* The `ValidateConstructionEstimte` method encapsulates the **business rule** that construction area must not exceed site area.
* A **custom exception** (`ConstructionEstimateException`) is used to represent rejection of construction estimates.
* The `try-catch` block ensures **controlled exception handling** and prevents abnormal program termination.
* This approach improves **readability, maintainability, and clarity** of validation logic.
