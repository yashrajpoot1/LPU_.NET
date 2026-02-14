namespace ITSupportTicketSeverity;

public abstract class User
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }

    protected User(string userId, string name, int age)
    {
        UserId = userId;
        Name = name;
        Age = age;
    }

    public abstract void ResolveTicket(SupportTicket ticket);
}

public class Employee : User
{
    public Employee(string userId, string name, int age) : base(userId, name, age) { }

    public override void ResolveTicket(SupportTicket ticket)
    {
        Console.WriteLine($"✓ Employee {Name} resolved ticket: {ticket.Description}");
    }
}

public class Admin : User
{
    public Admin(string userId, string name, int age) : base(userId, name, age) { }

    public override void ResolveTicket(SupportTicket ticket)
    {
        Console.WriteLine($"✓ Admin {Name} resolved critical ticket: {ticket.Description}");
    }
}

public class SupportTicket
{
    public string TicketId { get; set; }
    public User Reporter { get; set; }
    private int _severity;
    public int Severity
    {
        get => _severity;
        set
        {
            if (value < 1 || value > 5)
                throw new InvalidPriorityException(value);
            _severity = value;
        }
    }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public SupportTicket(string ticketId, User reporter, int severity, string description)
    {
        TicketId = ticketId;
        Reporter = reporter;
        Severity = severity;
        Description = description;
        CreatedAt = DateTime.Now;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Ticket {TicketId}: {Description}");
        Console.WriteLine($"  Reporter: {Reporter.Name}, Severity: {Severity}");
        Console.WriteLine($"  Created: {CreatedAt:yyyy-MM-dd HH:mm:ss}");
    }
}

public class TicketManager
{
    private readonly SortedDictionary<int, Queue<SupportTicket>> _tickets = new();
    private readonly int _maxQueueSize;
    private int _totalTickets = 0;

    public TicketManager(int maxQueueSize)
    {
        _maxQueueSize = maxQueueSize;
    }

    public void AddTicket(SupportTicket ticket)
    {
        if (!_tickets.ContainsKey(ticket.Severity))
            _tickets[ticket.Severity] = new Queue<SupportTicket>();

        _tickets[ticket.Severity].Enqueue(ticket);
        _totalTickets++;
        Console.WriteLine($"✓ Ticket added: {ticket.TicketId} (Severity: {ticket.Severity})");
    }

    public void ProcessNextTicket()
    {
        foreach (var kvp in _tickets)
        {
            if (kvp.Value.Count > 0)
            {
                var ticket = kvp.Value.Dequeue();
                _totalTickets--;
                ticket.Reporter.ResolveTicket(ticket);
                return;
            }
        }
        Console.WriteLine("No tickets to process");
    }

    public void DisplayTickets()
    {
        Console.WriteLine("\n========== TICKETS (By Severity) ==========");
        foreach (var kvp in _tickets)
        {
            if (kvp.Value.Count > 0)
            {
                Console.WriteLine($"\n--- Severity {kvp.Key} ({kvp.Value.Count} tickets) ---");
                foreach (var ticket in kvp.Value)
                {
                    ticket.DisplayInfo();
                }
            }
        }
    }

    public int GetTotalTickets() => _totalTickets;
}

public class TicketException : Exception
{
    public TicketException(string message) : base(message) { }
}

public class InvalidPriorityException : TicketException
{
    public InvalidPriorityException(int priority)
        : base($"Invalid priority: {priority}. Must be between 1 and 5") { }
}

public class TicketNotFoundException : TicketException
{
    public TicketNotFoundException(string ticketId)
        : base($"Ticket {ticketId} not found") { }
}
