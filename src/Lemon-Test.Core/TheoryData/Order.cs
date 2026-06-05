namespace Lemon_Test.Core.TheoryData;

public class Order
{
    public List<OrderItem> Items { get; set; } = new();
    public decimal Total => Items.Sum(item => item.Price * item.Quantity);

    public Order()
    {
        Items = new List<OrderItem>();
    }

    public Order(params OrderItem[] items)
    {
        Items = items.ToList();
    }
}

public class OrderItem
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 1;

    public OrderItem(string name, decimal price, int quantity = 1)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
}
