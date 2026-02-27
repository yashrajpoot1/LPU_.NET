using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagementSystem
{
    // Custom Exceptions
    public class DoctorNotAvailableException : Exception
    {
        public DoctorNotAvailableException(string message) : base(message) { }
    }

    public class InvalidAppointmentException : Exception
    {
        public InvalidAppointmentException(string message) : base(message) { }
    }

    public class PatientNotFoundExcept : Exception
    {
        public PatientNotFoundExcept(string message) : base(message) { }
    }

    public class DuplicateMedicalRecordException : Exception
    {
        public DuplicateMedicalRecordException(string message) : base(message) { }
    }

    // Billing Interface
    public interface IBillable
    {
        decimal CalculateBill();
    }

    // Base Person Class
    public abstract class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string ContactNumber { get; set; }

        public Person(int id, string name, int age, string contactNumber)
        {
            Id = id;
            Name = name;
            Age = age;
            ContactNumber = contactNumber;
        }
    }

    // Doctor Class
    public class Doctor : Person
    {
        public string Specialization { get; set; }
        public decimal ConsultationFee { get; set; }
        public List<DateTime> AvailableSlots { get; set; }

        public Doctor(int id, string name, int age, string contactNumber, string specialization, decimal consultationFee)
            : base(id, name, age, contactNumber)
        {
            Specialization = specialization;
            ConsultationFee = consultationFee;
            AvailableSlots = new List<DateTime>();
        }

        public bool IsAvailable(DateTime dateTime)
        {
            return !AvailableSlots.Contains(dateTime);
        }

        public void BookSlot(DateTime dateTime)
        {
            AvailableSlots.Add(dateTime);
        }

        public void ReleaseSlot(DateTime dateTime)
        {
            AvailableSlots.Remove(dateTime);
        }
    }

    // Patient Class
    public class Patient : Person
    {
        public string Disease { get; set; }
        public DateTime AdmissionDate { get; set; }

        public Patient(int id, string name, int age, string contactNumber, string disease)
            : base(id, name, age, contactNumber)
        {
            Disease = disease;
            AdmissionDate = DateTime.Now;
        }
    }

    // Appointment Class
    public class Appointment : IBillable
    {
        public int AppointmentId { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string Status { get; set; }
        public decimal AdditionalCharges { get; set; }

        public Appointment(int id, Doctor doctor, Patient patient, DateTime dateTime)
        {
            AppointmentId = id;
            Doctor = doctor;
            Patient = patient;
            AppointmentDateTime = dateTime;
            Status = "Scheduled";
            AdditionalCharges = 0;
        }

        public decimal CalculateBill()
        {
            return Doctor.ConsultationFee + AdditionalCharges;
        }
    }

    // Medical Record Class
    public class MedicalRecord
    {
        public int RecordId { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public DateTime RecordDate { get; set; }

        public MedicalRecord(int recordId, Patient patient, Doctor doctor, string diagnosis, string prescription)
        {
            RecordId = recordId;
            Patient = patient;
            Doctor = doctor;
            Diagnosis = diagnosis;
            Prescription = prescription;
            RecordDate = DateTime.Now;
        }
    }

    class Program
    {
        static List<Doctor> doctors = new List<Doctor>();
        static List<Patient> patients = new List<Patient>();
        static List<Appointment> appointments = new List<Appointment>();
        static Dictionary<int, MedicalRecord> medicalRecords = new Dictionary<int, MedicalRecord>();
        static int appointmentIdCounter = 1;
        static int recordIdCounter = 1;

        static void Main(string[] args)
        {
            InitializeSampleData();

            while (true)
            {
                Console.WriteLine("\n╔════════════════════════════════════════╗");
                Console.WriteLine("║   HOSPITAL MANAGEMENT SYSTEM           ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                Console.WriteLine("1. View Doctors");
                Console.WriteLine("2. View Patients");
                Console.WriteLine("3. Register Patient");
                Console.WriteLine("4. Book Appointment");
                Console.WriteLine("5. View Appointments");
                Console.WriteLine("6. Add Medical Record");
                Console.WriteLine("7. View Medical Records");
                Console.WriteLine("8. Calculate Bill");
                Console.WriteLine("9. LINQ Reports");
                Console.WriteLine("10. Exit");
                Console.Write("\nEnter choice: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": ViewDoctors(); break;
                        case "2": ViewPatients(); break;
                        case "3": RegisterPatient(); break;
                        case "4": BookAppointment(); break;
                        case "5": ViewAppointments(); break;
                        case "6": AddMedicalRecord(); break;
                        case "7": ViewMedicalRecords(); break;
                        case "8": CalculateBill(); break;
                        case "9": ShowLinqReports(); break;
                        case "10": return;
                        default: Console.WriteLine("Invalid choice!"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Error: {ex.Message}");
                }
            }
        }

        static void InitializeSampleData()
        {
            // Doctors
            doctors.Add(new Doctor(1, "Dr. Rajesh Kumar", 45, "9876543210", "Cardiologist", 1500));
            doctors.Add(new Doctor(2, "Dr. Priya Sharma", 38, "9876543211", "Neurologist", 1800));
            doctors.Add(new Doctor(3, "Dr. Amit Patel", 42, "9876543212", "Orthopedic", 1200));
            doctors.Add(new Doctor(4, "Dr. Sneha Singh", 35, "9876543213", "Pediatrician", 1000));

            // Patients
            patients.Add(new Patient(1, "Rahul Verma", 30, "9988776655", "Fever"));
            patients.Add(new Patient(2, "Anita Desai", 45, "9988776656", "Diabetes"));
            patients.Add(new Patient(3, "Vikram Singh", 55, "9988776657", "Heart Disease"));

            // Sample Appointments
            var appointment1 = new Appointment(appointmentIdCounter++, doctors[0], patients[2], DateTime.Now.AddDays(-5));
            appointment1.Status = "Completed";
            appointment1.AdditionalCharges = 500;
            appointments.Add(appointment1);

            var appointment2 = new Appointment(appointmentIdCounter++, doctors[1], patients[0], DateTime.Now.AddDays(-2));
            appointment2.Status = "Completed";
            appointments.Add(appointment2);
        }

        static void ViewDoctors()
        {
            Console.WriteLine("\n--- Doctors List ---");
            Console.WriteLine($"{"ID",-5} {"Name",-25} {"Specialization",-20} {"Fee",-10}");
            Console.WriteLine(new string('-', 65));
            foreach (var doctor in doctors)
            {
                Console.WriteLine($"{doctor.Id,-5} {doctor.Name,-25} {doctor.Specialization,-20} ₹{doctor.ConsultationFee,-10}");
            }
        }

        static void ViewPatients()
        {
            Console.WriteLine("\n--- Patients List ---");
            Console.WriteLine($"{"ID",-5} {"Name",-25} {"Age",-5} {"Disease",-20}");
            Console.WriteLine(new string('-', 60));
            foreach (var patient in patients)
            {
                Console.WriteLine($"{patient.Id,-5} {patient.Name,-25} {patient.Age,-5} {patient.Disease,-20}");
            }
        }

        static void RegisterPatient()
        {
            Console.WriteLine("\n--- Register New Patient ---");
            Console.Write("Enter Patient ID: ");
            int id = int.Parse(Console.ReadLine());

            if (patients.Any(p => p.Id == id))
            {
                Console.WriteLine("❌ Patient ID already exists!");
                return;
            }

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Enter Contact Number: ");
            string contact = Console.ReadLine();

            Console.Write("Enter Disease: ");
            string disease = Console.ReadLine();

            patients.Add(new Patient(id, name, age, contact, disease));
            Console.WriteLine("✅ Patient registered successfully!");
        }

        static void BookAppointment()
        {
            Console.Write("\nEnter Patient ID: ");
            int patientId = int.Parse(Console.ReadLine());

            var patient = patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
                throw new PatientNotFoundExcept("Patient not found!");

            ViewDoctors();
            Console.Write("\nEnter Doctor ID: ");
            int doctorId = int.Parse(Console.ReadLine());

            var doctor = doctors.FirstOrDefault(d => d.Id == doctorId);
            if (doctor == null)
            {
                Console.WriteLine("❌ Doctor not found!");
                return;
            }

            Console.Write("Enter Appointment Date (dd-MM-yyyy HH:mm): ");
            DateTime appointmentDate = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy HH:mm", null);

            if (appointmentDate < DateTime.Now)
                throw new InvalidAppointmentException("Appointment date cannot be in the past");

            // Check for overlapping appointments
            var overlapping = appointments.Any(a => 
                a.Doctor.Id == doctorId && 
                Math.Abs((a.AppointmentDateTime - appointmentDate).TotalMinutes) < 30);

            if (overlapping)
                throw new DoctorNotAvailableException($"Dr. {doctor.Name} is not available at this time. Please choose another slot.");

            if (!doctor.IsAvailable(appointmentDate))
                throw new DoctorNotAvailableException($"Dr. {doctor.Name} is not available at this time");

            doctor.BookSlot(appointmentDate);
            var appointment = new Appointment(appointmentIdCounter++, doctor, patient, appointmentDate);
            appointments.Add(appointment);

            Console.WriteLine($"✅ Appointment booked successfully! Appointment ID: {appointment.AppointmentId}");
        }

        static void ViewAppointments()
        {
            Console.WriteLine("\n--- All Appointments ---");
            foreach (var appointment in appointments)
            {
                Console.WriteLine($"\nAppointment ID: {appointment.AppointmentId}");
                Console.WriteLine($"Doctor: {appointment.Doctor.Name} ({appointment.Doctor.Specialization})");
                Console.WriteLine($"Patient: {appointment.Patient.Name}");
                Console.WriteLine($"Date & Time: {appointment.AppointmentDateTime:dd-MMM-yyyy HH:mm}");
                Console.WriteLine($"Status: {appointment.Status}");
                Console.WriteLine($"Bill Amount: ₹{appointment.CalculateBill():F2}");
            }
        }

        static void AddMedicalRecord()
        {
            Console.Write("\nEnter Patient ID: ");
            int patientId = int.Parse(Console.ReadLine());

            var patient = patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
                throw new PatientNotFoundExcept("Patient not found!");

            Console.Write("Enter Doctor ID: ");
            int doctorId = int.Parse(Console.ReadLine());

            var doctor = doctors.FirstOrDefault(d => d.Id == doctorId);
            if (doctor == null)
            {
                Console.WriteLine("❌ Doctor not found!");
                return;
            }

            if (medicalRecords.Values.Any(r => r.Patient.Id == patientId && r.Doctor.Id == doctorId && r.RecordDate.Date == DateTime.Now.Date))
                throw new DuplicateMedicalRecordException("Medical record already exists for this patient and doctor today");

            Console.Write("Enter Diagnosis: ");
            string diagnosis = Console.ReadLine();

            Console.Write("Enter Prescription: ");
            string prescription = Console.ReadLine();

            var record = new MedicalRecord(recordIdCounter, patient, doctor, diagnosis, prescription);
            medicalRecords[recordIdCounter] = record;
            recordIdCounter++;

            Console.WriteLine("✅ Medical record added successfully!");
        }

        static void ViewMedicalRecords()
        {
            Console.WriteLine("\n--- Medical Records ---");
            foreach (var record in medicalRecords.Values)
            {
                Console.WriteLine($"\nRecord ID: {record.RecordId}");
                Console.WriteLine($"Patient: {record.Patient.Name}");
                Console.WriteLine($"Doctor: {record.Doctor.Name}");
                Console.WriteLine($"Diagnosis: {record.Diagnosis}");
                Console.WriteLine($"Prescription: {record.Prescription}");
                Console.WriteLine($"Date: {record.RecordDate:dd-MMM-yyyy}");
            }
        }

        static void CalculateBill()
        {
            Console.Write("\nEnter Appointment ID: ");
            int appointmentId = int.Parse(Console.ReadLine());

            var appointment = appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);
            if (appointment == null)
            {
                Console.WriteLine("❌ Appointment not found!");
                return;
            }

            Console.WriteLine("\n--- Bill Details ---");
            Console.WriteLine($"Patient: {appointment.Patient.Name}");
            Console.WriteLine($"Doctor: {appointment.Doctor.Name}");
            Console.WriteLine($"Consultation Fee: ₹{appointment.Doctor.ConsultationFee:F2}");
            Console.WriteLine($"Additional Charges: ₹{appointment.AdditionalCharges:F2}");
            Console.WriteLine($"Total Bill: ₹{appointment.CalculateBill():F2}");
        }

        static void ShowLinqReports()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║   LINQ REPORTS                         ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            // 1. Doctors with more than 10 appointments
            Console.WriteLine("\n1. Doctors with more than 1 appointment:");
            var busyDoctors = appointments
                .GroupBy(a => a.Doctor.Name)
                .Where(g => g.Count() > 1)
                .Select(g => new { Doctor = g.Key, Count = g.Count() });
            foreach (var doc in busyDoctors)
            {
                Console.WriteLine($"   {doc.Doctor}: {doc.Count} appointments");
            }

            // 2. Patients treated in last 30 days
            Console.WriteLine("\n2. Patients treated in last 30 days:");
            var recentPatients = appointments
                .Where(a => a.AppointmentDateTime >= DateTime.Now.AddDays(-30))
                .Select(a => a.Patient.Name)
                .Distinct();
            foreach (var patient in recentPatients)
            {
                Console.WriteLine($"   {patient}");
            }

            // 3. Appointments grouped by doctor
            Console.WriteLine("\n3. Appointments Grouped by Doctor:");
            var groupedAppointments = appointments.GroupBy(a => a.Doctor.Name);
            foreach (var group in groupedAppointments)
            {
                Console.WriteLine($"   {group.Key}: {group.Count()} appointments");
            }

            // 4. Top 3 highest earning doctors
            Console.WriteLine("\n4. Top 3 Highest Earning Doctors:");
            var topEarningDoctors = appointments
                .GroupBy(a => a.Doctor.Name)
                .Select(g => new { Doctor = g.Key, TotalEarnings = g.Sum(a => a.CalculateBill()) })
                .OrderByDescending(x => x.TotalEarnings)
                .Take(3);
            foreach (var doc in topEarningDoctors)
            {
                Console.WriteLine($"   {doc.Doctor}: ₹{doc.TotalEarnings:F2}");
            }

            // 5. Patients by disease
            Console.WriteLine("\n5. Patients Grouped by Disease:");
            var patientsByDisease = patients.GroupBy(p => p.Disease);
            foreach (var group in patientsByDisease)
            {
                Console.WriteLine($"   {group.Key}: {group.Count()} patients");
            }

            // 6. Total revenue
            decimal totalRevenue = appointments.Sum(a => a.CalculateBill());
            Console.WriteLine($"\n6. Total Hospital Revenue: ₹{totalRevenue:F2}");
        }
    }
}
