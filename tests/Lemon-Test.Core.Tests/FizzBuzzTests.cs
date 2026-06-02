namespace Lemon_Test.Core.Tests;

public class FizzBuzzTests
{
    private readonly FizzBuzz _fizzBuzz = new();
    
    [Fact]
    public void Convert_NumberDivisibleByThree_ReturnsFizz()
    {
        // Arrange
        const int number = 3;

        // Act
        var result = _fizzBuzz.Convert(number);

        // Assert
        Assert.Equal("Fizz", result);
    }

    [Fact]
    public void Convert_NumberDivisibleByFive_ReturnsBuzz()
    {
        // Arrange
        const int number = 5;

        // Act
        var result = _fizzBuzz.Convert(number);

        // Assert
        Assert.Equal("Buzz", result);
    }

    [Fact]
    public void Convert_NumberDivisibleByBoth_ReturnsFizzBuzz()
    {
        // Arrange
        const int number = 15;

        // Act
        var result = _fizzBuzz.Convert(number);

        // Assert
        Assert.Equal("FizzBuzz", result);
    }

    [Fact]
    public void Convert_RegularNumber_ReturnsNumberAsString()
    {
        // Arrange
        const int number = 7;

        // Act
        var result = _fizzBuzz.Convert(number);

        // Assert
        Assert.Equal("7", result);
    }

    [Fact]
    public void Convert_Six_ReturnsFizz()
    {
        // Arrange
        var fizzBuzz = new FizzBuzz();
        const int number = 6;

        // Act
        var result = fizzBuzz.Convert(number);

        // Assert
        Assert.Equal("Fizz", result);
    }

    [Fact]
    public void Convert_Nine_ReturnsFizz()
    {
        // Arrange
        const int number = 9;

        // Act
        var result = _fizzBuzz.Convert(number);

        // Assert
        Assert.Equal("Fizz", result);
    }

    [Fact]
    public void Convert_Ten_ReturnsBuzz()
    {
        // Arrange
        const int number = 10;

        // Act
        var result = _fizzBuzz.Convert(number);

        // Assert
        Assert.Equal("Buzz", result);
    }

    [Fact]
    public void Convert_Twenty_ReturnsBuzz()
    {
        // Arrange
        const int number = 20;

        // Act
        var result = _fizzBuzz.Convert(number);

        // Assert
        Assert.Equal("Buzz", result);
    }

    [Fact]
    public void Convert_Thirty_ReturnsFizzBuzz()
    {
        // Arrange
        const int number = 30;

        // Act
        var result = _fizzBuzz.Convert(number);

        // Assert
        Assert.Equal("FizzBuzz", result);
    }

    [Fact]
    public void Convert_FortyFive_ReturnsFizzBuzz()
    {
        // Arrange
        const int number = 45;

        // Act
        var result = _fizzBuzz.Convert(number);

        // Assert
        Assert.Equal("FizzBuzz", result);
    }

    [Fact]
    public void Convert_One_ReturnsOne()
    {
        // Arrange
        const int number = 1;

        // Act
        var result = _fizzBuzz.Convert(number);

        // Assert
        Assert.Equal("1", result);
    }

    [Fact]
    public void Convert_Two_ReturnsTwo()
    {
        // Arrange
        const int number = 2;

        // Act
        var result = _fizzBuzz.Convert(number);

        // Assert
        Assert.Equal("2", result);
    }

    [Fact]
    public void Convert_Four_ReturnsFour()
    {
        // Arrange
        const int number = 4;

        // Act
        var result = _fizzBuzz.Convert(number);

        // Assert
        Assert.Equal("4", result);
    }
}