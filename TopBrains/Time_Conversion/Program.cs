using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter time in seconds");
        int time = Convert.ToInt32(Console.ReadLine());

        int min = time/60;
        int sec = time%60;

        Console.WriteLine($"m:ss -> {min}:{sec:D2}"); //:D2 ensures that sec are always in 2 digit
    }
}