# C# Custom Exceptions

- Objectives: create meaningful custom exceptions and use them to provide user-friendly messages while preserving system errors for logging.

## Overview

- Inherit from `Exception` to define domain-specific errors.
- Prefer including the original cause via `innerException` and avoid swallowing system messages.

## Basic custom exception

```csharp
using System;

namespace Coding_Class
{
    public class AppCustomException : Exception
    {
        public override string Message => "Internal Exception";
    }
}
```

## Logging system errors and returning friendly messages

```csharp
using System;

namespace Coding_Class
{
    public class AppCustomException : Exception
    {
        public AppCustomException(string systemMessage, Exception inner = null)
            : base(systemMessage, inner) {}

        public override string Message => HandleBase(base.Message);

        private static string HandleBase(string systemMessage)
        {
            Console.WriteLine("Log to File : " + systemMessage);
            return "Internal Exception. Contact Admin.";
        }
    }
}
```

## Usage example (division with error handling)

```csharp
using System;

namespace Coding_Class
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                int result = Divide(10, 0);
                Console.WriteLine("The Result is: " + result);
            }
            catch (AppCustomException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static int Divide(int a, int b)
        {
            try
            {
                return a / b;
            }
            catch (Exception e)
            {
                throw new AppCustomException("Division failed: " + e.Message, e);
            }
        }
    }
}
```

## Tips

- Provide multiple constructors (default, message, message + innerException).
- Avoid overusing overrides of `Message`; prefer storing details and exposing safe messages via application logic.
- Log technical details; return friendly messages to users.



## Exception Flow and Sequence

- Sequence concepts:
  - Try → Catch (most specific first) → Finally.
  - Uncaught exceptions propagate up the call stack until handled.
  - Rethrow with `throw;` preserves the original stack trace; `throw ex;` resets it.
  - Use `innerException` to wrap low-level errors with domain context.
  - Exception filters (`catch (ex) when (...)`) decide catchability before entering the block.

### End-to-end flow example

```csharp
using System;

namespace Coding_Class
{
    // Domain-specific exception carrying inner technical details
    public class AppCustomException : Exception
    {
        public AppCustomException(string message, Exception inner = null)
            : base(message, inner) {}
    }

    public static class Database
    {
        public static string Connect()
        {
            Console.WriteLine("[Database] Connecting...");
            throw new InvalidOperationException("Connection failed");
        }
    }

    public static class Repository
    {
        public static string GetData()
        {
            Console.WriteLine("[Repository] Fetching data...");
            try
            {
                return Database.Connect();
            }
            catch (Exception ex)
            {
                // Wrap low-level error with domain context
                throw new AppCustomException("Repository error while fetching data", ex);
            }
            finally
            {
                Console.WriteLine("[Repository] Cleanup in finally.");
            }
        }
    }

    public static class Service
    {
        public static string Process()
        {
            Console.WriteLine("[Service] Processing...");
            try
            {
                return Repository.GetData();
            }
            // Filter: only catch when inner is InvalidOperationException
            catch (AppCustomException ex) when (ex.InnerException is InvalidOperationException)
            {
                Console.WriteLine("[Service] Filtered catch: invalid operation detected.");
                // Log and rethrow preserving stack
                Console.WriteLine($"[Service] Inner: {ex.InnerException!.Message}");
                throw; // preserves stack trace
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("[Program] Start");
            try
            {
                Service.Process();
                Console.WriteLine("[Program] Completed successfully");
            }
            catch (AppCustomException ex)
            {
                Console.WriteLine("[Program] Caught AppCustomException");
                Console.WriteLine($"[Program] Message: {ex.Message}");
                Console.WriteLine($"[Program] Inner: {ex.InnerException?.GetType().Name} - {ex.InnerException?.Message}");
                Console.WriteLine("[Program] StackTrace (short):");
                Console.WriteLine(ex.StackTrace?.Split('\n')[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Program] Caught unexpected: " + ex.GetType().Name);
            }
            finally
            {
                Console.WriteLine("[Program] Finally: releasing resources");
            }
            Console.WriteLine("[Program] End");
        }
    }
}
```

### What happens, in order

- `[Program] Start` prints.
- `Service.Process()` calls `Repository.GetData()` → `Database.Connect()` throws `InvalidOperationException`.
- `Repository` catches and wraps into `AppCustomException` with `innerException`, then `finally` runs.
- `Service` catches via filter (`when inner is InvalidOperationException`), logs, then rethrows with `throw;`.
- `Program` catches `AppCustomException`, prints friendly message and inner details; `finally` executes.
- Control returns to end.

### Best practices

- Catch specific exceptions nearest to the source; avoid broad catches unless at boundaries.
- Use filters to separate concerns without extra `if` logic inside catch blocks.
- Prefer `throw;` to rethrow; keep stack intact for diagnostics.
- Always release resources in `finally` or via `using`/`IDisposable`.