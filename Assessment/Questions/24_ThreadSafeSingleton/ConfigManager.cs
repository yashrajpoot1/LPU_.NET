namespace ThreadSafeSingleton
{
    public sealed class ConfigManager
    {
        private static readonly Lazy<ConfigManager> _instance = 
            new Lazy<ConfigManager>(() => new ConfigManager());

        private readonly Dictionary<string, string> _settings = new();
        private readonly object _lock = new();

        private ConfigManager()
        {
            _settings["AppName"] = "MyApp";
            _settings["Version"] = "1.0.0";
        }

        public static ConfigManager Instance => _instance.Value;

        public string? GetSetting(string key)
        {
            lock (_lock)
            {
                return _settings.TryGetValue(key, out var value) ? value : null;
            }
        }

        public void SetSetting(string key, string value)
        {
            lock (_lock)
            {
                _settings[key] = value;
            }
        }
    }
}
