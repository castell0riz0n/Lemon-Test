using Lemon_Test.Core.TheoryData;

namespace Lemon_Test.Core.Tests.TheoryData;

public class OrderProcessingTests
{
    [Theory]
    [MemberData(nameof(SuccessfulPaymentScenarios))]
    [MemberData(nameof(SuccessfulPaymentScenariosV2))]
    public void ProcessPayment_WithValidScenarios_ShouldSucceed(
        Order order,
        PaymentMethod paymentMethod,
        bool expectedSuccess,
        decimal expectedAmount)
    {
        // Arrange
        var processor = new PaymentProcessor();

        // Act
        var result = processor.ProcessPayment(order, paymentMethod);

        // Assert
        Assert.Equal(expectedSuccess, result.Success);
        Assert.Equal(expectedAmount, result.FinalAmount);
    }

    [Theory]
    [MemberData(nameof(PaymentLimitScenarios))]
    public void ProcessPayment_ShouldRespectPaymentLimits(
        Order order,
        PaymentMethod method,
        bool expectedSuccess)
    {
        // Arrange
        var processor = new PaymentProcessor();

        // Act
        var result = processor.ProcessPayment(order, method);

        // Assert
        Assert.Equal(expectedSuccess, result.Success);
    }


    public static IEnumerable<TheoryDataRow<Order, PaymentMethod, bool, decimal>> SuccessfulPaymentScenariosV2()
    {
        yield return new TheoryDataRow<Order, PaymentMethod, bool, decimal>(
            new Order(new OrderItem("Item1", 100m)),
            PaymentMethod.CreditCard,
            true,
            103m // 100 + 3% fee
        ).WithTestDisplayName("MY TEST");
    }

    // Type-safe TheoryData with multiple generic parameters
    public static TheoryData<Order, PaymentMethod, bool, decimal> SuccessfulPaymentScenarios =>
        new()
        {
            {
                new Order(new OrderItem("Item1", 100m)),
                PaymentMethod.CreditCard,
                true,
                103m // 100 + 3% fee
            },
            {
                new Order(new OrderItem("Item2", 50m)),
                PaymentMethod.DebitCard,
                true,
                50.5m // 50 + 1% fee
            },
            {
                new Order(new OrderItem("Item3", 200m)),
                PaymentMethod.Cash,
                true,
                200m // No fee
            },
            {
                new Order(new OrderItem("Item4", 1000m)),
                PaymentMethod.BankTransfer,
                true,
                1005m // 1000 + 5 fixed fee
            }
        };

    // Type-safe TheoryData for payment limits
    public static TheoryData<Order, PaymentMethod, bool> PaymentLimitScenarios =>
        new()
        {
            // Within limits
            { new Order(new OrderItem("Small", 500m)), PaymentMethod.Cash, true },
            { new Order(new OrderItem("Medium", 3000m)), PaymentMethod.DebitCard, true },
            { new Order(new OrderItem("Large", 8000m)), PaymentMethod.CreditCard, true },

            // Exceeding limits
            { new Order(new OrderItem("TooMuchCash", 1500m)), PaymentMethod.Cash, false },
            { new Order(new OrderItem("TooMuchDebit", 6000m)), PaymentMethod.DebitCard, false },
            { new Order(new OrderItem("TooMuchCredit", 15000m)), PaymentMethod.CreditCard, false },

            // Bank transfer has no limit
            { new Order(new OrderItem("VeryLarge", 50000m)), PaymentMethod.BankTransfer, true }
        };
}