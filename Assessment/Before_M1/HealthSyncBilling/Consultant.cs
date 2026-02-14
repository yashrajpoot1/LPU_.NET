namespace HealthSyncBilling;

/// <summary>
/// Abstract base class representing a medical consultant
/// Demonstrates ABSTRACTION - prevents creation of generic consultants
/// </summary>
public abstract class Consultant
{
    public string ConsultantId { get; set; }
    public string Name { get; set; }
    public string Specialization { get; set; }

    protected Consultant(string consultantId, string name, string specialization)
    {
        // Validate ID before assignment
        if (!ValidateConsultantId(consultantId))
        {
            throw new InvalidConsultantIdException(consultantId);
        }

        ConsultantId = consultantId;
        Name = name;
        Specialization = specialization;
    }

    /// <summary>
    /// Validates consultant ID format: Must be exactly 6 chars, start with "DR", last 4 must be numeric
    /// </summary>
    public static bool ValidateConsultantId(string id)
    {
        if (string.IsNullOrEmpty(id))
            return false;

        // Check length
        if (id.Length != 6)
            return false;

        // Check prefix
        if (!id.StartsWith("DR"))
            return false;

        // Check last 4 characters are numeric
        string numericPart = id.Substring(2);
        return numericPart.All(char.IsDigit);
    }

    /// <summary>
    /// Abstract method - forces subclasses to implement their own payout calculation
    /// Demonstrates ABSTRACTION
    /// </summary>
    public abstract decimal CalculateGrossPayout();

    /// <summary>
    /// Virtual method for TDS calculation - can be overridden by subclasses
    /// Default: Sliding scale (5% for ≤5000, 15% for >5000)
    /// Demonstrates VIRTUAL LOGIC
    /// </summary>
    public virtual decimal CalculateTDS(decimal grossPayout)
    {
        if (grossPayout <= 5000)
            return grossPayout * 0.05m; // 5%
        else
            return grossPayout * 0.15m; // 15%
    }

    /// <summary>
    /// Calculates net payout after TDS deduction
    /// </summary>
    public decimal CalculateNetPayout()
    {
        decimal gross = CalculateGrossPayout();
        decimal tds = CalculateTDS(gross);
        return gross - tds;
    }

    /// <summary>
    /// Gets the TDS percentage applied
    /// </summary>
    public virtual string GetTDSPercentage(decimal grossPayout)
    {
        if (grossPayout <= 5000)
            return "5%";
        else
            return "15%";
    }

    /// <summary>
    /// Display consultant information and payout details
    /// </summary>
    public virtual void DisplayPayoutDetails()
    {
        decimal gross = CalculateGrossPayout();
        decimal tds = CalculateTDS(gross);
        decimal net = CalculateNetPayout();

        Console.WriteLine($"\n--- {GetConsultantType()} Consultant Payout ---");
        Console.WriteLine($"ID: {ConsultantId}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Specialization: {Specialization}");
        Console.WriteLine($"Gross Payout: ${gross:F2}");
        Console.WriteLine($"TDS Applied: {GetTDSPercentage(gross)}");
        Console.WriteLine($"TDS Amount: ${tds:F2}");
        Console.WriteLine($"Net Payout: ${net:F2}");
    }

    public abstract string GetConsultantType();
}
