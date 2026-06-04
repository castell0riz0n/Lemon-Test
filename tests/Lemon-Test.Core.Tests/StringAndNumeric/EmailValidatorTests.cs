using Lemon_Test.Core.StringAndNumeric;

namespace Lemon_Test.Core.Tests.StringAndNumeric;

public class EmailValidatorTests
{
    [Fact]
    public void Validate_WithValidEmail_ReturnsValidResult()
    {
        // Arrange
        var validator = new EmailValidator();
        var validEmail = "user@example.com";

        // Act
        var result = validator.Validate(validEmail);

        // Assert - Basic assertions
        Assert.True(result.IsValid);
        Assert.Empty(result.ErrorMessage);
        //Assert.Null(result.ErrorMessage);
    }

    [Fact]
    public void Validate_WithInvalidEmail_ReturnsInvalidResult()
    {
        // Arrange
        var validator = new EmailValidator();
        var invalidEmail = "not-an-email";

        // Act
        var result = validator.Validate(invalidEmail);

        // Assert - String assertions for error messages
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.ErrorMessage);
        Assert.Contains("Email address must contain an @ symbol", result.ErrorMessage);
    }

    [Fact]
    public void Validate_WithNullEmail_ReturnsAppropriateError()
    {
        // Arrange
        var validator = new EmailValidator();

        // Act
        var result = validator.Validate(null);

        // Assert - String content assertions
        Assert.False(result.IsValid);
        Assert.StartsWith("Email address cannot", result.ErrorMessage);
        Assert.EndsWith("null or empty", result.ErrorMessage);
    }

    [Fact]
    public void Validate_WithEmailMissingAtSymbol_ReturnsSpecificError()
    {
        // Arrange
        var validator = new EmailValidator();
        var emailWithoutAt = "userexample.com";

        // Act
        var result = validator.Validate(emailWithoutAt);

        // Assert - Specific string matching
        Assert.False(result.IsValid);
        Assert.Equal("EMAIL address must contain an @ symbol", 
            result.ErrorMessage, StringComparer.InvariantCultureIgnoreCase);
        Assert.Equal("Email address must contain an @ symbol", 
            result.ErrorMessage);
        Assert.Contains("@ SYMBOL", result.ErrorMessage, StringComparison.OrdinalIgnoreCase);
    }
    
    
    
    
    
    
    

    // TODO: Add more test cases to practice string assertions:
    // - Test emails with different cases (Assert with StringComparison options)
    // - Use Assert.Matches with regex patterns for email validation
    // - Practice Assert.DoesNotContain for invalid characters
    // - Test string length limits with Assert.InRange
}
