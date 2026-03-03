using HMS.Domain.Entities;
using HMS.Domain.Interfaces;

namespace HMS.Application.Services
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;

        public AppointmentService(
            IAppointmentRepository appointmentRepository,
            IPatientRepository patientRepository,
            IDoctorRepository doctorRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllAsync();
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            return await _appointmentRepository.GetByIdAsync(id);
        }

        public async Task<Appointment> BookAppointmentAsync(Appointment appointment)
        {
            // Validate patient exists
            var patient = await _patientRepository.GetByIdAsync(appointment.PatientId);
            if (patient == null)
                throw new Exception("Patient not found");

            // Validate doctor exists
            var doctor = await _doctorRepository.GetByIdAsync(appointment.DoctorId);
            if (doctor == null)
                throw new Exception("Doctor not found");

            appointment.Status = "Scheduled";
            return await _appointmentRepository.AddAsync(appointment);
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            await _appointmentRepository.UpdateAsync(appointment);
        }

        public async Task CancelAppointmentAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment != null)
            {
                appointment.Status = "Cancelled";
                await _appointmentRepository.UpdateAsync(appointment);
            }
        }

        public async Task<IEnumerable<Appointment>> GetPatientAppointmentsAsync(int patientId)
        {
            return await _appointmentRepository.GetByPatientIdAsync(patientId);
        }

        public async Task<IEnumerable<Appointment>> GetDoctorAppointmentsAsync(int doctorId)
        {
            return await _appointmentRepository.GetByDoctorIdAsync(doctorId);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date)
        {
            return await _appointmentRepository.GetByDateAsync(date);
        }
    }
}
