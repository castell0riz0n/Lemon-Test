using Lemon_Test.Core.TestNaming;

namespace Lemon_Test.Core.Tests.TestNaming;

public class EmailValidatorTests
{
    // MethodName_Scenario_ExpectedResult
    // Pattern breakdown:
    // - MethodName: `CreateOrder` - What method/functionality is being tested
    // - Scenario: `WithValidCustomerAndItems` - What conditions or input state
    // - ExpectedResult: `ReturnsNewOrderWithId` - What should happen
    
    
    // Should_return_new_order_with_id_When_customer_and_items_are_valid
    
    // Given__When__Then__
    
    
    [Fact(DisplayName = "Should throw if email is null")]
    public void IsValid_WhenIsAValidEmail_ShouldReturnTrue()
    {
        var validator = new EmailValidator();
        var result = validator.IsValid("user@domain.com");
        Assert.True(result);
    }

    [Fact]
    public void IsValid_WhenEmailIsEmpty_ShouldReturnFalse()
    {
        var validator = new EmailValidator();
        var result = validator.IsValid("");
        Assert.False(result);
    }

    [Fact]
    public void IsValid_WhenIsAnInvalidEmail_ShouldReturnFalse()
    {
        var validator = new EmailValidator();
        Assert.False(validator.IsValid("invalid"));
    }
    
    [Fact]
    public void X_should_be_gt_zero()
    {
        Assert.True(true);
    }
}
