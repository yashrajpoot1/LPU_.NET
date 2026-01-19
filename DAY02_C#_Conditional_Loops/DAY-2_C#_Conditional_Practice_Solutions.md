# Day 2 – C# Conditional Practice Solutions

## 🔹 Conditionals – Example Program

### 🔸 Goal

* Take user input
* Check **Odd / Even**
* Exit when user enters `q`

---

### 🔸 Key Concepts Used

* `while` loop
* `if-else`
* `int.TryParse()`
* Object method call
* String comparison (case-insensitive)

---

### ✅ Sample Program (Well Structured)

```csharp
using System;

/// <summary>
/// Sample program to check Odd or Even numbers
/// </summary>
class Program
{
    /// <summary>
    /// Checks whether a number is Even
    /// </summary>
    /// <param name="num">Input number</param>
    /// <returns>true if even, false if odd</returns>
    public bool IsEven(int num)
    {
        return num % 2 == 0;
    }

    /// <summary>
    /// Entry point of the program
    /// </summary>
    public static void Main()
    {
        Program pro = new Program();

        #region Variable Initialization
        Console.Write("Enter number (q to quit): ");
        string? choice = Console.ReadLine();
        int number;
        #endregion

        #region Process
        while (choice != "q" && choice != "Q")
        {
            if (int.TryParse(choice, out number))
            {
                Console.WriteLine(pro.IsEven(number) ? "Even" : "Odd");
            }
            else
            {
                Console.WriteLine("Invalid input");
            }

            Console.Write("Enter number (q to quit): ");
            choice = Console.ReadLine();
        }
        #endregion

        Console.WriteLine("Program exited successfully.");
    }
}
```

## Started Conditionals

### Program (Fine Tuned)

```csharp
using System;

/// <summary>
/// This is Sample Programm
/// </summary>
class Program
{
    /// <summary>
    /// This is checking given number is Even or Odd.
    /// </summary>
    /// <param name="num">This is Input to check even or odd.</param>
    /// <returns>True or False</returns>
    public bool IsEven(int num)
    {
        return num % 2 == 0;
    }

    /// <summary>
    /// This is Main Method of the Program
    /// </summary>
    public static void Main()
    {
        Program pro = new Program();

        #region Variable Initialization and Declarations
        Console.Write("Enter the number to find Odd or Even (q for quit): ");
        string? choice = Console.ReadLine();   // nullable to avoid CS8600
        int lNumber = 0;
        bool checkResult = false;
        string output = string.Empty;
        #endregion

        #region Process and Output
        while (!string.Equals(choice?.Trim(), "q", StringComparison.OrdinalIgnoreCase))
        {
            if (int.TryParse(choice, out lNumber))
            {
                checkResult = pro.IsEven(lNumber);
                output = checkResult ? "Even" : "Odd";
                Console.WriteLine(output);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number or 'q' to quit.");
            }

            Console.Write("Enter the number to find Odd or Even (q for quit): ");
            choice = Console.ReadLine();
        }
        #endregion

        Console.WriteLine("Program exited.");
    }
}
```

---

## Practice Questions on Conditionals

### Practice Q1: Height Category

Accept height in cm:

* `< 150` → Dwarf
* `150–165` → Average
* `165–190` → Tall
* `> 190` → Abnormal

```csharp
/// <summary>
/// This is Programm for Height Category
/// </summary>
class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        #region Declaration and User Input
        Console.WriteLine("Enter a Number to check its Category");
        string? input = Console.ReadLine();
        int Height = 0;
        #endregion

        #region Check Condition
        if (int.TryParse(input, out Height))
        {
            if (Height < 150) Console.WriteLine("Dwarf");
            else if (Height >= 150 && Height < 165) Console.WriteLine("Average");
            else if (Height >= 165 && Height < 190) Console.WriteLine("Tall");
            else Console.WriteLine("Abnormal");
        }
        else
        {
            Console.WriteLine("Invalid Input");
        }
        #endregion
    }
}
```

