using System;
using System.Collections.Generic;
using System.Linq;

namespace Scenario2_University
{
    // Base constraints
    public interface IStudent
    {
        int StudentId { get; }
        string Name { get; }
        int Semester { get; }
    }

    public interface ICourse
    {
        string CourseCode { get; }
        string Title { get; }
        int MaxCapacity { get; }
        int Credits { get; }
    }

    // 1. Generic enrollment system
    public class EnrollmentSystem<TStudent, TCourse>
        where TStudent : IStudent
        where TCourse : ICourse
    {
        private Dictionary<TCourse, List<TStudent>> _enrollments = new();

        public bool EnrollStudent(TStudent student, TCourse course)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            // Check if course exists in enrollments
            if (!_enrollments.ContainsKey(course))
            {
                _enrollments[course] = new List<TStudent>();
            }

            // Check capacity
            if (_enrollments[course].Count >= course.MaxCapacity)
            {
                Console.WriteLine($"Enrollment failed: {course.Title} is at full capacity");
                return false;
            }

            // Check if already enrolled
            if (_enrollments[course].Any(s => s.StudentId == student.StudentId))
            {
                Console.WriteLine($"Enrollment failed: {student.Name} is already enrolled in {course.Title}");
                return false;
            }

            // Check prerequisite (if course is LabCourse)
            if (course is LabCourse labCourse)
            {
                if (student.Semester < labCourse.RequiredSemester)
                {
                    Console.WriteLine($"Enrollment failed: {student.Name} (Semester {student.Semester}) " +
                                    $"does not meet prerequisite (Semester {labCourse.RequiredSemester}) for {course.Title}");
                    return false;
                }
            }

            _enrollments[course].Add(student);
            Console.WriteLine($"Success: {student.Name} enrolled in {course.Title}");
            return true;
        }

        public IReadOnlyList<TStudent> GetEnrolledStudents(TCourse course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            if (!_enrollments.ContainsKey(course))
                return new List<TStudent>().AsReadOnly();

            return _enrollments[course].AsReadOnly();
        }

        public IEnumerable<TCourse> GetStudentCourses(TStudent student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            return _enrollments
                .Where(kvp => kvp.Value.Any(s => s.StudentId == student.StudentId))
                .Select(kvp => kvp.Key);
        }

