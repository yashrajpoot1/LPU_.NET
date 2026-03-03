using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UMS.Domain.Entities;

namespace UMS.Data
{
    /// <summary>
    /// Database context for the University Management System
    /// Contains tables for Students, Courses, and Enrollments
    /// </summary>
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        // DbSets represent tables in the database
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);

            // Seed initial data
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@university.edu", PhoneNumber = "555-0101", DateOfBirth = new DateTime(2000, 5, 15), EnrollmentDate = new DateTime(2023, 9, 1) },
                new Student { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@university.edu", PhoneNumber = "555-0102", DateOfBirth = new DateTime(2001, 3, 22), EnrollmentDate = new DateTime(2023, 9, 1) },
                new Student { Id = 3, FirstName = "Mike", LastName = "Johnson", Email = "mike.johnson@university.edu", PhoneNumber = "555-0103", DateOfBirth = new DateTime(1999, 11, 8), EnrollmentDate = new DateTime(2022, 9, 1) }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, CourseCode = "CS101", CourseName = "Introduction to Programming", Description = "Learn the basics of programming", Credits = 3, Instructor = "Dr. Anderson" },
                new Course { Id = 2, CourseCode = "CS201", CourseName = "Data Structures", Description = "Advanced data structures and algorithms", Credits = 4, Instructor = "Prof. Williams" },
                new Course { Id = 3, CourseCode = "MATH101", CourseName = "Calculus I", Description = "Introduction to calculus", Credits = 4, Instructor = "Dr. Brown" }
            );

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { Id = 1, StudentId = 1, CourseId = 1, EnrollmentDate = new DateTime(2023, 9, 5), Grade = "A", Status = "Completed" },
                new Enrollment { Id = 2, StudentId = 1, CourseId = 2, EnrollmentDate = new DateTime(2024, 1, 10), Grade = "", Status = "Active" },
                new Enrollment { Id = 3, StudentId = 2, CourseId = 1, EnrollmentDate = new DateTime(2023, 9, 5), Grade = "B+", Status = "Completed" },
                new Enrollment { Id = 4, StudentId = 2, CourseId = 3, EnrollmentDate = new DateTime(2024, 1, 10), Grade = "", Status = "Active" },
                new Enrollment { Id = 5, StudentId = 3, CourseId = 2, EnrollmentDate = new DateTime(2023, 9, 5), Grade = "A-", Status = "Completed" }
            );
        }
    }
}
