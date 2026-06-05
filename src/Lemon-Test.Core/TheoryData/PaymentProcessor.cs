namespace Lemon_Test.Core.TheoryData;

public class PaymentProcessor
{
    public PaymentResult ProcessPayment(Order order, PaymentMethod method)
    {
        if (order.Total <= 0)
            return new PaymentResult(false, "Order total must be positive", 0);

        var fee = CalculateFee(order.Total, method);
        var finalAmount = order.Total + fee;

        var success = method switch
        {
            PaymentMethod.CreditCard => order.Total <= 10000, // Credit card limit
            PaymentMethod.DebitCard => order.Total <= 5000,   // Debit card limit
            PaymentMethod.Cash => order.Total <= 1000,        // Cash limit
            PaymentMethod.BankTransfer => true,               // No limit
            _ => false
        };

        return new PaymentResult(success, success ? "Payment successful" : "Payment failed", finalAmount);
    }

    private decimal CalculateFee(decimal amount, PaymentMethod method)
    {
        return method switch
        {
            PaymentMethod.CreditCard => amount * 0.03m, // 3% fee
            PaymentMethod.DebitCard => amount * 0.01m,  // 1% fee
            PaymentMethod.Cash => 0m,                   // No fee
            PaymentMethod.BankTransfer => 5.00m,        // Fixed fee
            _ => 0m
        };
    }
}

public enum PaymentMethod
{
    CreditCard,
    DebitCard,
    Cash,
    BankTransfer
}

public class PaymentResult
{
    public bool Success { get; }
    public string Message { get; }
    public decimal FinalAmount { get; }

    public PaymentResult(bool success, string message, decimal finalAmount)
    {
        Success = success;
        Message = message;
        FinalAmount = finalAmount;
    }
}
