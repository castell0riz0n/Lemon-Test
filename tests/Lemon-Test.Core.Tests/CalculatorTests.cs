namespace Lemon_Test.Core.Tests;

public class CalculatorTests
{
    private readonly Calculator _calculator;

    public CalculatorTests()
    {
        this._calculator = new Calculator();
    }

    [Fact]
    public void Add_TwoPositiveNumbers_ReturnSum()
    {
        //arrange
        var firstNumber = 1;
        var secondNumber = 2;
        
        //act
        var result = _calculator.Add(secondNumber, firstNumber);
        
        //assert
        Assert.Equal(3, result);
    }

    [Fact]
    public void Add_TwoNegativeNumbers_ReturnSum()
    {
        // arrange
        var firstNumber = -1;
        var secondNumber = -1;
        
        //act
        var result = _calculator.Add(secondNumber, firstNumber);
        
        //assert
        Assert.Equal(-2, result);
        
    }
}