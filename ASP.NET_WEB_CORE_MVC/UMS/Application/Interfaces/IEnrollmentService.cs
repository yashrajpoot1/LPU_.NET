using UMS.Domain.Entities;

namespace UMS.Application.Interfaces
{
    /// <summary>
    /// Service interface for enrollment operations
    /// </summary>
    public interface IEnrollmentService
    {
        Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync();
        Task<Enrollment?> GetEnrollmentByIdAsync(int id);
        Task AddEnrollmentAsync(Enrollment enrollment);
    }
}
