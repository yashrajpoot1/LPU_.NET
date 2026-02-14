namespace BookStoreApp;

/// <summary>
/// BookUtility class containing methods to manage book operations
/// </summary>
public class BookUtility
{
    private Book _book;

    public BookUtility(Book book)
    {
        _book = book;
    }

    /// <summary>
    /// Displays book details in the required format
    /// </summary>
    public void GetBookDetails()
    {
        Console.WriteLine($"Details: {_book.Id} {_book.Title} {_book.Price} {_book.Stock}");
    }

    /// <summary>
    /// Updates the book price
    /// </summary>
    /// <param name="newPrice">New price value</param>
    public void UpdateBookPrice(int newPrice)
    {
        _book.Price = newPrice;
        Console.WriteLine($"Updated Price: {newPrice}");
    }

    /// <summary>
    /// Updates the book stock
    /// </summary>
    /// <param name="newStock">New stock value</param>
    public void UpdateBookStock(int newStock)
    {
        _book.Stock = newStock;
        Console.WriteLine($"Updated Stock: {newStock}");
    }

    /// <summary>
    /// Optional: Apply discount percentage to book price
    /// </summary>
    /// <param name="discountPercent">Discount percentage (0-100)</param>
    public void ApplyDiscount(double discountPercent)
    {
        if (discountPercent < 0 || discountPercent > 100)
            throw new InvalidBookDataException("Discount percentage must be between 0 and 100");

        int discountedPrice = (int)(_book.Price * (1 - discountPercent / 100));
        _book.Price = discountedPrice;
        Console.WriteLine($"Discount Applied: {discountPercent}% - New Price: {discountedPrice}");
    }
}
