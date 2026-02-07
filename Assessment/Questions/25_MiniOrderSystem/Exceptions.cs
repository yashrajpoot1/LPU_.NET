namespace MiniOrderSystem
{
    public class InsufficientStockException : Exception
    {
        public string ProductId { get; }
        public int RequestedQuantity { get; }
        public int AvailableStock { get; }

        public InsufficientStockException(string productId, int requested, int available)
            : base($"Insufficient stock for product {productId}. Requested: {requested}, Available: {available}")
        {
            ProductId = productId;
            RequestedQuantity = requested;
            AvailableStock = available;
        }
    }

    public class InvalidCouponException : Exception
    {
        public InvalidCouponException(string message) : base(message) { }
    }

    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string productId)
            : base($"Product {productId} not found") { }
    }

    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(string customerId)
            : base($"Customer {customerId} not found") { }
    }
}
