using System;
using System.Collections.Generic;
using System.Linq;

namespace Scenario3_Hospital
{
    public interface IPatient
    {
        int PatientId { get; }
        string Name { get; }
        DateTime DateOfBirth { get; }
        BloodType BloodType { get; }
    }

    public enum BloodType { A, B, AB, O }
    public enum Condition { Stable, Critical, Recovering }

    // 1. Generic patient queue with priority
    public class PriorityQueue<T> where T : IPatient
    {
        private SortedDictionary<int, Queue<T>> _queues = new();

        public void Enqueue(T patient, int priority)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            if (priority < 1 || priority > 5)
                throw new ArgumentException("Priority must be between 1 (highest) and 5 (lowest)");

            if (!_queues.ContainsKey(priority))
            {
                _queues[priority] = new Queue<T>();
            }

            _queues[priority].Enqueue(patient);
            Console.WriteLine($"Enqueued: {patient.Name} with priority {priority}");
        }

        public T Dequeue()
        {
            if (_queues.Count == 0 || _queues.All(kvp => kvp.Value.Count == 0))
                throw new InvalidOperationException("Queue is empty");

            // Get highest priority (lowest number) non-empty queue
            var highestPriorityQueue = _queues.First(kvp => kvp.Value.Count > 0);
            var patient = highestPriorityQueue.Value.Dequeue();

            Console.WriteLine($"Dequeued: {patient.Name} from priority {highestPriorityQueue.Key}");
            return patient;
        }

        public T Peek()
        {
            if (_queues.Count == 0 || _queues.All(kvp => kvp.Value.Count == 0))
                throw new InvalidOperationException("Queue is empty");

            var highestPriorityQueue = _queues.First(kvp => kvp.Value.Count > 0);
            return highestPriorityQueue.Value.Peek();
        }

        public int GetCountByPriority(int priority)
        {
            if (priority < 1 || priority > 5)
                throw new ArgumentException("Priority must be between 1 and 5");

            return _queues.ContainsKey(priority) ? _queues[priority].Count : 0;
        }

