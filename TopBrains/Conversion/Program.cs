using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter feet: ");
        double feet = Convert.ToDouble(Console.ReadLine());
        double cm = feet * 30.48;
        Console.WriteLine(feet + " feet in cm = " + cm.ToString("F2"));
    }
}