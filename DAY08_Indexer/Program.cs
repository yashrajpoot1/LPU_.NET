using System;
using System.Collections.Generic;

namespace DAY08_Indexer
{
    /// <summary>
    /// Day 8 - Indexers in C#
    /// Contains examples of indexers, partial classes, and static classes
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== DAY 8: Indexers, Partial Classes, and Static Classes ===\n");
            
            bool continueProgram = true;
            
            while (continueProgram)
            {
                DisplayMenu();
                string? choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        BasicIndexerExample();
                        break;
                    case "2":
                        StringIndexerExample();
                        break;
                    case "3":
                        ValidationIndexerExample();
                        break;
                    case "4":
                        PartialClassExample();
                        break;
                    case "5":
                        StaticClassExample();
                        break;
                    case "6":
                        ExtensionMethodExample();
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
            Console.WriteLine("1. Basic Indexer Example (Array-based)");
            Console.WriteLine("2. String Indexer Example (Dictionary-style)");
            Console.WriteLine("3. Validation Indexer Example");
            Console.WriteLine("4. Partial Class Example");
            Console.WriteLine("5. Static Class Example");
            Console.WriteLine("6. Extension Method Example");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");
        }

        /// <summary>
        /// Demonstrates basic array-based indexer
        /// </summary>
        static void BasicIndexerExample()
        {
            Console.WriteLine("\n=== Basic Indexer Example ===");
            
            Marks marks = new Marks();
            
            // Setting values using indexer
            marks[0] = 85;
            marks[1] = 92;
            marks[2] = 78;
            marks[3] = 88;
            marks[4] = 95;
            
            Console.WriteLine("Student Marks:");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Subject {i + 1}: {marks[i]}");
            }
            
