# Day 2 Notes — C# Fundamentals

## Entry Point
- The `Main` method is the entry point in C#
- Modern .NET also supports top-level statements for simpler applications

```csharp
// Traditional entry point
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
```

## Comments and Documentation
- Comments are important in industry-level programming
- Always provide XML documentation summaries for methods and programs
- Use `///` for XML documentation comments

```csharp
/// <summary>
/// Calculates the sum of two numbers.
/// </summary>
/// <param name="a">First number</param>
/// <param name="b">Second number</param>
/// <returns>Sum of a and b</returns>
public static int Add(int a, int b)
{
    return a + b;
}
```

## Namespaces
- Namespace of a project typically comes from the solution/project name you declared
- Helps organize code and avoid naming conflicts

## Naming Conventions
- In C# .NET, the names of **Classes**, **Methods**, and **Properties** always start with a **capital letter** (PascalCase)
- Examples: `MyClass`, `CalculateTotal()`, `FirstName`
- Local variables use camelCase: `myVariable`, `counter`

## Memory Management

### Instance vs Static
- **Instance variables and methods** can be removed from program memory by Garbage Collection
- **Static variables** stay in memory until the program exits
- Use static for utility methods, instance for object-specific behavior

```csharp
public class Example
{
    // Instance variable - cleaned by GC
    private int instanceVar;
    
    // Static variable - lives until program exit
    private static int staticVar;
    
    // Instance method
    public void InstanceMethod() { }
    
    // Static method
    public static void StaticMethod() { }
}
```

## Performance Tips

### Variable Declaration in Loops
- Variable declarations should **not** be inside loops
- Declaring inside loops allocates memory repeatedly, causing performance issues

```csharp
// ❌ Bad - allocates memory each iteration
for (int i = 0; i < 1000; i++)
{
    StringBuilder sb = new StringBuilder();
    sb.Append(i);
}

// ✅ Good - allocates once
StringBuilder sb = new StringBuilder();
for (int i = 0; i < 1000; i++)
{
    sb.Clear();
    sb.Append(i);
}
```

## Code Organization

### Regions
- Use `#region` and `#endregion` to organize code sections
- Important for grouping related code together

```csharp
#region Variable Declarations
private int count;
private string name;
#endregion

#region Methods
public void DoSomething() { }
#endregion
```

## Key Takeaways
- ✅ Use `Main` as entry point
- ✅ Write XML documentation comments
- ✅ Follow PascalCase for public members
- ✅ Be mindful of static vs instance lifetime
- ✅ Avoid allocations inside loops
- ✅ Use regions to organize code