using HMS.Domain.Entities;
using HMS.Domain.Interfaces;

namespace HMS.Application.Services
{
    public class DoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _doctorRepository.GetAllAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            return await _doctorRepository.GetByIdAsync(id);
        }

        public async Task<Doctor> AddDoctorAsync(Doctor doctor)
        {
            doctor.JoinDate = DateTime.Now;
            doctor.IsActive = true;
            return await _doctorRepository.AddAsync(doctor);
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            await _doctorRepository.UpdateAsync(doctor);
        }

        public async Task DeleteDoctorAsync(int id)
        {
            await _doctorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsBySpecializationAsync(string specialization)
        {
            return await _doctorRepository.GetBySpecializationAsync(specialization);
        }
    }
}
