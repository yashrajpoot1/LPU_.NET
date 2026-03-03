using UMS.Domain.Entities;

namespace UMS.Application.Interfaces
{
    /// <summary>
    /// Service interface for student operations
    /// </summary>
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);
    }
}
