namespace BookStoreApp;

/// <summary>
/// Demo version with interactive menu and additional features
/// Rename this to Program.cs to use the interactive version
/// </summary>
class ProgramDemo
{
    static void MainDemo(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║          Book Store Management System                 ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

        try
        {
            // Initialize book
            Console.WriteLine("Enter book details (ID Title Price Stock):");
            Console.Write("> ");
            string[] bookData = Console.ReadLine()!.Split(' ');
            
            string bookId = bookData[0];
            string title = bookData[1];
            int price = int.Parse(bookData[2]);
            int stock = int.Parse(bookData[3]);

            Book book = new Book(bookId, title, "Author", price, stock);
            BookUtility bookUtility = new BookUtility(book);

            Console.WriteLine("\n✓ Book created successfully!\n");

            // Menu loop
            while (true)
            {
                DisplayMenu();
                Console.Write("> ");
                
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.\n");
                    continue;
                }

                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        bookUtility.GetBookDetails();
                        break;

                    case 2:
                        Console.Write("Enter new price: ");
                        if (int.TryParse(Console.ReadLine(), out int newPrice))
                        {
                            bookUtility.UpdateBookPrice(newPrice);
                        }
                        else
                        {
                            Console.WriteLine("Invalid price");
                        }
                        break;

                    case 3:
                        Console.Write("Enter new stock: ");
                        if (int.TryParse(Console.ReadLine(), out int newStock))
                        {
                            bookUtility.UpdateBookStock(newStock);
                        }
                        else
                        {
                            Console.WriteLine("Invalid stock");
                        }
                        break;

                    case 4:
                        Console.WriteLine("Thank You");
                        Console.WriteLine("\n════════════════════════════════════════════════════════");
                        return;

                    case 5:
                        // Optional: Apply discount
                        Console.Write("Enter discount percentage (0-100): ");
                        if (double.TryParse(Console.ReadLine(), out double discount))
                        {
                            bookUtility.ApplyDiscount(discount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid discount percentage");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select 1-5.");
                        break;
                }

                Console.WriteLine();
            }
        }
        catch (InvalidBookDataException ex)
        {
            Console.WriteLine($"\n✗ Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Unexpected error: {ex.Message}");
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("════════════════════════════════════════════════════════");
        Console.WriteLine("1. Display book details");
        Console.WriteLine("2. Update book price");
        Console.WriteLine("3. Update book stock");
        Console.WriteLine("4. Exit");
        Console.WriteLine("5. Apply discount (Optional)");
        Console.WriteLine("════════════════════════════════════════════════════════");
        Console.Write("Enter your choice: ");
    }
}
