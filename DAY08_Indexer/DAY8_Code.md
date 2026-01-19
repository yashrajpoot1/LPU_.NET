# Indexer in C# - Code Example

## Overview
This example demonstrates how to implement and use **indexers** in C#. Indexers allow objects to be indexed like arrays, providing a natural syntax for accessing elements within a class using the bracket notation `[]`.

## Key Concepts
- **Indexer**: A special property that enables array-like access to class members
- **Syntax**: Uses the `this` keyword with square brackets `this[int index]`
- **Benefits**: Provides intuitive array-like access to internal collections without exposing the underlying data structure

## Learning Objectives
1. Understand how to declare an indexer using the `this` keyword
2. Implement get and set accessors for the indexer
3. Use indexers to access and modify internal data in a class

---

## Code Implementation

```csharp
namespace Learning_Indexer
{
    /// <summary>
    /// Provides the entry point for the application.
    /// Purpose: To demonstrate the use of indexers in C#.
    /// Learning Outcome: Understand how to implement and use indexers in a class.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            Mydata mydata = new Mydata(); // Creating an instance of Mydata class
            mydata[0] = "C";  // Using indexer to set values
            mydata[1] = "C++";
            mydata[2] = "C#";

            Console.WriteLine("First Value is: " + mydata[0]); // Using indexer to get values
            Console.WriteLine("Second Value is: " + mydata[1]);
            Console.WriteLine("Third Value is: " + mydata[2]);
        }
    }


    /// <summary>
    /// Represents a collection of three string values accessible by index.
    /// </summary>
    /// <remarks>Use the indexer to get or set individual string values by their zero-based index. The valid
    /// index range is 0 to 2, inclusive.</remarks>
    class Mydata
    {
        private string[] values = new string[3]; // Internal array to hold string values

        public string this[int index] // Indexer to get/set values by index
        {
            get
            {
                return values[index];
            }
            set
            {
                values[index] = value;
            }
        }

    }
}
```

## Expected Output
```
First Value is: C
Second Value is: C++
Third Value is: C#
```

## How It Works
1. The `Mydata` class contains a private array `values` with 3 elements
2. The indexer `public string this[int index]` provides controlled access to this array
3. When you write `mydata[0] = "C"`, the set accessor is called
4. When you write `mydata[0]`, the get accessor returns the value at that index
5. This provides a clean, array-like interface without exposing the internal array directly






---

## Example 2: Student Books Management with Indexer

### Overview
This example demonstrates a more advanced use of indexers with a `Student` class. The indexer manages a private collection of books, providing controlled access through validation and error handling.

### Key Learning Points
- Using indexers with `List<T>` collections
- Implementing validation in indexer accessors
- Combining properties and indexers in a class
- Dynamic collection management through indexers

---

### Code Implementation

#### Student.cs
```csharp
using System;
using System.Collections.Generic;

namespace Learning_Indexer
{
    /// <summary>
    /// Represents a student with a roll number, name, address, and associated books.
    /// Demonstrates the use of indexers to access a private List collection.
    /// </summary>
    /// <remarks>
    /// Address and Books are privately held data. Properties and indexers provide
    /// controlled access to these fields from outside the class.
    /// </remarks>
    public class Student
    {
        public int RollNo { get; set; } // Auto-implemented property for Roll Number
        public string Name { get; set; } // Auto-implemented property for Name

        private string address; // Backing field for Address property
        public string Address // Property for Address with explicit get/set
        {
            get { return address; }
            set { address = value; }
        }

        private List<string> Books = new List<string>(); // List to hold book names

        /// <summary>
        /// Indexer to access books by index with validation.
        /// </summary>
        /// <param name="index">Zero-based index of the book</param>
        /// <returns>The book name at the specified index</returns>
        public string this[int index]
        {
            get
            {
                if (index >= 0 && index < Books.Count)
                {
                    return Books[index];
                }
                else
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
            }
            set
            {
                if (index >= 0 && index < Books.Count)
                {
                    Books[index] = value; // Update existing book
                }
                else if (index == Books.Count) // Allow adding a new book at the end
                {
                    Books.Add(value);
                }
                else
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
            }
        }
    }
}
```

