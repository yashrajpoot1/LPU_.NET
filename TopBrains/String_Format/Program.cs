using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public record Student(string Name, int Score);
public class Program
{
    public static string GetStudentsJson(string[] items, int minScore)
    {
        var students = new List<Student>(items.Length);

        foreach (var item in items)
        {
            var parts = item.Split(':');
            if (parts.Length == 2 && int.TryParse(parts[1], out int score))
            {
                if (score >= minScore)
                    students.Add(new Student(parts[0], score));
            }
        }

        var result = students
            .OrderByDescending(s => s.Score)
            .ThenBy(s => s.Name)
            .ToList();

        return JsonSerializer.Serialize(result);
    }

    static void Main()
    {
        string[] items =
        {
            "Arti:85",
            "Rahul:92",
            "Neha:78",
            "Aman:92"
        };

        int minScore = 80;

        string json = Program.GetStudentsJson(items, minScore);

        Console.WriteLine(json);
    }

}