        public int TotalCount => _queues.Sum(kvp => kvp.Value.Count);
    }

    // 2. Generic medical record
    public class MedicalRecord<T> where T : IPatient
    {
        private T _patient;
        private List<(string diagnosis, DateTime date)> _diagnoses = new();
        private Dictionary<DateTime, string> _treatments = new();

        public MedicalRecord(T patient)
        {
            _patient = patient ?? throw new ArgumentNullException(nameof(patient));
        }

        public T Patient => _patient;

        public void AddDiagnosis(string diagnosis, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(diagnosis))
                throw new ArgumentException("Diagnosis cannot be empty");

            _diagnoses.Add((diagnosis, date));
            Console.WriteLine($"Added diagnosis for {_patient.Name}: {diagnosis} on {date.ToShortDateString()}");
        }

        public void AddTreatment(string treatment, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(treatment))
                throw new ArgumentException("Treatment cannot be empty");

            _treatments[date] = treatment;
            Console.WriteLine($"Added treatment for {_patient.Name}: {treatment} on {date.ToShortDateString()}");
        }

        public IEnumerable<KeyValuePair<DateTime, string>> GetTreatmentHistory()
        {
            return _treatments.OrderBy(kvp => kvp.Key);
        }

        public IEnumerable<(string diagnosis, DateTime date)> GetDiagnoses()
        {
            return _diagnoses.OrderBy(d => d.date);
        }
    }

    // 3. Specialized patient types
    public class PediatricPatient : IPatient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public BloodType BloodType { get; set; }
        public string GuardianName { get; set; }
        public double Weight { get; set; } // in kg

        public int AgeInYears => (DateTime.Now - DateOfBirth).Days / 365;
    }

    public class GeriatricPatient : IPatient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public BloodType BloodType { get; set; }
        public List<string> ChronicConditions { get; } = new();
        public int MobilityScore { get; set; } // 1-10

        public int AgeInYears => (DateTime.Now - DateOfBirth).Days / 365;
    }

    // 4. Generic medication system
    public class MedicationSystem<T> where T : IPatient
    {
        private Dictionary<T, List<(string medication, DateTime time)>> _medications = new();

        public void PrescribeMedication(T patient, string medication, Func<T, bool> dosageValidator)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));
            if (string.IsNullOrWhiteSpace(medication))
                throw new ArgumentException("Medication name cannot be empty");
            if (dosageValidator == null)
                throw new ArgumentNullException(nameof(dosageValidator));

            // Validate dosage
            if (!dosageValidator(patient))
            {
                Console.WriteLine($"Prescription failed: Dosage validation failed for {patient.Name}");
                return;
            }

            if (!_medications.ContainsKey(patient))
            {
                _medications[patient] = new List<(string, DateTime)>();
            }

            _medications[patient].Add((medication, DateTime.Now));
            Console.WriteLine($"Prescribed {medication} to {patient.Name}");
        }

        public bool CheckInteractions(T patient, string newMedication)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));
            if (string.IsNullOrWhiteSpace(newMedication))
                throw new ArgumentException("Medication name cannot be empty");

            if (!_medications.ContainsKey(patient))
                return false;

            // Simple interaction check (in real system, would use drug database)
            var existingMedications = _medications[patient].Select(m => m.medication).ToList();

            // Example: Check for known interactions
            var knownInteractions = new Dictionary<string, List<string>>
            {
                { "Aspirin", new List<string> { "Warfarin", "Ibuprofen" } },
                { "Warfarin", new List<string> { "Aspirin", "Vitamin K" } }
            };

            if (knownInteractions.ContainsKey(newMedication))
            {
                var interactions = knownInteractions[newMedication];
                foreach (var existing in existingMedications)
                {
                    if (interactions.Contains(existing))
                    {
                        Console.WriteLine($"Warning: Interaction detected between {newMedication} and {existing}");
                        return true;
                    }
                }
            }

            return false;
        }

        public IEnumerable<(string medication, DateTime time)> GetPatientMedications(T patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            return _medications.ContainsKey(patient)
                ? _medications[patient].OrderBy(m => m.time)
                : Enumerable.Empty<(string, DateTime)>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Hospital Patient Management System Demo ===\n");

            try
            {
                // Create patients
                var pediatric1 = new PediatricPatient
                {
                    PatientId = 1,
                    Name = "Emma Wilson",
                    DateOfBirth = new DateTime(2018, 5, 15),
                    BloodType = BloodType.A,
                    GuardianName = "Sarah Wilson",
                    Weight = 20.5
                };

                var pediatric2 = new PediatricPatient
                {
                    PatientId = 2,
                    Name = "Liam Johnson",
                    DateOfBirth = new DateTime(2020, 8, 22),
                    BloodType = BloodType.O,
                    GuardianName = "Michael Johnson",
                    Weight = 15.2
                };

                var geriatric1 = new GeriatricPatient
                {
                    PatientId = 3,
                    Name = "Robert Davis",
                    DateOfBirth = new DateTime(1945, 3, 10),
                    BloodType = BloodType.B,
                    MobilityScore = 6
                };
                geriatric1.ChronicConditions.Add("Hypertension");
                geriatric1.ChronicConditions.Add("Diabetes");

                var geriatric2 = new GeriatricPatient
                {
                    PatientId = 4,
                    Name = "Margaret Smith",
                    DateOfBirth = new DateTime(1950, 11, 25),
                    BloodType = BloodType.AB,
                    MobilityScore = 8
                };
                geriatric2.ChronicConditions.Add("Arthritis");

                // Priority Queue Demo
                Console.WriteLine("=== Priority Queue Demo ===\n");
                var queue = new PriorityQueue<IPatient>();

                queue.Enqueue(pediatric1, 2);
                queue.Enqueue(geriatric1, 1); // Critical
                queue.Enqueue(pediatric2, 3);
                queue.Enqueue(geriatric2, 2);

                Console.WriteLine($"\nTotal patients in queue: {queue.TotalCount}");
                Console.WriteLine($"Priority 1 count: {queue.GetCountByPriority(1)}");
                Console.WriteLine($"Priority 2 count: {queue.GetCountByPriority(2)}");

                Console.WriteLine("\n=== Processing Patients by Priority ===");
                while (queue.TotalCount > 0)
                {
                    var patient = queue.Dequeue();
                    Console.WriteLine($"Processing: {patient.Name}");
                }

                // Medical Records Demo
                Console.WriteLine("\n=== Medical Records Demo ===\n");
                var record1 = new MedicalRecord<PediatricPatient>(pediatric1);
                record1.AddDiagnosis("Common Cold", DateTime.Now.AddDays(-5));
                record1.AddTreatment("Rest and fluids", DateTime.Now.AddDays(-5));
                record1.AddTreatment("Acetaminophen", DateTime.Now.AddDays(-3));

                var record2 = new MedicalRecord<GeriatricPatient>(geriatric1);
                record2.AddDiagnosis("Hypertension", DateTime.Now.AddYears(-2));
                record2.AddDiagnosis("Type 2 Diabetes", DateTime.Now.AddYears(-1));
                record2.AddTreatment("Lisinopril", DateTime.Now.AddYears(-2));
                record2.AddTreatment("Metformin", DateTime.Now.AddYears(-1));

                Console.WriteLine($"\n=== Treatment History for {record1.Patient.Name} ===");
                foreach (var treatment in record1.GetTreatmentHistory())
                {
                    Console.WriteLine($"{treatment.Key.ToShortDateString()}: {treatment.Value}");
                }

                Console.WriteLine($"\n=== Treatment History for {record2.Patient.Name} ===");
                foreach (var treatment in record2.GetTreatmentHistory())
                {
                    Console.WriteLine($"{treatment.Key.ToShortDateString()}: {treatment.Value}");
                }

                // Medication System Demo
                Console.WriteLine("\n=== Medication System Demo ===\n");
                var medSystem = new MedicationSystem<IPatient>();

                // Pediatric dosage validator (weight-based)
                Func<IPatient, bool> pediatricValidator = (patient) =>
                {
                    if (patient is PediatricPatient ped)
                    {
                        return ped.Weight >= 10; // Minimum weight for medication
                    }
                    return true;
                };

                // Geriatric dosage validator (mobility-based)
                Func<IPatient, bool> geriatricValidator = (patient) =>
                {
                    if (patient is GeriatricPatient ger)
                    {
                        return ger.MobilityScore >= 5; // Minimum mobility score
                    }
                    return true;
                };

                Console.WriteLine("=== Prescribing Medications ===");
                medSystem.PrescribeMedication(pediatric1, "Amoxicillin", pediatricValidator);
                medSystem.PrescribeMedication(geriatric1, "Aspirin", geriatricValidator);
                medSystem.PrescribeMedication(geriatric1, "Lisinopril", geriatricValidator);

                Console.WriteLine("\n=== Checking Drug Interactions ===");
                bool hasInteraction = medSystem.CheckInteractions(geriatric1, "Warfarin");
                Console.WriteLine($"Interaction with Warfarin: {hasInteraction}");

                hasInteraction = medSystem.CheckInteractions(geriatric1, "Metformin");
                Console.WriteLine($"Interaction with Metformin: {hasInteraction}");

                Console.WriteLine($"\n=== Medications for {geriatric1.Name} ===");
                foreach (var med in medSystem.GetPatientMedications(geriatric1))
                {
                    Console.WriteLine($"{med.time.ToShortDateString()}: {med.medication}");
                }

                Console.WriteLine("\n=== Demo Completed Successfully ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }
    }
}
