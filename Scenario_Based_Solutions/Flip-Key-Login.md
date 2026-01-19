# Flip-Key Login - String Transformation Algorithm

## Overview

This solution implements a complex string transformation algorithm called "CleanseAndInvert". It's useful for scenarios like password encoding, key generation, or data obfuscation where you need to transform user input through multiple processing steps.

## Problem Statement

Create a method that:
1. Accepts a string input from the user
2. Validates the input according to specific rules
3. Transforms the input through multiple processing steps
4. Returns a generated key or an error message

## Solution Approach

The solution uses a multi-step algorithm:
1. **Input Validation** - Ensure the input meets minimum requirements
2. **Character Filtering** - Keep only characters with odd ASCII values
3. **String Reversal** - Reverse the filtered string
4. **Case Transformation** - Apply uppercase to characters at even positions

---

## Complete Implementation

```csharp
using System;
using System.Text;

namespace DAY7_Learning
{
    /// <summary>
    /// The main program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Serves as the entry point for the application.
        /// </summary>
        /// <param name="args">An array of command-line arguments supplied to the application.</param>
        public static void Main(string[] args)
        {
            // Prompt the user to enter a password
            Console.WriteLine("Enter your Input: ");
            // Read the user input
            string word = Console.ReadLine();

            // Create an instance of Program to call the instance method
            Program program = new Program();
            // Call the CleanseAndInvert method and store the result
            string result = program.CleanseAndInvert(word);

            // Display the result or an error message based on the output
            if (result == "")
            {
                Console.WriteLine("Invalid input");
            }
            else
            {
                Console.WriteLine("The generated Key is: " + result);
            }
        }

        /// <summary>
        /// Cleanses and inverts the input string according to specific rules.
        /// </summary>
        /// <remarks>
        /// Processing rules:
        /// 1. Validates that input is not null and has at least 6 characters
        /// 2. Validates that input contains only letters (no spaces, digits, or special characters)
        /// 3. Converts input to lowercase
        /// 4. Removes all characters with even ASCII values
        /// 5. Reverses the remaining characters
        /// 6. Converts characters at even positions (0-based index) to uppercase
        /// </remarks>
        /// <param name="word">The input string to process.</param>
        /// <returns>The processed string, or an empty string if validation fails.</returns>
        public string CleanseAndInvert(string word)
        {
            // Input Validation for null or length less than 6
            if (string.IsNullOrEmpty(word) || word.Length < 6)
            {
                return "";
            }

            // Input Validation for digits, whitespace, or non-letter characters
            foreach (char c in word)
            {
                if (char.IsDigit(c) || char.IsWhiteSpace(c) || !char.IsLetter(c))
                {
                    return "";
                }
            }

            // Convert to lowercase
            word = word.ToLower();

            // Remove characters with even ASCII values
            StringBuilder sb = new StringBuilder();
            foreach (char c in word)
            {
                if ((int)c % 2 != 0)
                {
                    sb.Append(c);
                }
            }
            
            // Reverse the string and convert characters at even positions to uppercase
            char[] charArray = sb.ToString().ToCharArray();
            Array.Reverse(charArray);
            for (int i = 0; i < charArray.Length; i++)
            {
                if (i % 2 == 0)
                {
                    charArray[i] = char.ToUpper(charArray[i]);
                }
            }

            // Return the final processed string
            return new string(charArray);
        }
    }
}
```

---

## Algorithm Explanation

### Step 1: Input Validation

The method performs two levels of validation:

**Length Validation:**
```csharp
if (string.IsNullOrEmpty(word) || word.Length < 6)
{
    return "";
}
```
- Ensures the input is not null or empty
- Requires at least 6 characters

**Character Validation:**
```csharp
foreach (char c in word)
{
    if (char.IsDigit(c) || char.IsWhiteSpace(c) || !char.IsLetter(c))
    {
        return "";
    }
}
```
- Rejects inputs containing digits (0-9)
- Rejects inputs containing whitespace
- Rejects inputs containing special characters
- Only accepts letters (A-Z, a-z)

### Step 2: Normalize to Lowercase

```csharp
word = word.ToLower();
```
- Converts all characters to lowercase for consistent processing
- Example: "HELLO" → "hello"

### Step 3: Filter Characters with Odd ASCII Values

```csharp
StringBuilder sb = new StringBuilder();
foreach (char c in word)
{
    if ((int)c % 2 != 0)
    {
        sb.Append(c);
    }
}
```

**ASCII Values Reference:**

| Character | ASCII Value | Keep? |
|-----------|-------------|-------|
| a | 97 | ✓ (odd) |
| b | 98 | ✗ (even) |
| c | 99 | ✓ (odd) |
| d | 100 | ✗ (even) |
| e | 101 | ✓ (odd) |

Only characters with odd ASCII values are retained.

### Step 4: Reverse the String

```csharp
char[] charArray = sb.ToString().ToCharArray();
Array.Reverse(charArray);
```
- Converts the filtered string to a character array
- Reverses the order of characters
- Example: "ace" → "eca"

### Step 5: Uppercase at Even Positions

