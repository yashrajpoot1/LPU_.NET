namespace HospitalEmergencyQueue;

public class EmergencyException : Exception
{
    public EmergencyException(string message) : base(message) { }
}

public class InvalidSeverityException : EmergencyException
{
    public int Severity { get; }

    public InvalidSeverityException(int severity)
        : base($"Invalid severity level: {severity}. Must be between 1 (Critical) and 5 (Non-urgent)")
    {
        Severity = severity;
    }
}

public class PatientNotFoundException : EmergencyException
{
    public PatientNotFoundException(string patientId)
        : base($"Patient with ID {patientId} not found in queue") { }
}

public class QueueOverflowException : EmergencyException
{
    public int MaxSize { get; }

    public QueueOverflowException(int maxSize)
        : base($"Emergency queue is full. Maximum capacity: {maxSize}")
    {
        MaxSize = maxSize;
    }
}
