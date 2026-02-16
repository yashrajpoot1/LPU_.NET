using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the size of the array: ");
        int size = Convert.ToInt32(Console.ReadLine());

        int[] arr = new int[size];
        Console.WriteLine("Enter elements of array: ");
        for(int i = 0; i<size; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
        }
        int sum = 0;
        for(int i = 0; i<size; i++)
        {
            // sum += arr[i];
            if(arr[i] == 0)
            {
                break;
            }
            else if(arr[i] < 0)
            {
                continue;
            }
            else
            {
                sum += arr[i];
            }
        }
        Console.WriteLine("Sum: " + sum); 
    }


    
}