        public int CalculateStudentWorkload(TStudent student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            return GetStudentCourses(student).Sum(c => c.Credits);
        }
    }

    // 2. Specialized implementations
    public class EngineeringStudent : IStudent
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Semester { get; set; }
        public string Specialization { get; set; }
    }

    public class LabCourse : ICourse
    {
        public string CourseCode { get; set; }
        public string Title { get; set; }
        public int MaxCapacity { get; set; }
        public int Credits { get; set; }
        public string LabEquipment { get; set; }
        public int RequiredSemester { get; set; }
    }

    // 3. Generic gradebook
    public class GradeBook<TStudent, TCourse>
    {
        private Dictionary<(TStudent, TCourse), double> _grades = new();
        private EnrollmentSystem<TStudent, TCourse> _enrollmentSystem;

        public GradeBook(EnrollmentSystem<TStudent, TCourse> enrollmentSystem)
        {
            _enrollmentSystem = enrollmentSystem ?? throw new ArgumentNullException(nameof(enrollmentSystem));
        }

        public void AddGrade(TStudent student, TCourse course, double grade)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            if (grade < 0 || grade > 100)
                throw new ArgumentException("Grade must be between 0 and 100");

            // Check if student is enrolled
            var enrolledStudents = _enrollmentSystem.GetEnrolledStudents(course);
            if (!enrolledStudents.Any(s => s.StudentId == student.StudentId))
            {
                throw new InvalidOperationException($"Student {student.Name} is not enrolled in {course.Title}");
            }

            _grades[(student, course)] = grade;
            Console.WriteLine($"Grade added: {student.Name} - {course.Title}: {grade}");
        }

        public double? CalculateGPA(TStudent student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            var studentGrades = _grades.Where(kvp => kvp.Key.Item1.StudentId == student.StudentId).ToList();

            if (!studentGrades.Any())
                return null;

            double totalWeightedGrade = 0;
            int totalCredits = 0;

            foreach (var gradeEntry in studentGrades)
            {
                var course = gradeEntry.Key.Item2;
                var grade = gradeEntry.Value;
                totalWeightedGrade += grade * course.Credits;
                totalCredits += course.Credits;
            }

            return totalCredits > 0 ? totalWeightedGrade / totalCredits : null;
        }

        public (TStudent student, double grade)? GetTopStudent(TCourse course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            var courseGrades = _grades
                .Where(kvp => kvp.Key.Item2.CourseCode == course.CourseCode)
                .OrderByDescending(kvp => kvp.Value)
                .FirstOrDefault();

            if (courseGrades.Key.Item1 == null)
                return null;

            return (courseGrades.Key.Item1, courseGrades.Value);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== University Course Registration System Demo ===\n");

            try
            {
                // Create enrollment system
                var enrollmentSystem = new EnrollmentSystem<EngineeringStudent, LabCourse>();

                // Create students
                var student1 = new EngineeringStudent
                {
                    StudentId = 1,
                    Name = "Alice Johnson",
                    Semester = 3,
                    Specialization = "Computer Science"
                };

                var student2 = new EngineeringStudent
                {
                    StudentId = 2,
                    Name = "Bob Smith",
                    Semester = 5,
                    Specialization = "Electrical Engineering"
                };

                var student3 = new EngineeringStudent
                {
                    StudentId = 3,
                    Name = "Charlie Brown",
                    Semester = 2,
                    Specialization = "Mechanical Engineering"
                };

                // Create courses
                var course1 = new LabCourse
                {
                    CourseCode = "CS301",
                    Title = "Data Structures Lab",
                    MaxCapacity = 2,
                    Credits = 4,
                    LabEquipment = "Computers",
                    RequiredSemester = 3
                };

                var course2 = new LabCourse
                {
                    CourseCode = "EE401",
                    Title = "Advanced Electronics Lab",
                    MaxCapacity = 3,
                    Credits = 3,
                    LabEquipment = "Oscilloscopes",
                    RequiredSemester = 4
                };

                Console.WriteLine("=== Enrollment Tests ===\n");

                // Successful enrollment
                enrollmentSystem.EnrollStudent(student1, course1);
                enrollmentSystem.EnrollStudent(student2, course1);
                enrollmentSystem.EnrollStudent(student2, course2);

                Console.WriteLine();

                // Failed enrollment - capacity
                enrollmentSystem.EnrollStudent(student3, course1);

                Console.WriteLine();

                // Failed enrollment - prerequisite
                enrollmentSystem.EnrollStudent(student3, course2);

                Console.WriteLine();

                // Failed enrollment - already enrolled
                enrollmentSystem.EnrollStudent(student1, course1);

                Console.WriteLine("\n=== Student Workload ===");
                Console.WriteLine($"{student1.Name}: {enrollmentSystem.CalculateStudentWorkload(student1)} credits");
                Console.WriteLine($"{student2.Name}: {enrollmentSystem.CalculateStudentWorkload(student2)} credits");
                Console.WriteLine($"{student3.Name}: {enrollmentSystem.CalculateStudentWorkload(student3)} credits");

                Console.WriteLine("\n=== Enrolled Students per Course ===");
                Console.WriteLine($"\n{course1.Title}:");
                foreach (var student in enrollmentSystem.GetEnrolledStudents(course1))
                {
                    Console.WriteLine($"  - {student.Name} (Semester {student.Semester})");
                }

                Console.WriteLine($"\n{course2.Title}:");
                foreach (var student in enrollmentSystem.GetEnrolledStudents(course2))
                {
                    Console.WriteLine($"  - {student.Name} (Semester {student.Semester})");
                }

                // Gradebook demonstration
                Console.WriteLine("\n=== Gradebook Demo ===\n");
                var gradeBook = new GradeBook<EngineeringStudent, LabCourse>(enrollmentSystem);

                gradeBook.AddGrade(student1, course1, 85.5);
                gradeBook.AddGrade(student2, course1, 92.0);
                gradeBook.AddGrade(student2, course2, 88.5);

                Console.WriteLine("\n=== GPA Calculation ===");
                var gpa1 = gradeBook.CalculateGPA(student1);
                var gpa2 = gradeBook.CalculateGPA(student2);
                var gpa3 = gradeBook.CalculateGPA(student3);

                Console.WriteLine($"{student1.Name}: {(gpa1.HasValue ? gpa1.Value.ToString("F2") : "No grades")}");
                Console.WriteLine($"{student2.Name}: {(gpa2.HasValue ? gpa2.Value.ToString("F2") : "No grades")}");
                Console.WriteLine($"{student3.Name}: {(gpa3.HasValue ? gpa3.Value.ToString("F2") : "No grades")}");

                Console.WriteLine("\n=== Top Students per Course ===");
                var topInCourse1 = gradeBook.GetTopStudent(course1);
                if (topInCourse1.HasValue)
                {
                    Console.WriteLine($"{course1.Title}: {topInCourse1.Value.student.Name} - {topInCourse1.Value.grade}");
                }

                var topInCourse2 = gradeBook.GetTopStudent(course2);
                if (topInCourse2.HasValue)
                {
                    Console.WriteLine($"{course2.Title}: {topInCourse2.Value.student.Name} - {topInCourse2.Value.grade}");
                }

                Console.WriteLine("\n=== Demo Completed Successfully ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
