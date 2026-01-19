# C# Generics — Academic Notes with Code

Generics let you write reusable, type-safe code. Instead of working with `object` and casting, generics allow you to define classes and methods that operate on a placeholder type. This improves safety (compile-time checks), performance (less boxing/casting), and clarity.

## Why Use Generics
- Type safety: the compiler enforces correct types at compile time.
- Performance: avoids boxing/unboxing and runtime casts.
- Reusability: write once, use with many types.
- Expressiveness: constraints (`where T : ...`) model valid type shapes.

## Example 1: Single-Type Generic with Constraints
The generic class `MyGlobalType<T>` accepts only `Student` or derived types. The `where T : Student` constraint enforces this.

```csharp
using System;

namespace StudentData
{
    // Base type
    public class Student
    {
        public string Name { get; set; }
    }

    // Derived types
    public class UGStudent : Student
    {
        public int SchoolMarks { get; set; }
    }

    public class PGStudent : Student
    {
        public int DegreeMarks { get; set; }
    }

    // Generic constrained to Student hierarchy
    public class MyGlobalType<T> where T : Student
    {
        public string GetDataType(T student)
        {
            return student.GetType().Name; // runtime type name
        }
    }
}
```

Usage:

```csharp
using System;
using StudentData;

namespace DemoSingleType
{
    class Program
    {
        static void Main(string[] args)
        {
            var globalType = new MyGlobalType<Student>();

            Student s1 = new Student { Name = "Base Student" };
            Student s2 = new UGStudent { Name = "UG Student", SchoolMarks = 90 };
            Student s3 = new PGStudent { Name = "PG Student", DegreeMarks = 85 };

            Console.WriteLine(globalType.GetDataType(s1)); // Student
            Console.WriteLine(globalType.GetDataType(s2)); // UGStudent
            Console.WriteLine(globalType.GetDataType(s3)); // PGStudent
        }
    }
}
```

## Example 2: Multiple Datatypes in the Same Generic
You can define generics with more than one type parameter. Below, `StudentRecord<TStudent, TScore>` stores a student and a score. Constraints ensure valid usage.

```csharp
using System;

namespace MultiTypeGeneric
{
    // Reuse Student types from previous example or redefine minimal shape
    public class Student
    {
        public string Name { get; set; }
    }

    public class UGStudent : Student
    {
        public int SchoolMarks { get; set; }
    }

    public class PGStudent : Student
    {
        public int DegreeMarks { get; set; }
    }

    // Generic with two type parameters
    // TStudent must be a Student; TScore must be a value type (number-like)
    public class StudentRecord<TStudent, TScore>
        where TStudent : Student
        where TScore : struct
    {
        public TStudent Student { get; }
        public TScore Score { get; }

        public StudentRecord(TStudent student, TScore score)
        {
            Student = student;
            Score = score;
        }

        public override string ToString()
        {
            return $"{Student.GetType().Name}({Student.Name}) => Score: {Score}";
        }
    }
}
```

Usage with multiple datatypes:

```csharp
using System;
using MultiTypeGeneric;

namespace DemoMultiType
{
    class Program
    {
        static void Main(string[] args)
        {
            // UGStudent with int score
            var ug = new UGStudent { Name = "Alice", SchoolMarks = 92 };
            var ugRecord = new StudentRecord<UGStudent, int>(ug, ug.SchoolMarks);
            Console.WriteLine(ugRecord); // UGStudent(Alice) => Score: 92

            // PGStudent with decimal score
            var pg = new PGStudent { Name = "Bob", DegreeMarks = 86 };
            var pgRecord = new StudentRecord<PGStudent, decimal>(pg, 86.5m);
            Console.WriteLine(pgRecord); // PGStudent(Bob) => Score: 86.5

            // Built-in multi-parameter generic: Dictionary<TKey, TValue>
            var map = new System.Collections.Generic.Dictionary<string, int>();
            map["Math"] = 95;
            map["Science"] = 90;
            Console.WriteLine($"Subjects: {string.Join(", ", map.Keys)}");
        }
    }
}
```

## Key Takeaways
- `where T : BaseType` ensures only valid types are used.
- Multiple type parameters (`<T1, T2>`) model relationships between values.
- Built-in types like `Dictionary<TKey, TValue>` are everyday multi-type generics.
- Prefer value types for numeric scores (`where TScore : struct`) to avoid nulls; use `TScore?` if nullable is desired.

## Bonus: Generic Methods (Optional)
You can also make individual methods generic:

```csharp
public static class Utils
{
    public static T Echo<T>(T value) => value;
}
```

This compiles for any `T` and returns the same value while preserving type information.

---

**Practice Code (normalized): Constrained generic collection and type actions**

```csharp
using System;
using System.Collections.Generic;

namespace MyLocalNameSpace
{
    public class Student
    {
        public int Id { get; set; }
    }

    public class UGStudent : Student
    {
        public int HighSchoolMark { get; set; }
    }

    public class PGStudent : UGStudent
    {
        public int UGMark { get; set; }
    }
}

namespace LearningCSharp
{
    // Single-type generic with a constrained collection and helpers
    public class MyGlobalType<T> where T : MyLocalNameSpace.Student
    {
        public List<T> MyCollection { get; } = new List<T>();

        public string GetDataType(T t)
        {
            return t.GetType().Name; // e.g., UGStudent, PGStudent
        }

        public void AddItem(T t)
        {
            MyCollection.Add(t);
        }

        public List<T> GetCollection()
        {
            return MyCollection;
        }

        public string ActBasedOnType(T t)
        {
            if (t is MyLocalNameSpace.PGStudent) return "Type is PGStudent";
            if (t is MyLocalNameSpace.UGStudent) return "Type is UGStudent";
            return "Student";
        }
    }

    // Two-parameter generic example with constraints
    public class MyGlobalType2<TStudent, TScore>
        where TStudent : MyLocalNameSpace.Student
        where TScore : struct
    {
        public void MyGlobalFunction(TStudent student, TScore score)
        {
            Console.WriteLine($"{student.GetType().Name} Id={student.Id} Score={score}");
        }
    }
}

// Minimal usage demo
namespace DemoPractice
{
    using LearningCSharp;
    using MyLocalNameSpace;

    class Program
    {
        static void Main(string[] args)
        {
            var handler = new MyGlobalType<UGStudent>();
            var ug = new UGStudent { Id = 1, HighSchoolMark = 92 };

            Console.WriteLine(handler.GetDataType(ug));           // UGStudent
            Console.WriteLine(handler.ActBasedOnType(ug));        // Type is UGStudent

            handler.AddItem(ug);
            Console.WriteLine(handler.GetCollection().Count);     // 1

            var fn = new MyGlobalType2<PGStudent, int>();
            var pg = new PGStudent { Id = 2, UGMark = 86 };
            fn.MyGlobalFunction(pg, 88); // PGStudent Id=2 Score=88
        }
    }
}
```

Explanation:
- Initializes `MyCollection` to avoid null references when adding items.
- Uses `GetType().Name` for readable type output.
- Fixes label typo in `ActBasedOnType` and adds clear type routing.
- Adds constraints to `MyGlobalType2<TStudent, TScore>` to align with earlier multi-type examples.