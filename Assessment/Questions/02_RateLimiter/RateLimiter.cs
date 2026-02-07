using System.Collections.Concurrent;

namespace RateLimiterSystem
{
    public class RateLimiter
    {
        private readonly ConcurrentDictionary<string, Queue<DateTime>> _clientRequests = new();
        private readonly int _maxRequests;
        private readonly TimeSpan _timeWindow;
        private readonly object _lock = new();

        public RateLimiter(int maxRequests = 5, int windowSeconds = 10)
        {
            _maxRequests = maxRequests;
            _timeWindow = TimeSpan.FromSeconds(windowSeconds);
        }

        public bool AllowRequest(string clientId, DateTime now)
        {
            lock (_lock)
            {
                if (!_clientRequests.ContainsKey(clientId))
                {
                    _clientRequests[clientId] = new Queue<DateTime>();
                }

                var requests = _clientRequests[clientId];
                
                // Remove requests outside the sliding window
                while (requests.Count > 0 && (now - requests.Peek()) > _timeWindow)
                {
                    requests.Dequeue();
                }

                if (requests.Count >= _maxRequests)
                {
                    return false;
                }

                requests.Enqueue(now);
                return true;
            }
        }
    }
}
