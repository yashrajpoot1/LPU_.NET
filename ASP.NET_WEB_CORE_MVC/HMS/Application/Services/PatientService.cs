using HMS.Domain.Entities;
using HMS.Domain.Interfaces;

namespace HMS.Application.Services
{
    public class PatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _patientRepository.GetAllAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await _patientRepository.GetByIdAsync(id);
        }

        public async Task<Patient> RegisterPatientAsync(Patient patient)
        {
            patient.RegistrationDate = DateTime.Now;
            return await _patientRepository.AddAsync(patient);
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            await _patientRepository.UpdateAsync(patient);
        }

        public async Task DeletePatientAsync(int id)
        {
            await _patientRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Patient>> SearchPatientsByNameAsync(string name)
        {
            return await _patientRepository.SearchByNameAsync(name);
        }
    }
}
