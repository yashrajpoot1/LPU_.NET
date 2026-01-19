using System;

namespace DAY01_Fundamentals
{
    /// <summary>
    /// Day 1 - C# Fundamentals and .NET Framework
    /// Contains examples of basic C# programming concepts
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== DAY 1: C# Fundamentals ===\n");
            
            // Example 1: Prime Number Check
            Console.WriteLine("1. Prime Number Check:");
            PrimeNumberCheck();
            
            Console.WriteLine("\n" + new string('-', 40) + "\n");
            
            // Example 2: Age Validation
            Console.WriteLine("2. Age Validation:");
            AgeValidation();
            
            Console.WriteLine("\n" + new string('-', 40) + "\n");
            
            // Example 3: Feet to Centimeter Conversion
            Console.WriteLine("3. Feet to Centimeter Conversion:");
            FeetToCentimeter();
        }

        /// <summary>
        /// Checks if a given number is prime
        /// </summary>
        static void PrimeNumberCheck()
        {
            Console.Write("Enter your number: ");
            string? input = Console.ReadLine();
            
            if (!int.TryParse(input, out int n))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }

            if (n <= 1)
            {
                Console.WriteLine("Not a Prime");
                return;
            }

            if (n == 2 || n == 3)
            {
                Console.WriteLine("It's a Prime");
                return;
            }

            if (n % 2 == 0)
            {
                Console.WriteLine("Not a Prime");
                return;
            }

            for (int i = 3; i * i <= n; i += 2)
            {
                if (n % i == 0)
                {
                    Console.WriteLine("Not a Prime");
                    return;
                }
            }

            Console.WriteLine("It's a Prime");
        }

        /// <summary>
        /// Validates if a person is an adult based on age
        /// </summary>
        static void AgeValidation()
        {
            Console.Write("Enter Your Age: ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int age))
            {
                bool isAdult = age >= 18;
                Console.WriteLine("Adult? " + isAdult);
            }
            else
            {
                Console.WriteLine("Invalid Age");
            }
        }

        /// <summary>
        /// Converts feet to centimeters
        /// </summary>
        static void FeetToCentimeter()
        {
            const double conversionFactor = 30.48;
            Console.Write("Enter feet value: ");
            string? feet = Console.ReadLine();

            if (!double.TryParse(feet, out double f))
            {
                Console.WriteLine("Invalid number");
                return;
            }

            if (f < 0)
            {
                Console.WriteLine("Input is negative");
                return;
            }

            double cm = f * conversionFactor;
            Console.WriteLine($"{f} Feet in Cm is {cm}");
        }
    }
}