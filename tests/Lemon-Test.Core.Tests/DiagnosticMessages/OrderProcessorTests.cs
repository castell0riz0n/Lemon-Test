using System.Text;
using Lemon_Test.Core.DiagnosticMessages;

namespace Lemon_Test.Core.Tests.DiagnosticMessages;

public class OrderProcessorTests
{
    private readonly ITestOutputHelper _output;

    public OrderProcessorTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ProcessOrder_WithValidOrder_ProvidesDetailedContext()
    {
        // Arrange
        var processor = new OrderProcessor();
        var order = new Order
        {
            Id = 12345,
            CustomerId = 100,
            Amount = 299.99m,
            ProductId = "LAPTOP-001"
        };

        _output.WriteLine(
            $"Order details (Opt.1): ID={order.Id}, Customer={order.CustomerId}, Amount={order.Amount:C}, Product={order.ProductId}");
        _output.WriteLine(
            "Order details (Opt.2): ID={0}, Customer={1}, Amount={2:C}, Product={3}",
            order.Id, order.CustomerId, order.Amount, order.ProductId);

        // Act
        var result = processor.ProcessOrder(order);

        // Enhanced assertion with business context
        var diagnosticInfo = GenerateOrderDiagnostics(order, result);

        // Assert with rich context
        Assert.True(result.IsSuccess,
            $"Order processing should succeed for valid order. {diagnosticInfo}");

        Assert.NotNull(result.TransactionId);
        Assert.True(result.ProcessingTime > 0,
            $"Processing time should be recorded. Actual: {result.ProcessingTime}ms");
    }

    [Theory]
    [InlineData(0, "Invalid customer ID")]
    [InlineData(-1, "Invalid customer ID")]
    public void ProcessOrder_WithInvalidCustomer_ProvidesContextualFailureInfo(int customerId, string expectedError)
    {
        // Arrange
        var processor = new OrderProcessor();
        var order = new Order
        {
            Id = 999,
            CustomerId = customerId,
            Amount = 100.00m,
            ProductId = "TEST-PRODUCT"
        };

        // Act
        var result = processor.ProcessOrder(order);

        // Dynamic diagnostic message based on test parameters
        var context = GenerateFailureDiagnostics(order, result, expectedError);
        _output.WriteLine(context);
        
        TestContext.Current.AddAttachment("diagnostics", context);

        // Assert with comprehensive diagnostic information
        Assert.False(result.IsSuccess, context);
        Assert.Contains(expectedError, result.ErrorMessage ?? string.Empty);
    }

    private string GenerateOrderDiagnostics(Order order, ProcessingResult result)
    {
        var diagnostics = new StringBuilder();

        diagnostics.AppendLine("\n--- ORDER PROCESSING DIAGNOSTICS ---");
        diagnostics.AppendLine($"Order ID: {order.Id}");
        diagnostics.AppendLine($"Customer ID: {order.CustomerId}");
        diagnostics.AppendLine($"Amount: {order.Amount:C}");
        diagnostics.AppendLine($"Product: {order.ProductId}");
        diagnostics.AppendLine($"Created: {order.CreatedAt:yyyy-MM-dd HH:mm:ss}");
        diagnostics.AppendLine($"Success: {result.IsSuccess}");
        diagnostics.AppendLine($"Transaction ID: {result.TransactionId ?? "N/A"}");
        diagnostics.AppendLine($"Processing Time: {result.ProcessingTime}ms");
        diagnostics.AppendLine($"Summary: {result.Summary}");

        return diagnostics.ToString();
    }

    private string GenerateFailureDiagnostics(Order order, ProcessingResult result, string expectedError)
    {
        return $"Order validation failure scenario:\n" +
               $"  Order ID: {order.Id}\n" +
               $"  Customer ID: {order.CustomerId}\n" +
               $"  Amount: {order.Amount:C}\n" +
               $"  Expected Error: {expectedError}\n" +
               $"  Actual Success: {result.IsSuccess}\n" +
               $"  Actual Error: {result.ErrorMessage ?? "None"}\n" +
               $"  Processing Time: {result.ProcessingTime}ms";
    }
}