```csharp
for (int i = 0; i < charArray.Length; i++)
{
    if (i % 2 == 0)
    {
        charArray[i] = char.ToUpper(charArray[i]);
    }
}
```
- Converts characters at even indices (0, 2, 4, ...) to uppercase
- Example: "eca" → "EcA"

---

## Example Walkthrough

### Example 1: Valid Input

**Input:** `"Hello"`

1. **Validation:** Passes (6 characters, all letters)
2. **Lowercase:** "hello"
3. **Filter odd ASCII:**
   - h (104 - even) ✗
   - e (101 - odd) ✓
   - l (108 - even) ✗
   - l (108 - even) ✗
   - o (111 - odd) ✓
   - Result: "eo"
4. **Reverse:** "oe"
5. **Uppercase even positions:** "Oe" (index 0 becomes uppercase)

**Output:** `"Oe"`

### Example 2: Valid Input with More Characters

**Input:** `"Programming"`

1. **Validation:** Passes (11 characters, all letters)
2. **Lowercase:** "programming"
3. **Filter odd ASCII:**
   - p(112-✗), r(114-✗), o(111-✓), g(103-✓), r(114-✗), a(97-✓), m(109-✓), m(109-✓), i(105-✓), n(110-✗), g(103-✓)
   - Result: "ogammi g"... wait, only letters: "ogammig"
4. **Reverse:** "gimmago"
5. **Uppercase even positions:** "GiMmAgO"

**Output:** `"GiMmAgO"`

### Example 3: Invalid Input (Too Short)

**Input:** `"Hi"`

**Output:** `"Invalid input"` (Less than 6 characters)

### Example 4: Invalid Input (Contains Digits)

**Input:** `"Hello123"`

**Output:** `"Invalid input"` (Contains digits)

### Example 5: Invalid Input (Contains Spaces)

**Input:** `"Hello World"`

**Output:** `"Invalid input"` (Contains whitespace)

---

## Key Concepts Used

### 1. StringBuilder for String Manipulation

`StringBuilder` is more efficient than string concatenation for building strings in loops:

```csharp
StringBuilder sb = new StringBuilder();
foreach (char c in word)
{
    if ((int)c % 2 != 0)
    {
        sb.Append(c);  // Efficient appending
    }
}
```

**Why StringBuilder?**
- Strings in C# are immutable
- Each concatenation creates a new string object
- StringBuilder uses a mutable buffer, reducing memory allocations

### 2. ASCII Value Manipulation

```csharp
int asciiValue = (int)c;
if (asciiValue % 2 != 0)
{
    // Character has odd ASCII value
}
```

**Common ASCII Values:**
- 'A' = 65 (odd), 'B' = 66 (even), 'C' = 67 (odd)
- 'a' = 97 (odd), 'b' = 98 (even), 'c' = 99 (odd)

### 3. Character Array Operations

```csharp
char[] charArray = str.ToCharArray();  // Convert to array
Array.Reverse(charArray);              // Reverse in place
string result = new string(charArray); // Convert back to string
```

### 4. Input Validation Best Practices

```csharp
// Check for null or empty
if (string.IsNullOrEmpty(word))

// Validate character types
char.IsDigit(c)      // Is it a digit?
char.IsWhiteSpace(c) // Is it whitespace?
char.IsLetter(c)     // Is it a letter?
```

---

## Use Cases

1. **Password Encoding** - Transform passwords before storage
2. **Key Generation** - Create unique keys from user input
3. **Data Obfuscation** - Hide sensitive information
4. **Challenge-Response Systems** - Verify user identity
5. **API Token Generation** - Create tokens from seed values

---

## Potential Improvements

### 1. Return Error Codes Instead of Empty String

```csharp
public enum ValidationError
{
    None,
    TooShort,
    InvalidCharacters
}

public (string result, ValidationError error) CleanseAndInvert(string word)
{
    if (string.IsNullOrEmpty(word) || word.Length < 6)
        return ("", ValidationError.TooShort);
    
    // ... rest of the logic
}
```

### 2. Make Processing Rules Configurable

```csharp
public class TransformOptions
{
    public int MinLength { get; set; } = 6;
    public bool FilterByAscii { get; set; } = true;
    public bool ReverseString { get; set; } = true;
    public bool UppercaseEvenPositions { get; set; } = true;
}
```

### 3. Add Unit Tests

```csharp
[TestMethod]
public void CleanseAndInvert_ValidInput_ReturnsTransformedString()
{
    var program = new Program();
    string result = program.CleanseAndInvert("Hello");
    Assert.AreEqual("Oe", result);
}
```

---

## Performance Considerations

1. **StringBuilder Usage** - O(n) time complexity for character filtering
2. **Array Reversal** - O(n) time complexity
3. **Overall Complexity** - O(n) where n is the input length
4. **Space Complexity** - O(n) for the StringBuilder and character array

---

## Summary

This solution demonstrates:
- **Multi-step string transformation** algorithms
- **Input validation** best practices
- **StringBuilder** for efficient string building
- **ASCII value manipulation** for character filtering
- **Array operations** for string reversal
- **Character case transformation** based on position

The algorithm is particularly useful in scenarios requiring deterministic string transformation with validation, such as password encoding or key generation systems.
