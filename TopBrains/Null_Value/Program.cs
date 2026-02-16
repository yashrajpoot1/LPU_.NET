using System;

class Program
{
    static void Main()
    {
        double?[] values = {1.5, null, 2.5, 3.0};
        double? average = ComputeAverage(values);

        if (average.HasValue)
        {
            Console.WriteLine(average.Value);
        }
        else
        {
            Console.WriteLine("null");
        }
    }

    static double? ComputeAverage(double?[] values)
    {
        if(values == null || values.Length == 0)
        {
            return null;
        }

        double sum = 0;
        int count = 0;

        foreach(var v in values)
        {
            if (v.HasValue)
            {
                sum += v.Value;
                count++;
            }
        }

        if(count == 0)
        {
            return null;
        }

        double avg = sum/count;
        return Math.Round(avg, 2, MidpointRounding.AwayFromZero);
    }
}