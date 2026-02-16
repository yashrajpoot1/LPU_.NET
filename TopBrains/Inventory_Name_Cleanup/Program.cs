using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        Console.Write("Enter any string: ");
        string name = Console.ReadLine();

        string noDupli = "";

        for(int i = 0; i<name.Length; i++)
        {
            if (!noDupli.Contains(name[i])) // if result(which is empty string doesn't contain any character which is in name, it will extract it and will push into result)
            {
                noDupli += name[i];
            }
        }
        Console.WriteLine("Without duplicates: " + noDupli);

        string noWhiteSpace = noDupli.Trim();
        Console.WriteLine("Without whitespace: " + noWhiteSpace);

        TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
        Console.WriteLine("In Title Case:" +ti.ToTitleCase(noWhiteSpace));
    }
}