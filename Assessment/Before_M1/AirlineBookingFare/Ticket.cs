namespace AirlineBookingFare;

public abstract class Ticket
{
    public string TicketId { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public decimal BaseFare { get; set; }
    public string SeatNumber { get; set; }
    public decimal TotalFare { get; protected set; }

    protected Ticket(string ticketId, string origin, string destination, decimal baseFare, string seatNumber)
    {
        if (baseFare <= 0)
            throw new InvalidFareException(baseFare);

        TicketId = ticketId;
        Origin = origin;
        Destination = destination;
        BaseFare = baseFare;
        SeatNumber = seatNumber;
    }

    public abstract void CalculateFare();
    public abstract string GetClass();
    public abstract void DisplayInfo();
}

public class Economy : Ticket
{
    public int BaggageCount { get; set; }

    public Economy(string ticketId, string origin, string destination, decimal baseFare, 
                   string seatNumber, int baggageCount)
        : base(ticketId, origin, destination, baseFare, seatNumber)
    {
        BaggageCount = baggageCount;
        CalculateFare();
    }

    public override void CalculateFare()
    {
        TotalFare = BaseFare + (BaggageCount > 1 ? (BaggageCount - 1) * 50 : 0);
    }

    public override string GetClass() => "Economy";

    public override void DisplayInfo()
    {
        Console.WriteLine($"[{GetClass()}] {TicketId}: {Origin} → {Destination}");
        Console.WriteLine($"  Seat: {SeatNumber}, Baggage: {BaggageCount}");
        Console.WriteLine($"  Base: ${BaseFare:F2}, Total: ${TotalFare:F2}");
    }
}

public class Business : Ticket
{
    public bool HasLounge { get; set; }
    public int MealPreference { get; set; }

    public Business(string ticketId, string origin, string destination, decimal baseFare,
                   string seatNumber, bool hasLounge, int mealPreference)
        : base(ticketId, origin, destination, baseFare, seatNumber)
    {
        HasLounge = hasLounge;
        MealPreference = mealPreference;
        CalculateFare();
    }

    public override void CalculateFare()
    {
        TotalFare = BaseFare * 1.2m + (HasLounge ? 100 : 0);
    }

    public override string GetClass() => "Business";

    public override void DisplayInfo()
    {
        Console.WriteLine($"[{GetClass()}] {TicketId}: {Origin} → {Destination}");
        Console.WriteLine($"  Seat: {SeatNumber}, Lounge: {HasLounge}");
        Console.WriteLine($"  Base: ${BaseFare:F2}, Total: ${TotalFare:F2}");
    }
}

public class FirstClass : Ticket
{
    public bool HasSuite { get; set; }
    public bool HasChauffeur { get; set; }

    public FirstClass(string ticketId, string origin, string destination, decimal baseFare,
                     string seatNumber, bool hasSuite, bool hasChauffeur)
        : base(ticketId, origin, destination, baseFare, seatNumber)
    {
        HasSuite = hasSuite;
        HasChauffeur = hasChauffeur;
        CalculateFare();
    }

    public override void CalculateFare()
    {
        TotalFare = BaseFare * 1.5m + (HasSuite ? 500 : 0) + (HasChauffeur ? 300 : 0);
    }

    public override string GetClass() => "First Class";

    public override void DisplayInfo()
    {
        Console.WriteLine($"[{GetClass()}] {TicketId}: {Origin} → {Destination}");
        Console.WriteLine($"  Seat: {SeatNumber}, Suite: {HasSuite}, Chauffeur: {HasChauffeur}");
        Console.WriteLine($"  Base: ${BaseFare:F2}, Total: ${TotalFare:F2}");
    }
}
