## Events

# C# Events — Publisher/Subscriber Pattern

- Objectives: understand events in C#, how they relate to delegates, and implement a clean publisher/subscriber pattern with `EventHandler` and custom `EventArgs`.

## Overview

- Events are notifications raised by an object (publisher) and handled by other code (subscribers).
- An event wraps a delegate to control invocation from outside the class (only the publisher can raise it).
- Typical uses: UI notifications, domain state changes, background progress reporting.

## Key Concepts

- Delegate: the callable type behind an event (e.g., `EventHandler`, `EventHandler<TEventArgs>`).
- Publisher: exposes an `event`, raises it via a protected `On...` method.
- Subscriber: attaches a handler to the event and detaches when no longer needed.
- EventArgs: payload with extra data for subscribers.

## Example: Value threshold event with payload

```csharp
using System;

namespace LearningCSharp
{
    // Payload with the value and when it triggered
    public class ThresholdReachedEventArgs : EventArgs
    {
        public int Value { get; }
        public DateTime Timestamp { get; }

        public ThresholdReachedEventArgs(int value, DateTime timestamp)
        {
            Value = value;
            Timestamp = timestamp;
        }
    }

    // Publisher: raises event when input exceeds threshold
    public class ValueMonitor
    {
        public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;
        private readonly int _threshold;

        public ValueMonitor(int threshold = 500)
        {
            _threshold = threshold;
        }

        public void Submit(int value)
        {
            if (value >= _threshold)
            {
                OnThresholdReached(new ThresholdReachedEventArgs(value, DateTime.UtcNow));
            }
        }

        // Protected virtual raise pattern for extensibility
        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            ThresholdReached?.Invoke(this, e);
        }
    }

    // Subscriber: prints a friendly message
    public class ConsoleNotifier
    {
        public void OnThresholdReached(object sender, ThresholdReachedEventArgs e)
        {
            Console.WriteLine($"Threshold reached with value {e.Value} at {e.Timestamp:O}");
        }
    }

    public class Program
    {
        public static void Main()
        {
            var monitor = new ValueMonitor(500);
            var notifier = new ConsoleNotifier();

            // Subscribe
            monitor.ThresholdReached += notifier.OnThresholdReached;

            Console.WriteLine("Enter numbers (type 'exit' to quit):");
            while (true)
            {
                Console.Write("Number> ");
                var input = Console.ReadLine();
                if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                    break;

                if (int.TryParse(input, out var num))
                {
                    monitor.Submit(num);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }
            }

            // Unsubscribe when done (good practice)
            monitor.ThresholdReached -= notifier.OnThresholdReached;
        }
    }
}
```

## Notes and Best Practices

- Use `EventHandler`/`EventHandler<TEventArgs>` for consistency with .NET.
- Follow the raise pattern: protected `OnEventName(args)` method; use null-conditional `?.Invoke`.
- Prefer `EventArgs` subclasses over custom delegates that pass raw data.
- Unsubscribe to avoid memory leaks, especially for long-lived publishers.
- Name events with past tense or logical action: `Completed`, `Changed`, `ThresholdReached`.

## Practice

- Add a second subscriber that logs to a file (or console simulating a file).
- Extend `ThresholdReachedEventArgs` to include a `Threshold` property.
- Introduce a `Reset()` method in `ValueMonitor` and raise a `Reset` event; subscribe and observe both events.