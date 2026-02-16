using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter radius: ");
        double rad = Convert.ToDouble(Console.ReadLine());

        double area = Math.PI * rad * rad;

        Console.WriteLine("Area of circle: " + Math.Round(area, 2, MidpointRounding.AwayFromZero));
    }
}