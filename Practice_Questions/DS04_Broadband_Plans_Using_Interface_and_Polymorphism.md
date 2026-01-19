# Broadband Plans – C# Implementation Using Interface and Polymorphism

## Problem Description

This problem focuses on implementing the functionality to calculate the **broadband subscription plan amount** based on specific criteria.

The program must be implemented in **C#** and should strictly follow the given specifications.  
Unless explicitly mentioned, consider the **default visibility** of classes, data fields, and methods.

---

## Specifications

### Interface Definition

#### Interface: `IBroadbandPlan`
- Visibility: default

**Method Definition**
- `GetBroadbandPlanAmount()`
  - Return type: `int`
  - Visibility: default

---

## Class Definitions

### Class: `Black` (Implements `IBroadbandPlan`)

- Visibility: default

#### Member Fields
- `_isSubscriptionValid`
  - Type: `bool`
  - Visibility: private
  - Access: readonly

- `_discountPercentage`
  - Type: `int`
  - Visibility: private
  - Access: readonly

- `PlanAmount`
  - Value: `3000`
  - Type: `int`
  - Visibility: private
  - Access: constant

#### Constructor
- `Black(bool isSubscriptionValid, int discountPercentage)`
  - Initializes subscription validity and discount percentage
  - Throws `ArgumentOutOfRangeException` if:
    - `discountPercentage < 0`
    - `discountPercentage > 50`

#### Method
- `GetBroadbandPlanAmount()`
  - Returns discounted amount if subscription is valid
  - Otherwise returns the base plan amount

---

### Class: `Gold` (Implements `IBroadbandPlan`)

- Visibility: default

#### Member Fields
- `_isSubscriptionValid`
  - Type: `bool`
  - Visibility: private
  - Access: readonly

- `_discountPercentage`
  - Type: `int`
  - Visibility: private
  - Access: readonly

- `PlanAmount`
  - Value: `1500`
  - Type: `int`
  - Visibility: private
  - Access: constant

#### Constructor
- `Gold(bool isSubscriptionValid, int discountPercentage)`
  - Initializes subscription validity and discount percentage
  - Throws `ArgumentOutOfRangeException` if:
    - `discountPercentage < 0`
    - `discountPercentage > 30`

#### Method
- `GetBroadbandPlanAmount()`
  - Returns discounted amount if subscription is valid
  - Otherwise returns the base plan amount

---

### Class: `SubscribePlan`

#### Member Field
- `_broadbandPlans`
  - Type: `IList<IBroadbandPlan>`
  - Visibility: private
  - Access: readonly

#### Constructor
- `SubscribePlan(IList<IBroadbandPlan> broadbandPlans)`
  - Initializes the plan list
  - Throws `ArgumentNullException` if input is `null`

#### Method
- `GetSubscriptionPlan()`
  - Return type: `IList<Tuple<string, int>>`
  - Returns plan type and calculated subscription amount
  - Throws `ArgumentNullException` if plan list is null

---

## Sample Input

```csharp
var plans = new List<IBroadbandPlan>()
{
    new Black(true, 50),
    new Black(false, 10),
    new Gold(true, 30),
    new Black(true, 20),
    new Gold(false, 20)
};

var subscriptionPlans = new SubscribePlan(plans);
var result = subscriptionPlans.GetSubscriptionPlan();

foreach (var item in result)
{
    Console.WriteLine($"{item.Item1}, {item.Item2}");
}
```

---

## Sample Output

```
Black, 1500
Black, 3000
Gold, 1050
Black, 2400
Gold, 1500
```

---

## Complete C# Implementation

