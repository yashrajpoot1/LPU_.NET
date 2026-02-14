namespace BookStoreApp;

/// <summary>
/// Book class with encapsulated properties and validation
/// </summary>
public class Book
{
    private int _price;
    private int _stock;

    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }

    /// <summary>
    /// Price property with validation to prevent negative values
    /// </summary>
    public int Price
    {
        get => _price;
        set
        {
            if (value < 0)
                throw new InvalidBookDataException("Price cannot be negative");
            _price = value;
        }
    }

    /// <summary>
    /// Stock property with validation to prevent negative values
    /// </summary>
    public int Stock
    {
        get => _stock;
        set
        {
            if (value < 0)
                throw new InvalidBookDataException("Stock cannot be negative");
            _stock = value;
        }
    }

    public Book(string id, string title, string author, int price, int stock)
    {
        Id = id;
        Title = title;
        Author = author;
        Price = price; // Uses property setter for validation
        Stock = stock; // Uses property setter for validation
    }
}
