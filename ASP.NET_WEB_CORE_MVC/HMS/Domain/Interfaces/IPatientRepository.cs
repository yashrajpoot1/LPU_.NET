using HMS.Domain.Entities;

namespace HMS.Domain.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient?> GetByIdAsync(int id);
        Task<Patient> AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(int id);
        Task<IEnumerable<Patient>> SearchByNameAsync(string name);
    }
}
