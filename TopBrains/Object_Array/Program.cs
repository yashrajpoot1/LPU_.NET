using System;

class Program
{
    static int SumOfIntegers(object[] arr)
    {
        int sum = 0;

        foreach (var item in arr)
        {
            if(item is int x)
            {
                sum += x;
            }
        }
        return sum;
    }
    static void Main()
    {
        object[] arr = {"hello", 25, "abc", "$", -10, 86};
        int result = SumOfIntegers(arr);
        Console.WriteLine("Sum of Integers is: " + result);
    }
}