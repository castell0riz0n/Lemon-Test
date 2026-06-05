using Lemon_Test.Core.InlineData;

namespace Lemon_Test.Core.Tests.InlineData;

public class EmailValidatorTests
{
    [Theory]
    [InlineData("user@example.com", true)]
    [InlineData("user.name@domain.com", true)]
    [InlineData("user+tag@example.co.uk", true)]
    public void IsValid_WithValidEmails_ReturnsTrue(string email, bool expected)
    {
        // Arrange
        var validator = new EmailValidator();

        // Act
        var result = validator.IsValid(email);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData(null, false)]
    [InlineData("userexample.com", false)]
    [InlineData("@example.com", false)]
    [InlineData("user@", false)]
    [InlineData("user@.com", false)]
    public void IsValid_WithInvalidEmails_ReturnsFalse(string? email, bool expected)
    {
        // Arrange
        var validator = new EmailValidator();

        // Act
        var result = validator.IsValid(email);

        // Assert
        Assert.Equal(expected, result);
    }
}
