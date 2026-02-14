namespace TrafficViolationMonitoring;

public abstract class Vehicle
{
    public string LicensePlate { get; set; }
    public string Model { get; set; }
    public string Owner { get; set; }

    protected Vehicle(string licensePlate, string model, string owner)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
            throw new InvalidVehicleException("License plate cannot be empty");

        LicensePlate = licensePlate;
        Model = model;
        Owner = owner;
    }

    public abstract string GetVehicleType();
}

public class Car : Vehicle
{
    public Car(string licensePlate, string model, string owner)
        : base(licensePlate, model, owner) { }

    public override string GetVehicleType() => "Car";
}

public class Truck : Vehicle
{
    public Truck(string licensePlate, string model, string owner)
        : base(licensePlate, model, owner) { }

    public override string GetVehicleType() => "Truck";
}

public class Bike : Vehicle
{
    public Bike(string licensePlate, string model, string owner)
        : base(licensePlate, model, owner) { }

    public override string GetVehicleType() => "Bike";
}

public class Violation
{
    public string ViolationId { get; set; }
    public Vehicle Vehicle { get; set; }
    public decimal Penalty { get; set; }
    public string Description { get; set; }
    public DateTime Timestamp { get; set; }

    public Violation(string violationId, Vehicle vehicle, decimal penalty, string description)
    {
        ViolationId = violationId;
        Vehicle = vehicle;
        Penalty = penalty;
        Description = description;
        Timestamp = DateTime.Now;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Violation {ViolationId}: {Description}");
        Console.WriteLine($"  Vehicle: {Vehicle.GetVehicleType()} - {Vehicle.LicensePlate} ({Vehicle.Owner})");
        Console.WriteLine($"  Penalty: ${Penalty:F2}, Time: {Timestamp:yyyy-MM-dd HH:mm:ss}");
    }
}
