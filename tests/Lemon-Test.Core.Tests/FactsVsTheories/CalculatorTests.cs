using Lemon_Test.Core.FactsVsTheories;

namespace Lemon_Test.Core.Tests.FactsVsTheories;

public class CalculatorTests
{
    [Fact]
    public void Add_2And2_Returns4()
    {
        var calc = new Core.FactsVsTheories.Calculator();
        var result = calc.Add(2, 2);
        Assert.Equal(4, result);
    }
    
    [Fact]
    public void Add_5And3_Returns8()
    {
        var calc = new Core.FactsVsTheories.Calculator();
        var result = calc.Add(5, 3);
        Assert.Equal(8, result);
    }

    [Fact]
    public void Add_0And5_Returns5()
    {
        var calc = new Core.FactsVsTheories.Calculator();
        var result = calc.Add(0, 5);
        Assert.Equal(5, result);
    }

    [Theory]
    [InlineData(1,2,3)]
    [InlineData(2,2,4)]
    [InlineData(5,3,8)]
    [InlineData(5,0,5)]
    public void Add_TwoNumbers_ReturnExpectedResult(int a, int b, int expectedResult)
    {
        var calc = new Core.FactsVsTheories.Calculator();
        var result = calc.Add(a, b);
        Assert.Equal(expectedResult, result);
    }


    [Fact]
    public void Divide_ByZero_ThrowsException()
    {
        var calc = new Core.FactsVsTheories.Calculator();
        Assert.Throws<DivideByZeroException>(() => calc.Divide(10, 0));
    }
    
    [Theory]
    [InlineData(2, 2, 4)]
    [InlineData(2, -2, 0)]
    [InlineData(-2, -2, -4)]
    [InlineData(0, 5, 5)]
    public void Add_WithVariousNumbers_ReturnsCorrectSum(int first, int second, int expected)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(first, second);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(5, 3, 2)]
    [InlineData(10, 7, 3)]
    [InlineData(-5, -3, -2)]
    public void Subtract_WithVariousNumbers_ReturnsCorrectDifference(
        int minuend, int subtrahend, int expected)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Subtract(minuend, subtrahend);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Divide_WithZeroDivisor_ThrowsDivideByZeroException()
    {
        // Arrange
        var calculator = new Calculator();
        const int dividend = 10;
        const int zeroDivisor = 0;

        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => calculator.Divide(dividend, zeroDivisor));
    }
}
