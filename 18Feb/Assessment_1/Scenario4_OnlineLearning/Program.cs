using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineLearningPlatform
{
    // Custom Exceptions
    public class DuplicateEnrollmentException : Exception
    {
        public DuplicateEnrollmentException(string message) : base(message) { }
    }

    public class CourseCapacityExceededException : Exception
    {
        public CourseCapacityExceededException(string message) : base(message) { }
    }

    public class LateSubmissionException : Exception
    {
        public LateSubmissionException(string message) : base(message) { }
    }

    // Generic Repository Pattern
    public interface IRepository<T>
    {
        void Add(T item);
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Update(T item);
        void Delete(int id);
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        private List<T> _items = new List<T>();
        private Func<T, int> _idSelector;

        public Repository(Func<T, int> idSelector)
        {
            _idSelector = idSelector;
        }

        public void Add(T item) => _items.Add(item);
        public T GetById(int id) => _items.FirstOrDefault(item => _idSelector(item) == id);
        public IEnumerable<T> GetAll() => _items;
        public void Update(T item) { }
        public void Delete(int id) => _items.RemoveAll(item => _idSelector(item) == id);
    }

    // Entities
    public class Instructor : IComparable<Instructor>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }

        public Instructor(int id, string name, string email, string specialization)
        {
            Id = id;
            Name = name;
            Email = email;
            Specialization = specialization;
        }

        public int CompareTo(Instructor other)
        {
            return Name.CompareTo(other.Name);
        }
    }

    public class Course : IComparable<Course>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Instructor Instructor { get; set; }
        public int MaxCapacity { get; set; }
        public int CurrentEnrollment { get; set; }
        public decimal Rating { get; set; }

        public Course(int id, string name, Instructor instructor, int maxCapacity)
        {
            Id = id;
            Name = name;
            Instructor = instructor;
            MaxCapacity = maxCapacity;
            CurrentEnrollment = 0;
            Rating = 0;
        }

        public bool HasCapacity() => CurrentEnrollment < MaxCapacity;

        public int CompareTo(Course other)
        {
            return Rating.CompareTo(other.Rating);
        }
    }

    public class Student : IComparable<Student>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Student(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public int CompareTo(Student other)
        {
            return Name.CompareTo(other.Name);
        }
    }

    public class Enrollment
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public Enrollment(int id, Student student, Course course)
        {
            Id = id;
            Student = student;
            Course = course;
            EnrollmentDate = DateTime.Now;
        }
    }

    public class Assignment
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public List<Submission> Submissions { get; set; }

        public Assignment(int id, Course course, string title, DateTime deadline)
        {
            Id = id;
            Course = course;
            Title = title;
            Deadline = deadline;
            Submissions = new List<Submission>();
        }
    }

    public class Submission
    {
        public Student Student { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Content { get; set; }

        public Submission(Student student, string content)
        {
            Student = student;
            Content = content;
            SubmissionDate = DateTime.Now;
        }
    }

    class Program
    {
        static Repository<Course> courseRepo = new Repository<Course>(c => c.Id);
        static Repository<Student> studentRepo = new Repository<Student>(s => s.Id);
        static Repository<Instructor> instructorRepo = new Repository<Instructor>(i => i.Id);
        static Repository<Enrollment> enrollmentRepo = new Repository<Enrollment>(e => e.Id);
        static List<Assignment> assignments = new List<Assignment>();
        static int enrollmentIdCounter = 1;
        static int assignmentIdCounter = 1;

        static void Main(string[] args)
        {
            InitializeSampleData();

            while (true)
            {
                Console.WriteLine("\n╔════════════════════════════════════════╗");
                Console.WriteLine("║   ONLINE LEARNING PLATFORM             ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                Console.WriteLine("1. View Courses");
                Console.WriteLine("2. View Students");
                Console.WriteLine("3. View Instructors");
                Console.WriteLine("4. Enroll Student");
                Console.WriteLine("5. Create Assignment");
                Console.WriteLine("6. Submit Assignment");
                Console.WriteLine("7. View Enrollments");
                Console.WriteLine("8. LINQ Reports");
                Console.WriteLine("9. Exit");
                Console.Write("\nEnter choice: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": ViewCourses(); break;
                        case "2": ViewStudents(); break;
                        case "3": ViewInstructors(); break;
                        case "4": EnrollStudent(); break;
                        case "5": CreateAssignment(); break;
                        case "6": SubmitAssignment(); break;
                        case "7": ViewEnrollments(); break;
                        case "8": ShowLinqReports(); break;
                        case "9": return;
                        default: Console.WriteLine("Invalid choice!"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Error: {ex.Message}");
                }
            }
        }

        static void InitializeSampleData()
        {
            // Instructors
            instructorRepo.Add(new Instructor(1, "Prof. Rajesh Kumar", "rajesh@university.com", "Computer Science"));
            instructorRepo.Add(new Instructor(2, "Prof. Priya Sharma", "priya@university.com", "Data Science"));
            instructorRepo.Add(new Instructor(3, "Prof. Amit Patel", "amit@university.com", "Web Development"));

            // Courses
            var course1 = new Course(1, "C# Programming", instructorRepo.GetById(1), 60);
            course1.Rating = 4.5m;
            courseRepo.Add(course1);

            var course2 = new Course(2, "Data Structures", instructorRepo.GetById(1), 55);
            course2.Rating = 4.8m;
            courseRepo.Add(course2);

            var course3 = new Course(3, "Machine Learning", instructorRepo.GetById(2), 40);
            course3.Rating = 4.7m;
            courseRepo.Add(course3);

            var course4 = new Course(4, "Web Development", instructorRepo.GetById(3), 70);
            course4.Rating = 4.3m;
            courseRepo.Add(course4);

            // Students
            studentRepo.Add(new Student(1, "Rahul Verma", "rahul@student.com"));
            studentRepo.Add(new Student(2, "Anita Desai", "anita@student.com"));
            studentRepo.Add(new Student(3, "Vikram Singh", "vikram@student.com"));
            studentRepo.Add(new Student(4, "Sneha Patel", "sneha@student.com"));

            // Sample Enrollments
            var enrollment1 = new Enrollment(enrollmentIdCounter++, studentRepo.GetById(1), course1);
            course1.CurrentEnrollment++;
            enrollmentRepo.Add(enrollment1);

            var enrollment2 = new Enrollment(enrollmentIdCounter++, studentRepo.GetById(1), course2);
            course2.CurrentEnrollment++;
            enrollmentRepo.Add(enrollment2);

            var enrollment3 = new Enrollment(enrollmentIdCounter++, studentRepo.GetById(2), course1);
            course1.CurrentEnrollment++;
            enrollmentRepo.Add(enrollment3);
        }

        static void ViewCourses()
        {
            Console.WriteLine("\n--- Available Courses ---");
            Console.WriteLine($"{"ID",-5} {"Name",-25} {"Instructor",-25} {"Capacity",-15} {"Rating",-10}");
            Console.WriteLine(new string('-', 85));
            
            var sortedCourses = courseRepo.GetAll().OrderByDescending(c => c.Rating);
            foreach (var course in sortedCourses)
            {
                string capacity = $"{course.CurrentEnrollment}/{course.MaxCapacity}";
                Console.WriteLine($"{course.Id,-5} {course.Name,-25} {course.Instructor.Name,-25} {capacity,-15} {course.Rating,-10:F1}");
            }
        }

        static void ViewStudents()
        {
            Console.WriteLine("\n--- Students List ---");
            Console.WriteLine($"{"ID",-5} {"Name",-25} {"Email",-30}");
            Console.WriteLine(new string('-', 65));
            
            var sortedStudents = studentRepo.GetAll().OrderBy(s => s.Name);
            foreach (var student in sortedStudents)
            {
                Console.WriteLine($"{student.Id,-5} {student.Name,-25} {student.Email,-30}");
            }
        }

        static void ViewInstructors()
        {
            Console.WriteLine("\n--- Instructors List ---");
            Console.WriteLine($"{"ID",-5} {"Name",-25} {"Specialization",-25}");
            Console.WriteLine(new string('-', 60));
            
            var sortedInstructors = instructorRepo.GetAll().OrderBy(i => i.Name);
            foreach (var instructor in sortedInstructors)
            {
                Console.WriteLine($"{instructor.Id,-5} {instructor.Name,-25} {instructor.Specialization,-25}");
            }
        }

        static void EnrollStudent()
        {
            Console.Write("\nEnter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            var student = studentRepo.GetById(studentId);
            if (student == null)
            {
                Console.WriteLine("❌ Student not found!");
                return;
            }

            ViewCourses();
            Console.Write("\nEnter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            var course = courseRepo.GetById(courseId);
            if (course == null)
            {
                Console.WriteLine("❌ Course not found!");
                return;
            }

            // Check duplicate enrollment
            var existingEnrollment = enrollmentRepo.GetAll()
                .Any(e => e.Student.Id == studentId && e.Course.Id == courseId);

            if (existingEnrollment)
                throw new DuplicateEnrollmentException($"Student {student.Name} is already enrolled in {course.Name}");

            // Check capacity
            if (!course.HasCapacity())
                throw new CourseCapacityExceededException($"Course {course.Name} has reached maximum capacity");

            var enrollment = new Enrollment(enrollmentIdCounter++, student, course);
            enrollmentRepo.Add(enrollment);
            course.CurrentEnrollment++;

            Console.WriteLine($"✅ Student {student.Name} enrolled in {course.Name} successfully!");
        }

        static void CreateAssignment()
        {
            ViewCourses();
            Console.Write("\nEnter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            var course = courseRepo.GetById(courseId);
            if (course == null)
            {
                Console.WriteLine("❌ Course not found!");
                return;
            }

            Console.Write("Enter Assignment Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Deadline (dd-MM-yyyy): ");
            DateTime deadline = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", null);

            var assignment = new Assignment(assignmentIdCounter++, course, title, deadline);
            assignments.Add(assignment);

            Console.WriteLine("✅ Assignment created successfully!");
        }

        static void SubmitAssignment()
        {
            Console.Write("\nEnter Assignment ID: ");
            int assignmentId = int.Parse(Console.ReadLine());

            var assignment = assignments.FirstOrDefault(a => a.Id == assignmentId);
            if (assignment == null)
            {
                Console.WriteLine("❌ Assignment not found!");
                return;
            }

            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            var student = studentRepo.GetById(studentId);
            if (student == null)
            {
                Console.WriteLine("❌ Student not found!");
                return;
            }

            // Check if student is enrolled in the course
            var isEnrolled = enrollmentRepo.GetAll()
                .Any(e => e.Student.Id == studentId && e.Course.Id == assignment.Course.Id);

            if (!isEnrolled)
            {
                Console.WriteLine("❌ Student is not enrolled in this course!");
                return;
            }

            // Check deadline
            if (DateTime.Now > assignment.Deadline)
                throw new LateSubmissionException($"Assignment deadline ({assignment.Deadline:dd-MMM-yyyy}) has passed");

            Console.Write("Enter Submission Content: ");
            string content = Console.ReadLine();

            var submission = new Submission(student, content);
            assignment.Submissions.Add(submission);

            Console.WriteLine("✅ Assignment submitted successfully!");
        }

        static void ViewEnrollments()
        {
            Console.WriteLine("\n--- All Enrollments ---");
            foreach (var enrollment in enrollmentRepo.GetAll())
            {
                Console.WriteLine($"Student: {enrollment.Student.Name} | Course: {enrollment.Course.Name} | Date: {enrollment.EnrollmentDate:dd-MMM-yyyy}");
            }
        }

        static void ShowLinqReports()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║   LINQ REPORTS                         ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            // 1. Courses with more than 50 students
            Console.WriteLine("\n1. Courses with more than 1 student:");
            var popularCourses = courseRepo.GetAll().Where(c => c.CurrentEnrollment > 1);
            foreach (var course in popularCourses)
            {
                Console.WriteLine($"   {course.Name}: {course.CurrentEnrollment} students");
            }

            // 2. Students enrolled in more than 3 courses
            Console.WriteLine("\n2. Students enrolled in more than 1 course:");
            var activeStudents = enrollmentRepo.GetAll()
                .GroupBy(e => e.Student.Name)
                .Where(g => g.Count() > 1)
                .Select(g => new { Student = g.Key, CourseCount = g.Count() });
            foreach (var student in activeStudents)
            {
                Console.WriteLine($"   {student.Student}: {student.CourseCount} courses");
            }

            // 3. Most popular course
            var mostPopular = courseRepo.GetAll()
                .OrderByDescending(c => c.CurrentEnrollment)
                .FirstOrDefault();
            if (mostPopular != null)
                Console.WriteLine($"\n3. Most Popular Course: {mostPopular.Name} ({mostPopular.CurrentEnrollment} students)");

            // 4. Average course rating
            decimal avgRating = courseRepo.GetAll().Average(c => c.Rating);
            Console.WriteLine($"\n4. Average Course Rating: {avgRating:F2}");

            // 5. Instructors with highest enrollments
            Console.WriteLine("\n5. Instructors with Highest Enrollments:");
            var topInstructors = courseRepo.GetAll()
                .GroupBy(c => c.Instructor.Name)
                .Select(g => new { Instructor = g.Key, TotalEnrollments = g.Sum(c => c.CurrentEnrollment) })
                .OrderByDescending(x => x.TotalEnrollments);
            foreach (var instructor in topInstructors)
            {
                Console.WriteLine($"   {instructor.Instructor}: {instructor.TotalEnrollments} total enrollments");
            }

            // 6. LINQ Join - Students with their courses
            Console.WriteLine("\n6. Student-Course Details (LINQ Join):");
            var studentCourses = from enrollment in enrollmentRepo.GetAll()
                                 join student in studentRepo.GetAll() on enrollment.Student.Id equals student.Id
                                 join course in courseRepo.GetAll() on enrollment.Course.Id equals course.Id
                                 select new { Student = student.Name, Course = course.Name, Instructor = course.Instructor.Name };
            
            foreach (var item in studentCourses.Take(5))
            {
                Console.WriteLine($"   {item.Student} → {item.Course} (by {item.Instructor})");
            }
        }
    }
}
