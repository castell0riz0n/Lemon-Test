namespace Lemon_Test.Core.Tests.TheoryData;

public class SimpleTheoryDataTests
{
    [Theory]
    [MemberData(nameof(MathOperations))]
    public void PerformMathOperation_ShouldReturnCorrectResult(int a, int b, string operation, int expected)
    {
        // Act & Assert
        var result = operation switch
        {
            "add" => a + b,
            "subtract" => a - b,
            "multiply" => a * b,
            _ => throw new ArgumentException($"Unknown operation: {operation}")
        };

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(StringValidationData))]
    public void ValidateString_ShouldReturnExpectedResult(string input, int minLength, bool expected)
    {
        // Act
        var result = !string.IsNullOrEmpty(input) && input.Length >= minLength;

        // Assert
        Assert.Equal(expected, result);
    }

    // Type-safe TheoryData with primitive types
    public static TheoryData<int, int, string, int> MathOperations =>
        new()
        {
            { 5, 3, "add", 8 },
            { 10, 4, "subtract", 6 },
            { 3, 7, "multiply", 21 },
            { 0, 5, "add", 5 },
            { -1, 1, "add", 0 },
        };

    // Type-safe TheoryData with mixed types
    public static TheoryData<string, int, bool> StringValidationData =>
        new()
        {
            { "hello", 3, true },
            { "hi", 3, false },
            { "", 1, false },
            { "test", 0, true },
            { "validation", 5, true }
        };
}