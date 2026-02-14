namespace UniversityCourseRegistration;

public class RegistrationManager
{
    private readonly SortedDictionary<double, List<Student>> _registrations = new(Comparer<double>.Create((a, b) => b.CompareTo(a)));
    private readonly HashSet<string> _enrolledStudents = new();
    private readonly int _maxSeats;

    public RegistrationManager(int maxSeats)
    {
        _maxSeats = maxSeats;
    }

    public void RegisterStudent(Student student)
    {
        if (_enrolledStudents.Count >= _maxSeats)
            throw new CourseFullException(_maxSeats);

        if (_enrolledStudents.Contains(student.Id))
            throw new DuplicateEnrollmentException(student.Id);

        if (!_registrations.ContainsKey(student.GPA))
            _registrations[student.GPA] = new List<Student>();

        _registrations[student.GPA].Add(student);
        _enrolledStudents.Add(student.Id);

        Console.WriteLine($"✓ Registered: {student.Name} (GPA: {student.GPA:F2})");
    }

    public void DisplayRegistrations()
    {
        Console.WriteLine("\n========== REGISTRATIONS (By GPA Priority) ==========");
        foreach (var kvp in _registrations)
        {
            Console.WriteLine($"\n--- GPA: {kvp.Key:F2} ({kvp.Value.Count} students) ---");
            foreach (var student in kvp.Value)
            {
                student.DisplayInfo();
            }
        }
    }

    public int GetTotalStudents() => _enrolledStudents.Count;
}

public class RegistrationException : Exception
{
    public RegistrationException(string message) : base(message) { }
}

public class CourseFullException : RegistrationException
{
    public CourseFullException(int maxSeats)
        : base($"Course is full. Maximum seats: {maxSeats}") { }
}

public class InvalidGPAException : RegistrationException
{
    public InvalidGPAException(double gpa)
        : base($"Invalid GPA: {gpa}. Must be between 0.0 and 4.0") { }
}

public class DuplicateEnrollmentException : RegistrationException
{
    public DuplicateEnrollmentException(string studentId)
        : base($"Student {studentId} is already enrolled") { }
}
