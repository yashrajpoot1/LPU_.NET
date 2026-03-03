using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Models;

public partial class Course
{
    public int CourseId { get; set; }

    [Required(ErrorMessage = "Course title is required")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 150 characters")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Duration is required")]
    [Range(1, 365, ErrorMessage = "Duration must be between 1 and 365 days")]
    [Display(Name = "Duration (Days)")]
    public int DurationDays { get; set; }

    [Required(ErrorMessage = "Fee is required")]
    [Range(0.01, 999999.99, ErrorMessage = "Fee must be between 0.01 and 999999.99")]
    [DataType(DataType.Currency)]
    public decimal Fee { get; set; }

    [Required(ErrorMessage = "Level is required")]
    [StringLength(30)]
    public string Level { get; set; } = null!;

    [Display(Name = "Active")]
    public bool IsActive { get; set; }

    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
