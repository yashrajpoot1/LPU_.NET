namespace LogisticsProShipment;

/// <summary>
/// ShipmentDetails class that inherits from Shipment
/// Provides validation and cost calculation functionality
/// </summary>
public class ShipmentDetails : Shipment
{
    public ShipmentDetails() : base()
    {
    }

    public ShipmentDetails(string shipmentCode, string transportMode, double weight, int storageDays)
        : base(shipmentCode, transportMode, weight, storageDays)
    {
    }

    /// <summary>
    /// Validates the shipment code
    /// Rules:
    /// - Length must be exactly 7 characters
    /// - Prefix must be "GC#"
    /// - Characters after prefix must be digits
    /// </summary>
    /// <returns>True if valid, False otherwise</returns>
    public bool ValidateShipmentCode()
    {
        // Check if ShipmentCode is null or empty
        if (string.IsNullOrEmpty(ShipmentCode))
            return false;

        // Check length (must be exactly 7)
        if (ShipmentCode.Length != 7)
            return false;

        // Check prefix (must be "GC#")
        if (!ShipmentCode.StartsWith("GC#"))
            return false;

        // Check if characters after prefix are digits
        string digitsAfterPrefix = ShipmentCode.Substring(3);
        foreach (char c in digitsAfterPrefix)
        {
            if (!char.IsDigit(c))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Calculates the total shipping cost
    /// Formula: TotalCost = (Weight × RatePerKg) + √StorageDays
    /// </summary>
    /// <returns>Total cost rounded to 2 decimal places</returns>
    public double CalculateTotalCost()
    {
        // Get rate per kg based on transport mode
        double ratePerKg = GetRatePerKg(TransportMode);

        // Calculate total cost: (Weight × RatePerKg) + √StorageDays
        double totalCost = (Weight * ratePerKg) + Math.Sqrt(StorageDays);

        // Round to 2 decimal places
        return Math.Round(totalCost, 2);
    }

    /// <summary>
    /// Gets the rate per kg based on transport mode
    /// Transport modes are case-sensitive
    /// </summary>
    /// <param name="mode">Transport mode (Sea, Air, Land)</param>
    /// <returns>Rate per kg in USD</returns>
    private double GetRatePerKg(string mode)
    {
        return mode switch
        {
            "Sea" => 15.00,
            "Air" => 50.00,
            "Land" => 25.00,
            _ => 0.00 // Invalid mode
        };
    }
}