#### Program.cs
```csharp
using System;

namespace Learning_Indexer
{
    /// <summary>
    /// Provides the entry point for the application.
    /// Purpose: To demonstrate the use of indexers with a Student class managing books.
    /// Learning Outcome: Understand how indexers work with List collections and validation.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // Create a new Student instance
            Student student1 = new Student();
            student1.RollNo = 101;
            student1.Name = "Asad Ali";
            student1.Address = "Jalandhar Punjab";

            // Add books using indexer
            student1[0] = "Mathematics";
            student1[1] = "Physics";
            student1[2] = "Chemistry";

            // Display student information
            Console.WriteLine("Student's Roll No: " + student1.RollNo);
            Console.WriteLine("Student Name: " + student1.Name);
            Console.WriteLine("Student Address: " + student1.Address);
            Console.WriteLine("Book 1: " + student1[0]);
            Console.WriteLine("Book 2: " + student1[1]);
            Console.WriteLine("Book 3: " + student1[2]);
        }
    }
}
```

### Expected Output
```
Student's Roll No: 101
Student Name: Asad Ali
Student Address: Jalandhar Punjab
Book 1: Mathematics
Book 2: Physics
Book 3: Chemistry
```

### How It Works
1. **Private Collection**: The `Books` list is private, preventing direct external access
2. **Controlled Access**: The indexer provides a controlled way to add and retrieve books
3. **Validation**: 
   - The `get` accessor ensures the index is within valid range
   - The `set` accessor allows updating existing books or adding new ones at the end
4. **Error Handling**: Throws `IndexOutOfRangeException` for invalid indices
5. **Dynamic Growth**: When setting a value at `Books.Count` index, it adds a new book (allows growth)

### Key Differences from Example 1
- Uses `List<string>` instead of fixed-size array
- Implements validation and error handling
- Supports dynamic addition of elements
- Combines properties and indexers in a practical scenario








---

## Example 3: Partial Classes with Indexer

### Overview
This example demonstrates how indexers work with **partial classes** in C#. The `Student` class is split across multiple files using the `partial` keyword, while maintaining the indexer functionality.

### Key Learning Points
- Using indexers in partial classes
- Splitting class implementation across multiple files
- Organizing code with partial class methods
- Maintaining encapsulation across partial definitions

---

### Code Implementation

#### Student.cs (Main Class)
```csharp
using System;
using System.Collections.Generic;

namespace Learning_Indexer
{
    /// <summary>
    /// Represents a student with a roll number, name, address, and associated books.
    /// This is the main partial class containing core properties and the indexer.
    /// </summary>
    public partial class Student
    {
        public int RollNo { get; set; } // Auto-implemented property for Roll Number
        public string Name { get; set; } // Auto-implemented property for Name

        private string address; // Backing field for Address property
        public string Address // Property for Address with explicit get/set
        {
            get { return address; }
            set { address = value; }
        }

        private List<string> Books = new List<string>(); // List to hold book names

        /// <summary>
        /// Indexer to access books by index with validation.
        /// </summary>
        public string this[int index]
        {
            get
            {
                if (index >= 0 && index < Books.Count)
                {
                    return Books[index];
                }
                else
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
            }
            set
            {
                if (index >= 0 && index < Books.Count)
                {
                    Books[index] = value;
                }
                else if (index == Books.Count) // Allow adding a new book at the end
                {
                    Books.Add(value);
                }
                else
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
            }
        }

        public void PrintMainClass()
        {
            Console.WriteLine("This is Main Class method");
        }
    }
}
```

#### Student.Partial.cs (Partial Class Extension)
```csharp
using System;

namespace Learning_Indexer
{
    /// <summary>
    /// Partial class extension for Student.
    /// Demonstrates how to extend functionality across multiple files.
    /// </summary>
    public partial class Student
    {
        public void PrintPartial()
        {
            Console.WriteLine("This is Partial Class method");
        }
    }
}
```

#### Program.cs
```csharp
using System;

namespace Learning_Indexer
{
    /// <summary>
    /// Provides the entry point for the application.
    /// Purpose: To demonstrate indexers with partial classes.
    /// Learning Outcome: Understand how indexers work with partial class definitions.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // Create a new Student instance
            Student student = new Student();
            student.RollNo = 102;
            student.Name = "Sara Ahmed";
            student.Address = "Lahore, Pakistan";

            // Add books using indexer
            student[0] = "Data Structures";
            student[1] = "Algorithms";
            student[2] = "Operating Systems";

            // Display student information
            Console.WriteLine("Student's Roll No: " + student.RollNo);
            Console.WriteLine("Student Name: " + student.Name);
            Console.WriteLine("Student Address: " + student.Address);
            Console.WriteLine("Book 1: " + student[0]);
            Console.WriteLine("Book 2: " + student[1]);
            Console.WriteLine("Book 3: " + student[2]);

            Console.WriteLine(); // Empty line

            // Call methods from both partial class files
            student.PrintMainClass();
            student.PrintPartial();
        }
    }
}
```

