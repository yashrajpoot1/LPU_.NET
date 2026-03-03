using HMS.Domain.Entities;

namespace HMS.Domain.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(int id);
        Task<Doctor> AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
        Task DeleteAsync(int id);
        Task<IEnumerable<Doctor>> GetBySpecializationAsync(string specialization);
    }
}
