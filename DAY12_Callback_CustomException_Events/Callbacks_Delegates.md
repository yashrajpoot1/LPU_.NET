# C# Callbacks via Delegates

- Objectives: understand callbacks and implement them using delegates and built-in `Action`/`Func`.

## Overview

- A callback is a function passed to be invoked later.
- In C#, delegates (and `Action`/`Func`) represent callable types, enabling decoupled designs.

## Delegate-based callback example

```csharp
using System;

namespace Coding_Class
{
    public delegate void Notify(string message);

    public class Callback
    {
        public void PlaceOrder(string orderId, Notify callback)
        {
            Console.WriteLine($"Your Order id is: {orderId}");
            callback?.Invoke($"{orderId} confirmation done");
        }
    }

    public class Program
    {
        public static void Main()
        {
            var cb = new Callback();
            cb.PlaceOrder("ORD-123", SendEmail);
            cb.PlaceOrder("ORD-124", SendSMS);
        }

        public static void SendEmail(string message) =>
            Console.WriteLine($"Email: {message}");

        public static void SendSMS(string message) =>
            Console.WriteLine($"SMS: {message}");
    }
}
```

## Using built-in delegates (`Action`)

```csharp
using System;

namespace Coding_Class
{
    public class CallbackWithAction
    {
        public void PlaceOrder(string orderId, Action<string> callback)
        {
            Console.WriteLine($"Order: {orderId}");
            callback?.Invoke($"{orderId} processed");
        }
    }
}
```

## Notes

- Callbacks above are synchronous. For async work, use `Task` and `async/await` or continuations.
- Events (`event`) build on delegates for publish/subscribe.
- Prefer `Action<T>`/`Func<T>` for brevity unless a named delegate improves clarity.

## Practice

- Refactor `PlaceOrder` to accept `Action<string>` and add a push notification callback.
- Add error handling: if `callback` is null, write a default message.