---

### Practice Q2: Largest of Three

Find the maximum using **nested if**.

```csharp
/// <summary>
/// This is Programm for Maximum of three
/// </summary>
class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        #region Declaration and User Input
        Console.WriteLine("Enter first Number to check Maximum");
        int firstNumber = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter second Number to check Maximum");
        int secondNumber = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter third Number to check Maximum");
        int thirdNumber = int.Parse(Console.ReadLine()!);
        #endregion

        #region Business Logic
        if (firstNumber > secondNumber)
        {
            if (firstNumber > thirdNumber)
                Console.WriteLine("Maximum is " + firstNumber);
            else
                Console.WriteLine("Maximum is " + thirdNumber);
        }
        else
        {
            if (secondNumber > thirdNumber)
                Console.WriteLine("Maximum is " + secondNumber);
            else
                Console.WriteLine("Maximum is " + thirdNumber);
        }
        #endregion
    }
}
```

---

### Practice Q3: Leap Year Checker

Leap year rule:

* Divisible by **400**
* OR divisible by **4** and **NOT** divisible by **100**

```csharp
/// <summary>
/// This is Programm for Checking Leap Year
/// </summary>
class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter a year to check Leap Year or Not");
            int year = int.Parse(Console.ReadLine()!);
            #endregion

            #region Business Logic
            if (year % 4 == 0)
            {
                if (year % 100 == 0)
                {
                    if (year % 400 == 0)
                        Console.WriteLine("{0} is a Leap Year", year);
                    else
                        Console.WriteLine("{0} is not a Leap Year", year);
                }
                else
                {
                    Console.WriteLine("{0} is a Leap Year", year);
                }
            }
            else
            {
                Console.WriteLine("{0} is not a Leap Year", year);
            }
            #endregion
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid year.");
        }
    }
}
```

---

### Practice Q4: Quadratic Equation

Calculate roots of:

$$ax^2 + bx + c = 0$$

```csharp
/// <summary>
/// This is Program for Checking Quadratic Equation ax^2 + bx + c = 0
/// </summary>
class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter FirstNumber of Quadratic Equation:");
            int firstNumber = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter SecondNumber of Quadratic Equation:");
            int secondNumber = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter ThirdNumber of Quadratic Equation:");
            int thirdNumber = int.Parse(Console.ReadLine()!);
            #endregion

            #region Calculation
            double discriminant = (secondNumber * secondNumber) - (4 * firstNumber * thirdNumber);

            if (discriminant > 0)
            {
                double root1 = (-secondNumber + Math.Sqrt(discriminant)) / (2 * firstNumber);
                double root2 = (-secondNumber - Math.Sqrt(discriminant)) / (2 * firstNumber);
                Console.WriteLine($"Roots are real and different. Root1: {root1}, Root2: {root2}");
            }
            else if (discriminant == 0)
            {
                double root = -secondNumber / (2 * firstNumber);
                Console.WriteLine($"Roots are real and same. Root: {root}");
            }
            else
            {
                Console.WriteLine("Roots are complex and different.");
            }
            #endregion
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}
```

---

### Practice Q5: Admission Eligibility

Criteria:

* Math ≥ 65
* Physics ≥ 55
* Chemistry ≥ 50
* AND `(Total ≥ 180 OR Math + Physics ≥ 140)`

```csharp
/// <summary>
/// This is Programm for Checking Admission Eligibility
/// </summary>
class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter your Math Marks:");
            int MathMarks = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter your Physics Marks:");
            int PhysicsMarks = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter Your Chemistry Marks:");
            int ChemistryMarks = int.Parse(Console.ReadLine()!);

            int sum = MathMarks + PhysicsMarks + ChemistryMarks;
            int PhysicsMathSum = MathMarks + PhysicsMarks;
            #endregion

            #region Calculation
            if (MathMarks >= 65 && PhysicsMarks >= 55 && ChemistryMarks >= 50)
            {
                if (sum >= 180 || PhysicsMathSum >= 140)
                    Console.WriteLine("You are Eligible for admission");
                else
                    Console.WriteLine("You are Not Eligible for admission");
            }
            else
            {
                Console.WriteLine("You are Not Eligible for admission");
            }
            #endregion
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}
```

