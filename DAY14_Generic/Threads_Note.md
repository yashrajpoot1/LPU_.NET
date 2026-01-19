## C# Threads — Structured Notes

This note teaches the fundamentals of multithreading in C#: creating threads, coordinating work, avoiding common pitfalls, and writing correct, modern code. It also contains corrected, runnable examples that improve on the original snippet.

### Learning Objectives
- Understand what a thread is and when to use it
- Create and start threads with `Thread`
- Pass data to threads and handle exceptions safely
- Coordinate threads with `Join`, locks, and atomic operations
- Distinguish foreground vs background threads
- Recognize pitfalls: race conditions, deadlocks, interleaved output
- Know when to prefer `Task`/async over raw threads

---

### 1) What Is a Thread?
A thread is a lightweight unit of execution within a process. Multiple threads can run concurrently on multi-core CPUs, enabling parallel or asynchronous work. Threads share process memory, so accessing shared state must be synchronized to avoid data races.

---

### 2) Minimal Example: Two Threads Printing Even/Odd
Issues fixed from the original:
- Added `using System.Threading;`
- Use `Join()` instead of `Console.ReadLine()` to wait correctly
- Consistent output formatting to reduce interleaving
- Named threads for easier debugging

```csharp
using System;
using System.Threading;

namespace LearningThreadBasics
{
    public class Program
    {
        public static void Main()
        {
            Thread t1 = new Thread(PrintEvens) { Name = "EvenThread" };
            Thread t2 = new Thread(PrintOdds)  { Name = "OddThread"  };

            t1.Start();
            t2.Start();

            // Wait for both threads to complete instead of blocking on ReadLine
            t1.Join();
            t2.Join();

            Console.WriteLine("Done.");
        }

        static void PrintEvens()
        {
            Console.WriteLine("Even numbers started.");
            for (int i = 0; i < 20; i += 2)
            {
                Thread.Sleep(50);
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }

        static void PrintOdds()
        {
            Console.WriteLine("Odd numbers started.");
            for (int i = 1; i < 20; i += 2)
            {
                Thread.Sleep(50);
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }
    }
}
```

Note: Console output from multiple threads may still interleave. See synchronization below.

---

### 3) Avoid Interleaved Output with `lock`
When multiple threads write to the console, messages can interleave. Use a shared lock to make the writes atomic.

```csharp
using System;
using System.Threading;

public class SynchronizedConsoleDemo
{
    private static readonly object _consoleLock = new object();

    public static void Main()
    {
        var t1 = new Thread(() => PrintRange("Even", 0));
        var t2 = new Thread(() => PrintRange("Odd", 1));

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();
    }

    static void PrintRange(string label, int start)
    {
        for (int i = start; i < 10; i += 2)
        {
            Thread.Sleep(30);
            lock (_consoleLock)
            {
                Console.WriteLine($"[{label}] {i} (T#{Thread.CurrentThread.ManagedThreadId})");
            }
        }
    }
}
```

---

### 4) Passing Data to Threads
You can pass data using a lambda or `ParameterizedThreadStart` (legacy). Lambdas are simpler and type-safe.

```csharp
using System;
using System.Threading;

public class PassingDataDemo
{
    public static void Main()
    {
        int count = 5;
        string tag = "alpha";

        Thread t = new Thread(() => Work(count, tag));
        t.Start();
        t.Join();
    }

    static void Work(int n, string name)
    {
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"[{name}] iteration {i}");
            Thread.Sleep(20);
        }
    }
}
```

---

### 5) Foreground vs Background Threads
- Foreground threads keep the process alive until they finish (default).
- Background threads do not prevent process exit. Use for ancillary work; ensure graceful shutdown.

```csharp
var worker = new Thread(() => {
    while (true)
    {
        Console.Write(".");
        Thread.Sleep(200);
    }
});
worker.IsBackground = true; // process may exit even if this is running
worker.Start();

Thread.Sleep(1000);
Console.WriteLine("Main exiting; background thread may be terminated.");
```

---

### 6) Synchronization Primitives (Quick Tour)
- `lock`/`Monitor`: Mutual exclusion around a critical section.
- `Interlocked`: Atomic increments/decrements without locks (for simple counters/flags).
- `AutoResetEvent`/`ManualResetEventSlim`: Signal between threads.
- `SemaphoreSlim`: Limit concurrency to N.
- `Mutex`: Cross-process mutual exclusion (heavier).

