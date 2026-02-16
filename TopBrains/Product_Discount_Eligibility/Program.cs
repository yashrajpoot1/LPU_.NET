using System;
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public bool IsPremiumCustomer { get; set; }
}

public class DiscountEngine{
    public void CheckDiscount(Product product, string scheme, Predicate<Product>rule){
        bool eligible = rule(product);
        Console.WriteLine("========= DISCOUNT ELIGIBILITY =========");
        Console.WriteLine("Product  : " + product.Name);
        Console.WriteLine("Scheme   : " + scheme);
        Console.WriteLine("Eligible : " + eligible);
        Console.WriteLine("-------------------------------------");
        Console.WriteLine();
    }
}
public class Solution
{
    public static void Main()
    {
        // STEP 1: Create product object (hardcoded dataset)
        Product product = new Product
        {
            ProductId = 901,
            Name = "Laptop",
            Price = 60000,
            Quantity = 12,
            IsPremiumCustomer = true
        };

        Predicate<Product> festivalRule = p => p.Price >=5000;
        Predicate<Product> bulkRule = p => p.Quantity >=10;
        Predicate<Product> premiumRule = p => p.IsPremiumCustomer;

        DiscountEngine obj = new DiscountEngine();

        obj.CheckDiscount(product, "Festival", festivalRule);
        obj.CheckDiscount(product, "Bulk", bulkRule);
        obj.CheckDiscount(product, "Premium", premiumRule);

    }
}
