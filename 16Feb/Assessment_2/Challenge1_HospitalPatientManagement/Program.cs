using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge1_HospitalPatientManagement
{
    // Task 1: Patient class with proper encapsulation
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Condition { get; set; }
        public List<string> MedicalHistory { get; set; }

        public Patient(int id, string name, int age, string condition)
        {
            Id = id;
            Name = name;
            Age = age;
            Condition = condition;
            MedicalHistory = new List<string>();
        }

        public void AddMedicalRecord(string record)
        {
            MedicalHistory.Add($"{DateTime.Now:yyyy-MM-dd}: {record}");
        }

        public override string ToString()
        {
            return $"[{Id}] {Name}, Age: {Age}, Condition: {Condition}";
        }
    }

    // Task 2: HospitalManager class
    public class HospitalManager
    {
        private Dictionary<int, Patient> _patients = new Dictionary<int, Patient>();
        private Queue<Patient> _appointmentQueue = new Queue<Patient>();

        // Add a new patient to the system
        public void RegisterPatient(int id, string name, int age, string condition)
        {
            if (_patients.ContainsKey(id))
            {
                Console.WriteLine($"Error: Patient with ID {id} already exists.");
                return;
            }

            var patient = new Patient(id, name, age, condition);
            _patients.Add(id, patient);
            Console.WriteLine($"Registered: {patient}");
        }

        // Add patient to appointment queue
        public void ScheduleAppointment(int patientId)
        {
            if (!_patients.ContainsKey(patientId))
            {
                Console.WriteLine($"Error: Patient with ID {patientId} not found.");
                return;
            }

            var patient = _patients[patientId];
            _appointmentQueue.Enqueue(patient);
            Console.WriteLine($"Scheduled appointment for: {patient.Name}");
        }

        // Process next appointment (remove from queue)
        public Patient ProcessNextAppointment()
        {
            if (_appointmentQueue.Count == 0)
            {
                Console.WriteLine("No appointments in queue.");
                return null;
            }

            var patient = _appointmentQueue.Dequeue();
            Console.WriteLine($"Processing appointment for: {patient.Name}");
            return patient;
        }

        // Find patients with specific condition using LINQ
        public List<Patient> FindPatientsByCondition(string condition)
        {
            return _patients.Values
                .Where(p => p.Condition.Equals(condition, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Additional methods
        public Patient GetPatient(int id)
        {
            return _patients.ContainsKey(id) ? _patients[id] : null;
        }

        public int GetAppointmentQueueCount()
        {
            return _appointmentQueue.Count;
        }

        public List<Patient> GetAllPatients()
        {
            return _patients.Values.ToList();
        }

        public List<Patient> GetPatientsByAgeRange(int minAge, int maxAge)
        {
            return _patients.Values
                .Where(p => p.Age >= minAge && p.Age <= maxAge)
                .OrderBy(p => p.Age)
                .ToList();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Hospital Patient Management System ===\n");

            HospitalManager manager = new HospitalManager();

            // Test Case 1: Register patients
            Console.WriteLine("--- Registering Patients ---");
            manager.RegisterPatient(1, "John Doe", 45, "Hypertension");
            manager.RegisterPatient(2, "Jane Smith", 32, "Diabetes");
            manager.RegisterPatient(3, "Bob Johnson", 58, "Hypertension");
            manager.RegisterPatient(4, "Alice Williams", 28, "Asthma");

            // Test Case 2: Schedule appointments
            Console.WriteLine("\n--- Scheduling Appointments ---");
            manager.ScheduleAppointment(1);
            manager.ScheduleAppointment(2);
            manager.ScheduleAppointment(3);

            Console.WriteLine($"Appointments in queue: {manager.GetAppointmentQueueCount()}");

            // Test Case 3: Process appointments
            Console.WriteLine("\n--- Processing Appointments ---");
            var nextPatient = manager.ProcessNextAppointment();
            Console.WriteLine($"Next patient processed: {nextPatient.Name}"); // Should output: John Doe

            // Test Case 4: Find patients by condition
            Console.WriteLine("\n--- Finding Patients by Condition ---");
            var diabeticPatients = manager.FindPatientsByCondition("Diabetes");
            Console.WriteLine($"Diabetic patients count: {diabeticPatients.Count}"); // Should output: 1
            foreach (var patient in diabeticPatients)
            {
                Console.WriteLine($"  - {patient}");
            }

            var hypertensionPatients = manager.FindPatientsByCondition("Hypertension");
            Console.WriteLine($"\nHypertension patients count: {hypertensionPatients.Count}"); // Should output: 2
            foreach (var patient in hypertensionPatients)
            {
                Console.WriteLine($"  - {patient}");
            }

            // Test Case 5: Medical history
            Console.WriteLine("\n--- Adding Medical Records ---");
            var patient1 = manager.GetPatient(1);
            patient1.AddMedicalRecord("Blood pressure check: 140/90");
            patient1.AddMedicalRecord("Prescribed medication: Lisinopril");
            
            Console.WriteLine($"\nMedical history for {patient1.Name}:");
            foreach (var record in patient1.MedicalHistory)
            {
                Console.WriteLine($"  {record}");
            }

            // Test Case 6: Age range query
            Console.WriteLine("\n--- Patients by Age Range (30-50) ---");
            var ageRangePatients = manager.GetPatientsByAgeRange(30, 50);
            foreach (var patient in ageRangePatients)
            {
                Console.WriteLine($"  - {patient}");
            }

            // Test Case 7: Process remaining appointments
            Console.WriteLine("\n--- Processing Remaining Appointments ---");
            while (manager.GetAppointmentQueueCount() > 0)
            {
                manager.ProcessNextAppointment();
            }

            Console.WriteLine("\n=== Demo Completed Successfully ===");
        }
    }
}
