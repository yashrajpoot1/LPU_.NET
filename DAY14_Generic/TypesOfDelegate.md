# C# Delegates — Types, Examples, and Use Cases

Delegates are type-safe function pointers in C#. They let you pass methods as parameters, store them, and invoke them later. Delegates are foundational for callbacks, events, and composing behavior.

## Why Delegates Matter
- Decouple caller from callee: pass behavior instead of hard-coding it.
- Enable callbacks: run code after a task finishes.
- Power events: `event` is built on delegates.
- Compose logic: multicast delegates, pipelines, strategy pattern.

## 1) Custom Delegates (Single and Multicast)
Define your own delegate type that matches a method signature.

```csharp
// Define a delegate type
public delegate int BinaryOp(int x, int y);

// Target methods
int Add(int a, int b) => a + b;
int Mul(int a, int b) => a * b;

// Use it
BinaryOp op = Add;                  // single-cast
Console.WriteLine(op(2, 3));       // 5

op += Mul;                         // multicast: Add then Mul
foreach (BinaryOp d in op.GetInvocationList())
{
	Console.WriteLine(d(2, 3));    // 5 then 6
}
```

Notes:
- Multicast delegates call targets in order added. Only the last return value is kept when invoked directly; use `GetInvocationList()` if you need all results.

## 2) Anonymous Methods and Lambdas
Delegates can reference inline logic without naming a method.

```csharp
public delegate void Log(string message);

// Anonymous method
Log logger = delegate(string msg) { Console.WriteLine($"[LOG] {msg}"); };
logger("Started");

// Lambda expression
logger = msg => Console.WriteLine($"[LOG] {msg}");
logger("Finished");
```

## 3) Built-in Delegates: Action, Func, Predicate
Use standard library types instead of custom delegate declarations.

```csharp
// Action: no return value
Action<string> print = s => Console.WriteLine(s);
print("Hello Action");

// Func: returns a value
Func<int, int, int> max = (a, b) => a > b ? a : b;
Console.WriteLine(max(7, 4));

// Predicate: returns bool, usually for filtering
Predicate<int> isEven = n => n % 2 == 0;
Console.WriteLine(isEven(8)); // True
```

## 4) Events (Delegates Under the Hood)
`event` restricts delegate usage to add/remove/raise patterns and prevents external overwriting.

```csharp
using System;

public class Downloader
{
	public event Action<int> ProgressChanged; // percent

	public void Start()
	{
		for (int p = 0; p <= 100; p += 25)
		{
			ProgressChanged?.Invoke(p); // safe invoke
		}
	}
}

// Usage
var d = new Downloader();
d.ProgressChanged += p => Console.WriteLine($"Progress: {p}%");
d.Start();
```

## 5) Practical Use Cases
- Callbacks: notify completion or progress of long-running work.
- Strategy pattern: inject behavior (e.g., `Func<T, TResult>`) to vary processing.
- LINQ-style pipelines: chain `Func<T, T>` transforms.
- UI/event handling: subscribe to user interactions.
- Validation/filtering: pass `Predicate<T>` to select items.

## Quick Runnable Snippet
Copy this into a `Program.cs` to see delegates in action.

```csharp
using System;

public delegate int BinaryOp(int x, int y);

class Program
{
	static int Add(int a, int b) => a + b;
	static int Mul(int a, int b) => a * b;

	static void Main()
	{
		// Custom delegate
		BinaryOp op = Add;
		Console.WriteLine(op(2, 3)); // 5
		op += Mul;
		foreach (var d in op.GetInvocationList())
			Console.WriteLine(((BinaryOp)d)(2, 3)); // 5, 6

		// Built-ins
		Action<string> log = m => Console.WriteLine($"[LOG] {m}");
		Func<int, int, int> max = (x, y) => x > y ? x : y;
		Predicate<int> isEven = n => n % 2 == 0;

		log($"Max: {max(10, 7)}");              // Max: 10
		log($"Is 8 even? {isEven(8)}");         // True

		// Event demo
		var dl = new Downloader();
		dl.ProgressChanged += p => Console.WriteLine($"Progress {p}%");
		dl.Start();
	}
}

public class Downloader
{
	public event Action<int> ProgressChanged;
	public void Start()
	{
		for (int p = 0; p <= 100; p += 50)
			ProgressChanged?.Invoke(p);
	}
}
```

## Tips
- Prefer `Action`, `Func`, and `Predicate` for common signatures.
- Use `GetInvocationList()` when you need individual results from a multicast delegate.
- Use `?.Invoke(...)` to safely raise events when there are subscribers.

---

## Practice Programs (kept and normalized)

These small programs illustrate practical uses of delegates.

### Program 1: Strategy via `Action<string>` logger
Select logging behavior at runtime using delegates.

```csharp
using System;

class Program
{
	static void Main()
	{
		Action<string> logger = DateTime.Now.Hour < 12
			? CreateMorningLogger()
			: CreateDayLogger();

		logger("Application Started");
	}

	static Action<string> CreateMorningLogger() =>
		msg => Console.WriteLine($"Good Morning! {msg}");

	static Action<string> CreateDayLogger() =>
		msg => Console.WriteLine($"Good Day! {msg}");
}
```

### Program 2: `Func<int,int,string>` with formatting
Return a formatted string using a `Func` that multiplies inputs.

```csharp
using System;

class Program
{
	static void Main()
	{
		Func<int, int, string> multiplyResult = (x, y) =>
			$"Multiplication of {x} and {y} is: {x * y}";

		Console.WriteLine(multiplyResult(5, 10));
	}
}
```
