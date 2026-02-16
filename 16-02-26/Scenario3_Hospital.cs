using System;
using System.Collections.Generic;
using System.Linq;

namespace Generics_Assessment.Scenario3
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

        /// <summary>
        /// Enqueues patient with priority (1=highest, 5=lowest)
        /// </summary>
        public void Enqueue(T patient, int priority)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            // Validate priority range
            if (priority < 1 || priority > 5)
                throw new ArgumentException("Priority must be between 1 and 5", nameof(priority));

            // Create queue if doesn't exist for priority
            if (!_queues.ContainsKey(priority))
            {
                _queues[priority] = new Queue<T>();
            }

            _queues[priority].Enqueue(patient);
            Console.WriteLine($"✓ Enqueued {patient.Name} with priority {priority}");
        }

        /// <summary>
        /// Dequeues highest priority patient
        /// </summary>
        public T Dequeue()
        {
            // Return patient from highest non-empty priority
            foreach (var kvp in _queues)
            {
                if (kvp.Value.Count > 0)
                {
                    var patient = kvp.Value.Dequeue();
                    Console.WriteLine($"✓ Dequeued {patient.Name} from priority {kvp.Key}");
                    return patient;
                }
            }

            // Throw if empty
            throw new InvalidOperationException("Queue is empty");
        }

        /// <summary>
        /// Peeks at next patient without removing
        /// </summary>
        public T Peek()
        {
            foreach (var kvp in _queues)
            {
                if (kvp.Value.Count > 0)
                {
                    return kvp.Value.Peek();
                }
            }

            throw new InvalidOperationException("Queue is empty");
        }

        /// <summary>
        /// Gets count by priority
        /// </summary>
        public int GetCountByPriority(int priority)
        {
            if (priority < 1 || priority > 5)
                throw new ArgumentException("Priority must be between 1 and 5");

            return _queues.ContainsKey(priority) ? _queues[priority].Count : 0;
        }

        /// <summary>
        /// Gets total count
        /// </summary>
        public int TotalCount => _queues.Sum(kvp => kvp.Value.Count);

        /// <summary>
        /// Checks if queue is empty
        /// </summary>
        public bool IsEmpty => TotalCount == 0;
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

        /// <summary>
        /// Adds diagnosis with date
        /// </summary>
        public void AddDiagnosis(string diagnosis, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(diagnosis))
                throw new ArgumentException("Diagnosis cannot be empty", nameof(diagnosis));

            if (date > DateTime.Now)
                throw new ArgumentException("Diagnosis date cannot be in the future");

            _diagnoses.Add((diagnosis, date));
            Console.WriteLine($"✓ Added diagnosis for {_patient.Name}: {diagnosis} on {date:yyyy-MM-dd}");
        }

        /// <summary>
        /// Adds treatment
        /// </summary>
        public void AddTreatment(string treatment, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(treatment))
                throw new ArgumentException("Treatment cannot be empty", nameof(treatment));

            if (date > DateTime.Now)
                throw new ArgumentException("Treatment date cannot be in the future");

            _treatments[date] = treatment;
            Console.WriteLine($"✓ Added treatment for {_patient.Name}: {treatment} on {date:yyyy-MM-dd}");
        }

        /// <summary>
        /// Gets treatment history sorted by date
        /// </summary>
        public IEnumerable<KeyValuePair<DateTime, string>> GetTreatmentHistory()
        {
            return _treatments.OrderBy(kvp => kvp.Key);
        }

        /// <summary>
        /// Gets all diagnoses
        /// </summary>
        public IReadOnlyList<(string diagnosis, DateTime date)> GetDiagnoses()
        {
            return _diagnoses.OrderBy(d => d.date).ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets patient age
        /// </summary>
        public int GetPatientAge()
        {
            var today = DateTime.Today;
            var age = today.Year - _patient.DateOfBirth.Year;
            if (_patient.DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }

    // 3. Specialized patient types
    public class PediatricPatient : IPatient
    {
        public int PatientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public BloodType BloodType { get; set; }
        public string GuardianName { get; set; } = string.Empty;
        public double Weight { get; set; } // in kg

        public override string ToString()
        {
            return $"[Pediatric] {Name} (ID: {PatientId}, Guardian: {GuardianName}, Weight: {Weight}kg)";
        }
    }

    public class GeriatricPatient : IPatient
    {
        public int PatientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public BloodType BloodType { get; set; }
        public List<string> ChronicConditions { get; } = new();
        public int MobilityScore { get; set; } // 1-10

        public override string ToString()
        {
            return $"[Geriatric] {Name} (ID: {PatientId}, Mobility: {MobilityScore}/10, " +
                   $"Conditions: {ChronicConditions.Count})";
        }
    }

    public class AdultPatient : IPatient
    {
        public int PatientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public BloodType BloodType { get; set; }
        public string EmergencyContact { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"[Adult] {Name} (ID: {PatientId}, Emergency: {EmergencyContact})";
        }
    }

    // 4. Generic medication system
    public class MedicationSystem<T> where T : IPatient
    {
        private Dictionary<T, List<(string medication, DateTime time, string dosage)>> _medications = new();

        /// <summary>
        /// Prescribes medication with dosage validation
        /// </summary>
        public void PrescribeMedication(T patient, string medication, string dosage,
            Func<T, bool> dosageValidator)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            if (string.IsNullOrWhiteSpace(medication))
                throw new ArgumentException("Medication name cannot be empty");

            if (dosageValidator == null)
                throw new ArgumentNullException(nameof(dosageValidator));

            // Check if dosage is valid for patient type
            if (!dosageValidator(patient))
            {
                Console.WriteLine($"✗ Dosage validation failed for {patient.Name}");
                throw new InvalidOperationException(
                    $"Dosage {dosage} is not appropriate for patient type");
            }

            // Pediatric: weight-based validation
            if (patient is PediatricPatient pediatric)
            {
                if (pediatric.Weight < 10)
                {
                    Console.WriteLine($"⚠ Warning: Low weight patient ({pediatric.Weight}kg). " +
                                    $"Reduced dosage recommended.");
                }
            }

            // Geriatric: kidney function consideration
            if (patient is GeriatricPatient geriatric)
            {
                if (geriatric.ChronicConditions.Any(c => c.Contains("Kidney") || c.Contains("Renal")))
                {
                    Console.WriteLine($"⚠ Warning: Patient has kidney conditions. " +
                                    $"Dosage adjustment may be required.");
                }
            }

            if (!_medications.ContainsKey(patient))
            {
                _medications[patient] = new List<(string, DateTime, string)>();
            }

            _medications[patient].Add((medication, DateTime.Now, dosage));
            Console.WriteLine($"✓ Prescribed {medication} ({dosage}) to {patient.Name}");
        }

        /// <summary>
        /// Checks for drug interactions
        /// </summary>
        public bool CheckInteractions(T patient, string newMedication)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            if (!_medications.ContainsKey(patient))
                return false; // No existing medications

            var existingMedications = _medications[patient].Select(m => m.medication).ToList();

            // Simple interaction check (in real system, would use drug database)
            var knownInteractions = new Dictionary<string, List<string>>
            {
                ["Warfarin"] = new List<string> { "Aspirin", "Ibuprofen" },
                ["Aspirin"] = new List<string> { "Warfarin", "Heparin" },
                ["Metformin"] = new List<string> { "Alcohol", "Contrast Dye" }
            };

            if (knownInteractions.ContainsKey(newMedication))
            {
                var interactions = knownInteractions[newMedication];
                var conflictingMeds = existingMedications.Intersect(interactions).ToList();

                if (conflictingMeds.Any())
                {
                    Console.WriteLine($"⚠ WARNING: {newMedication} interacts with: " +
                                    $"{string.Join(", ", conflictingMeds)}");
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets all medications for a patient
        /// </summary>
        public IReadOnlyList<(string medication, DateTime time, string dosage)> GetMedications(T patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            return _medications.ContainsKey(patient)
                ? _medications[patient].AsReadOnly()
                : new List<(string, DateTime, string)>().AsReadOnly();
        }
    }

    // Demo class for Scenario 3
    public static class Scenario3Demo
    {
        public static void Run()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   SCENARIO 3: Hospital Patient Management System          ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");

            try
            {
                // a) Create 2 PediatricPatient and 2 GeriatricPatient
                Console.WriteLine("\n--- Creating Patients ---");

                var pediatricPatients = new List<PediatricPatient>
                {
                    new PediatricPatient
                    {
                        PatientId = 2001,
                        Name = "Emma Wilson",
                        DateOfBirth = new DateTime(2018, 5, 15),
                        BloodType = BloodType.A,
                        GuardianName = "Sarah Wilson",
                        Weight = 18.5
                    },
                    new PediatricPatient
                    {
                        PatientId = 2002,
                        Name = "Liam Chen",
                        DateOfBirth = new DateTime(2020, 8, 22),
                        BloodType = BloodType.O,
                        GuardianName = "Michael Chen",
                        Weight = 12.3
                    }
                };

                var geriatricPatients = new List<GeriatricPatient>
                {
                    new GeriatricPatient
                    {
                        PatientId = 3001,
                        Name = "Margaret Thompson",
                        DateOfBirth = new DateTime(1945, 3, 10),
                        BloodType = BloodType.B,
                        MobilityScore = 6
                    },
                    new GeriatricPatient
                    {
                        PatientId = 3002,
                        Name = "Robert Davis",
                        DateOfBirth = new DateTime(1938, 11, 25),
                        BloodType = BloodType.AB,
                        MobilityScore = 4
                    }
                };

                geriatricPatients[0].ChronicConditions.Add("Hypertension");
                geriatricPatients[0].ChronicConditions.Add("Diabetes Type 2");
                geriatricPatients[1].ChronicConditions.Add("Chronic Kidney Disease");
                geriatricPatients[1].ChronicConditions.Add("Arthritis");

                foreach (var patient in pediatricPatients)
                {
                    Console.WriteLine($"  {patient}");
                }
                foreach (var patient in geriatricPatients)
                {
                    Console.WriteLine($"  {patient}");
                }

                // b) Add them to priority queue with different priorities
                Console.WriteLine("\n--- Priority Queue Management ---");
                var queue = new PriorityQueue<IPatient>();

                // Critical cases get priority 1, stable cases get priority 3-5
                queue.Enqueue(geriatricPatients[1], 1); // Critical - kidney disease
                queue.Enqueue(pediatricPatients[0], 2);  // High priority - child
                queue.Enqueue(geriatricPatients[0], 3);  // Medium - stable chronic
                queue.Enqueue(pediatricPatients[1], 2);  // High priority - child

                Console.WriteLine($"\nTotal patients in queue: {queue.TotalCount}");
                for (int priority = 1; priority <= 5; priority++)
                {
                    int count = queue.GetCountByPriority(priority);
                    if (count > 0)
                    {
                        Console.WriteLine($"  Priority {priority}: {count} patient(s)");
                    }
                }

                // c) Create medical records with diagnoses/treatments
                Console.WriteLine("\n--- Medical Records ---");

                var record1 = new MedicalRecord<PediatricPatient>(pediatricPatients[0]);
                record1.AddDiagnosis("Acute Bronchitis", DateTime.Now.AddDays(-3));
                record1.AddTreatment("Nebulizer therapy", DateTime.Now.AddDays(-3));
                record1.AddTreatment("Antibiotics prescribed", DateTime.Now.AddDays(-2));

                var record2 = new MedicalRecord<GeriatricPatient>(geriatricPatients[1]);
                record2.AddDiagnosis("Chronic Kidney Disease Stage 3", DateTime.Now.AddMonths(-6));
                record2.AddDiagnosis("Acute Kidney Injury", DateTime.Now.AddDays(-1));
                record2.AddTreatment("IV Fluids", DateTime.Now.AddDays(-1));
                record2.AddTreatment("Dialysis consultation", DateTime.Now);

                Console.WriteLine($"\n{pediatricPatients[0].Name} - Age: {record1.GetPatientAge()}");
                Console.WriteLine("Diagnoses:");
                foreach (var diagnosis in record1.GetDiagnoses())
                {
                    Console.WriteLine($"  - {diagnosis.diagnosis} ({diagnosis.date:yyyy-MM-dd})");
                }
                Console.WriteLine("Treatment History:");
                foreach (var treatment in record1.GetTreatmentHistory())
                {
                    Console.WriteLine($"  - {treatment.Value} ({treatment.Key:yyyy-MM-dd})");
                }

                Console.WriteLine($"\n{geriatricPatients[1].Name} - Age: {record2.GetPatientAge()}");
                Console.WriteLine("Diagnoses:");
                foreach (var diagnosis in record2.GetDiagnoses())
                {
                    Console.WriteLine($"  - {diagnosis.diagnosis} ({diagnosis.date:yyyy-MM-dd})");
                }
                Console.WriteLine("Treatment History:");
                foreach (var treatment in record2.GetTreatmentHistory())
                {
                    Console.WriteLine($"  - {treatment.Value} ({treatment.Key:yyyy-MM-dd})");
                }

                // d) Prescribe medications with type-specific validation
                Console.WriteLine("\n--- Medication Management ---");

                var medicationSystem = new MedicationSystem<IPatient>();

                // Pediatric: weight-based validation
                Func<IPatient, bool> pediatricValidator = (patient) =>
                {
                    if (patient is PediatricPatient ped)
                    {
                        // Dosage should be appropriate for weight
                        return ped.Weight >= 10; // Minimum weight for standard dosage
                    }
                    return false;
                };

                // Geriatric: kidney function consideration
                Func<IPatient, bool> geriatricValidator = (patient) =>
                {
                    if (patient is GeriatricPatient ger)
                    {
                        // Check if patient has kidney issues
                        bool hasKidneyIssues = ger.ChronicConditions
                            .Any(c => c.Contains("Kidney") || c.Contains("Renal"));
                        return !hasKidneyIssues; // Simplified - would need adjusted dosage
                    }
                    return false;
                };

                // General validator
                Func<IPatient, bool> generalValidator = (patient) => true;

                medicationSystem.PrescribeMedication(
                    pediatricPatients[0],
                    "Amoxicillin",
                    "250mg twice daily",
                    pediatricValidator);

                medicationSystem.PrescribeMedication(
                    geriatricPatients[0],
                    "Metformin",
                    "500mg daily",
                    generalValidator);

                // This should show warning due to kidney condition
                try
                {
                    medicationSystem.PrescribeMedication(
                        geriatricPatients[1],
                        "Ibuprofen",
                        "400mg as needed",
                        geriatricValidator);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"✗ {ex.Message}");
                }

                // e) Drug interaction checking
                Console.WriteLine("\n--- Drug Interaction Checks ---");

                medicationSystem.PrescribeMedication(
                    geriatricPatients[0],
                    "Warfarin",
                    "5mg daily",
                    generalValidator);

                bool hasInteraction = medicationSystem.CheckInteractions(
                    geriatricPatients[0],
                    "Aspirin");

                if (hasInteraction)
                {
                    Console.WriteLine("✗ Cannot prescribe Aspirin due to interaction with Warfarin");
                }

                // f) Priority-based patient processing
                Console.WriteLine("\n--- Processing Patients by Priority ---");
                int processedCount = 0;
                while (!queue.IsEmpty && processedCount < 3)
                {
                    var patient = queue.Dequeue();
                    Console.WriteLine($"Processing: {patient.Name} (ID: {patient.PatientId})");

                    var meds = medicationSystem.GetMedications(patient);
                    if (meds.Count > 0)
                    {
                        Console.WriteLine("  Current Medications:");
                        foreach (var med in meds)
                        {
                            Console.WriteLine($"    - {med.medication} ({med.dosage})");
                        }
                    }
                    processedCount++;
                }

                Console.WriteLine($"\nRemaining patients in queue: {queue.TotalCount}");

                Console.WriteLine("\n✓ Scenario 3 completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error in Scenario 3: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
