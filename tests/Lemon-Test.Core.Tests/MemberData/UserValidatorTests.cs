using Lemon_Test.Core.MemberData;

namespace Lemon_Test.Core.Tests.MemberData;

public class UserValidatorTests
{
    [Theory]
    [MemberData(nameof(GetValidUserScenarios))]
    public void Validate_WithValidUsers_ReturnsTrue(User user, string scenario)
    {
        // Arrange
        var validator = new UserValidator();

        // Act
        var result = validator.Validate(user);

        // Assert
        Assert.True(result.IsValid, $"Failed scenario: {scenario}");
        Assert.Empty(result.ErrorMessage);
    }

    [Theory]
    [MemberData(nameof(GetInvalidUserScenarios))]
    public void Validate_WithInvalidUsers_ReturnsFalse(User user, string expectedError, string scenario)
    {
        // Arrange
        var validator = new UserValidator();

        // Act
        var result = validator.Validate(user);

        // Assert
        Assert.False(result.IsValid, $"Should fail scenario: {scenario}");
        Assert.Contains(expectedError, result.ErrorMessage);
    }

    public static IEnumerable<object[]> GetValidUserScenarios()
    {
        yield return new object[] { new User("John", "john@test.com", 25), "Standard valid user" };
        yield return new object[] { new User("Jane", "jane@company.co.uk", 18), "Minimum age user" };
        yield return new object[] { new User("Bob", "bob+work@domain.org", 65), "Complex email format" };
    }

    public static IEnumerable<object[]> ValidUserScenarios =>
        new List<object[]>()
        {
            new object[] { new User("Nick", "nick@dometrain.com", 18), "Standard valid user" },
            new object[] { new User("Jane", "jane@company.co.uk", 18), "Minimum age user" },
            new object[] { new User("Bob", "bob+work@domain.org", 65), "Complex email format" }
        };

    public static IEnumerable<object[]> GetInvalidUserScenarios()
    {
        yield return new object[] { new User("", "john@test.com", 25), "Name is required", "Empty name" };
        yield return new object[] { new User("John", "", 25), "Email is required", "Empty email" };
        yield return new object[] { new User("John", "invalid-email", 25), "Invalid email format", "Malformed email" };
        yield return new object[] { new User("John", "john@test.com", 17), "Must be 18 or older", "Underage user" };
        yield return new object[] { new User("John", "john@test.com", -5), "Age must be positive", "Negative age" };
    }
}
