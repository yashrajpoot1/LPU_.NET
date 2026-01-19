# Find Items – Academic Note

## Problem Statement

### Functionalities

In the `Program` class, a static dictionary is already provided:

```csharp
public static SortedDictionary<string, long> ItemDetails;
```

Using this dictionary, implement the following methods.

---

## Method Specifications

### 1. FindItemDetails

```csharp
public SortedDictionary<string, long> FindItemDetails(long soldCount)
```

**Description:**

* Used to find item details based on sold count.
* If the sold count exists in `ItemDetails`, return the item as a `SortedDictionary`.
* If the sold count does not exist, return an empty `SortedDictionary`.
* If an empty dictionary is returned, print **"Invalid sold count"** in the `Main` method.

---

### 2. FindMinandMaxSoldItem

```csharp
public List<string> FindMinandMaxSoldItem()
```

**Description:**

* Finds the minimum and maximum sold items from `ItemDetails`.
* Stores item names in a `List<string>`.
* **Note:**

  * First element → minimum sold item name
  * Second element → maximum sold item name

---

### 3. SortByCount

```csharp
public Dictionary<string, long> SortByCount()
```

**Description:**

* Sorts all items in ascending order based on sold count.
* Returns the sorted result as a `Dictionary`.

---

## Main Method Instructions

1. Read item details from the user.
2. Call all required methods.
3. Display:

   * Item details by sold count
   * Minimum and maximum sold items
   * Sorted item list by sold count

---

## C# Implementation (As Written)

> ⚠️ **Note:**
> The following code is included **exactly as written**, without any modification.
> All comments and alternative approaches are preserved.

```csharp
using System;
using System.Linq;
namespace Learning_Thread
{
    public class Program
    {
        public static SortedDictionary<string, long> ItemDetails = new SortedDictionary<string, long>();

        public static void Main(string[] args)
        {
            System.Console.WriteLine("Enter the Numbers of Items you want to add: ");
            int n = int.Parse(Console.ReadLine());
            while (n > 0)
            {
                System.Console.WriteLine("Write item Name: ");
                string? item = Console.ReadLine();
                System.Console.WriteLine("Enter Item SoldCount: ");
                long count = long.Parse(Console.ReadLine()!);
                ItemDetails.Add(item, count);
                n -= 1;
            }
            long search = 2;
            var find = FindItemDetails(search);
            if (find.Count > 0)
            {
                foreach (var item in find)
                    System.Console.WriteLine($"Item: {item.Key} Is available as Count: {item.Value}");
            }
            else
            {
                System.Console.WriteLine("Invalid Sold Count");
            }

            var MinMax = FindMinandMaxSoldItem();
            System.Console.WriteLine($"Minimum Value item Name is " + MinMax[0]);
            System.Console.WriteLine($"Maximum Value item Name is " + MinMax[1]);

            var Sorteddic = SortByCount();
            System.Console.WriteLine("Sorted Item is Here: ");
            foreach (var i in Sorteddic)
            {
                System.Console.WriteLine($"Item: {i.Key} Count: {i.Value}");
            }
        }

        /// <summary>
        /// Finds and returns item details by sold count.
        /// </summary>
        public static SortedDictionary<string, long> FindItemDetails(long SoldCount)
        {
            SortedDictionary<string, long> res = new SortedDictionary<string, long>();
            foreach (var item in ItemDetails)
            {
                if (item.Value == SoldCount)
                {
                    res.Add(item.Key, item.Value);
                    return res;
                }
            }
            return res;
        }

        /// <summary>
        /// Finds and returns the minimum and maximum sold items.
        /// </summary>
        public static List<string> FindMinandMaxSoldItem()
        {
            List<string> res = new List<string>();
            long Min = long.MaxValue;
            long Max = long.MinValue;
            string MinItem = "";
            string MaxItem = "";
            foreach (var item in ItemDetails)
            {
                if (item.Value < Min)
                {
                    Min = item.Value;
                    MinItem = item.Key;
                }
                if (item.Value > Max)
                {
                    Max = item.Value;
                    MaxItem = item.Key;
                }
            }
            res.Add(MinItem);
            res.Add(MaxItem);
            return res;
        }

        /// <summary>
        /// Sorts items by count in ascending order.
        /// </summary>
        public static Dictionary<string, long> SortByCount()
        {
            // Method 1: Using LINQ with Lambda (Method Syntax)
            return ItemDetails.OrderBy(X => X.Value).ToDictionary(X => X.Key, Y => Y.Value);

            // Method 2: Using LINQ Query Syntax
            // var result = from item in ItemDetails
            //              orderby item.Value
            //              select item;
            // return result.ToDictionary(X => X.Key, Y => Y.Value);

            // Method 2: Using Loop-based approach (Manual sorting)
            // List<KeyValuePair<string, long>> items = new List<KeyValuePair<string, long>>(ItemDetails);
            // for (int i = 0; i < items.Count - 1; i++)
            // {
            //     for (int j = i + 1; j < items.Count; j++)
            //     {
            //         if (items[i].Value > items[j].Value)
            //         {
            //             var temp = items[i];
            //             items[i] = items[j];
            //             items[j] = temp;
            //         }
            //     }
            // }
            // Dictionary<string, long> result = new Dictionary<string, long>();
            // foreach (var item in items)
            // {
            //     result.Add(item.Key, item.Value);
            // }
            // return result;
        }
    }
}
```

---

## Conclusion

This program demonstrates:

* Use of `SortedDictionary` for structured data storage
* Searching elements by value
* Finding minimum and maximum values manually
* Sorting collections using LINQ
* Modular method-based design

