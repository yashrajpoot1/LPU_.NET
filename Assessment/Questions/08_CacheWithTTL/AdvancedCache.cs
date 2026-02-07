namespace CacheWithTTL
{
    public class CacheItem<TValue>
    {
        public TValue Value { get; set; }
        public DateTime ExpiryTime { get; set; }
        public DateTime LastAccessed { get; set; }

        public CacheItem(TValue value, DateTime expiryTime)
        {
            Value = value;
            ExpiryTime = expiryTime;
            LastAccessed = DateTime.UtcNow;
        }
    }

    public class AdvancedCache<TKey, TValue> where TKey : notnull
    {
        private readonly Dictionary<TKey, CacheItem<TValue>> _cache = new();
        private readonly int _capacity;
        private readonly object _lock = new();

        public AdvancedCache(int capacity)
        {
            _capacity = capacity;
        }

        public void Set(TKey key, TValue value, int ttlSeconds)
        {
            lock (_lock)
            {
                var expiryTime = DateTime.UtcNow.AddSeconds(ttlSeconds);
                
                if (_cache.ContainsKey(key))
                {
                    _cache[key] = new CacheItem<TValue>(value, expiryTime);
                }
                else
                {
                    if (_cache.Count >= _capacity)
                    {
                        EvictLRU();
                    }
                    _cache[key] = new CacheItem<TValue>(value, expiryTime);
                }
            }
        }

        public (bool found, TValue? value) Get(TKey key)
        {
            lock (_lock)
            {
                if (!_cache.TryGetValue(key, out var item))
                {
                    return (false, default);
                }

                if (DateTime.UtcNow > item.ExpiryTime)
                {
                    _cache.Remove(key);
                    return (false, default);
                }

                item.LastAccessed = DateTime.UtcNow;
                return (true, item.Value);
            }
        }

        private void EvictLRU()
        {
            var lruKey = _cache
                .OrderBy(kvp => kvp.Value.LastAccessed)
                .First()
                .Key;
            
            _cache.Remove(lruKey);
        }
    }
}
