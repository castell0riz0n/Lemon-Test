namespace Lemon_Test.Core.Collections;

public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public decimal TotalPrice => Price * Quantity;
}

public class ShoppingCart
{
    private readonly List<CartItem> _items = new();

    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

    public void AddItem(CartItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        var existingItem = _items.FirstOrDefault(i => i.ProductId == item.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += item.Quantity;
        }
        else
        {
            _items.Add(item);
        }
    }

    public void RemoveItem(int productId)
    {
        _items.RemoveAll(i => i.ProductId == productId);
    }

    public void Clear()
    {
        _items.Clear();
    }

    public decimal GetTotalAmount()
    {
        return _items.Sum(i => i.TotalPrice);
    }

    public int GetTotalItemCount()
    {
        return _items.Sum(i => i.Quantity);
    }

    public bool HasItem(int productId)
    {
        return _items.Any(i => i.ProductId == productId);
    }

    public IEnumerable<CartItem> GetExpensiveItems(decimal threshold)
    {
        return _items.Where(i => i.TotalPrice >= threshold);
    }
}
