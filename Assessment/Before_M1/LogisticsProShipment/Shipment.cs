namespace LogisticsProShipment;

/// <summary>
/// Base class representing a shipment
/// </summary>
public class Shipment
{
    public string ShipmentCode { get; set; }
    public string TransportMode { get; set; }
    public double Weight { get; set; }
    public int StorageDays { get; set; }

    public Shipment()
    {
        ShipmentCode = string.Empty;
        TransportMode = string.Empty;
        Weight = 0.0;
        StorageDays = 0;
    }

    public Shipment(string shipmentCode, string transportMode, double weight, int storageDays)
    {
        ShipmentCode = shipmentCode;
        TransportMode = transportMode;
        Weight = weight;
        StorageDays = storageDays;
    }
}
