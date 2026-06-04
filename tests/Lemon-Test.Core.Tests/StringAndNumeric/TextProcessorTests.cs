using Lemon_Test.Core.StringAndNumeric;

namespace Lemon_Test.Core.Tests.StringAndNumeric;

public class TextProcessorTests
{
    [Fact]
    public void ProcessText_WithValidInput_ReturnsProcessedText()
    {
        // Arrange
        var processor = new TextProcessor();
        var input = "  hello world  ";

        // Act
        var result = processor.ProcessText(input);

        // Assert - String pattern assertions
        Assert.StartsWith("PROCESSED:", result);    
        Assert.EndsWith("HELLO WORLD", result);
        Assert.Contains("HELLO WORLD", result);
        Assert.Equal("PROCESSED: HELLO WORLD", result);
    }

    [Fact]
    public void ExtractDomain_WithValidEmail_ReturnsDomain()
    {
        // Arrange
        var processor = new TextProcessor();
        var email = "user@example.com";

        // Act
        var domain = processor.ExtractDomain(email);

        // Assert - String extraction validation
        Assert.Equal("example.com", domain);
        Assert.DoesNotContain("@", domain);
        Assert.DoesNotContain("user", domain);
    }

    [Fact]
    public void ContainsKeyword_WithCaseInsensitiveSearch_ReturnsTrue()
    {
        // Arrange
        var processor = new TextProcessor();
        var text = "This is a Sample Text";
        var keyword = "SAMPLE";

        // Act
        var result = processor.ContainsKeyword(text, keyword);

        // Assert - Case-insensitive string matching
        Assert.True(result);
    }

    [Fact]
    public void OrderNumberGenerator_GeneratesValidFormat()
    {
        // Arrange
        var generator = new OrderNumberGenerator();

        // Act
        var orderNumber = generator.GenerateOrderNumber();

        // Assert - Regex pattern matching
        Assert.StartsWith("ORD-", orderNumber);
        Assert.Matches(@"^ORD-\d{8}-\d{4}$", orderNumber);
        
        // Additional pattern validation
        var parts = orderNumber.Split('-');
        Assert.Equal(3, parts.Length);
        Assert.Equal("ORD", parts[0]);
        Assert.Equal(8, parts[1].Length); // Date part
        Assert.Equal(4, parts[2].Length); // Sequence part
    }

    // TODO: Add more test cases to practice string and regex assertions:
    // - Test Assert.Matches with more complex regex patterns
    // - Use StringComparison options for culture-sensitive testing
    // - Practice Assert.DoesNotMatch for negative pattern testing
    // - Test string normalization and trimming scenarios
}
