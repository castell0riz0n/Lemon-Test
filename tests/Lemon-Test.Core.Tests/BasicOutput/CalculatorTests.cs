using Lemon_Test.Core.BasicOutput;

namespace Lemon_Test.Core.Tests.BasicOutput;

public class CalculatorTests
{
    private readonly ITestOutputHelper _output;

    public CalculatorTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Add_WithTwoNumbers_ReturnsSum()
    {
        // Arrange
        _output.WriteLine("Testing basic addition operation");
        var calculator = new Calculator();
        var a = 5;
        var b = 3;
        _output.WriteLine($"Input values: a={a}, b={b}");

        // Act
        var result = calculator.Add(a, b);
        _output.WriteLine($"Addition result: {result}");

        // Assert
        Assert.Equal(8, result);
        _output.WriteLine("Test completed successfully");
    }

    [Fact]
    public void ProcessNumbers_WithMultipleValues_LogsProgress()
    {
        // Arrange
        _output.WriteLine("=== Testing number processing ===");
        var calculator = new Calculator();
        var numbers = new[] { 1, 2, 3, 4, 5 };

        // Act
        var result = calculator.ProcessNumbers(numbers);
        _output.WriteLine($"Processed results: [{string.Join(", ", result)}]");
        _output.WriteLine($"Input count: {numbers.Length}, Output count: {result.Count}");

        // Assert
        Assert.Equal(numbers.Length, result.Count);
        Assert.Equal(new[] { 2, 4, 6, 8, 10 }, result);
    }
}