### Expected Output
```
Student's Roll No: 102
Student Name: Sara Ahmed
Student Address: Lahore, Pakistan
Book 1: Data Structures
Book 2: Algorithms
Book 3: Operating Systems

This is Main Class method
This is Partial Class method
```

### How It Works
1. **Partial Classes**: The `Student` class is split across two files using the `partial` keyword
2. **Shared State**: Both partial definitions share the same fields and properties
3. **Indexer Access**: The indexer defined in the main class works seamlessly
4. **Method Distribution**: Methods can be distributed across partial files for better organization
5. **Compilation**: At compile time, the compiler combines all partial definitions into a single class

### Key Benefits of Partial Classes with Indexers
- **Code Organization**: Split large classes into logical sections
- **Separation of Concerns**: Keep generated code separate from custom code
- **Team Collaboration**: Multiple developers can work on different parts of the same class
- **Maintainability**: Easier to manage and navigate large class definitions

---

## Example 4: Properties and Constructors (No Indexer)

### Overview
This example demonstrates a `YoungProfessional` class focusing on **properties with different access levels** and **constructor overloading**. While this doesn't include an indexer, it's included to show other important C# concepts.

### Key Learning Points
- Private setters for controlled property access
- Default and parameterized constructors
- Method-based property modification

---

### Code Implementation

#### YoungProfessional.cs
```csharp
using System;

namespace Learning_Indexer
{
    /// <summary>
    /// Represents a young professional with identifying information and personal details.
    /// </summary>
    public class YoungProfessional
    {
        // Default Constructor
        public YoungProfessional()
        {
        }

        // Parameterized Constructor
        public YoungProfessional(string dob)
        {
            DateOfBirth = dob; // Set DateOfBirth using the parameter
        }

        public int PersonalId { get; private set; } // Private setter - can only be set within the class
        public int RNo { get; set; } // Public getter and setter
        public string DateOfBirth { get; private set; } // Private setter - requires method to change
        public string Name { get; set; } // Public getter and setter

        /// <summary>
        /// Method to set DateOfBirth since the setter is private.
        /// This provides controlled access to the private setter.
        /// </summary>
        public void SetDateOfBirth(string dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
        }

        /// <summary>
        /// Method to set PersonalId since the setter is private.
        /// </summary>
        public void SetPersonalId(int id)
        {
            PersonalId = id;
        }
    }
}
```

#### Program.cs
```csharp
using System;

namespace Learning_Indexer
{
    /// <summary>
    /// Provides the entry point for the application.
    /// Purpose: To demonstrate properties with private setters and constructor overloading.
    /// Learning Outcome: Understand property access control and constructor usage.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // Using default constructor
            YoungProfessional yp1 = new YoungProfessional();
            yp1.Name = "John Doe";
            yp1.RNo = 501;
            yp1.SetPersonalId(12345);
            yp1.SetDateOfBirth("1995-05-15");

            Console.WriteLine("=== Young Professional 1 ===");
            Console.WriteLine("Name: " + yp1.Name);
            Console.WriteLine("Registration No: " + yp1.RNo);
            Console.WriteLine("Personal ID: " + yp1.PersonalId);
            Console.WriteLine("Date of Birth: " + yp1.DateOfBirth);

            Console.WriteLine(); // Empty line

            // Using parameterized constructor
            YoungProfessional yp2 = new YoungProfessional("1998-08-20");
            yp2.Name = "Jane Smith";
            yp2.RNo = 502;
            yp2.SetPersonalId(67890);

            Console.WriteLine("=== Young Professional 2 ===");
            Console.WriteLine("Name: " + yp2.Name);
            Console.WriteLine("Registration No: " + yp2.RNo);
            Console.WriteLine("Personal ID: " + yp2.PersonalId);
            Console.WriteLine("Date of Birth: " + yp2.DateOfBirth);
        }
    }
}
```

### Expected Output
```
=== Young Professional 1 ===
Name: John Doe
Registration No: 501
Personal ID: 12345
Date of Birth: 1995-05-15

=== Young Professional 2 ===
Name: Jane Smith
Registration No: 502
Personal ID: 67890
Date of Birth: 1998-08-20
```

### How It Works
1. **Private Setters**: `PersonalId` and `DateOfBirth` have private setters, preventing direct external modification
2. **Controlled Access**: Public methods (`SetDateOfBirth`, `SetPersonalId`) provide controlled ways to modify these properties
3. **Constructor Overloading**: Two constructors provide different initialization options
4. **Encapsulation**: Sensitive data is protected while still being accessible through methods

