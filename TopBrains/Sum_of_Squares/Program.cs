using System;

class Program
{
    static void Main()
    {
        // Sample Inputs
        int num1 = 6;
        int num2 = 8;

        // Process and display result
        PrintResult(num1, num2);
    }

    // Function to calculate sum of squares
    static int SumOfSquares(int a, int b)
    {
        return ((a*a) + (b*b));
    }

    // Function to handle condition and output
    static void PrintResult(int a, int b)
    {
       int sum = SumOfSquares(a,b);

       if(sum > 100)
       {
        // Console.WriteLine($"Numbers: {a}, {b}");
        Console.WriteLine($"{a}, {b}");
       }
       else{
        // Console.WriteLine($"Sum of Squares: {sum}");
        Console.WriteLine($"{sum}");
       }
    }
}
