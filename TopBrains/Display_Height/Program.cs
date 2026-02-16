using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter height in cm: ");
        int height = Convert.ToInt32(Console.ReadLine());

        if(height < 150)
        {
            Console.WriteLine("Short");
        }
        else if(height >= 150 && height < 180)
        {
            Console.WriteLine("Average");
        }
        else
        {
            Console.WriteLine("Tall");
        }
    }
}