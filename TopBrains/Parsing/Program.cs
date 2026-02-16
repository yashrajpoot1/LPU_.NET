using System;

class Program
{
    static int SumValidIntegers(string[] elements)
    {
        int sum = 0;

        foreach(string element in elements)
        {
            if(int.TryParse(element, out int value))
            {
                sum += value;
            }
        }
        return sum;
    }

    static void Main()
    {
        string[] elements = {"10", "abc", "20", "99999999999", "-5"};

        int result = SumValidIntegers(elements);
        Console.WriteLine(result);
    }
}