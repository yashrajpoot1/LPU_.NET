namespace HealthSyncBilling;

/// <summary>
/// Manages consultant billing operations
/// </summary>
public class BillingManager
{
    private readonly List<Consultant> _consultants = new();

    /// <summary>
    /// Add a consultant to the billing system
    /// </summary>
    public void AddConsultant(Consultant consultant)
    {
        if (_consultants.Any(c => c.ConsultantId == consultant.ConsultantId))
        {
            Console.WriteLine($"⚠ Warning: Consultant {consultant.ConsultantId} already exists. Skipping.");
            return;
        }

        _consultants.Add(consultant);
        Console.WriteLine($"✓ Added {consultant.GetConsultantType()} Consultant: {consultant.Name} ({consultant.ConsultantId})");
    }

    /// <summary>
    /// Process payouts for all consultants
    /// </summary>
    public void ProcessAllPayouts()
    {
        Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║         HEALTHSYNC BILLING - PAYOUT PROCESSING        ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.WriteLine($"\nTotal Consultants: {_consultants.Count}\n");

        foreach (var consultant in _consultants)
        {
            consultant.DisplayPayoutDetails();
        }
    }

    /// <summary>
    /// Get consultant by ID
    /// </summary>
    public Consultant? GetConsultant(string consultantId)
    {
        return _consultants.FirstOrDefault(c => c.ConsultantId == consultantId);
    }

    /// <summary>
    /// Display summary statistics
    /// </summary>
    public void DisplaySummary()
    {
        Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║              BILLING SUMMARY STATISTICS               ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");

        var inHouse = _consultants.OfType<InHouseConsultant>().ToList();
        var visiting = _consultants.OfType<VisitingConsultant>().ToList();

        Console.WriteLine($"\nIn-House Consultants: {inHouse.Count}");
        if (inHouse.Any())
        {
            decimal totalInHouse = inHouse.Sum(c => c.CalculateNetPayout());
            Console.WriteLine($"  Total Net Payout: ${totalInHouse:F2}");
        }

        Console.WriteLine($"\nVisiting Consultants: {visiting.Count}");
        if (visiting.Any())
        {
            decimal totalVisiting = visiting.Sum(c => c.CalculateNetPayout());
            Console.WriteLine($"  Total Net Payout: ${totalVisiting:F2}");
        }

        decimal grandTotal = _consultants.Sum(c => c.CalculateNetPayout());
        decimal totalTDS = _consultants.Sum(c => c.CalculateTDS(c.CalculateGrossPayout()));

        Console.WriteLine($"\n--- Overall Totals ---");
        Console.WriteLine($"Total Gross Payout:   ${_consultants.Sum(c => c.CalculateGrossPayout()):F2}");
        Console.WriteLine($"Total TDS Deducted:   ${totalTDS:F2}");
        Console.WriteLine($"Total Net Payout:     ${grandTotal:F2}");
        Console.WriteLine($"════════════════════════════════════════════════════════\n");
    }

    public int GetTotalConsultants() => _consultants.Count;
}
