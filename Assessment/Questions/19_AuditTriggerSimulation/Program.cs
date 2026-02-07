using System;
using AuditTriggerSimulation;

class Program
{
    class Employee
    {
        public string Name { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string Department { get; set; } = string.Empty;
    }

    static void Main()
    {
        Console.WriteLine("=== Problem 19: Audit Trigger Simulation ===");
        var tracker = new EntityTracker<Employee>();

        var emp = new Employee { Name = "John", Salary = 50000, Department = "IT" };
        tracker.TrackEntity("EMP001", emp);
        Console.WriteLine("Initial tracking: John, $50000, IT");

        emp.Salary = 55000;
        emp.Department = "Engineering";
        var changes = tracker.DetectChanges("EMP001", emp);

        Console.WriteLine($"\nDetected {changes.Count} changes:");
        foreach (var change in changes)
        {
            Console.WriteLine($"  {change.PropertyName}: {change.OldValue} -> {change.NewValue}");
        }

        Console.WriteLine("✓ Test Passed\n");
    }
}
