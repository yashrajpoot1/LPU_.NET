using System;

class Program
{
    static int SumOfSalary(Dictionary<int, int> empSalary, int[] ids)
    {
        int sum = 0;
        foreach(int id in ids)
        {
            if (empSalary.ContainsKey(id))
            {
                sum += empSalary[id];
            }
        }
        return sum;
    }

    static void Main()
    {
        Dictionary<int, int> empSalary = new Dictionary<int, int>()
        {
            {1, 20000},
            {4, 40000},
            {5, 15000}
        };

        int[] ids = {1, 4, 5};
        int result = SumOfSalary(empSalary, ids);
        Console.WriteLine("Sum of salary: " + result);
    }
}