---
### PQ6: Electric Bill Calculation

```csharp

using System;
/// <summary>
///Electricity Bill: Calculate bill: First 199 units @ 1.20; 200-400 @ 1.50; 400-600 @ 1.80; above 600 @ 2.00. Add 15% surcharge if bill > 400.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter the number of units consumed:");
            double units = Convert.ToDouble(Console.ReadLine());
            double billAmount = 0;
            #endregion

            #region Bill Calculation
            if (units <= 199)
            {
                billAmount = units * 1.20;
            }
            else if (units >= 200 && units < 400)
            {
                billAmount = (199 * 1.20) + ((units - 199) * 1.50);
            }
            else if (units >= 400 && units < 600)
            {
                billAmount = (199 * 1.20) + (200 * 1.50) + ((units - 399) * 1.80);
            }
            else if (units >= 600)
            {
                billAmount = (199 * 1.20) + (200 * 1.50) + (200 * 1.80) + ((units - 599) * 2.00);
            }
            if (billAmount > 400)
            {
                billAmount += billAmount * 0.15;
            }
            Console.WriteLine($"Total bill amount: {billAmount}");
            #endregion
            
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}

```

---

### PQ7: Check Vowels

```csharp
using System;
/// <summary>
// Vowel or Consonant: Use a switch statement to check if a character is a vowel.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter a character:");
            char inputChar = Convert.ToChar(Console.ReadLine().ToLower());
            #endregion

            #region Vowel or Consonant Check
            switch (inputChar)
            {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                    Console.WriteLine($"{inputChar} is a vowel.");
                    break;
                default:
                    if (char.IsLetter(inputChar))
                    {
                        Console.WriteLine($"{inputChar} is a consonant.");
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid alphabet character.");
                    }
                    break;
            }
            #endregion

        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}

```

---

### PQ8: Triangle Type Check if a triangle is Equilateral, Isosceles, or Scalene based on side lengths.

```csharp
using System;
/// <summary>
// Triangle Type: Check if a triangle is Equilateral, Isosceles, or Scalene based on side lengths.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter length of side A:");
            double sideA = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter length of side B:");
            double sideB = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter length of side C:");
            double sideC = Convert.ToDouble(Console.ReadLine());

            #endregion

            #region Triangle Type Determination
            if (sideA <= 0 || sideB <= 0 || sideC <= 0)
            {
                Console.WriteLine("Side lengths must be positive numbers.");
            }
            else if (sideA == sideB && sideB == sideC)
            {
                Console.WriteLine("The triangle is Equilateral.");
            }
            else if (sideA == sideB || sideB == sideC || sideA == sideC)
            {
                Console.WriteLine("The triangle is Isosceles.");
            }
            else
            {
                Console.WriteLine("The triangle is Scalene.");
            }
            
            #endregion

        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}

```

---

### PQ9: Quadrant Finder Take (x,y) coordinates and print which quadrant they lie in.

```csharp
using System;
/// <summary>
// Quadrant Finder: Take (x,y) coordinates and print which quadrant they lie in.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter the X coordinate:");
            double x = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter the Y coordinate:");
            double y = Convert.ToDouble(Console.ReadLine());
            #endregion

            #region Quadrant Determination
            if (x > 0 && y > 0)
            {
                Console.WriteLine("The point ({0}, {1}) lies in Quadrant 1.", x, y);
            }
            else if (x < 0 && y > 0)
            {
                Console.WriteLine("The point ({0}, {1}) lies in Quadrant 2.", x, y);
            }
            else if (x < 0 && y < 0)
            {
                Console.WriteLine("The point ({0}, {1}) lies in Quadrant 3.", x, y);
            }
            else if (x > 0 && y < 0)
            {
                Console.WriteLine("The point ({0}, {1}) lies in Quadrant 4.", x, y);
            }
            else if (x == 0 && y != 0)
            {
                Console.WriteLine("The point ({0}, {1}) lies on the Y axis.", x, y);
            }
            else if (y == 0 && x != 0)
            {
                Console.WriteLine("The point ({0}, {1}) lies on the X axis.", x, y);
            }
            else
            {
                Console.WriteLine("The point ({0}, {1}) is at the Origin.", x, y);
            }
            #endregion

        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}

```

