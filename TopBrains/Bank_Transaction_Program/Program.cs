using System;

class Program
{
    static void Main()
    {
        int initialBalance = 0;
        Console.WriteLine("Enter transaction size:");
        int transaction_size = Convert.ToInt32(Console.ReadLine());

        int[] transactions = new int[transaction_size];

        for(int i = 0; i < transaction_size; i++)
        {
            transactions[i] = Convert.ToInt32(Console.ReadLine());
        }
        
        for(int i = 0; i < transaction_size; i++)
        {
            int t = transactions[i];

            if(t > 0)
            {
                initialBalance += t;
            }
            else 
            {
                if(initialBalance+t >= 0)
                {
                    initialBalance += t;
                }
            }
        }
        Console.WriteLine("Final Balance: " + initialBalance);
    }
}