using Lemon_Test.Core.BasicAssertions;

namespace Lemon_Test.Core.Tests.BasicAssertions;

public class CalculatorTests
{
    [Fact]
    public void Add_WithTwoIntegers_ReturnsSum()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(5, 3);

        // Assert
        Assert.Equal(8, result);
    }

    [Fact]
    public void Subtract_WithTwoIntegers_ReturnsDifference()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Subtract(10, 4);

        // Assert
        Assert.Equal(6, result);
    }

    [Fact]
    public void IsPositive_WithPositiveNumber_ReturnsTrue()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.IsPositive(5);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsPositive_WithNegativeNumber_ReturnsFalse()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.IsPositive(-3);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsEven_WithEvenNumber_ReturnsTrue()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.IsEven(4);

        // Assert
        Assert.True(result, "the number isn't an even number");
    }

    [Fact]
    public void CalculateAverage_WithValidArray_ReturnsCorrectAverage()
    {
        // Arrange
        var calculator = new Calculator();
        var values = new double[] { 2.0, 4.0, 6.0 };

        // Act
        var result = calculator.CalculateAverage(values);

        // Assert - Note: Using precision for floating-point comparison
        Assert.Equal(4.0, result, precision: 10);
    }

    
    
    
    // TODO: Add more test cases to practice different assertion patterns:
    // - Test floating-point division with precision
    // - Test boundary conditions (zero, negative numbers)
    // - Practice Assert.NotEqual scenarios
    // - Explore Assert.InRange for numeric validations
}
