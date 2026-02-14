namespace HealthSyncBilling;

/// <summary>
/// Visiting Consultant paid per consultation
/// Demonstrates POLYMORPHISM and VIRTUAL METHOD OVERRIDE
/// </summary>
public class VisitingConsultant : Consultant
{
    public int ConsultationsCount { get; set; }
    public decimal RatePerVisit { get; set; }

    public VisitingConsultant(
        string consultantId,
        string name,
        string specialization,
        int consultationsCount,
        decimal ratePerVisit)
        : base(consultantId, name, specialization)
    {
        if (consultationsCount < 0)
            throw new ArgumentException("Consultations count cannot be negative");

        if (ratePerVisit < 0)
            throw new ArgumentException("Rate per visit cannot be negative");

        ConsultationsCount = consultationsCount;
        RatePerVisit = ratePerVisit;
    }

    /// <summary>
    /// Override: Calculate gross payout for Visiting consultant
    /// Formula: ConsultationsCount × RatePerVisit
    /// Demonstrates POLYMORPHISM
    /// </summary>
    public override decimal CalculateGrossPayout()
    {
        return ConsultationsCount * RatePerVisit;
    }

    /// <summary>
    /// Override: Visiting consultants have flat 10% TDS rate
    /// Demonstrates VIRTUAL METHOD OVERRIDE - opting out of default behavior
    /// </summary>
    public override decimal CalculateTDS(decimal grossPayout)
    {
        return grossPayout * 0.10m; // Flat 10%
    }

    /// <summary>
    /// Override: Return flat TDS percentage for visiting consultants
    /// </summary>
    public override string GetTDSPercentage(decimal grossPayout)
    {
        return "10%"; // Always 10% for visiting consultants
    }

    public override string GetConsultantType() => "Visiting";

    public override void DisplayPayoutDetails()
    {
        Console.WriteLine($"\n╔════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║          VISITING CONSULTANT PAYOUT SLIP              ║");
        Console.WriteLine($"╚════════════════════════════════════════════════════════╝");
        Console.WriteLine($"ID: {ConsultantId}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Specialization: {Specialization}");
        Console.WriteLine($"\n--- Earnings Breakdown ---");
        Console.WriteLine($"Consultations:        {ConsultationsCount,10}");
        Console.WriteLine($"Rate per Visit:       ${RatePerVisit,10:F2}");
        Console.WriteLine($"                      ─────────────");

        decimal gross = CalculateGrossPayout();
        decimal tds = CalculateTDS(gross);
        decimal net = CalculateNetPayout();

        Console.WriteLine($"Gross Payout:         ${gross,10:F2}");
        Console.WriteLine($"\n--- Tax Deduction ---");
        Console.WriteLine($"TDS Rate Applied:     {GetTDSPercentage(gross),10} (Flat)");
        Console.WriteLine($"TDS Amount:           ${tds,10:F2}");
        Console.WriteLine($"                      ─────────────");
        Console.WriteLine($"NET PAYOUT:           ${net,10:F2}");
        Console.WriteLine($"════════════════════════════════════════════════════════\n");
    }
}
