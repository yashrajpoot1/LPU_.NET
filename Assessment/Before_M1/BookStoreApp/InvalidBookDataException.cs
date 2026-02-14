namespace BookStoreApp;

/// <summary>
/// Custom exception for invalid book data
/// </summary>
public class InvalidBookDataException : Exception
{
    public InvalidBookDataException(string message) : base(message)
    {
    }

    public InvalidBookDataException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
