using System;
using System.Collections.Generic;

class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int Marks { get; set; }
}

class StudentComparer : IComparer<Student>
{
    public int Compare(Student x, Student y)
    {
        // 1. Highest Marks first
        int marksCompare = y.Marks.CompareTo(x.Marks);
        if (marksCompare != 0)
            return marksCompare;

        // 2. If Marks equal → Youngest Age first
        return x.Age.CompareTo(y.Age);
    }
}

class Program
{
    static void Main()
    {
        List<Student> students = new List<Student>
        {
            new Student { Name = "Amit", Age = 20, Marks = 90 },
            new Student { Name = "Riya", Age = 18, Marks = 90 },
            new Student { Name = "Karan", Age = 21, Marks = 85 }
        };

        students.Sort(new StudentComparer());

        foreach (var s in students)
        {
            Console.WriteLine($"{s.Name} {s.Age} {s.Marks}");
        }
    }
}
