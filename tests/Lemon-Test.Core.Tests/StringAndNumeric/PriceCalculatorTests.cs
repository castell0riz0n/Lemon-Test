using Lemon_Test.Core.StringAndNumeric;

namespace Lemon_Test.Core.Tests.StringAndNumeric;

public class PriceCalculatorTests
{
    [Fact]
    public void CalculateTotal_WithValidPrices_ReturnsCorrectSum()
    {
        // Arrange
        var calculator = new PriceCalculator();
        var prices = new decimal[] { 10.50m, 25.99m, 5.00m };

        // Act
        var total = calculator.CalculateTotal(prices);

        // Assert - Decimal precision
        Assert.Equal(41.49m, total);
    }

    [Fact]
    public void ApplyDiscount_WithValidDiscount_ReturnsDiscountedPrice()
    {
        // Arrange
        var calculator = new PriceCalculator();
        var originalPrice = 100.00m;
        var discount = 20m; // 20%

        // Act
        var discountedPrice = calculator.ApplyDiscount(originalPrice, discount);

        // Assert - Numeric precision
        Assert.Equal(80.00m, discountedPrice);
        Assert.InRange(discountedPrice, 70m, 90m); // Range validation
    }

    [Fact]
    public void FormatCurrency_WithUSD_ReturnsCorrectFormat()
    {
        // Arrange
        var calculator = new PriceCalculator();
        var amount = 123.45m;

        // Act
        var formatted = calculator.FormatCurrency(amount, "USD");

        // Assert - String formatting assertions
        Assert.StartsWith("$", formatted);
        Assert.Contains("123.45", formatted);
        Assert.Matches(@"\$\d+\.\d{2}", formatted); // Regex pattern matching
    }

    [Fact]
    public void IsValidPrice_WithDifferentValues_ReturnsExpectedResults()
    {
        // Arrange
        var calculator = new PriceCalculator();

        // Act & Assert - Range testing
        Assert.True(calculator.IsValidPrice(0m));      // Boundary: minimum
        Assert.True(calculator.IsValidPrice(100.50m)); // Valid middle value
        Assert.True(calculator.IsValidPrice(999999.99m)); // Boundary: maximum
        Assert.False(calculator.IsValidPrice(-1m));    // Invalid: negative
        Assert.False(calculator.IsValidPrice(1000000m)); // Invalid: too high
    }

    [Fact]
    public void GetRandomScore_GeneratesScoreInValidRange()
    {
        // Arrange
        var calculator = new PriceCalculator();

        // Act & Assert - Range testing for random values
        for (int i = 0; i < 10; i++) // Test multiple times for randomness
        {
            var score = calculator.GetRandomScore();
            Assert.InRange(score, 0, 100);
        }
    }

    [Fact]
    public void Other_number_assertions()
    {
        Assert.Equal(Math.PI, 3.14, precision: 1); // 10 decimal places
    }
    
    // TODO: Add more test cases to practice numeric assertions:
    // - Test floating-point calculations with Assert.Equal(expected, actual, precision)
    // - Use Assert.InRange for boundary testing
    // - Practice currency formatting with different cultures
    // - Test numeric edge cases (zero, negative, very large numbers)
}
