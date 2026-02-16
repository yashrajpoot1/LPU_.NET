using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter first word: ");
        string first = Console.ReadLine();

        Console.WriteLine("Enter second word: ");
        string sec = Console.ReadLine();

        string res = ""'

        // --- REMOVING COMMON CONSTANTS
        char[] first1 = first.ToCharArray();
        char[] sec1 = sec.ToCharArray();

        for(int i = 0; i<first1.Length; i++)
        {
            for(int j = 0; j<sec1.Length; j++)
            {
                if(first1[i] == sec1[j])
                {
                    first1.Remove();
                }
            }
        }
    }
}