Example with `Interlocked`:

```csharp
using System;
using System.Threading;

public class InterlockedDemo
{
    private static int _count = 0;

    public static void Main()
    {
        Thread[] threads = new Thread[4];
        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < 1000; j++)
                    Interlocked.Increment(ref _count);
            });
            threads[i].Start();
        }

        foreach (var t in threads) t.Join();
        Console.WriteLine($"Count = {_count}"); // Always 4000
    }
}
```

---

### 7) Handling Exceptions in Threads
Exceptions thrown inside a thread do not propagate to the thread that started it. Catch inside the thread entry method, log, and communicate via shared state if needed.

```csharp
var t = new Thread(() =>
{
    try
    {
        ThrowingWork();
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Worker failed: {ex.Message}");
    }
});
t.Start();
t.Join();
```

---

### 8) Prefer `Task`/`async` for Most Scenarios
In modern .NET, use `Task`, `Task.Run`, and `async`/`await` for composable concurrency, cancellation (`CancellationToken`), and better error propagation.

```csharp
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class TaskBasedDemo
{
    public static async Task Main()
    {
        var cts = new CancellationTokenSource();
        var evenTask = Task.Run(() => PrintSeq(0, 10, 2, cts.Token));
        var oddTask  = Task.Run(() => PrintSeq(1, 10, 2, cts.Token));

        await Task.WhenAll(evenTask, oddTask);
        Console.WriteLine("Done (Tasks).");
    }

    static void PrintSeq(int start, int end, int step, CancellationToken token)
    {
        for (int i = start; i < end; i += step)
        {
            token.ThrowIfCancellationRequested();
            Console.Write(i + " ");
            Thread.Sleep(20);
        }
        Console.WriteLine();
    }
}
```

---

### 9) Common Pitfalls and How to Avoid Them
- Race conditions: Protect shared data with `lock` or `Interlocked`.
- Deadlocks: Avoid nested locks; keep critical sections small; lock in a consistent order.
- Interleaved output: Synchronize console writes (see Section 3).
- Busy waiting: Prefer blocking primitives/events over spin loops.
- Unobserved failures: Catch exceptions inside threads and surface results explicitly.

---

### 10) Quick Exercises
1) Modify the minimal example to print up to 100 with a 10 ms delay and ensure the console output never interleaves.
2) Add a shared counter updated by both threads; first without synchronization (observe wrong totals), then fix using `Interlocked.Increment`.
3) Convert the even/odd example from `Thread` to `Task.Run` and add cancellation after 200 ms.
4) Name your threads and log `Thread.CurrentThread.ManagedThreadId` in each iteration.

---

### 11) Key Takeaways
- Use raw `Thread` for low-level control; prefer `Task`/`async` for most app code.
- Always synchronize access to shared state.
- Use `Join` to wait for completion; avoid relying on `Console.ReadLine()` to keep the process alive.
- Make output deterministic with locks when teaching/demoing.

---

### 12) References
- Microsoft Docs: .NET Threading
- Joseph Albahari, "Threading in C#" (online resource)
- Microsoft Docs: Task Parallel Library (TPL)



#### Async/Await Example: Composing asynchronous operations

A simple example showing `async`/`await`, composing multiple awaits, and avoiding `Console.ReadLine()` by awaiting completion in `Main`.

```csharp
using System;
using System.Threading.Tasks;

namespace LearningThreadAsync
{
    public class ThreadingExample
    {
        public static async Task AsyncMethod()
        {
            Console.WriteLine("Async method started.");
            await Task.Delay(3000);
            Console.WriteLine("Async method completed in 3 seconds.");
        }

        public async Task CallMethodAsync()
        {
            Console.WriteLine("Fetching data (2s)...");
            string result = await FetchDataAsync("https://jsonplaceholder.typicode.com/todos/");
            Console.WriteLine(result);
            await AsyncMethod();
        }

        private static async Task<string> FetchDataAsync(string url)
        {
            await Task.Delay(2000); // simulate I/O work
            return $"Data fetched from {url}";
        }
    }

    public class Program
    {
        public static async Task Main()
        {
            var example = new ThreadingExample();
            await example.CallMethodAsync();
            Console.WriteLine("All async work completed.");
        }
    }
}
```