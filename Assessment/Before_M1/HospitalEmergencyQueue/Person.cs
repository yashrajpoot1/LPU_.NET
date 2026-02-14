namespace HospitalEmergencyQueue;

public abstract class Person
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }

    protected Person(string id, string name, int age)
    {
        Id = id;
        Name = name;
        Age = age;
    }

    public abstract void DisplayInfo();
}

public class Patient : Person
{
    private int _severity;
    public int Severity
    {
        get => _severity;
        set
        {
            if (value < 1 || value > 5)
                throw new InvalidSeverityException(value);
            _severity = value;
        }
    }

    public string Condition { get; set; }
    public DateTime ArrivalTime { get; set; }

    public Patient(string id, string name, int age, int severity, string condition)
        : base(id, name, age)
    {
        Severity = severity; // Uses property setter for validation
        Condition = condition;
        ArrivalTime = DateTime.Now;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Patient ID: {Id}, Name: {Name}, Age: {Age}");
        Console.WriteLine($"  Severity: {Severity} ({GetSeverityLabel()})");
        Console.WriteLine($"  Condition: {Condition}");
        Console.WriteLine($"  Arrival: {ArrivalTime:HH:mm:ss}");
    }

    public virtual void Treat()
    {
        Console.WriteLine($"✓ Treating {Name} for {Condition} (Severity: {Severity})");
    }

    private string GetSeverityLabel()
    {
        return Severity switch
        {
            1 => "Critical",
            2 => "Urgent",
            3 => "Moderate",
            4 => "Minor",
            5 => "Non-urgent",
            _ => "Unknown"
        };
    }
}