---

### PQ10: Grade Description Input grade (E, V, G, A, F) and print (Excellent, Very Good, Good, Average, Fail) using switch.

```csharp
using System;
/// <summary>
// Grade Description: Input grade (E, V, G, A, F) and print (Excellent, Very Good, Good, Average, Fail) using switch.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter your grade (E, V, G, A, F): ");
            char grade = Convert.ToChar(Console.ReadLine().ToUpper());
            #endregion

            #region Switch Case to Determine Grade Description
            switch (grade)
            {
                case 'E':
                    Console.WriteLine("Excellent");
                    break;
                case 'V':
                    Console.WriteLine("Very Good");
                    break;
                case 'G':
                    Console.WriteLine("Good");
                    break;
                case 'A':
                    Console.WriteLine("Average");
                    break;
                case 'F':
                    Console.WriteLine("Fail");
                    break;
                default:
                    Console.WriteLine("Invalid grade entered.");
                    break;
            }
            
            #endregion

        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}

```

---

### PQ11: Valid Date Check Input day/month/year and check if it's a valid calendar date (handle Feb 29).

```csharp
using System;
/// <summary>
// Valid Date Check: Input day/month/year and check if it's a valid calendar date (handle Feb 29).
/// </summary>
class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter day:");
            int day = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter month:");
            int month = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter year:");
            int year = Convert.ToInt32(Console.ReadLine());

            #endregion

            #region Date Validation Logic
            bool isValidDate = false;
            if (year >= 1 && month >= 1 && month <= 12 && day >= 1)
            {
                int[] daysInMonth = { 31, (IsLeapYear(year) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                if (day <= daysInMonth[month - 1])
                {
                    isValidDate = true;
                }
            }
            if (isValidDate)
            {
                Console.WriteLine("The date {0}/{1}/{2} is valid.", day, month, year);
            }
            else
            {
                Console.WriteLine("The date {0}/{1}/{2} is invalid.", day, month, year);
            }
            bool IsLeapYear(int yr)
            {
                return (yr % 4 == 0 && yr % 100 != 0) || (yr % 400 == 0);
            }
            #endregion

        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}

```

---

### PQ12: ATM Withdrawal Nested if to check: 1. Card inserted, 2. Pin valid, 3. Balance sufficient.

```csharp
using System;
/// <summary>
// ATM Withdrawal: Nested if to check: 1. Card inserted, 2. Pin valid, 3. Balance sufficient.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter Card Inserted (true/false): ");
            bool isCardInserted = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("Enter Pin Valid (true/false): ");
            bool isPinValid = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("Enter Account Balance: ");
            double accountBalance = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter Withdrawal Amount: ");
            double withdrawalAmount = Convert.ToDouble(Console.ReadLine());
            #endregion

            #region ATM Withdrawal Logic
            if (isCardInserted)
            {
                if (isPinValid)
                {
                    if (accountBalance >= withdrawalAmount)
                    {
                        accountBalance -= withdrawalAmount;
                        Console.WriteLine($"Withdrawal successful! New balance: {accountBalance}");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient balance.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid PIN.");
                }
            }
            else
            {
                Console.WriteLine("Please insert your card.");
            }
            #endregion

        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}


```

---

### PQ13: Profit/Loss Calculate profit or loss percentage based on Cost Price and Selling Price.

