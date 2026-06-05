using Lemon_Test.Core.InlineData;

namespace Lemon_Test.Core.Tests.InlineData;

public class CalculatorTests
{
    [Theory]
    [InlineData(2, 3, 5)] 
    [InlineData(0, 5, 5)]
    [InlineData(-1, 1, 0)]
    [InlineData(10, -3, 7)]
    public void Add_ShouldReturnCorrectSum(int a, int b, int expected)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(a, b);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(2, true)]
    [InlineData(3, false)]
    [InlineData(0, true)]
    [InlineData(-2, true)]
    [InlineData(-3, false)]
    public void IsEven_ShouldIdentifyEvenNumbers(int number, bool expected)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.IsEven(number);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(2, 3, 6)]
    [InlineData(0, 5, 0)]
    [InlineData(1, 100, 100)]
    [InlineData(-2, 3, -6)]
    public void Multiply_ShouldReturnCorrectProduct(int a, int b, int expected)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Multiply(a, b);

        // Assert
        Assert.Equal(expected, result);
    }
}
