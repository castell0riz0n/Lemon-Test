using Lemon_Test.Core.TestStructure;

namespace Lemon_Test.Core.Tests.TestStructure;

public class OrderServiceTests
{
    private OrderService _service;

    public OrderServiceTests()
    {
        _service = new OrderService();
    }

    [Fact]
    public void TestCreateOrder()
    {
        // Arrange
        const string name = "Guilherme";
        var customer = CreateCustomer(name);
        
        // Act
        var order = _service.Create(customer, 100);
        
        // Assert
        Assert.NotNull(order);
        Assert.Equal(name, order.CustomerName);
        Assert.Equal(100, order.Amount);
    }


    [Fact]
    public void TestException()
    {
        // arrange
        // act
        // assert
        Assert.Throws<ArgumentException>(() 
            => _service.Create(null!, 100));
    }
    
    // NOTE: Alternative -> Use the Builder Pattern
    private static Customer CreateCustomer(string name = "Gui")
    {
        var customer = new Customer()
        {
            Id = Guid.NewGuid(),
            CustomerName = name,
            Email = "gui@guiferreira.me"
        };
        return customer;
    }
}
