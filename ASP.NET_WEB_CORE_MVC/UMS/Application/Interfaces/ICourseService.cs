using UMS.Domain.Entities;

namespace UMS.Application.Interfaces
{
    /// <summary>
    /// Service interface for course operations
    /// </summary>
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course?> GetCourseByIdAsync(int id);
        Task AddCourseAsync(Course course);
    }
}
