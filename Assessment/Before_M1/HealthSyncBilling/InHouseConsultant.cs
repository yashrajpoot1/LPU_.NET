namespace HealthSyncBilling;

/// <summary>
/// In-House Consultant with monthly stipend and allowances
/// Demonstrates POLYMORPHISM through method overriding
/// </summary>
public class InHouseConsultant : Consultant
{
    public decimal MonthlyStipend { get; set; }
    public decimal TravelAllowance { get; set; }
    public decimal PerformanceBonus { get; set; }

    public InHouseConsultant(
        string consultantId,
        string name,
        string specialization,
        decimal monthlyStipend,
        decimal travelAllowance = 0,
        decimal performanceBonus = 0)
        : base(consultantId, name, specialization)
    {
        if (monthlyStipend < 0)
            throw new ArgumentException("Monthly stipend cannot be negative");

        MonthlyStipend = monthlyStipend;
        TravelAllowance = travelAllowance;
        PerformanceBonus = performanceBonus;
    }

    /// <summary>
    /// Override: Calculate gross payout for In-House consultant
    /// Formula: MonthlyStipend + TravelAllowance + PerformanceBonus
    /// Demonstrates POLYMORPHISM
    /// </summary>
    public override decimal CalculateGrossPayout()
    {
        return MonthlyStipend + TravelAllowance + PerformanceBonus;
    }

    /// <summary>
    /// In-House consultants use the default TDS logic (sliding scale)
    /// No override needed - inherits virtual method from base class
    /// </summary>

    public override string GetConsultantType() => "In-House";

    public override void DisplayPayoutDetails()
    {
        Console.WriteLine($"\n╔════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║           IN-HOUSE CONSULTANT PAYOUT SLIP             ║");
        Console.WriteLine($"╚════════════════════════════════════════════════════════╝");
        Console.WriteLine($"ID: {ConsultantId}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Specialization: {Specialization}");
        Console.WriteLine($"\n--- Earnings Breakdown ---");
        Console.WriteLine($"Monthly Stipend:      ${MonthlyStipend,10:F2}");
        Console.WriteLine($"Travel Allowance:     ${TravelAllowance,10:F2}");
        Console.WriteLine($"Performance Bonus:    ${PerformanceBonus,10:F2}");
        Console.WriteLine($"                      ─────────────");

        decimal gross = CalculateGrossPayout();
        decimal tds = CalculateTDS(gross);
        decimal net = CalculateNetPayout();

        Console.WriteLine($"Gross Payout:         ${gross,10:F2}");
        Console.WriteLine($"\n--- Tax Deduction ---");
        Console.WriteLine($"TDS Rate Applied:     {GetTDSPercentage(gross),10}");
        Console.WriteLine($"TDS Amount:           ${tds,10:F2}");
        Console.WriteLine($"                      ─────────────");
        Console.WriteLine($"NET PAYOUT:           ${net,10:F2}");
        Console.WriteLine($"════════════════════════════════════════════════════════\n");
    }
}
