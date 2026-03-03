namespace UMS.Domain.Entities
{
    /// <summary>
    /// Represents a student's enrollment in a course
    /// </summary>
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Grade { get; set; } = string.Empty;
        public string Status { get; set; } = "Active"; // Active, Completed, Dropped
        
        // Navigation properties
        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}