            Console.WriteLine($"Total Marks: {marks.GetTotal()}");
            Console.WriteLine($"Average: {marks.GetAverage():F2}");
        }

        /// <summary>
        /// Demonstrates string-based indexer (dictionary style)
        /// </summary>
        static void StringIndexerExample()
        {
            Console.WriteLine("\n=== String Indexer Example ===");
            
            Student student = new Student();
            
            // Setting values using string indexer
            student["Math"] = 95;
            student["Physics"] = 88;
            student["Chemistry"] = 92;
            student["English"] = 85;
            
            Console.WriteLine("Student Grades:");
            Console.WriteLine($"Math: {student["Math"]}");
            Console.WriteLine($"Physics: {student["Physics"]}");
            Console.WriteLine($"Chemistry: {student["Chemistry"]}");
            Console.WriteLine($"English: {student["English"]}");
            Console.WriteLine($"Biology (not set): {student["Biology"]}");
            
            student.DisplayAllSubjects();
        }

        /// <summary>
        /// Demonstrates indexer with validation
        /// </summary>
        static void ValidationIndexerExample()
        {
            Console.WriteLine("\n=== Validation Indexer Example ===");
            
            ValidatedMarks validatedMarks = new ValidatedMarks();
            
            try
            {
                validatedMarks[0] = 85;
                validatedMarks[1] = 92;
                Console.WriteLine($"Mark 1: {validatedMarks[0]}");
                Console.WriteLine($"Mark 2: {validatedMarks[1]}");
                
                // This will throw an exception
                Console.WriteLine("Trying to set invalid mark (150)...");
                validatedMarks[2] = 150;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
            }
            
            try
            {
                // This will throw an exception
                Console.WriteLine("Trying to access invalid index (10)...");
                int mark = validatedMarks[10];
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Index Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Demonstrates partial class usage
        /// </summary>
        static void PartialClassExample()
        {
            Console.WriteLine("\n=== Partial Class Example ===");
            
            Customer customer = new Customer();
            customer.Id = 1001;
            customer.Name = "John Doe";
            customer.Email = "john.doe@example.com";
            
            Console.WriteLine($"Display Name: {customer.GetDisplayName()}");
            Console.WriteLine($"Full Info: {customer.GetFullInfo()}");
            Console.WriteLine($"Is Valid: {customer.IsValid()}");
        }

        /// <summary>
        /// Demonstrates static class usage
        /// </summary>
        static void StaticClassExample()
        {
            Console.WriteLine("\n=== Static Class Example ===");
            
            Console.WriteLine($"Pi constant: {MathUtils.Pi}");
            Console.WriteLine($"Add(5, 3): {MathUtils.Add(5, 3)}");
            Console.WriteLine($"Multiply(4, 7): {MathUtils.Multiply(4, 7)}");
            Console.WriteLine($"Power(2, 8): {MathUtils.Power(2, 8)}");
            Console.WriteLine($"Is Even(10): {MathUtils.IsEven(10)}");
            Console.WriteLine($"Is Even(7): {MathUtils.IsEven(7)}");
        }

        /// <summary>
        /// Demonstrates extension methods
        /// </summary>
        static void ExtensionMethodExample()
        {
            Console.WriteLine("\n=== Extension Method Example ===");
            
            string text = "hello world";
            Console.WriteLine($"Original: {text}");
            Console.WriteLine($"To Title Case: {text.ToTitleCase()}");
            Console.WriteLine($"Is Null or Empty: {text.IsNullOrEmpty()}");
            Console.WriteLine($"Word Count: {text.WordCount()}");
            
            string? nullText = null;
            Console.WriteLine($"Null text Is Null or Empty: {nullText.IsNullOrEmpty()}");
            
            int number = 12345;
            Console.WriteLine($"Number: {number}");
            Console.WriteLine($"Is Even: {number.IsEven()}");
            Console.WriteLine($"Square: {number.Square()}");
        }
    }

    #region Basic Indexer Classes

    /// <summary>
    /// Basic marks class with array-based indexer
    /// </summary>
    public class Marks
    {
        private int[] _marks = new int[5];

        /// <summary>
        /// Indexer for accessing marks by index
        /// </summary>
        /// <param name="index">Index of the mark</param>
        /// <returns>Mark value</returns>
        public int this[int index]
        {
            get { return _marks[index]; }
            set { _marks[index] = value; }
        }

        /// <summary>
        /// Gets total of all marks
        /// </summary>
        /// <returns>Sum of all marks</returns>
        public int GetTotal()
        {
            int total = 0;
            for (int i = 0; i < _marks.Length; i++)
            {
                total += _marks[i];
            }
            return total;
        }

        /// <summary>
        /// Gets average of all marks
        /// </summary>
        /// <returns>Average mark</returns>
        public double GetAverage()
        {
            return (double)GetTotal() / _marks.Length;
        }
    }

    /// <summary>
    /// Student class with string-based indexer (dictionary style)
    /// </summary>
    public class Student
    {
        private Dictionary<string, int> _subjects = new Dictionary<string, int>();

        /// <summary>
        /// Indexer for accessing grades by subject name
        /// </summary>
        /// <param name="subject">Subject name</param>
        /// <returns>Grade for the subject</returns>
        public int this[string subject]
        {
            get
            {
                return _subjects.ContainsKey(subject) ? _subjects[subject] : 0;
            }
            set
            {
                _subjects[subject] = value;
            }
        }

        /// <summary>
        /// Displays all subjects and their grades
        /// </summary>
        public void DisplayAllSubjects()
        {
            Console.WriteLine("\nAll Subjects:");
            foreach (var kvp in _subjects)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
    }

    /// <summary>
    /// Marks class with validation in indexer
    /// </summary>
    public class ValidatedMarks
    {
        private int[] _marks = new int[5];

        /// <summary>
        /// Validated indexer for accessing marks
        /// </summary>
        /// <param name="index">Index of the mark</param>
        /// <returns>Mark value</returns>
        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= _marks.Length)
                    throw new IndexOutOfRangeException($"Index {index} is out of range. Valid range is 0-{_marks.Length - 1}");

                return _marks[index];
            }
            set
            {
                if (index < 0 || index >= _marks.Length)
                    throw new IndexOutOfRangeException($"Index {index} is out of range. Valid range is 0-{_marks.Length - 1}");

                if (value < 0 || value > 100)
                    throw new ArgumentException("Marks must be between 0 and 100");

                _marks[index] = value;
            }
        }
    }

    #endregion

    #region Partial Class Example

    /// <summary>
    /// Partial class - Core properties and constructor
    /// </summary>
    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        partial void OnCreated();

        public Customer()
        {
            OnCreated();
        }
    }

    /// <summary>
    /// Partial class - Business logic methods
    /// </summary>
    public partial class Customer
    {
        /// <summary>
        /// Gets display name for the customer
        /// </summary>
        /// <returns>Formatted display name</returns>
        public string GetDisplayName() => $"#{Id} — {Name}";

        /// <summary>
        /// Gets full customer information
        /// </summary>
        /// <returns>Full customer details</returns>
        public string GetFullInfo() => $"ID: {Id}, Name: {Name}, Email: {Email}";

        /// <summary>
        /// Validates customer data
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid() => Id > 0 && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email);

        /// <summary>
        /// Partial method implementation
        /// </summary>
        partial void OnCreated()
        {
            Console.WriteLine("Customer object created with partial method hook");
        }
    }

    #endregion

    #region Static Class Example

    /// <summary>
    /// Static utility class for mathematical operations
    /// </summary>
    public static class MathUtils
    {
        public const double Pi = 3.141592653589793;

        static MathUtils()
        {
            Console.WriteLine("MathUtils static constructor called");
        }

        /// <summary>
        /// Adds two integers
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Sum of a and b</returns>
        public static int Add(int a, int b) => a + b;

        /// <summary>
        /// Multiplies two integers
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Product of a and b</returns>
        public static int Multiply(int a, int b) => a * b;

        /// <summary>
        /// Calculates power of a number
        /// </summary>
        /// <param name="baseNum">Base number</param>
        /// <param name="exponent">Exponent</param>
        /// <returns>Base raised to the power of exponent</returns>
        public static double Power(double baseNum, double exponent) => Math.Pow(baseNum, exponent);

        /// <summary>
        /// Checks if a number is even
        /// </summary>
        /// <param name="number">Number to check</param>
        /// <returns>True if even, false if odd</returns>
        public static bool IsEven(int number) => number % 2 == 0;
    }

    #endregion

    #region Extension Methods

    /// <summary>
    /// Extension methods for string operations
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if string is null or empty
        /// </summary>
        /// <param name="value">String to check</param>
        /// <returns>True if null or empty</returns>
        public static bool IsNullOrEmpty(this string? value)
            => string.IsNullOrEmpty(value);

        /// <summary>
        /// Converts string to title case
        /// </summary>
        /// <param name="value">String to convert</param>
        /// <returns>Title case string</returns>
        public static string ToTitleCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }

        /// <summary>
        /// Counts words in a string
        /// </summary>
        /// <param name="value">String to count words in</param>
        /// <returns>Number of words</returns>
        public static int WordCount(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;

            return value.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }

    /// <summary>
    /// Extension methods for integer operations
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// Checks if integer is even
        /// </summary>
        /// <param name="number">Number to check</param>
        /// <returns>True if even</returns>
        public static bool IsEven(this int number) => number % 2 == 0;

        /// <summary>
        /// Calculates square of a number
        /// </summary>
        /// <param name="number">Number to square</param>
        /// <returns>Square of the number</returns>
        public static int Square(this int number) => number * number;
    }

    #endregion
}