using Lemon_Test.Core.CustomAssertions;

namespace Lemon_Test.Core.Tests.CustomAssertions;

public static class OrderAssertions
{
    public static void ShouldBeProcessed(this Order order)
    {
        Assert.NotNull(order);
        Assert.Equal(OrderStatus.Processing, order.Status);
        Assert.True(order.ProcessedDate.HasValue, 
            "Processed order should have a processing date");
        Assert.NotNull(order.ProcessingId);
        Assert.False(string.IsNullOrWhiteSpace(order.ProcessingId), 
            "Processed order should have a valid processing ID");
    }
    
    public static void ShouldHaveCustomer(this Order order, int customerId)
    {
        Assert.Equal(customerId, order.CustomerId);
        Assert.True(order.CustomerId > 0, "Order should have a valid customer ID");
    }
    
    
    public static void ShouldHaveValidItems(this Order order)
    {
        Assert.NotNull(order.Items);
        Assert.NotEmpty(order.Items);
        Assert.All(order.Items, item =>
        {
            Assert.True(item.Quantity > 0, $"Item {item.ProductName} should have positive quantity");
            Assert.True(item.Price > 0, $"Item {item.ProductName} should have positive price");
            Assert.False(string.IsNullOrWhiteSpace(item.ProductName), 
                $"Item with ProductId {item.ProductId} should have a name");
        });
    }
}

public static class FluentOrderAssertions
{
    public static OrderAssertion Should(this Order order)
    {
        return new OrderAssertion(order);
    }
}


public class OrderAssertion
{
    private readonly Order _order;

    public OrderAssertion(Order order)
    {
        _order = order;
    }

    public OrderAssertion HaveCustomer(int customerId)
    {
        _order.ShouldHaveCustomer(customerId);
        return this;
    }

    public OrderAssertion HaveValidItems()
    {
        _order.ShouldHaveValidItems();
        return this;
    }

    public OrderAssertion BeProcessed()
    {
        _order.ShouldBeProcessed();
        return this;
    }
}