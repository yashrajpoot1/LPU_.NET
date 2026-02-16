using System;

class Program
{
    // USING OUT
    // static void SwapValues(out int a, out int b, int x, int y)
    // {
    //     a = y;
    //     b = x;
    // }
    // static void Main()
    // {
    //     int x = 10;
    //     int y = 20;

    //     int a, b;

    //     SwapValues(out a, out b, x,y);
    //     Console.WriteLine(a+ " " + b);
    // }

    // USING REF
    static void SwapMe(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }

    static void Main()
    {
        int x = 10;
        int y = 20;
        SwapMe(ref x, ref y);
        Console.WriteLine(x + " " + y);
    }
}