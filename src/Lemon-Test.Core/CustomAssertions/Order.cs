namespace Lemon_Test.Core.CustomAssertions;

public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerEmail { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ProcessedDate { get; set; }
    public string? ProcessingId { get; set; }
    public List<OrderItem> Items { get; set; } = new();
}

public class OrderItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    
    public decimal TotalPrice => Price * Quantity;
}

public class OrderService
{
    private static int _nextProcessingId = 1000;

    public Order ProcessOrder(Order order)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));

        if (order.Status != OrderStatus.Pending)
            throw new InvalidOperationException("Only pending orders can be processed");

        order.Status = OrderStatus.Processing;
        order.ProcessedDate = DateTime.UtcNow;
        order.ProcessingId = $"PROC-{DateTime.UtcNow:yyyyMMdd}-{_nextProcessingId:D4}";
        _nextProcessingId++;

        return order;
    }

    public decimal CalculateOrderTotal(Order order)
    {
        if (order?.Items == null)
            return 0;

        return order.Items.Sum(item => item.TotalPrice);
    }

    public bool IsValidOrder(Order order)
    {
        return order != null &&
               order.CustomerId > 0 &&
               !string.IsNullOrWhiteSpace(order.CustomerEmail) &&
               order.Amount > 0 &&
               order.Items.Any() &&
               order.Items.All(item => item.Quantity > 0 && item.Price > 0);
    }
}
