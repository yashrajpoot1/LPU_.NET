## Practice on Loops

**PQ1: Fibonacci Series**
Fibonacci Series: Display the first N terms of the Fibonacci sequence.
```csharp
/// <summary>
/// This is Programm for N fabonacci Series 
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
            Console.WriteLine("Enter your Number for Fabonacci Series:");
            int Number = int.Parse(Console.ReadLine()!);
            int firstNumber = 0, secondNumber = 1, nextNumber;
            Console.WriteLine("Fabonacci Series:");
            #endregion

            #region Calculation
            for (int i = 0; i < Number; i++)
            {
                if (i <= 1)
                {
                    nextNumber = i;
                }
                else
                {
                    nextNumber = firstNumber + secondNumber;
                    firstNumber = secondNumber;
                    secondNumber = nextNumber;
                }
                Console.WriteLine(nextNumber);
                
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

---

**PQ2: Prime Number**
Check if a number is prime using a for loop and break.

```csharp
using System;
/// <summary>
// Check if a number is prime using a for loop and break.
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
            Console.WriteLine("Enter a number to check if it is prime:");
            int number = int.Parse(Console.ReadLine());
            bool isPrime = true;

            #endregion

            #region Prime Check Logic
            if (number <= 1)
            {
                isPrime = false;
            }
            else
            {
                for (int i = 2; i <= Math.Sqrt(number); i++)
                {
                    if (number % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
            }
            Console.WriteLine(isPrime ? "The number is prime." : "The number is not prime.");
            
            
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

**PQ3: Armstrong Number**
Check if a number equals the sum of its digits raised to the power of number of digits.

```csharp
using System;
/// <summary>
//Check if a number equals the sum of its digits raised to the power of number of digits.
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
            Console.WriteLine("Enter a number:");
            int number = int.Parse(Console.ReadLine());


            #endregion

            #region Logic
            int originalNumber = number;
            int sum = 0;
            int numberOfDigits = number.ToString().Length;
            while (number > 0)
            {
                int digit = number % 10;
                sum += (int)Math.Pow(digit, numberOfDigits);
                number /= 10;
            }
            if (sum == originalNumber)
            {
                Console.WriteLine($"{originalNumber} is equal to the sum of its digits raised to the power of number of digits.");
            }
            else
            {
                Console.WriteLine($"{originalNumber} is not equal to the sum of its digits raised to the power of number of digits.");
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

**PQ4: Reverse & Palindrome**
Reverse an integer and check if it is a palindrome using while.

```csharp
using System;
/// <summary>
// Reverse an integer and check if it is a palindrome using while.
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
            Console.WriteLine("Enter an integer:");
            int number = Convert.ToInt32(Console.ReadLine());
            int reversedNumber = 0;

            #endregion

            #region Reversing the Integer
            int tempNumber = number;
            while (tempNumber != 0)
            {
                int digit = tempNumber % 10;
                reversedNumber = reversedNumber * 10 + digit;
                tempNumber /= 10;
            }
            
            Console.WriteLine("Reversed Number: " + reversedNumber);
            Console.WriteLine("Is Palindrome: " + (number == reversedNumber));
            
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

**PQ5: GCD and LCM**
Find the Greatest Common Divisor and Least Common Multiple of two numbers.

```csharp
using System;
/// <summary>
// Find the Greatest Common Divisor and Least Common Multiple of two numbers.

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
            Console.WriteLine("Enter first number:");
            int num1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter second number:");
            int num2 = int.Parse(Console.ReadLine());

            #endregion

            #region GCD Calculation
            int a = num1;
            int b = num2;
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            int gcd = a;
            int lcm = (num1 * num2) / gcd;
            Console.WriteLine("Greatest Common Divisor (GCD): " + gcd);
            Console.WriteLine("Least Common Multiple (LCM): " + lcm);   
        
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

**PQ6: Pascal's Triangle**
Use nested loops to print Pascal's triangle up to N rows.

```csharp
using System;
/// <summary>
// Pascal's Triangle Use nested loops to print Pascal's triangle up to N rows.

/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    /// </summary>
    public static void Main(String[] args)
    {
       
            Console.Write("Enter the number of rows for Pascal's Triangle: ");
            int n = int.Parse(Console.ReadLine()!);

            for (int line = 0; line < n; line++)
            {
                int C = 1; // Used to represent C(line, i)
                for (int i = 0; i <= line; i++)
                {
                    Console.Write(C + " ");
                    C = C * (line - i) / (i + 1);
                }
                Console.WriteLine();
            }
    }
}

```

---

**PQ7: Binary to Decimal**
Convert a binary number string to decimal without using built-in library functions.

```csharp
using System;
/// <summary>
/// Convert a binary number string to decimal without using built-in library functions.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    /// </summary>
    public static void Main(String[] args)
    {
       
        string binaryString = "1101"; // Example binary string
        try
        {
            int decimalValue = BinaryToDecimal(binaryString);
            Console.WriteLine($"The decimal value of binary {binaryString} is {decimalValue}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    /// <summary>
    /// Converts a binary string to its decimal equivalent.
    /// </summary>
    /// <param name="binaryString">The binary string to convert.</param>
    /// <returns>The decimal equivalent of the binary string.</returns>
    /// <exception cref="ArgumentException">Thrown when the input string is not a valid binary number.</exception>
    public static int BinaryToDecimal(string binaryString)
    {
        int decimalValue = 0;
        foreach (char c in binaryString)
        {
            if (c != '0' && c != '1')
            {
                throw new ArgumentException("Input string is not a valid binary number.");
            }
            decimalValue = decimalValue * 2 + (c - '0');
        }
        return decimalValue;
    }
}
```

---

**PQ8: Diamond Pattern**
Print a diamond shape using `*` characters with nested loops.

```csharp
using System;
/// <summary>
/// Print a diamond shape using `*` characters with nested loops.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    /// </summary>
    public static void Main(String[] args)
    {
        int n = 5; // Height of the diamond

        // Upper part of the diamond
        for (int i = 1; i <= n; i++)
        {
            // Print leading spaces
            for (int j = i; j < n; j++)
            {
                Console.Write(" ");
            }
            // Print stars
            for (int k = 1; k <= (2 * i - 1); k++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }

        // Lower part of the diamond
        for (int i = n - 1; i >= 1; i--)
        {
            // Print leading spaces
            for (int j = n; j > i; j--)
            {
                Console.Write(" ");
            }
            // Print stars
            for (int k = 1; k <= (2 * i - 1); k++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }

    }

}
```

---

**PQ9: Factorial (Large Numbers)**
Calculate N! and handle potential overflow for larger integers.

```csharp
using System;
/// <summary>
/// Calculate N! and handle potential overflow for larger integers.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    /// </summary>
    public static void Main(String[] args)
    {
        Console.WriteLine("Enter a non-negative integer to calculate its factorial:");
        if (int.TryParse(Console.ReadLine(), out int number) && number >= 0)
        {
            try
            {
                long result = Factorial(number);
                Console.WriteLine($"Factorial of {number} is {result}");
            }
            catch (OverflowException)
            {
                Console.WriteLine("The result is too large to fit in a 64-bit integer.");
            }
        }
        else
        {
            Console.WriteLine("Please enter a valid non-negative integer.");
        }

        static long Factorial(int n)
        {
            if (n == 0 || n == 1)
                return 1;

            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                checked
                {
                    result *= i;
                }
            }
            return result;
        }
        

    }

}
```

---

**PQ10: Guessing Game**
Use do-while to let a user guess a secret number until they get it right.

```csharp
using System;
/// <summary>
/// Use do-while to let a user guess a secret number until they get it right.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    /// </summary>
    public static void Main(String[] args)
    {
        int secretNumber = 7;
        int userGuess = 0;

        do
        {
            Console.Write("Guess the secret number (between 1 and 10): ");
            string input = Console.ReadLine();

            // Validate input
            if (int.TryParse(input, out userGuess))
            {
                if (userGuess < 1 || userGuess > 10)
                {
                    Console.WriteLine("Please enter a number within the range of 1 to 10.");
                }
                else if (userGuess < secretNumber)
                {
                    Console.WriteLine("Too low! Try again.");
                }
                else if (userGuess > secretNumber)
                {
                    Console.WriteLine("Too high! Try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }

        } while (userGuess != secretNumber);

        Console.WriteLine("Congratulations! You've guessed the secret number.");
        
    }

}
```

---

**PQ11: Sum of Digits (Digital Root)**
Repeatedly sum digits of a number until the result is a single digit.

```csharp
using System;
/// <summary>
/// Repeatedly sum digits of a number until the result is a single digit.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    /// </summary>
    public static void Main(String[] args)
    {
        Console.WriteLine("Enter a number:");
        string input = Console.ReadLine();
        int number;
        if (int.TryParse(input, out number))
        {
            int result = SumDigitsUntilSingleDigit(number);
            Console.WriteLine($"The single digit result is: {result}");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid integer.");
        }

        static int SumDigitsUntilSingleDigit(int number)
        {
            while (number >= 10)
            {
                int sum = 0;
                while (number > 0)
                {
                    sum += number % 10;
                    number /= 10;
                }
                number = sum;
            }
            return number;
        }

    }

}
```

---

**PQ12: Continue Usage**
Print numbers from 1 to 50, but skip all multiples of 3 using `continue`.

```csharp
using System;
/// <summary>
/// Print numbers from 1 to 50, but skip all multiples of 3 using `continue`.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    /// </summary>
    public static void Main(String[] args)
    {
        for (int i = 1; i <= 50; i++)
        {
            if (i % 3 == 0)
            {
                continue;
            }
            Console.WriteLine(i);
        }
        

    }

}
```

---

**PQ13: Menu System**
Use do-while and switch to create a persistent console menu.

```csharp
using System;
/// <summary>
/// Use do-while and switch to create a persistent console menu.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    /// </summary>
    public static void Main(String[] args)
    {
        int choice;
        do
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Option 1");
            Console.WriteLine("2. Option 2");
            Console.WriteLine("3. Option 3");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice (1-4): ");
            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("You selected Option 1.");
                    break;
                case 2:
                    Console.WriteLine("You selected Option 2.");
                    break;
                case 3:
                    Console.WriteLine("You selected Option 3.");
                    break;
                case 4:
                    Console.WriteLine("Exiting the program.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            Console.WriteLine();
        } while (choice != 4);
    }
    
}
```

---

**PQ14: Strong Number**
Check if the sum of the factorial of digits is equal to the number itself.

```csharp
using System;
/// <summary>
/// Check if the sum of the factorial of digits is equal to the number itself.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    /// </summary>
    public static void Main(String[] args)
    {
        int number = 145; // Example number
        if (IsFactorion(number))
        {
            Console.WriteLine($"{number} is a factorion.");
        }
        else
        {
            Console.WriteLine($"{number} is not a factorion.");
        }
    }

    /// <summary>
    /// Check if a number is a factorion
    /// </summary>
    /// <param name="num">The number to check</param>
    /// <returns>True if the number is a factorion, otherwise false</returns>
    public static bool IsFactorion(int num)
    {
        int sum = 0;
        int temp = num;

        while (temp > 0)
        {
            int digit = temp % 10;
            sum += Factorial(digit);
            temp /= 10;
        }

        return sum == num;
    }
    /// <summary>
    /// Calculate the factorial of a digit
    /// </summary>
    /// <param name="digit">The digit to calculate the factorial for</param>
    /// <returns>The factorial of the digit</returns>
    public static int Factorial(int digit)
    {
        if (digit == 0 || digit == 1)
            return 1;

        int result = 1;
        for (int i = 2; i <= digit; i++)
        {
            result *= i;
        }
        return result;
    }

}
```

---

**PQ15: Search with Goto**
Implement a deep-nested loop search that uses `goto` to exit all levels instantly upon finding a result.

```csharp
using System;
/// <summary>
/// Implement a deep-nested loop search that uses `goto` to exit all levels instantly upon finding a result.
/// </summary>
class Program
{
    /// <summary>
    /// This is Main methode of Program
    /// </summary>
    /// </summary>
    public static void Main(String[] args)
    {
        int target = 42; // The value we are searching for
        bool found = false;

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                for (int k = 0; k < 100; k++)
                {
                    // Simulate some computation
                    int value = i * j + k;

                    if (value == target)
                    {
                        found = true;
                        goto Found; // Exit all loops immediately
                    }
                }
            }
        }
    Found:
        if (found)
        {
            Console.WriteLine("Target found!");
        }
        else
        {
            Console.WriteLine("Target not found.");
        }
    }

}

```

---
