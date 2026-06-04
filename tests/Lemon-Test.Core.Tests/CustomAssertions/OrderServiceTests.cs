using Lemon_Test.Core.CustomAssertions;

namespace Lemon_Test.Core.Tests.CustomAssertions;

public class OrderServiceTests
{
    [Fact]
    public void ProcessOrder_WithValidOrder_MarksOrderAsProcessed()
    {
        // Arrange
        var service = new OrderService();
        var order = new Order
        {
            Id = 1,
            CustomerId = 123,
            CustomerEmail = "customer@example.com",
            Amount = 99.99m,
            Status = OrderStatus.Pending,
            Items = new List<OrderItem>
            {
                new() { ProductId = 1, ProductName = "Test Product", Price = 99.99m, Quantity = 1 }
            }
        };

        // Act
        var result = service.ProcessOrder(order);

        // Assert
        order.ShouldBeProcessed();
    }

    [Fact]
    public void ProcessOrder_WithValidOrder_UsingFluentAssertions()
    {
        // Arrange
        var service = new OrderService();
        var order = new Order
        {
            Id = 2,
            CustomerId = 456,
            Amount = 149.99m,
            Status = OrderStatus.Pending,
            Items = new List<OrderItem>
            {
                new() { ProductId = 2, ProductName = "Another Product", Price = 149.99m, Quantity = 1 }
            }
        };

        // Act
        var processOrder = service.ProcessOrder(order);

        // Assert - Using fluent custom assertions
        processOrder
            .Should()
            .BeProcessed()
            .HaveCustomer(456)
            .HaveValidItems();
    }


    [Fact]
    public void IsValidOrder_WithValidOrder_ReturnsTrue()
    {
        // Arrange
        var service = new OrderService();
        var validOrder = new Order
        {
            CustomerId = 123,
            CustomerEmail = "test@example.com",
            Amount = 100.00m,
            Items = new List<OrderItem>
            {
                new() { ProductId = 1, ProductName = "Valid Item", Price = 100.00m, Quantity = 1 }
            }
        };

        // Act
        var isValid = service.IsValidOrder(validOrder);

        // Assert
        Assert.True(isValid);
        validOrder.ShouldHaveCustomer(123);
        validOrder.ShouldHaveValidItems();
        
    }


    // TODO: Practice creating more custom assertions:
    // - Create CustomerAssertions for Customer domain objects
    // - Build fluent APIs for complex validation scenarios
    // - Create assertions that encapsulate multiple related validations
    // - Practice testing your custom assertions themselves
    // - Build reusable assertion libraries for your domain
}