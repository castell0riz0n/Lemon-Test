namespace Lemon_Test.Core.BasicAssertions;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreatedDate { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Product other) return false;
        return Id == other.Id && Name == other.Name && Price == other.Price;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Price);
    }
}

public class ProductService
{
    private static int _nextId = 1;

    public Product CreateProduct(string name, decimal price)
    {
        return new Product
        {
            Id = _nextId++,
            Name = name,
            Price = price,
            IsAvailable = true,
            CreatedDate = DateTime.UtcNow
        };
    }

    public bool AreProductsIdentical(Product product1, Product product2)
    {
        return ReferenceEquals(product1, product2);
    }

    public decimal CalculateDiscountedPrice(decimal originalPrice, decimal discountPercentage)
    {
        return originalPrice * (1 - discountPercentage / 100);
    }
}
