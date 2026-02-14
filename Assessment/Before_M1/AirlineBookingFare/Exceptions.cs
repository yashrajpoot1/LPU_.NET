namespace AirlineBookingFare;

public class BookingException : Exception
{
    public BookingException(string message) : base(message) { }
}

public class SeatAlreadyBookedException : BookingException
{
    public string SeatNumber { get; }

    public SeatAlreadyBookedException(string seatNumber)
        : base($"Seat {seatNumber} is already booked")
    {
        SeatNumber = seatNumber;
    }
}

public class InvalidFareException : BookingException
{
    public decimal Fare { get; }

    public InvalidFareException(decimal fare)
        : base($"Invalid fare amount: ${fare:F2}. Fare must be positive")
    {
        Fare = fare;
    }
}
