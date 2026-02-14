namespace UniversityCourseRegistration;

public abstract class Person
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }

    protected Person(string id, string name, int age)
    {
        Id = id;
        Name = name;
        Age = age;
    }
}

public class Student : Person
{
    private double _gpa;
    public double GPA
    {
        get => _gpa;
        set
        {
            if (value < 0.0 || value > 4.0)
                throw new InvalidGPAException(value);
            _gpa = value;
        }
    }

    public string Major { get; set; }
    public DateTime EnrollmentDate { get; set; }

    public Student(string id, string name, int age, double gpa, string major)
        : base(id, name, age)
    {
        GPA = gpa;
        Major = major;
        EnrollmentDate = DateTime.Now;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Student: {Name} (ID: {Id})");
        Console.WriteLine($"  GPA: {GPA:F2}, Major: {Major}, Age: {Age}");
        Console.WriteLine($"  Enrolled: {EnrollmentDate:yyyy-MM-dd HH:mm:ss}");
    }
}
