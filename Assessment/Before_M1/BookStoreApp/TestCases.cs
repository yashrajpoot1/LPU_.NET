namespace BookStoreApp;

/// <summary>
/// Test cases for Book Store Application
/// </summary>
public class TestCases
{
    public static void RunAllTests()
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║          Book Store Application - Test Suite          ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

        int passed = 0;
        int failed = 0;

        // Test 1: Basic book creation
        Console.WriteLine("========== TEST 1: Basic Book Creation ==========");
        try
        {
            Book book1 = new Book("BK01", "JavaBook", "Author", 750, 20);
            Console.WriteLine($"✓ Book created: {book1.Id} {book1.Title} {book1.Price} {book1.Stock}");
            passed++;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Failed: {ex.Message}");
            failed++;
        }

        // Test 2: Display book details
        Console.WriteLine("\n========== TEST 2: Display Book Details ==========");
        try
        {
            Book book2 = new Book("BK01", "JavaBook", "Author", 750, 20);
            BookUtility util2 = new BookUtility(book2);
            util2.GetBookDetails();
            Console.WriteLine("✓ Test passed");
            passed++;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Failed: {ex.Message}");
            failed++;
        }

        // Test 3: Update price
        Console.WriteLine("\n========== TEST 3: Update Book Price ==========");
        try
        {
            Book book3 = new Book("BK01", "JavaBook", "Author", 750, 20);
            BookUtility util3 = new BookUtility(book3);
            util3.UpdateBookPrice(800);
            if (book3.Price == 800)
            {
                Console.WriteLine("✓ Price updated successfully");
                passed++;
            }
            else
            {
                Console.WriteLine("✗ Price not updated correctly");
                failed++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Failed: {ex.Message}");
            failed++;
        }

        // Test 4: Update stock
        Console.WriteLine("\n========== TEST 4: Update Book Stock ==========");
        try
        {
            Book book4 = new Book("BK01", "JavaBook", "Author", 750, 20);
            BookUtility util4 = new BookUtility(book4);
            util4.UpdateBookStock(15);
            if (book4.Stock == 15)
            {
                Console.WriteLine("✓ Stock updated successfully");
                passed++;
            }
            else
            {
                Console.WriteLine("✗ Stock not updated correctly");
                failed++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Failed: {ex.Message}");
            failed++;
        }

        // Test 5: Negative price validation
        Console.WriteLine("\n========== TEST 5: Negative Price Validation ==========");
        try
        {
            Book book5 = new Book("BK01", "JavaBook", "Author", -100, 20);
            Console.WriteLine("✗ Should have thrown exception for negative price");
            failed++;
        }
        catch (InvalidBookDataException ex)
        {
            Console.WriteLine($"✓ Exception caught correctly: {ex.Message}");
            passed++;
        }

        // Test 6: Negative stock validation
        Console.WriteLine("\n========== TEST 6: Negative Stock Validation ==========");
        try
        {
            Book book6 = new Book("BK01", "JavaBook", "Author", 750, -10);
            Console.WriteLine("✗ Should have thrown exception for negative stock");
            failed++;
        }
        catch (InvalidBookDataException ex)
        {
            Console.WriteLine($"✓ Exception caught correctly: {ex.Message}");
            passed++;
        }

        // Test 7: Update to negative price
        Console.WriteLine("\n========== TEST 7: Update to Negative Price ==========");
        try
        {
            Book book7 = new Book("BK01", "JavaBook", "Author", 750, 20);
            BookUtility util7 = new BookUtility(book7);
            util7.UpdateBookPrice(-50);
            Console.WriteLine("✗ Should have thrown exception");
            failed++;
        }
        catch (InvalidBookDataException ex)
        {
            Console.WriteLine($"✓ Exception caught correctly: {ex.Message}");
            passed++;
        }

        // Test 8: Apply discount
        Console.WriteLine("\n========== TEST 8: Apply Discount ==========");
        try
        {
            Book book8 = new Book("BK01", "JavaBook", "Author", 1000, 20);
            BookUtility util8 = new BookUtility(book8);
            util8.ApplyDiscount(10); // 10% discount
            if (book8.Price == 900)
            {
                Console.WriteLine("✓ Discount applied correctly");
                passed++;
            }
            else
            {
                Console.WriteLine($"✗ Expected 900, got {book8.Price}");
                failed++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Failed: {ex.Message}");
            failed++;
        }

        // Test 9: Invalid discount percentage
        Console.WriteLine("\n========== TEST 9: Invalid Discount Percentage ==========");
        try
        {
            Book book9 = new Book("BK01", "JavaBook", "Author", 1000, 20);
            BookUtility util9 = new BookUtility(book9);
            util9.ApplyDiscount(150); // Invalid: > 100%
            Console.WriteLine("✗ Should have thrown exception");
            failed++;
        }
        catch (InvalidBookDataException ex)
        {
            Console.WriteLine($"✓ Exception caught correctly: {ex.Message}");
            passed++;
        }

        // Test 10: Complete workflow (Sample test case)
        Console.WriteLine("\n========== TEST 10: Complete Workflow ==========");
        try
        {
            Book book10 = new Book("BK01", "JavaBook", "Author", 750, 20);
            BookUtility util10 = new BookUtility(book10);
            
            Console.WriteLine("Initial:");
            util10.GetBookDetails();
            
            util10.UpdateBookPrice(800);
            util10.GetBookDetails();
            
            util10.UpdateBookStock(15);
            util10.GetBookDetails();
            
            Console.WriteLine("✓ Complete workflow executed successfully");
            passed++;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Failed: {ex.Message}");
            failed++;
        }

        // Summary
        Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                    TEST SUMMARY                        ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.WriteLine($"\nTotal Tests: {passed + failed}");
        Console.WriteLine($"Passed: {passed} ✓");
        Console.WriteLine($"Failed: {failed} ✗");
        Console.WriteLine($"Success Rate: {(double)passed / (passed + failed) * 100:F1}%");
        Console.WriteLine("\n════════════════════════════════════════════════════════\n");
    }
}
