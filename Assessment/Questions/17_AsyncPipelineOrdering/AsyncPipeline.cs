namespace AsyncPipelineOrdering
{
    public class Input
    {
        public int Id { get; set; }
        public string Data { get; set; } = string.Empty;
    }

    public class Output
    {
        public int Id { get; set; }
        public string Result { get; set; } = string.Empty;
    }

    public class AsyncPipeline
    {
        private readonly SemaphoreSlim _semaphore = new(4);

        public async Task<List<Output>> ProcessAsync(List<Input> items)
        {
            var tasks = items.Select(async (item, index) =>
            {
                await _semaphore.WaitAsync();
                try
                {
                    var result = await ProcessItemAsync(item);
                    return (index, result);
                }
                finally
                {
                    _semaphore.Release();
                }
            });

            var results = await Task.WhenAll(tasks);
            
            return results
                .OrderBy(r => r.index)
                .Select(r => r.result)
                .ToList();
        }

        private async Task<Output> ProcessItemAsync(Input item)
        {
            await Task.Delay(100);
            return new Output
            {
                Id = item.Id,
                Result = $"Processed: {item.Data}"
            };
        }
    }
}
