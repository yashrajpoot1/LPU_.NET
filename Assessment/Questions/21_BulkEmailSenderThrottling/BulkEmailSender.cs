namespace BulkEmailSenderThrottling
{
    public class Email
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }

    public class BulkSendReport
    {
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
        public List<string> FailedEmails { get; set; } = new();
    }

    public class BulkEmailSender
    {
        private const int MaxEmailsPerSecond = 10;
        private const int MaxRetries = 2;
        private int _successCount = 0;

        public async Task<BulkSendReport> SendBulkAsync(List<Email> emails)
        {
            var report = new BulkSendReport();
            var retryQueue = new Queue<(Email email, int retryCount)>();
            var semaphore = new SemaphoreSlim(MaxEmailsPerSecond);
            var tasks = new List<Task>();

            foreach (var email in emails)
            {
                await semaphore.WaitAsync();
                
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        var success = await SendEmailAsync(email);
                        if (success)
                        {
                            Interlocked.Increment(ref _successCount);
                        }
                        else
                        {
                            lock (retryQueue)
                            {
                                retryQueue.Enqueue((email, 0));
                            }
                        }
                    }
                    finally
                    {
                        await Task.Delay(1000 / MaxEmailsPerSecond);
                        semaphore.Release();
                    }
                }));
            }

            await Task.WhenAll(tasks);

            while (retryQueue.Count > 0)
            {
                var (email, retryCount) = retryQueue.Dequeue();
                
                if (retryCount >= MaxRetries)
                {
                    report.FailureCount++;
                    report.FailedEmails.Add(email.To);
                    continue;
                }

                await semaphore.WaitAsync();
                var success = await SendEmailAsync(email);
                await Task.Delay(1000 / MaxEmailsPerSecond);
                semaphore.Release();

                if (success)
                {
                    Interlocked.Increment(ref _successCount);
                }
                else
                {
                    retryQueue.Enqueue((email, retryCount + 1));
                }
            }

            report.SuccessCount = _successCount;
            return report;
        }

        private async Task<bool> SendEmailAsync(Email email)
        {
            await Task.Delay(50);
            return new Random().Next(100) > 10;
        }
    }
}
