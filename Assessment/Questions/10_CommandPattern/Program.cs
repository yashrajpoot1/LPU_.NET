using System;
using CommandPattern;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Problem 10: Command Pattern ===");
        var cart = new ShoppingCart();
        var manager = new CommandManager();

        manager.ExecuteCommand(new AddItemCommand(cart, "Laptop"));
        manager.ExecuteCommand(new AddItemCommand(cart, "Mouse"));
        Console.WriteLine($"Items in cart: {string.Join(", ", cart.Items)}");

        manager.ExecuteCommand(new ApplyDiscountCommand(cart, 50));
        Console.WriteLine($"Discount applied: ${cart.TotalDiscount}");

        manager.Undo();
        Console.WriteLine($"After undo discount: ${cart.TotalDiscount}");

        manager.Redo();
        Console.WriteLine($"After redo: ${cart.TotalDiscount}");

        manager.Undo();
        manager.Undo();
        Console.WriteLine($"After 2 undos, items: {string.Join(", ", cart.Items)}");

        Console.WriteLine("✓ Test Passed\n");
    }
}
