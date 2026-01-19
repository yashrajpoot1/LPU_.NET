# Calculate Numbers – Academic Note

## Problem Statement

### 3. Calculate Numbers

Develop a C# application.

In the code template, a static list is already provided:

```csharp
public static List<int> NumberList;
```

In the `Program` class, implement the below-given methods.

---

## Method Specifications

### 1. AddNumber

```csharp
public void AddNumber(int Numbers)
```

**Description:**

* This method is used to add numbers to the `NumberList`.
* The number is passed as an argument.

---

### 2. GetGPAScored

```csharp
public double GetGPAScored()
```

**Description:**

* This method calculates the GPA of all numbers scored in the semester.
* GPA is calculated based on the **sum of products of each number and credit point**, divided by the **sum of credits**.
* Credit point for each subject is **3**.

**Formula:**

```
GPA = (Number1 × 3) + (Number2 × 3) + ... + (NumberN × 3)
      ---------------------------------------------------
                (ListCount × 3)
```

* If the list is empty:

  * Return **-1**
  * Print **"No Numbers Available"** in the `Main` method.

---

### 3. GetGradeScored

```csharp
public char GetGradeScored(double gpa)
```

**Description:**

* This method returns the grade based on the GPA passed as an argument.
* If GPA is **less than 5** or **greater than 10**:

  * Return a **null character**
  * Print **"Invalid GPA"** in the `Main` method.

---

## GPA to Grade Mapping

| GPA Range    | Grade |
| ------------ | ----- |
| Equal to 10  | S     |
| ≥ 9 and < 10 | A     |
| ≥ 8 and < 9  | B     |
| ≥ 7 and < 8  | C     |
| ≥ 6 and < 7  | D     |
| ≥ 5 and < 6  | E     |

---

## Main Method Instructions

1. Get values from the user.
2. Call the methods accordingly.
3. Display the GPA and Grade.
4. In the Sample Input/Output, **bold text** represents user input.

---

## C# Implementation (As Written)

> ⚠️ **Note:**
> The following code is included **exactly as written**,
> with **original indentation preserved** and **no logic changed**.

```csharp
using System;
using System.Globalization;
using System.Linq;
using System.Net;
namespace Learning_Thread
{
    public class Program
    {
        public static List<int> NumberList =new List<int>();
        public static void Main(string[] args)
        {
            System.Console.WriteLine("Enter the numbers of Subjects: ");
            int n = int.Parse(Console.ReadLine());
            int num = 0;
            int cnt = 0;
            while (n > 0)
            {
                System.Console.WriteLine($"Enter the {++cnt} Subject Marks: ");
                num = int.Parse(Console.ReadLine());
                AddNumber(num);
                n--;
            }

            var getgpa = GetGPAScored();
            if (getgpa == null)
            {
                System.Console.WriteLine("Invalid gpa");
                return;
            }
            Console.WriteLine($"Your GPA is {getgpa.Value:F2}");

            char? grd = GetGradeScored(getgpa.Value);
            Console.WriteLine($"Your grade is {grd}");
        }

        /// <summary>
        /// Adds a number to the NumberList.
        /// </summary>
        public static void AddNumber(int Numbers)
        {
            NumberList.Add(Numbers);
        }

        /// <summary>
        /// Calculates the GPA based on numbers in the NumberList.
        /// </summary>
        public static double? GetGPAScored()
        {
            if (NumberList.Count == 0)
            {
                return null;
            }
            
            double res = 0;
            foreach (var num in NumberList)
            {
                res += num * 3;
            }
            return (double)res / (NumberList.Count * 3);
        }

        /// <summary>
        /// Returns the grade based on the provided GPA value.
        /// </summary>
        public static char? GetGradeScored(double gpa)
        {
            if (gpa > 10 || gpa < 5)
                return null;

            if (gpa >= 9.99)
                return 'S';
            else if (gpa >= 9)
                return 'A';
            else if (gpa >= 8)
                return 'B';
            else if (gpa >= 7)
                return 'C';
            else if (gpa >= 6)
                return 'D';
            else
                return 'E';
        }
    }
}
```

---

## Conclusion

This program demonstrates:

* Use of `List<int>` for data storage
* Method-based GPA calculation
* Conditional grading logic
* Nullable return types for validation
* Clean separation of input, processing, and output
