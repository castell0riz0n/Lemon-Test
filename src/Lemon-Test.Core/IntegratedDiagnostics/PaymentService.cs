namespace Lemon_Test.Core.IntegratedDiagnostics;

public class PaymentService
{
    private readonly List<string> _processedPayments = new();
    
    public PaymentResult ProcessPayment(decimal amount, int customerId)
    {
        // Simulate processing delay
        Thread.Sleep(50);
        
        // Business rules
        if (amount <= 0)
        {
            return new PaymentResult
            {
                IsSuccess = false,
                ErrorMessage = "Amount must be positive",
                ProcessingTimeMs = 10
            };
        }
        
        if (amount > 10000)
        {
            return new PaymentResult
            {
                IsSuccess = false,
                ErrorMessage = "Amount exceeds maximum limit",
                ProcessingTimeMs = 25
            };
        }
        
        var transactionId = $"PAY-{Guid.NewGuid():N}"[..10];
        _processedPayments.Add(transactionId);
        
        return new PaymentResult
        {
            IsSuccess = true,
            TransactionId = transactionId,
            ProcessingTimeMs = Random.Shared.Next(40, 80),
            ProcessedAmount = amount
        };
    }
    
    public List<string> GetProcessedPayments() => _processedPayments.ToList();
}

public class PaymentResult
{
    public bool IsSuccess { get; set; }
    public string? TransactionId { get; set; }
    public string? ErrorMessage { get; set; }
    public int ProcessingTimeMs { get; set; }
    public decimal ProcessedAmount { get; set; }
    public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
}
