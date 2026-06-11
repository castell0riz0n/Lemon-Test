namespace Lemon_Test.Core.DiagnosticMessages;

public class OrderProcessor
{
    public ProcessingResult ProcessOrder(Order order)
    {
        // Validate customer
        if (order.CustomerId <= 0)
        {
            return new ProcessingResult
            {
                IsSuccess = false,
                ErrorMessage = "Invalid customer ID",
                ProcessingTime = 10
            };
        }
        
        // Check amount
        if (order.Amount <= 0)
        {
            return new ProcessingResult
            {
                IsSuccess = false,
                ErrorMessage = "Invalid order amount",
                ProcessingTime = 5
            };
        }
        
        // Simulate processing time based on amount
        var processingTime = order.Amount > 1000 ? 500 : 100;
        
        return new ProcessingResult
        {
            IsSuccess = true,
            TransactionId = $"TXN-{Guid.NewGuid():N}"[..10],
            ProcessingTime = processingTime
        };
    }
}

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public decimal Amount { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class ProcessingResult
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public string? TransactionId { get; set; }
    public int ProcessingTime { get; set; }
    public string Summary => IsSuccess ? "Order processed successfully" : $"Order failed: {ErrorMessage}";
}
