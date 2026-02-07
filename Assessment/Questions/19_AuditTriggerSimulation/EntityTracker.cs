namespace AuditTriggerSimulation
{
    public class AuditEntry
    {
        public string EntityId { get; set; } = string.Empty;
        public string PropertyName { get; set; } = string.Empty;
        public object? OldValue { get; set; }
        public object? NewValue { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class EntityTracker<T> where T : class
    {
        private readonly Dictionary<string, Dictionary<string, object?>> _snapshots = new();
        private readonly List<AuditEntry> _auditLog = new();

        public void TrackEntity(string entityId, T entity)
        {
            var properties = typeof(T).GetProperties();
            var snapshot = new Dictionary<string, object?>();

            foreach (var prop in properties)
            {
                snapshot[prop.Name] = prop.GetValue(entity);
            }

            _snapshots[entityId] = snapshot;
        }

        public List<AuditEntry> DetectChanges(string entityId, T entity)
        {
            if (!_snapshots.ContainsKey(entityId))
            {
                TrackEntity(entityId, entity);
                return new List<AuditEntry>();
            }

            var changes = new List<AuditEntry>();
            var oldSnapshot = _snapshots[entityId];
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                var newValue = prop.GetValue(entity);
                var oldValue = oldSnapshot[prop.Name];

                if (!Equals(oldValue, newValue))
                {
                    var auditEntry = new AuditEntry
                    {
                        EntityId = entityId,
                        PropertyName = prop.Name,
                        OldValue = oldValue,
                        NewValue = newValue
                    };

                    changes.Add(auditEntry);
                    _auditLog.Add(auditEntry);
                }
            }

            TrackEntity(entityId, entity);
            return changes;
        }

        public List<AuditEntry> GetAuditLog() => _auditLog;
    }
}
