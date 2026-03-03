using HMS.Application.Services;
using HMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HMS.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppointmentService _appointmentService;
        private readonly PatientService _patientService;
        private readonly DoctorService _doctorService;

        public AppointmentsController(
            AppointmentService appointmentService,
            PatientService patientService,
            DoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _doctorService = doctorService;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return View(appointments);
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        // GET: Appointments/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View();
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                await _appointmentService.BookAppointmentAsync(appointment);
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdowns();
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound();

            await PopulateDropdowns();
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Appointment appointment)
        {
            if (id != appointment.AppointmentId)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _appointmentService.UpdateAppointmentAsync(appointment);
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdowns();
            return View(appointment);
        }

        // POST: Appointments/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            await _appointmentService.CancelAppointmentAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdowns()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            var doctors = await _doctorService.GetAllDoctorsAsync();

            ViewBag.Patients = new SelectList(patients, "PatientId", "Name");
            ViewBag.Doctors = new SelectList(doctors, "DoctorId", "Name");
        }
    }
}
