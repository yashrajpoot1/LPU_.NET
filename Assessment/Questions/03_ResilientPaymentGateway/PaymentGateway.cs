namespace ResilientPaymentGateway
{
    public class PaymentGateway
    {
        private readonly Queue<DateTime> _failures = new();
        private DateTime? _circuitOpenedAt;
        private readonly object _lock = new();
        private const int MaxRetries = 3;
        private const int FailureThreshold = 5;
        private readonly TimeSpan _failureWindow = TimeSpan.FromMinutes(1);
        private readonly TimeSpan _circuitOpenDuration = TimeSpan.FromSeconds(30);

        public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request, CancellationToken cancellationToken = default)
        {
            if (IsCircuitOpen())
            {
                return new PaymentResult 
                { 
                    Success = false, 
                    Message = "Circuit breaker is open. Service temporarily unavailable.",
                    TransactionId = request.TransactionId
                };
            }

            int attempt = 0;
            Exception? lastException = null;

            while (attempt < MaxRetries)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var result = await CallPaymentGatewayAsync(request, cancellationToken);
                    
                    if (result.Success)
                    {
                        return result;
                    }
                    
                    attempt++;
                    if (attempt < MaxRetries)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, attempt)), cancellationToken);
                    }
                }
                catch (TimeoutException ex)
                {
                    lastException = ex;
                    attempt++;
                    RecordFailure();
                    
                    if (attempt < MaxRetries)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, attempt)), cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    RecordFailure();
                    return new PaymentResult 
                    { 
                        Success = false, 
                        Message = $"Payment failed: {ex.Message}",
                        TransactionId = request.TransactionId
                    };
                }
            }

            RecordFailure();
            return new PaymentResult 
            { 
                Success = false, 
                Message = $"Payment failed after {MaxRetries} retries: {lastException?.Message}",
                TransactionId = request.TransactionId
            };
        }

        private async Task<PaymentResult> CallPaymentGatewayAsync(PaymentRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(100, cancellationToken);
            return new PaymentResult 
            { 
                Success = true, 
                Message = "Payment processed successfully",
                TransactionId = request.TransactionId
            };
        }

        private bool IsCircuitOpen()
        {
            lock (_lock)
            {
                if (_circuitOpenedAt.HasValue)
                {
                    if (DateTime.UtcNow - _circuitOpenedAt.Value < _circuitOpenDuration)
                    {
                        return true;
                    }
                    _circuitOpenedAt = null;
                    _failures.Clear();
                }
                return false;
            }
        }

        private void RecordFailure()
        {
            lock (_lock)
            {
                var now = DateTime.UtcNow;
                _failures.Enqueue(now);

                while (_failures.Count > 0 && (now - _failures.Peek()) > _failureWindow)
                {
                    _failures.Dequeue();
                }

                if (_failures.Count >= FailureThreshold)
                {
                    _circuitOpenedAt = now;
                }
            }
        }
    }
}
