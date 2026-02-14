namespace UniversityCourseRegistration;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║  University Course Registration Priority System      ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

        var registrationSystem = new RegistrationManager(maxSeats: 3);

        try
        {
            registrationSystem.RegisterStudent(new Student("S001", "Alice Johnson", 22, 3.9, "Computer Science"));
            registrationSystem.RegisterStudent(new Student("S002", "Bob Smith", 21, 3.5, "Engineering"));
            registrationSystem.RegisterStudent(new Student("S003", "Charlie Brown", 23, 3.8, "Mathematics"));
            registrationSystem.RegisterStudent(new Student("S004", "Diana Prince", 20, 3.7, "Physics"));

            registrationSystem.DisplayRegistrations();

            // Test course full
            Console.WriteLine("\n========== TESTING COURSE FULL ==========");
            try
            {
                registrationSystem.RegisterStudent(new Student("S005", "Eve Adams", 21, 3.6, "Chemistry"));
            }
            catch (CourseFullException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            // Test invalid GPA
            Console.WriteLine("\n========== TESTING INVALID GPA ==========");
            try
            {
                registrationSystem.RegisterStudent(new Student("S006", "Frank Miller", 22, 5.0, "Biology"));
            }
            catch (InvalidGPAException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            // Test duplicate enrollment
            Console.WriteLine("\n========== TESTING DUPLICATE ENROLLMENT ==========");
            try
            {
                registrationSystem.RegisterStudent(new Student("S001", "Alice Duplicate", 22, 3.9, "CS"));
            }
            catch (DuplicateEnrollmentException ex)
            {
                Console.WriteLine($"✗ Exception: {ex.Message}");
            }

            Console.WriteLine($"\n✅ Total Students Registered: {registrationSystem.GetTotalStudents()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
