# C# Enums — Named Constants

- Objectives: use `enum` for type-safe named constants and avoid magic numbers.

## Overview

- An `enum` defines a set of named integral values. Default underlying type is `int`, starting at 0.
- Use enums for states, categories, days, error codes, and permissions.

## Custom enum example

```csharp
using System;

namespace Coding_Class
{
    public enum DaysOfWeek
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    public class EnumDemo
    {
        public static void Main()
        {
            DaysOfWeek today = DaysOfWeek.Wednesday;
            Console.WriteLine($"Today is: {today}");

            foreach (DaysOfWeek day in Enum.GetValues(typeof(DaysOfWeek)))
            {
                Console.WriteLine($"Name: {day}, Numeric: {(int)day}");
            }
        }
    }
}
```

## Using built-in `DayOfWeek`

```csharp
using System;

namespace Coding_Class
{
    public class MenuPlanner
    {
        public static void Main()
        {
            var day = DayOfWeek.Thursday;
            Console.WriteLine($"Menu for {day}: {MenuByDay(day)}");
        }

        public static string MenuByDay(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Sunday:    return "Roast Chicken";
                case DayOfWeek.Monday:    return "Spaghetti Bolognese";
                case DayOfWeek.Tuesday:   return "Taco Tuesday";
                case DayOfWeek.Wednesday: return "Grilled Salmon";
                case DayOfWeek.Thursday:  return "Stir Fry Vegetables";
                case DayOfWeek.Friday:    return "Pizza Night";
                case DayOfWeek.Saturday:  return "BBQ Ribs";
                default:                  return "Unknown Day";
            }
        }
    }
}
```

## Tips

- Cast to numeric: `int value = (int)DayOfWeek.Thursday;`.
- Parse strings safely: `Enum.TryParse("Wednesday", out DaysOfWeek w)`.
- Use `[Flags]` when representing bitwise combinations; otherwise prefer simple enums.

## Practice

- Create `OrderStatus` enum (`Pending`, `Confirmed`, `Shipped`, `Delivered`, `Cancelled`) and return a message for each.
- Parse user input into `OrderStatus` using `Enum.TryParse` and handle invalid input.
