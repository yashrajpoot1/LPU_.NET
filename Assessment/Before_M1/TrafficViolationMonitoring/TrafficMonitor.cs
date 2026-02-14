namespace TrafficViolationMonitoring;

public class TrafficMonitor
{
    private readonly SortedDictionary<decimal, List<Violation>> _violations = new();
    private readonly decimal _maxPenalty;

    public TrafficMonitor(decimal maxPenalty)
    {
        _maxPenalty = maxPenalty;
    }

    public void AddViolation(Violation violation)
    {
        if (violation.Penalty > _maxPenalty)
            throw new PenaltyExceedsLimitException(violation.Penalty, _maxPenalty);

        if (!_violations.ContainsKey(violation.Penalty))
            _violations[violation.Penalty] = new List<Violation>();

        _violations[violation.Penalty].Add(violation);
        Console.WriteLine($"✓ Violation added: {violation.ViolationId} (Penalty: ${violation.Penalty:F2})");
    }

    public void DisplayViolations()
    {
        Console.WriteLine("\n========== VIOLATIONS (Sorted by Penalty) ==========");
        foreach (var kvp in _violations)
        {
            Console.WriteLine($"\n--- Penalty: ${kvp.Key:F2} ({kvp.Value.Count} violations) ---");
            foreach (var violation in kvp.Value)
            {
                violation.DisplayInfo();
            }
        }
    }

    public int GetTotalViolations() => _violations.Values.Sum(list => list.Count);
}

public class TrafficException : Exception
{
    public TrafficException(string message) : base(message) { }
}

public class InvalidVehicleException : TrafficException
{
    public InvalidVehicleException(string message) : base(message) { }
}

public class PenaltyExceedsLimitException : TrafficException
{
    public PenaltyExceedsLimitException(decimal penalty, decimal limit)
        : base($"Penalty ${penalty:F2} exceeds maximum limit of ${limit:F2}") { }
}
