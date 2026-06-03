namespace Lemon_Test.Core.TestStructure;
public class OrderService
{
    public Order Create(Customer customer, decimal amount)
    {
        if (customer is null)
            throw new ArgumentException("Customer required");

        return new Order
        {
            Id = 123,
            CustomerName = customer.CustomerName,
            Amount = amount
        };
    }
}

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
