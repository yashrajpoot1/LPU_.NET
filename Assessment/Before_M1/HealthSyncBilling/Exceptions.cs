namespace HealthSyncBilling;

/// <summary>
/// Base exception for HealthSync Billing System
/// </summary>
public class HealthSyncException : Exception
{
    public HealthSyncException(string message) : base(message) { }
}

/// <summary>
/// Exception thrown when consultant ID validation fails
/// </summary>
public class InvalidConsultantIdException : HealthSyncException
{
    public string InvalidId { get; }

    public InvalidConsultantIdException(string invalidId)
        : base($"Invalid doctor id: {invalidId}. ID must be 6 characters, start with 'DR', and last 4 must be numeric.")
    {
        InvalidId = invalidId;
    }
}

/// <summary>
/// Exception thrown for invalid payout calculations
/// </summary>
public class InvalidPayoutException : HealthSyncException
{
    public InvalidPayoutException(string message) : base(message) { }
}
