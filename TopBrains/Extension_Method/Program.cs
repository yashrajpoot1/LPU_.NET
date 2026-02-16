using System;
using System.Collections.Generic;

static class Extensions
{
    public static string[] DistinctById(this string[] items)
    {
        if (items == null || items.Length == 0)
            return Array.Empty<string>();

        HashSet<string> seenIds = new HashSet<string>();
        List<string> result = new List<string>();

        foreach (var item in items)
        {
            var parts = item.Split(':');
            if (parts.Length != 2) continue;

            string id = parts[0];
            string name = parts[1];

            if (seenIds.Add(id))   // true only for first occurrence
            {
                result.Add(name);
            }
        }

        return result.ToArray();
    }
}

class Program
{
    static void Main()
    {
        string[] items =
        {
            "1:Arti",
            "2:Rahul",
            "1:Neha",
            "3:Aman",
            "2:Kiran"
        };

        string[] result = items.DistinctById();

        Console.WriteLine(string.Join(", ", result));
    }
}

