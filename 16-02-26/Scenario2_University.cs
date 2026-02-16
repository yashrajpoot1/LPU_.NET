using System;
using System.Collections.Generic;
using System.Linq;

namespace Generics_Assessment.Scenario2
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
        private Dictionary<TStudent, List<TCourse>> _studentCourses = new();

        /// <summary>
        /// Enrolls student with constraints
        /// </summary>
        public bool EnrollStudent(TStudent student, TCourse course)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            // Rule: Course not at capacity
            if (_enrollments.ContainsKey(course) &&
                _enrollments[course].Count >= course.MaxCapacity)
            {
                Console.WriteLine($"✗ Enrollment failed: {course.Title} is at full capacity");
                return false;
            }

            // Rule: Student not already enrolled
            if (_enrollments.ContainsKey(course) &&
                _enrollments[course].Any(s => s.StudentId == student.StudentId))
            {
                Console.WriteLine($"✗ Enrollment failed: {student.Name} is already enrolled in {course.Title}");
                return false;
            }

            // Rule: Student semester >= course prerequisite (if any)
            if (course is LabCourse labCourse)
            {
                if (student.Semester < labCourse.RequiredSemester)
                {
                    Console.WriteLine($"✗ Enrollment failed: {student.Name} (Semester {student.Semester}) " +
                                    $"doesn't meet prerequisite (Semester {labCourse.RequiredSemester}) for {course.Title}");
                    return false;
                }
            }

            // Add to enrollments
            if (!_enrollments.ContainsKey(course))
            {
                _enrollments[course] = new List<TStudent>();
            }
            _enrollments[course].Add(student);

            // Add to student courses
            if (!_studentCourses.ContainsKey(student))
            {
                _studentCourses[student] = new List<TCourse>();
            }
            _studentCourses[student].Add(course);

            Console.WriteLine($"✓ Successfully enrolled {student.Name} in {course.Title}");
            return true;
        }

        /// <summary>
        /// Gets students enrolled in a course
        /// </summary>
        public IReadOnlyList<TStudent> GetEnrolledStudents(TCourse course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            return _enrollments.ContainsKey(course)
                ? _enrollments[course].AsReadOnly()
                : new List<TStudent>().AsReadOnly();
        }

        /// <summary>
        /// Gets courses for a student
        /// </summary>
        public IEnumerable<TCourse> GetStudentCourses(TStudent student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            return _studentCourses.ContainsKey(student)
                ? _studentCourses[student]
                : Enumerable.Empty<TCourse>();
        }

        /// <summary>
        /// Calculates student workload (total credits)
        /// </summary>
        public int CalculateStudentWorkload(TStudent student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            return GetStudentCourses(student).Sum(c => c.Credits);
        }

        /// <summary>
        /// Gets enrollment statistics
        /// </summary>
        public Dictionary<TCourse, int> GetEnrollmentStatistics()
        {
            return _enrollments.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Count
            );
        }
    }

    // 2. Specialized implementations
    public class EngineeringStudent : IStudent
    {
        public int StudentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Semester { get; set; }
        public string Specialization { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"Student #{StudentId}: {Name} (Semester {Semester}, {Specialization})";
        }
    }

    public class LabCourse : ICourse
    {
        public string CourseCode { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int MaxCapacity { get; set; }
        public int Credits { get; set; }
        public string LabEquipment { get; set; } = string.Empty;
        public int RequiredSemester { get; set; } // Prerequisite

        public override string ToString()
        {
            return $"{CourseCode}: {Title} ({Credits} credits, Max: {MaxCapacity}, Req: Sem {RequiredSemester})";
        }
    }

    public class TheoryCourse : ICourse
    {
        public string CourseCode { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int MaxCapacity { get; set; }
        public int Credits { get; set; }

        public override string ToString()
        {
            return $"{CourseCode}: {Title} ({Credits} credits, Max: {MaxCapacity})";
        }
    }

    // 3. Generic gradebook
    public class GradeBook<TStudent, TCourse>
        where TStudent : IStudent
        where TCourse : ICourse
    {
        private Dictionary<(TStudent, TCourse), double> _grades = new();
        private EnrollmentSystem<TStudent, TCourse> _enrollmentSystem;

        public GradeBook(EnrollmentSystem<TStudent, TCourse> enrollmentSystem)
        {
            _enrollmentSystem = enrollmentSystem ?? throw new ArgumentNullException(nameof(enrollmentSystem));
        }

        /// <summary>
        /// Adds grade with validation
        /// </summary>
        public void AddGrade(TStudent student, TCourse course, double grade)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            // Grade must be between 0 and 100
            if (grade < 0 || grade > 100)
            {
                throw new ArgumentException($"Grade must be between 0 and 100. Provided: {grade}");
            }

            // Student must be enrolled in course
            var enrolledStudents = _enrollmentSystem.GetEnrolledStudents(course);
            if (!enrolledStudents.Any(s => s.StudentId == student.StudentId))
            {
                throw new InvalidOperationException(
                    $"{student.Name} is not enrolled in {course.Title}");
            }

            _grades[(student, course)] = grade;
            Console.WriteLine($"✓ Grade recorded: {student.Name} - {course.Title}: {grade:F2}");
        }

        /// <summary>
        /// Calculates GPA for student (weighted by course credits)
        /// </summary>
        public double? CalculateGPA(TStudent student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            var studentGrades = _grades
                .Where(kvp => kvp.Key.Item1.StudentId == student.StudentId)
                .ToList();

            if (!studentGrades.Any())
                return null;

            double totalPoints = 0;
            int totalCredits = 0;

            foreach (var gradeEntry in studentGrades)
            {
                var course = gradeEntry.Key.Item2;
                var grade = gradeEntry.Value;

                // Convert percentage to GPA scale (0-4.0)
                double gradePoints = ConvertToGradePoints(grade);

                totalPoints += gradePoints * course.Credits;
                totalCredits += course.Credits;
            }

            return totalCredits > 0 ? totalPoints / totalCredits : null;
        }

        /// <summary>
        /// Finds top student in course
        /// </summary>
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

        /// <summary>
        /// Gets all grades for a student
        /// </summary>
        public Dictionary<TCourse, double> GetStudentGrades(TStudent student)
        {
            return _grades
                .Where(kvp => kvp.Key.Item1.StudentId == student.StudentId)
                .ToDictionary(kvp => kvp.Key.Item2, kvp => kvp.Value);
        }

        /// <summary>
        /// Gets grade distribution for a course
        /// </summary>
        public Dictionary<string, int> GetGradeDistribution(TCourse course)
        {
            var distribution = new Dictionary<string, int>
            {
                ["A (90-100)"] = 0,
                ["B (80-89)"] = 0,
                ["C (70-79)"] = 0,
                ["D (60-69)"] = 0,
                ["F (0-59)"] = 0
            };

            var courseGrades = _grades
                .Where(kvp => kvp.Key.Item2.CourseCode == course.CourseCode)
                .Select(kvp => kvp.Value);

            foreach (var grade in courseGrades)
            {
                if (grade >= 90) distribution["A (90-100)"]++;
                else if (grade >= 80) distribution["B (80-89)"]++;
                else if (grade >= 70) distribution["C (70-79)"]++;
                else if (grade >= 60) distribution["D (60-69)"]++;
                else distribution["F (0-59)"]++;
            }

            return distribution;
        }

        private double ConvertToGradePoints(double percentage)
        {
            if (percentage >= 90) return 4.0;
            if (percentage >= 80) return 3.0;
            if (percentage >= 70) return 2.0;
            if (percentage >= 60) return 1.0;
            return 0.0;
        }
    }

    // Demo class for Scenario 2
    public static class Scenario2Demo
    {
        public static void Run()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   SCENARIO 2: University Course Registration System       ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");

            try
            {
                // a) Create 3 EngineeringStudent instances
                Console.WriteLine("\n--- Creating Students ---");
                var students = new List<EngineeringStudent>
                {
                    new EngineeringStudent
                    {
                        StudentId = 1001,
                        Name = "Alice Johnson",
                        Semester = 5,
                        Specialization = "Computer Science"
                    },
                    new EngineeringStudent
                    {
                        StudentId = 1002,
                        Name = "Bob Smith",
                        Semester = 3,
                        Specialization = "Electrical Engineering"
                    },
                    new EngineeringStudent
                    {
                        StudentId = 1003,
                        Name = "Charlie Brown",
                        Semester = 6,
                        Specialization = "Mechanical Engineering"
                    }
                };

                foreach (var student in students)
                {
                    Console.WriteLine($"  {student}");
                }

                // b) Create 2 LabCourse instances with prerequisites
                Console.WriteLine("\n--- Creating Courses ---");
                var courses = new List<ICourse>
                {
                    new LabCourse
                    {
                        CourseCode = "CS401",
                        Title = "Advanced Algorithms Lab",
                        MaxCapacity = 2,
                        Credits = 4,
                        LabEquipment = "High-performance computers",
                        RequiredSemester = 4
                    },
                    new LabCourse
                    {
                        CourseCode = "EE301",
                        Title = "Digital Electronics Lab",
                        MaxCapacity = 3,
                        Credits = 3,
                        LabEquipment = "Oscilloscopes, Logic Analyzers",
                        RequiredSemester = 3
                    },
                    new TheoryCourse
                    {
                        CourseCode = "MATH201",
                        Title = "Linear Algebra",
                        MaxCapacity = 50,
                        Credits = 3
                    }
                };

                foreach (var course in courses)
                {
                    Console.WriteLine($"  {course}");
                }

                // Create enrollment system and gradebook
                var enrollmentSystem = new EnrollmentSystem<EngineeringStudent, ICourse>();
                var gradeBook = new GradeBook<EngineeringStudent, ICourse>(enrollmentSystem);

                // c) Demonstrate successful enrollment
                Console.WriteLine("\n--- Successful Enrollments ---");
                enrollmentSystem.EnrollStudent(students[0], courses[0]); // Alice in CS401 (Sem 5 >= 4) ✓
                enrollmentSystem.EnrollStudent(students[2], courses[0]); // Charlie in CS401 (Sem 6 >= 4) ✓
                enrollmentSystem.EnrollStudent(students[1], courses[1]); // Bob in EE301 (Sem 3 >= 3) ✓
                enrollmentSystem.EnrollStudent(students[0], courses[2]); // Alice in MATH201 ✓

                // d) Demonstrate failed enrollment (capacity, prerequisite)
                Console.WriteLine("\n--- Failed Enrollments (Testing Constraints) ---");

                // Test capacity constraint
                var tempStudent = new EngineeringStudent
                {
                    StudentId = 1004,
                    Name = "David Lee",
                    Semester = 5,
                    Specialization = "Computer Science"
                };
                enrollmentSystem.EnrollStudent(tempStudent, courses[0]); // Should fail - capacity reached

                // Test prerequisite constraint
                enrollmentSystem.EnrollStudent(students[1], courses[0]); // Bob in CS401 (Sem 3 < 4) ✗

                // Test duplicate enrollment
                enrollmentSystem.EnrollStudent(students[0], courses[0]); // Alice already enrolled ✗

                // Display enrollment statistics
                Console.WriteLine("\n--- Enrollment Statistics ---");
                var stats = enrollmentSystem.GetEnrollmentStatistics();
                foreach (var stat in stats)
                {
                    var enrolled = enrollmentSystem.GetEnrolledStudents(stat.Key);
                    Console.WriteLine($"\n{stat.Key.Title}:");
                    Console.WriteLine($"  Enrolled: {stat.Value}/{stat.Key.MaxCapacity}");
                    foreach (var student in enrolled)
                    {
                        Console.WriteLine($"    - {student.Name}");
                    }
                }

                // Display student workloads
                Console.WriteLine("\n--- Student Workloads ---");
                foreach (var student in students)
                {
                    int workload = enrollmentSystem.CalculateStudentWorkload(student);
                    var studentCourses = enrollmentSystem.GetStudentCourses(student);
                    Console.WriteLine($"\n{student.Name}:");
                    Console.WriteLine($"  Total Credits: {workload}");
                    Console.WriteLine($"  Courses:");
                    foreach (var course in studentCourses)
                    {
                        Console.WriteLine($"    - {course.Title} ({course.Credits} credits)");
                    }
                }

                // e) Grade assignment
                Console.WriteLine("\n--- Assigning Grades ---");
                gradeBook.AddGrade(students[0], courses[0], 92.5); // Alice - CS401
                gradeBook.AddGrade(students[2], courses[0], 88.0); // Charlie - CS401
                gradeBook.AddGrade(students[1], courses[1], 85.5); // Bob - EE301
                gradeBook.AddGrade(students[0], courses[2], 95.0); // Alice - MATH201

                // Test invalid grade
                Console.WriteLine("\n--- Testing Invalid Grade ---");
                try
                {
                    gradeBook.AddGrade(students[0], courses[0], 105); // Invalid grade
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✗ {ex.Message}");
                }

                // f) GPA calculation
                Console.WriteLine("\n--- GPA Calculations ---");
                foreach (var student in students)
                {
                    var gpa = gradeBook.CalculateGPA(student);
                    if (gpa.HasValue)
                    {
                        Console.WriteLine($"{student.Name}: GPA = {gpa.Value:F2}/4.0");

                        var grades = gradeBook.GetStudentGrades(student);
                        foreach (var grade in grades)
                        {
                            Console.WriteLine($"  - {grade.Key.Title}: {grade.Value:F2}%");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{student.Name}: No grades recorded");
                    }
                }

                // g) Top student per course
                Console.WriteLine("\n--- Top Students per Course ---");
                foreach (var course in courses)
                {
                    var topStudent = gradeBook.GetTopStudent(course);
                    if (topStudent.HasValue)
                    {
                        Console.WriteLine($"{course.Title}:");
                        Console.WriteLine($"  Top Student: {topStudent.Value.student.Name} " +
                                        $"with {topStudent.Value.grade:F2}%");
                    }
                }

                // h) Grade distribution
                Console.WriteLine("\n--- Grade Distribution ---");
                foreach (var course in courses.Take(2))
                {
                    var distribution = gradeBook.GetGradeDistribution(course);
                    Console.WriteLine($"\n{course.Title}:");
                    foreach (var grade in distribution)
                    {
                        if (grade.Value > 0)
                        {
                            Console.WriteLine($"  {grade.Key}: {grade.Value} student(s)");
                        }
                    }
                }

                Console.WriteLine("\n✓ Scenario 2 completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error in Scenario 2: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
