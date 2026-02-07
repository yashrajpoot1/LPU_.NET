namespace MiniOrderSystem
{
    public class Cart
    {
        public string CustomerId { get; set; } = string.Empty;
        public List<OrderItem> Items { get; set; } = new();
    }

    public class Coupon
    {
        public string Code { get; set; } = string.Empty;
        public decimal DiscountPercent { get; set; }
        public decimal MinimumAmount { get; set; }
    }

    public class OrderService
    {
        private readonly Dictionary<string, Customer> _customers = new();
        private readonly Dictionary<string, Product> _products = new();
        private readonly Dictionary<string, Cart> _carts = new();
        private readonly Dictionary<string, Order> _orders = new();
        private readonly Dictionary<string, Coupon> _coupons = new();
        private readonly object _lock = new();
        private int _invoiceCounter = 1000;

        public void AddCustomer(Customer customer) => _customers[customer.CustomerId] = customer;
        public void AddProduct(Product product) => _products[product.ProductId] = product;
        
        public void AddCoupon(Coupon coupon) => _coupons[coupon.Code] = coupon;

        public void AddToCart(string customerId, string productId, int quantity)
        {
            if (!_customers.ContainsKey(customerId))
                throw new CustomerNotFoundException(customerId);

            if (!_products.ContainsKey(productId))
                throw new ProductNotFoundException(productId);

            var product = _products[productId];

            if (!_carts.ContainsKey(customerId))
                _carts[customerId] = new Cart { CustomerId = customerId };

            var cart = _carts[customerId];
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new OrderItem
                {
                    ProductId = productId,
                    ProductName = product.Name,
                    Quantity = quantity,
                    UnitPrice = product.Price
                });
            }
        }

        public Order PlaceOrder(string customerId, string? couponCode = null)
        {
            if (!_customers.ContainsKey(customerId))
                throw new CustomerNotFoundException(customerId);

            if (!_carts.ContainsKey(customerId) || _carts[customerId].Items.Count == 0)
                throw new InvalidOperationException("Cart is empty");

            lock (_lock)
            {
                var cart = _carts[customerId];

                foreach (var item in cart.Items)
                {
                    var product = _products[item.ProductId];
                    if (product.Stock < item.Quantity)
                    {
                        throw new InsufficientStockException(item.ProductId, item.Quantity, product.Stock);
                    }
                }

                foreach (var item in cart.Items)
                {
                    _products[item.ProductId].Stock -= item.Quantity;
                }

                var order = new Order
                {
                    CustomerId = customerId,
                    Items = new List<OrderItem>(cart.Items),
                    Subtotal = cart.Items.Sum(i => i.TotalPrice),
                    InvoiceNumber = GenerateInvoiceNumber()
                };

                if (!string.IsNullOrWhiteSpace(couponCode))
                {
                    order.Discount = ApplyCoupon(couponCode, order.Subtotal);
                }

                _orders[order.OrderId] = order;
                _carts[customerId].Items.Clear();

                return order;
            }
        }

        private decimal ApplyCoupon(string couponCode, decimal subtotal)
        {
            if (!_coupons.TryGetValue(couponCode, out var coupon))
                throw new InvalidCouponException($"Coupon {couponCode} not found");

            if (subtotal < coupon.MinimumAmount)
                throw new InvalidCouponException($"Minimum amount {coupon.MinimumAmount} not met");

            return subtotal * (coupon.DiscountPercent / 100);
        }

        private string GenerateInvoiceNumber()
        {
            return $"INV-{DateTime.UtcNow:yyyyMMdd}-{Interlocked.Increment(ref _invoiceCounter)}";
        }

        public Order? GetOrder(string orderId)
        {
            return _orders.TryGetValue(orderId, out var order) ? order : null;
        }
    }
}
