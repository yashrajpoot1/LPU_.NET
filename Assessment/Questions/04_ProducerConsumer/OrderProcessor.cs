using System.Collections.Concurrent;

namespace ProducerConsumer
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }

    public class OrderProcessor
    {
        private readonly BlockingCollection<Order> _orderQueue = new();
        private int _processedCount = 0;

        public async Task<int> ProcessOrdersAsync(List<Order> orders, int consumerCount = 3)
        {
            var producerTask = Task.Run(() => Producer(orders));
            var consumerTasks = Enumerable.Range(0, consumerCount)
                .Select(_ => Task.Run(() => Consumer()))
                .ToArray();

            await producerTask;
            _orderQueue.CompleteAdding();
            await Task.WhenAll(consumerTasks);

            return _processedCount;
        }

        private void Producer(List<Order> orders)
        {
            foreach (var order in orders)
            {
                _orderQueue.Add(order);
            }
        }

        private async Task Consumer()
        {
            foreach (var order in _orderQueue.GetConsumingEnumerable())
            {
                await Task.Delay(100);
                Interlocked.Increment(ref _processedCount);
            }
        }
    }
}