### Key Concepts Demonstrated
- **Property Access Control**: Using `{ get; private set; }` to restrict property modification
- **Constructor Overloading**: Providing multiple ways to initialize an object
- **Method-Based Setters**: Using methods to set properties with private setters
- **Encapsulation**: Protecting internal state while providing controlled access

---

## Example 5: Static Classes and Members

### Overview
This example demonstrates **static classes** in C#. A static class cannot be instantiated and can only contain static members. Static classes are ideal for utility methods and shared functionality that doesn't require object state.

### Key Learning Points
- Understanding static classes and their restrictions
- Static constructors and when they execute
- Accessing static members without creating instances
- Static constructor execution (runs only once per AppDomain)

---

### Code Implementation

#### StaticLearning.cs
```csharp
using System;

namespace Learning_Indexer
{
    /// <summary>
    /// Provides static members for managing and retrieving a roll number value.
    /// This class demonstrates static class functionality with utility methods.
    /// </summary>
    /// <remarks>
    /// This class cannot be instantiated. All members are static and shared 
    /// across the application domain.
    /// </remarks>
    public static class StaticLearning
    {
        public static int RollNo; // Static variable - shared across all access points

        /// <summary>
        /// Static constructor - called automatically once before first use.
        /// </summary>
        static StaticLearning()
        {
            RollNo = 2;
            Console.WriteLine("Static constructor called - initializing RollNo");
        }

        /// <summary>
        /// Static method to retrieve the current RollNo value.
        /// </summary>
        /// <returns>The current RollNo value</returns>
        public static int GetRollNo()
        {
            return RollNo;
        }

        /// <summary>
        /// Static method to count the number of characters in a string.
        /// </summary>
        /// <param name="sentence">The string to count characters from</param>
        /// <returns>The number of characters in the string</returns>
        public static int WordCount(this string sentence)
        {
            return sentence.Length;
        }
    }
}
```

#### Program.cs
```csharp
using System;

namespace Learning_Indexer
{
    /// <summary>
    /// Provides the entry point for the application.
    /// Purpose: To demonstrate static classes and members in C#.
    /// Learning Outcome: Understand how static classes work and when to use them.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Static Class Demo ===");
            Console.WriteLine();

            // First call to static class - triggers static constructor
            int rollNo1 = StaticLearning.GetRollNo();
            Console.WriteLine("First call - Roll No: " + rollNo1);

            // Second call to static class - constructor NOT called again
            int rollNo2 = StaticLearning.GetRollNo();
            Console.WriteLine("Second call - Roll No: " + rollNo2);

            Console.WriteLine();

            // Using static utility method
            string name = "Hello World";
            int count = StaticLearning.WordCount(name);
            Console.WriteLine("String: " + name);
            Console.WriteLine("Number of characters in the string: " + count);
        }
    }
}
```

### Expected Output
```
=== Static Class Demo ===

Static constructor called - initializing RollNo
First call - Roll No: 2
Second call - Roll No: 2

String: Hello World
Number of characters in the string: 11
```

### How It Works
1. **Static Class**: The `StaticLearning` class is marked as `static`, meaning it cannot be instantiated
2. **Static Constructor**: Runs automatically once before the first access to any static member
3. **Static Members**: All fields and methods must be static
4. **No Instance Required**: Methods are called directly on the class: `StaticLearning.GetRollNo()`
5. **Shared State**: The `RollNo` variable is shared across all access points in the application
6. **Single Initialization**: The static constructor runs only once, not on every method call

### Key Characteristics of Static Classes
- **Cannot be instantiated**: No `new` keyword allowed
- **Cannot inherit or be inherited**: Implicitly sealed
- **Only static members**: All fields, properties, and methods must be static
- **Thread-safe initialization**: Static constructor guarantees single execution
- **Application-wide lifetime**: Static members exist for the entire application duration

### When to Use Static Classes
- **Utility Methods**: Math helpers, string formatters, validation functions
- **Extension Methods**: Extension methods must be in a static class
- **Constants and Configuration**: Shared values across the application
- **Stateless Operations**: Methods that don't require object state

### When NOT to Use Static Classes
- **When state management is needed**: Use instance classes instead
- **For dependency injection**: Static classes don't work well with DI containers
- **When testability is important**: Static dependencies are harder to mock
- **For polymorphism**: Static classes cannot implement interfaces or inherit

