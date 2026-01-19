# LINQ — Beginner Friendly Notes

LINQ (Language Integrated Query) lets you query and transform data (arrays, lists, database rows, files, processes) using simple, readable code.

## What You Need
- Add `using System.Linq;` at the top of your file.
- LINQ works with anything that implements `IEnumerable` (arrays, lists) and with `IQueryable` (databases via EF). Here we focus on in-memory examples.

## Two Ways to Write LINQ
- Query syntax (SQL-like):
  ```csharp
  var bobNames = from name in names
                 where string.Equals(name, "Bob", StringComparison.OrdinalIgnoreCase)
                 select name;
  ```
- Method syntax (chaining):
  ```csharp
  var bobNames = names
      .Where(name => string.Equals(name, "Bob", StringComparison.OrdinalIgnoreCase))
      .Select(name => name);
  ```
Both do the same thing—use whichever you find easier to read.

## Core Ideas (with your `names` example)
- Filter with `Where`:
  ```csharp
  var onlyBob = names.Where(n => n.Equals("Bob", StringComparison.OrdinalIgnoreCase));
  ```
- Transform with `Select`:
  ```csharp
  var upper = names.Select(n => n.ToUpper());
  ```
- Sort with `OrderBy` / `OrderByDescending`:
  ```csharp
  var sortedDesc = names.OrderByDescending(n => n);
  ```
- Run the query by iterating:
  ```csharp
  foreach (var n in upper) Console.WriteLine(n);
  // Iteration (foreach, ToList, ToArray, etc.) actually executes the query.
  ```

## Anonymous Types (your process example)
- Anonymous type = shape data without making a class.
  ```csharp
  var processes = from p in System.Diagnostics.Process.GetProcesses()
                  select new { Name = p.ProcessName, Id = p.Id };

  foreach (var proc in processes)
      Console.WriteLine($"Process Name = {proc.Name} Id = {proc.Id}");
  ```
- Same idea with a class:
  ```csharp
  var processes2 = from p in System.Diagnostics.Process.GetProcesses()
                   select new MyProcess { Name = p.ProcessName, Id = p.Id };
  ```
Use anonymous types for quick shaping; use classes when you need to return or store the result.

## Small Gotchas
- String comparisons are case-sensitive by default. Use `StringComparison.OrdinalIgnoreCase` when matching names like "BoB" vs "Bob".
- Most queries are built first and only run when you iterate (deferred execution). A `foreach` or `ToList()` triggers execution.
- Remember `using System.Linq;` or you won’t see LINQ methods.

## Putting It Together (based on your code)
```csharp
string[] names = { "Asad", "BoB", "Varav", "Abhi" };

// Find "Bob" (case-insensitive)
var findBob = from n in names
              where string.Equals(n, "Bob", StringComparison.OrdinalIgnoreCase)
              select n;

// Uppercase all names
var caps = from n in names select n.ToUpper();

// Sort descending
var sorted = from n in names orderby n descending select n;

foreach (var n in findBob)
    Console.WriteLine(IsPalindrome(n));

static string IsPalindrome(string name)
{
    if (string.IsNullOrWhiteSpace(name)) return $"Not Palindrome {name}";
    var reversed = new string(name.Reverse().ToArray());
    // Case-insensitive check keeps "BoB" palindromic
    return string.Equals(name, reversed, StringComparison.OrdinalIgnoreCase)
        ? $"Palindrome {name}"
        : $"Not Palindrome {name}";
}
```

## Quick Practice
- Find all names that start with "A":
  ```csharp
  var aNames = names.Where(n => n.StartsWith("A", StringComparison.OrdinalIgnoreCase));
  ```
- Top 5 processes by ID:
  ```csharp
  var top5 = System.Diagnostics.Process.GetProcesses()
      .OrderByDescending(p => p.Id)
      .Take(5)
      .Select(p => new { p.ProcessName, p.Id });
  ```

## Cheat Sheet
- Filter: `source.Where(x => condition)`
- Select: `source.Select(x => projection)`
- Order: `source.OrderBy(key)` / `OrderByDescending(key)`
- First or default: `source.FirstOrDefault()`
- Run it: iterate (`foreach`) or materialize (`ToList()`, `ToArray()`).

Keep it simple: filter → select → order → iterate. That’s LINQ.