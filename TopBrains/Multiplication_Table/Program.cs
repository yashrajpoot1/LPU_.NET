using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the number for multiplication: ");
        int n = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Upto: ");
        int upto = Convert.ToInt32(Console.ReadLine());
        int[] row;
        Console.Write("[");
        for(int i = 1; i<=upto; i++)
        {
            Console.Write(n*i +",");
        }
        Console.Write("]");
    }
}