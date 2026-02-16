using System;

class Program
{
    static int DigitSum(int n)
    {
        int sum = 0;
        while (n > 0)
        {
            sum += n % 10;
            n /= 10;
        }
        return sum;
    }

    static bool IsPrime(int n)
    {
        if (n <= 1) return false;
        for (int i = 2; i * i <= n; i++)
            if (n % i == 0)
                return false;
        return true;
    }

    static void Main()
    {
        int m = 20;
        int n = 30;
        int count = 0;

        for (int x = m; x <= n; x++)
        {
            if (!IsPrime(x))
            {
                int s1 = DigitSum(x);
                int s2 = DigitSum(x * x);

                if (s2 == s1 * s1)
                    count++;
            }
        }

        Console.WriteLine(count);
    }
}
