using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter first number: ");
        int first = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter second number: ");
        int sec = Convert.ToInt32(Console.ReadLine());

        while(sec != 0)
        {
            int temp = sec;
            sec = first%sec;
            first = temp;
        }

        Console.WriteLine("GCD: " + first);
    }
}