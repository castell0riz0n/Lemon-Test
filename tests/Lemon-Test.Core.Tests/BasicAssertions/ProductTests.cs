using Lemon_Test.Core.BasicAssertions;

namespace Lemon_Test.Core.Tests.BasicAssertions;

public class ProductTests
{
    [Fact]
    public void CreateProduct_WithValidData_ReturnsProduct()
    {
        // Arrange
        var service = new ProductService();

        // Act
        var product = service.CreateProduct("Laptop", 999.99m);

        // Assert - Basic equality assertions
        Assert.Equal("Laptop", product.Name);
        Assert.Equal(999.99m, product.Price);
        Assert.True(product.IsAvailable);
        Assert.True(product.Id > 0);
    }

    [Fact]
    public void Products_WithSameValues_AreEqual()
    {
        // Arrange
        var product1 = new Product { Id = 1, Name = "Mouse", Price = 29.99m };
        var product2 = new Product { Id = 1, Name = "Mouse", Price = 29.99m };

        // Act & Assert - Testing value equality vs reference equality
        Assert.Equal(product1, product2);
        //Assert.NotEqual(product1, product2);
        Assert.NotSame(product1, product2);
        Assert.Same(product1, product1);
    }

    [Fact]
    public void AreProductsIdentical_WithSameReference_ReturnsTrue()
    {
        // Arrange
        var service = new ProductService();
        var product = service.CreateProduct("Keyboard", 79.99m);

        // Act
        var result = service.AreProductsIdentical(product, product);

        // Assert - Testing reference identity
        Assert.True(result);
        Assert.Same(product, product); // Same reference
    }

    [Fact]
    public void CalculateDiscountedPrice_WithValidDiscount_ReturnsCorrectPrice()
    {
        // Arrange
        var service = new ProductService();
        var originalPrice = 100.00m;
        var discountPercentage = 20m;

        // Act
        var discountedPrice = service.CalculateDiscountedPrice(originalPrice, discountPercentage);

        // Assert - Testing decimal precision
        Assert.Equal(80.00m, discountedPrice);
        Assert.NotEqual(originalPrice, discountedPrice);

    }

    // TODO: Add more test cases to practice:
    // - Test Assert.NotEqual with different products
    // - Practice Assert.Same vs Assert.Equal differences
    // - Test floating-point precision with Assert.Equal(expected, actual, precision)
    // - Create scenarios for Assert.True/False vs Assert.Equal comparisons
}
