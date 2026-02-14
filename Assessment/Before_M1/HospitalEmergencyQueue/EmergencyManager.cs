namespace HospitalEmergencyQueue;

public class EmergencyManager
{
    private readonly SortedDictionary<int, Queue<Patient>> _emergencyQueue = new();
    private readonly Dictionary<string, (int severity, Patient patient)> _patientLookup = new();
    private readonly int _maxQueueSize;
    private int _totalPatients = 0;

    public EmergencyManager(int maxQueueSize = 100)
    {
        _maxQueueSize = maxQueueSize;
    }

    public void AddPatient(Patient patient)
    {
        if (_totalPatients >= _maxQueueSize)
            throw new QueueOverflowException(_maxQueueSize);

        if (_patientLookup.ContainsKey(patient.Id))
            throw new PatientNotFoundException($"Patient {patient.Id} already in queue");

        if (!_emergencyQueue.ContainsKey(patient.Severity))
            _emergencyQueue[patient.Severity] = new Queue<Patient>();

        _emergencyQueue[patient.Severity].Enqueue(patient);
        _patientLookup[patient.Id] = (patient.Severity, patient);
        _totalPatients++;

        Console.WriteLine($"✓ Added patient: {patient.Name} (Severity: {patient.Severity})");
    }

    public Patient? GetNextPatient()
    {
        foreach (var kvp in _emergencyQueue)
        {
            if (kvp.Value.Count > 0)
            {
                var patient = kvp.Value.Dequeue();
                _patientLookup.Remove(patient.Id);
                _totalPatients--;

                if (kvp.Value.Count == 0)
                    _emergencyQueue.Remove(kvp.Key);

                Console.WriteLine($"✓ Next patient: {patient.Name} (Severity: {patient.Severity})");
                return patient;
            }
        }
        Console.WriteLine("No patients in queue");
        return null;
    }

    public void RemovePatient(string patientId)
    {
        if (!_patientLookup.TryGetValue(patientId, out var info))
            throw new PatientNotFoundException(patientId);

        // Rebuild queue without the patient
        var severity = info.severity;
        var tempQueue = new Queue<Patient>();

        while (_emergencyQueue[severity].Count > 0)
        {
            var p = _emergencyQueue[severity].Dequeue();
            if (p.Id != patientId)
                tempQueue.Enqueue(p);
        }

        _emergencyQueue[severity] = tempQueue;
        if (_emergencyQueue[severity].Count == 0)
            _emergencyQueue.Remove(severity);

        _patientLookup.Remove(patientId);
        _totalPatients--;

        Console.WriteLine($"✓ Removed patient: {patientId}");
    }

    public void DisplayQueue()
    {
        Console.WriteLine("\n========== EMERGENCY QUEUE (By Severity) ==========");
        Console.WriteLine($"Total Patients: {_totalPatients}");

        foreach (var kvp in _emergencyQueue)
        {
            Console.WriteLine($"\n--- Severity {kvp.Key} ({kvp.Value.Count} patients) ---");
            foreach (var patient in kvp.Value)
            {
                patient.DisplayInfo();
            }
        }
    }

    public int GetTotalWaiting() => _totalPatients;
}