```csharp
using System;
/// <summary>
// Profit/Loss: Calculate profit or loss percentage based on Cost Price and Selling Price.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter Cost Price:");
            double costPrice = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter Selling Price:");
            double sellingPrice = Convert.ToDouble(Console.ReadLine());

            #endregion

            #region Profit/Loss Calculation
            if (sellingPrice > costPrice)
            {
                double profit = sellingPrice - costPrice;
                double profitPercentage = (profit / costPrice) * 100;
                Console.WriteLine($"Profit: {profit}, Profit Percentage: {profitPercentage}%");
            }
            else if (costPrice > sellingPrice)
            {
                double loss = costPrice - sellingPrice;
                double lossPercentage = (loss / costPrice) * 100;
                Console.WriteLine($"Loss: {loss}, Loss Percentage: {lossPercentage}%");
            }
            else
            {
                Console.WriteLine("No profit, no loss.");
            }
            
            
            #endregion

        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}

```

---

### PQ14: Rock Paper Scissors Logic for a 2-player game using nested conditionals.

```csharp
using System;
/// <summary>
// Rock Paper Scissors: Logic for a 2-player game using nested conditionals.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Rock Paper Scissors Game
            Console.WriteLine("Player 1: Enter your choice (rock, paper, scissors):");
            string player1Choice = Console.ReadLine().ToLower();
            Console.WriteLine("Player 2: Enter your choice (rock, paper, scissors):");
            string player2Choice = Console.ReadLine().ToLower();
            #endregion

            #region Determine Winner
            if (player1Choice == player2Choice)
            {
                Console.WriteLine("It's a tie!");
            }
            else if (player1Choice == "rock")
            {
                if (player2Choice == "scissors")
                {
                    Console.WriteLine("Player 1 wins! Rock crushes scissors.");
                }
                else if (player2Choice == "paper")
                {
                    Console.WriteLine("Player 2 wins! Paper covers rock.");
                }
                else
                {
                    Console.WriteLine("Invalid choice by Player 2.");
                }
            }
            else if (player1Choice == "paper")
            {
                if (player2Choice == "rock")
                {
                    Console.WriteLine("Player 1 wins! Paper covers rock.");
                }
                else if (player2Choice == "scissors")
                {
                    Console.WriteLine("Player 2 wins! Scissors cut paper.");
                }
                else
                {
                    Console.WriteLine("Invalid choice by Player 2.");
                }
            }
            else if (player1Choice == "scissors")
            {
                if (player2Choice == "paper")
                {
                    Console.WriteLine("Player 1 wins! Scissors cut paper.");
                }
                else if (player2Choice == "rock")
                {
                    Console.WriteLine("Player 2 wins! Rock crushes scissors.");
                }
                else
                {
                    Console.WriteLine("Invalid choice by Player 2.");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice by Player 1.");
            }
            #endregion

        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}


```

---

### PQ15: Simple Calculator Use switch to perform +, -, *, / based on user operator input.

```csharp
using System;
/// <summary>
// Simple Calculator: Use switch to perform +, -, *, / based on user operator input.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main method of Program
    /// </summary>
    public static void Main(String[] args)
    {
        try
        {
            #region Declaration and User Input
            Console.WriteLine("Enter first number:");
            double num1 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter second number:");
            double num2 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter operator (+, -, *, /):");
            char op = Console.ReadLine()[0];
            double result = 0;

            #endregion

            #region Switch Case for Operation
            switch (op)
            {
                case '+':
                    result = num1 + num2;
                    break;
                case '-':
                    result = num1 - num2;
                    break;
                case '*':
                    result = num1 * num2;
                    break;
                case '/':
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    else
                    {
                        Console.WriteLine("Error: Division by zero is not allowed.");
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("Invalid operator. Please use +, -, *, or /.");
                    return;
            }
            Console.WriteLine("Result: " + result);
            #endregion

        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }
}

```
---
