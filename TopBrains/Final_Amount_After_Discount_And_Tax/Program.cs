using System;

public class Solution
{
    public decimal CalculateFinalAmount(decimal price, decimal discountPercentage, decimal taxPercentage)
    {
        decimal discountedPrice = price - (price * discountPercentage / 100);
        decimal finalAmount = discountedPrice + (discountedPrice * taxPercentage / 100);
        return finalAmount;
    }
}