```csharp
using System;
using System.Collections.Generic;

namespace BroadbandPlans
{
    /// <summary>
    /// Defines the contract for broadband subscription plans.
    /// </summary>
    interface IBroadbandPlan
    {
        /// <summary>
        /// Calculates and returns the payable amount for the plan.
        /// </summary>
        /// <returns>The subscription amount in whole currency units.</returns>
        int GetBroadbandPlanAmount();
    }

    /// <summary>
    /// Represents the Black broadband plan with optional discount (0–50%).
    /// </summary>
    class Black : IBroadbandPlan
    {
        // Indicates whether discount should be applied.
        private readonly bool _isSubscriptionValid;
        // Percentage discount to apply when subscription is valid.
        private readonly int _discountPercentage;
        // Base plan amount for Black.
        private const int PlanAmount = 3000;

        /// <summary>
        /// Creates a new Black plan and validates the discount range.
        /// </summary>
        public Black(bool isSubscriptionValid, int discountPercentage)
        {
            _isSubscriptionValid = isSubscriptionValid;
            _discountPercentage = discountPercentage;

            // Ensure discount is within allowed range for Black.
            if (_discountPercentage > 50 || _discountPercentage < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Calculates payable amount: discounted if valid; otherwise base amount.
        /// </summary>
        public int GetBroadbandPlanAmount()
        {
            if (_isSubscriptionValid == true)
            {
                // Apply percentage discount on base plan amount.
                return PlanAmount - (PlanAmount * _discountPercentage / 100);
            }
            return PlanAmount;
        }

    }

    /// <summary>
    /// Represents the Gold broadband plan with optional discount (0–30%).
    /// </summary>
    class Gold : IBroadbandPlan
    {
        // Indicates whether discount should be applied.
        private readonly bool _isSubscriptionValid;
        // Percentage discount to apply when subscription is valid.
        private readonly int _discountPercentage;
        // Base plan amount for Gold.
        private const int PlanAmount = 1500;

        /// <summary>
        /// Creates a new Gold plan and validates the discount range.
        /// </summary>
        public Gold(bool isSubscriptionValid, int discountPercentage)
        {
            _isSubscriptionValid = isSubscriptionValid;
            _discountPercentage = discountPercentage;

            // Ensure discount is within allowed range for Gold.
            if (discountPercentage < 0 || discountPercentage > 30)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Calculates payable amount: discounted if valid; otherwise base amount.
        /// </summary>
        public int GetBroadbandPlanAmount()
        {
            if (_isSubscriptionValid)
            {
                // Apply percentage discount on base plan amount.
                return PlanAmount - (PlanAmount * _discountPercentage / 100);
            }
            return PlanAmount;
        }
    }
    
    /// <summary>
    /// Aggregates broadband plans and produces (plan type, amount) subscriptions.
    /// </summary>
    class SubscribePlan
    {
        // Backing list of plans to process.
        private readonly IList<IBroadbandPlan> _broadbandPlans;

        /// <summary>
        /// Initializes with a list of plans; throws if list is null.
        /// </summary>
        public SubscribePlan(IList<IBroadbandPlan> broadbandPlans)
        {
            _broadbandPlans = broadbandPlans ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// Builds the subscription output as tuples of (plan name, amount).
        /// </summary>
        public IList<Tuple<string, int>> GetSubscriptionPlan()
        {
            // Defensive check (should not trigger due to constructor guard).
            if (_broadbandPlans == null)
            {
                throw new ArgumentNullException();
            }

            IList<Tuple<string, int>> result = new List<Tuple<string, int>>();

            foreach (var plan in _broadbandPlans)
            {
                // Use runtime type name ("Black" or "Gold") for output label.
                string planType = plan.GetType().Name;
                // Compute amount via the plan's implementation.
                int amount = plan.GetBroadbandPlanAmount();
                result.Add(new Tuple<string, int>(planType, amount));
            }

            return result;
        }
    }

    public class Program
    {
        /// <summary>
        /// Demonstrates sample usage and prints computed subscription amounts.
        /// </summary>
        public static void Main(string[] args)
        {
            // Prepare sample plans following the provided specification.
            var plans = new List<IBroadbandPlan>()
            {
                new Black(true, 50),
                new Black(false, 10),
                new Gold(true, 30),
                new Black(true, 20),
                new Gold(false, 20)
            };

            var subscriptionPlans = new SubscribePlan(plans);
            var result = subscriptionPlans.GetSubscriptionPlan();

            // Print each plan type and its calculated amount.
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Item1}, {item.Item2}");
            }
        }
    }
}
```

---

## Notes

* Demonstrates **interface-based polymorphism**
* Uses **runtime binding** to calculate plan amounts
* Enforces **business rules via exceptions**
* Suitable for **OOPS exams, lab submissions, and GitHub portfolios**
