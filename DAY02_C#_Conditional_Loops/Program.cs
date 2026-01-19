using System;

namespace DAY02_ConditionalLoops
{
    /// <summary>
    /// Day 2 - C# Conditionals and Loops
    /// Contains examples of conditional statements and loop constructs
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== DAY 2: C# Conditionals and Loops ===\n");
            
            bool continueProgram = true;
            
            while (continueProgram)
            {
                DisplayMenu();
                string? choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        OddEvenChecker();
                        break;
                    case "2":
                        HeightCategory();
                        break;
                    case "3":
                        LargestOfThree();
                        break;
                    case "4":
                        LeapYearChecker();
                        break;
                    case "5":
                        QuadraticEquation();
                        break;
                    case "6":
                        AdmissionEligibility();
                        break;
                    case "7":
                        FibonacciSeries();
                        break;
                    case "8":
                        PrimeNumberCheck();
                        break;
                    case "9":
                        ArmstrongNumber();
                        break;
                    case "10":
                        PalindromeCheck();
                        break;
                    case "0":
                        continueProgram = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                
                if (continueProgram)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// Displays the main menu
        /// </summary>
        static void DisplayMenu()
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Odd/Even Checker");
            Console.WriteLine("2. Height Category");
            Console.WriteLine("3. Largest of Three Numbers");
            Console.WriteLine("4. Leap Year Checker");
            Console.WriteLine("5. Quadratic Equation Solver");
            Console.WriteLine("6. Admission Eligibility");
            Console.WriteLine("7. Fibonacci Series");
            Console.WriteLine("8. Prime Number Check");
            Console.WriteLine("9. Armstrong Number Check");
            Console.WriteLine("10. Palindrome Check");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");
        }

        /// <summary>
        /// Checks whether numbers are odd or even
        /// </summary>
        static void OddEvenChecker()
        {
            Console.WriteLine("\n=== Odd/Even Checker ===");
            Console.Write("Enter number (q to quit): ");
            string? choice = Console.ReadLine();

            while (choice != "q" && choice != "Q")
            {
                if (int.TryParse(choice, out int number))
                {
                    Console.WriteLine(IsEven(number) ? "Even" : "Odd");
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }

                Console.Write("Enter number (q to quit): ");
                choice = Console.ReadLine();
            }
        }

        /// <summary>
        /// Checks if a number is even
        /// </summary>
        /// <param name="num">Input number</param>
        /// <returns>true if even, false if odd</returns>
        static bool IsEven(int num)
        {
            return num % 2 == 0;
        }

        /// <summary>
        /// Categorizes height
        /// </summary>
        static void HeightCategory()
        {
            Console.WriteLine("\n=== Height Category ===");
            Console.Write("Enter height in cm: ");
            
            if (int.TryParse(Console.ReadLine(), out int height))
            {
                if (height < 150) 
                    Console.WriteLine("Dwarf");
                else if (height >= 150 && height < 165) 
                    Console.WriteLine("Average");
                else if (height >= 165 && height < 190) 
                    Console.WriteLine("Tall");
                else 
                    Console.WriteLine("Abnormal");
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }
        }

