# DAY 9: Extension Method - Practice Code Examples

## 📚 Table of Contents
1. [Project Overview](#project-overview)
2. [Complete Working Code](#complete-working-code)
3. [Code Explanation](#code-explanation)
4. [How to Run](#how-to-run)
5. [Expected Output](#expected-output)
6. [Code Improvements](#code-improvements)
7. [Additional Examples](#additional-examples)
8. [Practice Exercises](#practice-exercises)

---

## Project Overview

**Namespace:** `ExtentionMethodDemo`  
**Purpose:** Demonstrates practical implementation of Extension Methods in C#  
**Features:**
- String Palindrome Checker (Two-Pointer Algorithm)
- Word Count Extension Method
- Clean, well-documented code with XML comments

---

## Complete Working Code

### File 1: PalindromExtensions.cs

```csharp
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtentionMethodDemo
{
    /// <summary>
    /// Provides extension methods for determining whether a string is a palindrome.
    /// </summary>
    /// <remarks>
    /// A palindrome is a string that reads the same forwards and backwards. 
    /// This class contains static methods that can be used to check for palindromic strings.
    /// </remarks>
    public static class PalindromExtensions
    {
        /// <summary>
        /// Determines whether the specified string is a palindrome.
        /// </summary>
        /// <remarks>
        /// The comparison is case-sensitive and considers all characters, including whitespace
        /// and punctuation. An empty string is considered a palindrome.
        /// Algorithm: Uses two-pointer approach with O(n) time complexity and O(1) space complexity.
        /// </remarks>
        /// <param name="str">The string to evaluate for palindrome characteristics. Cannot be null.</param>
        /// <returns>A message indicating whether the string is a palindrome or not.</returns>
        public static string IsPalindrom(this string str)
        {
            // Null check for safety
            if (str == null)
                return "Invalid input: String is null";
            
            // Empty string is considered a palindrome
            if (str.Length == 0)
                return "Palindrom";
            
            // Checking Palindrom using two pointer approach:
            // - One pointer starts from beginning (i)
            // - Other pointer starts from end (j)
            // - Moving towards each other and comparing characters
            // - When characters are not equal, we can say it's not palindrome
            
            int i = 0;                    // Start pointer
            int j = str.Length - 1;       // End pointer
            
            while (i < j)
            {
                if (str[i] != str[j])
                {
                    return "Not Palindrom";
                }
                i++;   // Move start pointer forward
                j--;   // Move end pointer backward
            }
            
            return "Palindrom";
        }
        
        /// <summary>
        /// Determines whether the specified string is a palindrome (case-insensitive).
        /// </summary>
        /// <param name="str">The string to evaluate. Cannot be null.</param>
        /// <returns>True if the string is a palindrome; otherwise, false.</returns>
        public static bool IsPalindromIgnoreCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return true;
            
            string lower = str.ToLower();
            int i = 0;
            int j = lower.Length - 1;
            
            while (i < j)
            {
                if (lower[i] != lower[j])
                    return false;
                i++;
                j--;
            }
            
            return true;
        }
    }
}
```

### File 2: StringExtensions.cs

```csharp
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtentionMethodDemo
{
    /// <summary>
    /// Provides extension methods for string manipulation and analysis.
    /// </summary>
    /// <remarks>
    /// This static class contains extension methods that add additional functionality to the string
    /// type. All methods are static and can be called as if they were instance methods on string objects.
    /// </remarks>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns the number of words in the specified string.
        /// </summary>
        /// <param name="str">The string to count words in. Cannot be null.</param>
        /// <returns>The number of words found in the input string. Returns 0 if the string is empty or contains only whitespace.</returns>
        public static int WordCount(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return 0;
            
            // Split by space, tab, and newline characters
            string[] words = str.Split(new[] { ' ', '\t', '\n', '\r' }, 
                                      StringSplitOptions.RemoveEmptyEntries);
            
            return words.Length;
        }
        
        /// <summary>
        /// Returns the number of characters in the string (excluding whitespace).
        /// </summary>
        /// <param name="str">The string to count characters in.</param>
        /// <returns>The number of non-whitespace characters.</returns>
        public static int CharacterCount(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;
            
            int count = 0;
            foreach (char c in str)
            {
                if (!char.IsWhiteSpace(c))
                    count++;
            }
            
            return count;
        }
        
        /// <summary>
        /// Returns the total length of the string including all characters.
        /// </summary>
        /// <param name="str">The string to measure.</param>
        /// <returns>The total number of characters in the string.</returns>
        public static int TotalLength(this string str)
        {
            return str?.Length ?? 0;
        }
    }
}
```

### File 3: Program.cs

```csharp
using System;
using System.Text.RegularExpressions;

namespace ExtentionMethodDemo
{
    /// <summary>
    /// Provides the entry point for the application.
    /// </summary>
    /// <remarks>
    /// This class contains the Main method, which serves as the starting point when the application
    /// is executed. It demonstrates the use of string extension methods for counting words and 
    /// checking for palindromes.
    /// </remarks>
    public class Program
    {
        /// <summary>
        /// Serves as the entry point for the application.
        /// </summary>
        /// <remarks>
        /// This method demonstrates the use of extension methods for:
        /// - Counting words and characters in a string
        /// - Checking if a string is a palindrome
        /// </remarks>
        /// <param name="args">An array of command-line arguments supplied to the application.</param>
        static void Main(string[] args)
        {
            Console.WriteLine("====================================");
            Console.WriteLine("  Extension Methods Demo");
            Console.WriteLine("====================================\n");
            
            // Example 1: Word Count Extension Method
            Console.WriteLine("--- Example 1: Word Count ---");
            string name = "Hello World";
            int wordCount = name.WordCount();
            int charCount = name.CharacterCount();
            int totalLength = name.TotalLength();
            
            Console.WriteLine($"Text: \"{name}\"");
            Console.WriteLine($"Word Count: {wordCount}");
            Console.WriteLine($"Character Count (no spaces): {charCount}");
            Console.WriteLine($"Total Length: {totalLength}");
            Console.WriteLine();
            
            // Example 2: Palindrome Check Extension Method
            Console.WriteLine("--- Example 2: Palindrome Check ---");
            string palin = "madam";
            string isPalin = palin.IsPalindrom();
            Console.WriteLine($"Text: \"{palin}\"");
            Console.WriteLine($"Result: {isPalin}");
            Console.WriteLine();
            
            // Additional palindrome tests
            Console.WriteLine("--- Additional Palindrome Tests ---");
            string[] testStrings = { "racecar", "hello", "noon", "civic", "C#" };
            
            foreach (string test in testStrings)
            {
                string result = test.IsPalindrom();
                Console.WriteLine($"\"{test}\" -> {result}");
            }
            Console.WriteLine();
            
            // Case-insensitive palindrome check
            Console.WriteLine("--- Case-Insensitive Palindrome ---");
            string mixedCase = "Racecar";
            bool isCaseInsensitive = mixedCase.IsPalindromIgnoreCase();
            Console.WriteLine($"\"{mixedCase}\" (ignore case) -> {(isCaseInsensitive ? "Palindrom" : "Not Palindrom")}");
            
            Console.WriteLine("\n====================================");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
```

---

## Code Explanation

### 1. Extension Method Class Structure

```csharp
public static class ExtensionClassName  // ✅ Must be static
{
    public static ReturnType MethodName(this Type obj, params)  // ✅ Static method with 'this'
    {
        // Implementation
    }
}
```

### 2. Palindrome Algorithm Explanation

**Two-Pointer Approach:**
```
Input: "madam"
Indices: 0 1 2 3 4

Step 1: Compare positions 0 and 4 -> 'm' == 'm' ✅
Step 2: Compare positions 1 and 3 -> 'a' == 'a' ✅
Step 3: Compare positions 2 and 2 -> Middle reached ✅
Result: Palindrome!
```

**Time Complexity:** O(n/2) = O(n)  
**Space Complexity:** O(1)

### 3. Word Count Logic

**Original Issue:**
```csharp
// ❌ WRONG: Returns character count, not word count
public static int WordCount(this string str)
{
    return str.Length;
}
```

**Fixed Version:**
```csharp
// ✅ CORRECT: Returns actual word count
public static int WordCount(this string str)
{
    if (string.IsNullOrWhiteSpace(str))
        return 0;
    
    string[] words = str.Split(new[] { ' ', '\t', '\n', '\r' }, 
                              StringSplitOptions.RemoveEmptyEntries);
    return words.Length;
}
```

---

## How to Run

### Option 1: Using Visual Studio
1. Create a new **Console App (.NET Core)** project
2. Copy each class into separate files
3. Press **F5** or click **Run** button

### Option 2: Using Command Line
```bash
# Navigate to project directory
cd ExtentionMethodDemo

# Build the project
dotnet build

# Run the application
dotnet run
```

### Option 3: Using Visual Studio Code
1. Open the project folder
2. Press **Ctrl + F5** (Run without debugging)
3. Or use Terminal: `dotnet run`

---

## Expected Output

```
====================================
  Extension Methods Demo
====================================

--- Example 1: Word Count ---
Text: "Hello World"
Word Count: 2
Character Count (no spaces): 10
Total Length: 11

--- Example 2: Palindrome Check ---
Text: "madam"
Result: Palindrom

--- Additional Palindrome Tests ---
"racecar" -> Palindrom
"hello" -> Not Palindrom
"noon" -> Palindrom
"civic" -> Palindrom
"C#" -> Not Palindrom

--- Case-Insensitive Palindrome ---
"Racecar" (ignore case) -> Palindrom

====================================
Press any key to exit...
```

---

## Code Improvements

### Issues Fixed:

| Original Issue | Fixed Solution |
|---------------|----------------|
| ❌ `WordCount()` returns `str.Length` (character count) | ✅ Returns actual word count using `Split()` |
| ❌ No null/empty string validation | ✅ Added null and empty string checks |
| ❌ Class name typo: "Palindrom" | ✅ Renamed to "PalindromExtensions" |
| ❌ Limited functionality | ✅ Added case-insensitive palindrome check |

### Best Practices Applied:

✅ **Null Validation:** All methods check for null/empty inputs  
✅ **Proper Naming:** Descriptive class and method names  
✅ **XML Documentation:** Comprehensive comments for IntelliSense  
✅ **Error Handling:** Graceful handling of edge cases  
✅ **Single Responsibility:** Each method does one thing well  

---

## Additional Examples

### Example 1: Chain Multiple Extension Methods

```csharp
string sentence = "A man a plan a canal Panama";
int words = sentence.WordCount();
bool isPalin = sentence.Replace(" ", "").IsPalindromIgnoreCase();

Console.WriteLine($"Words: {words}");
Console.WriteLine($"Palindrome (no spaces): {isPalin}");

// Output:
// Words: 6
// Palindrome (no spaces): True
```

### Example 2: Create More String Extensions

```csharp
public static class MoreStringExtensions
{
    // Reverse a string
    public static string Reverse(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;
        
        char[] charArray = str.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
    
    // Count vowels
    public static int VowelCount(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return 0;
        
        int count = 0;
        string vowels = "aeiouAEIOU";
        
        foreach (char c in str)
        {
            if (vowels.Contains(c))
                count++;
        }
        
        return count;
    }
    
    // Capitalize first letter of each word
    public static string ToTitleCase(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return str;
        
        string[] words = str.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length > 0)
            {
                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
            }
        }
        
        return string.Join(" ", words);
    }
}

// Usage:
string text = "hello world";
Console.WriteLine(text.Reverse());        // "dlrow olleh"
Console.WriteLine(text.VowelCount());     // 3
Console.WriteLine(text.ToTitleCase());    // "Hello World"
```

---

## Practice Exercises

### Exercise 1: String Contains Extension
Create an extension method that checks if a string contains only digits.

```csharp
public static bool IsNumeric(this string str)
{
    // Your code here
}

// Test:
"12345".IsNumeric();  // Should return true
"123a5".IsNumeric();  // Should return false
```

### Exercise 2: String Truncate Extension
Create an extension method that truncates a string to a specified length and adds "..." if truncated.

```csharp
public static string Truncate(this string str, int maxLength)
{
    // Your code here
}

// Test:
"Hello World".Truncate(5);  // Should return "Hello..."
```

### Exercise 3: Integer Extension
Create extension methods for integers:
- `IsEven()` - Check if number is even
- `IsPrime()` - Check if number is prime
- `ToWords()` - Convert number to words (e.g., 5 -> "five")

```csharp
// Test:
10.IsEven();     // true
7.IsPrime();     // true
5.ToWords();     // "five"
```

---

## Solutions

### Solution 1: IsNumeric
```csharp
public static bool IsNumeric(this string str)
{
    if (string.IsNullOrEmpty(str))
        return false;
    
    foreach (char c in str)
    {
        if (!char.IsDigit(c))
            return false;
    }
    
    return true;
}
```

### Solution 2: Truncate
```csharp
public static string Truncate(this string str, int maxLength)
{
    if (string.IsNullOrEmpty(str))
        return str;
    
    if (str.Length <= maxLength)
        return str;
    
    return str.Substring(0, maxLength) + "...";
}
```

### Solution 3: Integer Extensions
```csharp
public static class IntExtensions
{
    public static bool IsEven(this int number)
    {
        return number % 2 == 0;
    }
    
    public static bool IsPrime(this int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;
        
        for (int i = 3; i <= Math.Sqrt(number); i += 2)
        {
            if (number % i == 0)
                return false;
        }
        
        return true;
    }
    
    public static string ToWords(this int number)
    {
        string[] words = { "zero", "one", "two", "three", "four", 
                          "five", "six", "seven", "eight", "nine" };
        
        if (number >= 0 && number <= 9)
            return words[number];
        
        return number.ToString();
    }
}
```

---

## Key Takeaways

🎯 **Extension methods** must be in a **static class**  
🎯 The method itself must be **static**  
🎯 First parameter uses **`this`** keyword  
🎯 Can be called like **instance methods**  
🎯 Only access **public members** of the extended type  
🎯 Always validate **input parameters** (null checks)  
🎯 Use **meaningful names** for clarity  

---

**Practice File Created by:** Senior Architecture Team  
**Last Updated:** December 30, 2025  
**Related Notes:** [DAY9_Note.md](DAY9_Note.md)