        /// <summary>
        /// Finds the largest of three numbers using nested if
        /// </summary>
        static void LargestOfThree()
        {
            Console.WriteLine("\n=== Largest of Three Numbers ===");
            
            try
            {
                Console.Write("Enter first number: ");
                int first = int.Parse(Console.ReadLine()!);
                
                Console.Write("Enter second number: ");
                int second = int.Parse(Console.ReadLine()!);
                
                Console.Write("Enter third number: ");
                int third = int.Parse(Console.ReadLine()!);

                if (first > second)
                {
                    if (first > third)
                        Console.WriteLine($"Maximum is {first}");
                    else
                        Console.WriteLine($"Maximum is {third}");
                }
                else
                {
                    if (second > third)
                        Console.WriteLine($"Maximum is {second}");
                    else
                        Console.WriteLine($"Maximum is {third}");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter valid numbers.");
            }
        }

        /// <summary>
        /// Checks if a year is a leap year
        /// </summary>
        static void LeapYearChecker()
        {
            Console.WriteLine("\n=== Leap Year Checker ===");
            
            try
            {
                Console.Write("Enter a year: ");
                int year = int.Parse(Console.ReadLine()!);

                if (year % 4 == 0)
                {
                    if (year % 100 == 0)
                    {
                        if (year % 400 == 0)
                            Console.WriteLine($"{year} is a Leap Year");
                        else
                            Console.WriteLine($"{year} is not a Leap Year");
                    }
                    else
                    {
                        Console.WriteLine($"{year} is a Leap Year");
                    }
                }
                else
                {
                    Console.WriteLine($"{year} is not a Leap Year");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid year.");
            }
        }

        /// <summary>
        /// Solves quadratic equation ax² + bx + c = 0
        /// </summary>
        static void QuadraticEquation()
        {
            Console.WriteLine("\n=== Quadratic Equation Solver ===");
            
            try
            {
                Console.Write("Enter coefficient a: ");
                double a = double.Parse(Console.ReadLine()!);
                
                Console.Write("Enter coefficient b: ");
                double b = double.Parse(Console.ReadLine()!);
                
                Console.Write("Enter coefficient c: ");
                double c = double.Parse(Console.ReadLine()!);

                if (a == 0)
                {
                    Console.WriteLine("Not a quadratic equation (a cannot be 0)");
                    return;
                }

                double discriminant = (b * b) - (4 * a * c);

                if (discriminant > 0)
                {
                    double root1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                    double root2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                    Console.WriteLine($"Roots are real and different. Root1: {root1:F2}, Root2: {root2:F2}");
                }
                else if (discriminant == 0)
                {
                    double root = -b / (2 * a);
                    Console.WriteLine($"Roots are real and same. Root: {root:F2}");
                }
                else
                {
                    Console.WriteLine("Roots are complex and different.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter valid numbers.");
            }
        }

        /// <summary>
        /// Checks admission eligibility based on marks
        /// </summary>
        static void AdmissionEligibility()
        {
            Console.WriteLine("\n=== Admission Eligibility ===");
            
            try
            {
                Console.Write("Enter Math marks: ");
                int math = int.Parse(Console.ReadLine()!);
                
                Console.Write("Enter Physics marks: ");
                int physics = int.Parse(Console.ReadLine()!);
                
                Console.Write("Enter Chemistry marks: ");
                int chemistry = int.Parse(Console.ReadLine()!);

                int total = math + physics + chemistry;
                int mathPhysics = math + physics;

                if (math >= 65 && physics >= 55 && chemistry >= 50)
                {
                    if (total >= 180 || mathPhysics >= 140)
                        Console.WriteLine("You are Eligible for admission");
                    else
                        Console.WriteLine("You are Not Eligible for admission");
                }
                else
                {
                    Console.WriteLine("You are Not Eligible for admission");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter valid numbers.");
            }
        }

        /// <summary>
        /// Displays Fibonacci series
        /// </summary>
        static void FibonacciSeries()
        {
            Console.WriteLine("\n=== Fibonacci Series ===");
            
            try
            {
                Console.Write("Enter number of terms: ");
                int n = int.Parse(Console.ReadLine()!);
                
                if (n <= 0)
                {
                    Console.WriteLine("Please enter a positive number.");
                    return;
                }

                int first = 0, second = 1;
                Console.WriteLine("Fibonacci Series:");
                
                for (int i = 0; i < n; i++)
                {
                    if (i <= 1)
                    {
                        Console.Write(i + " ");
                    }
                    else
                    {
                        int next = first + second;
                        Console.Write(next + " ");
                        first = second;
                        second = next;
                    }
                }
                Console.WriteLine();
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        /// <summary>
        /// Checks if a number is prime
        /// </summary>
        static void PrimeNumberCheck()
        {
            Console.WriteLine("\n=== Prime Number Check ===");
            
            try
            {
                Console.Write("Enter a number: ");
                int number = int.Parse(Console.ReadLine()!);
                bool isPrime = true;

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
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        /// <summary>
        /// Checks if a number is an Armstrong number
        /// </summary>
        static void ArmstrongNumber()
        {
            Console.WriteLine("\n=== Armstrong Number Check ===");
            
            try
            {
                Console.Write("Enter a number: ");
                int number = int.Parse(Console.ReadLine()!);
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
                    Console.WriteLine($"{originalNumber} is an Armstrong number.");
                }
                else
                {
                    Console.WriteLine($"{originalNumber} is not an Armstrong number.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        /// <summary>
        /// Checks if a number is a palindrome
        /// </summary>
        static void PalindromeCheck()
        {
            Console.WriteLine("\n=== Palindrome Check ===");
            
            try
            {
                Console.Write("Enter an integer: ");
                int number = int.Parse(Console.ReadLine()!);
                int originalNumber = number;
                int reversedNumber = 0;

                while (number != 0)
                {
                    int digit = number % 10;
                    reversedNumber = reversedNumber * 10 + digit;
                    number /= 10;
                }
                
                Console.WriteLine($"Original Number: {originalNumber}");
                Console.WriteLine($"Reversed Number: {reversedNumber}");
                Console.WriteLine($"Is Palindrome: {originalNumber == reversedNumber}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }
}