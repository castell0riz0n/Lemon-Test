using Lemon_Test.Core.BasicAssertions;
using Lemon_Test.Core.Collections;

namespace Lemon_Test.Core.Tests.Exercises;

public class AssertionsPracticeTests
{
    // Part 1: Basic Calculator Tests
    
    [Fact]
    public void Calculator_Add_ReturnsCorrectSum()
    {
        // Arrange
        var calculator = new Calculator();
        
        // Act
        var result = calculator.Add(5, 3);
        
        // Assert
        Assert.Equal(8, result);
    }

    [Fact]
    public void Calculator_Divide_ReturnsCorrectQuotient()
    {
        // Arrange
        var calculator = new Calculator();
        
        // Act
        var result = calculator.Divide(10, 3);
        
        // Assert
        Assert.Equal(3.33, result, precision: 2);
    }

    // Part 2: User Management Tests
    
    [Fact]
    public void UserService_CreateUser_GeneratesValidEmail()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        var user = userService.CreateUser("John", "Doe");
        
        // Assert
        Assert.NotNull(user);
        Assert.Equal("john.doe@company.com", user.Email);
        Assert.Contains("John", user.FullName);
        Assert.Contains("Doe", user.FullName);
        Assert.StartsWith("John", user.FullName);
        Assert.EndsWith("Doe", user.FullName);
    }

    [Fact]
    public void UserService_GetActiveUsers_ReturnsFilteredList()
    {
        // Arrange
        var userService = new UserService();
        userService.AddUser(new User { Name = "Alice", IsActive = true });
        userService.AddUser(new User { Name = "Bob", IsActive = false });
        userService.AddUser(new User { Name = "Charlie", IsActive = true });
        
        // Act
        var activeUsers = userService.GetActiveUsers();
        
        // Assert
        Assert.Equal(2, activeUsers.Count);
        Assert.All(activeUsers, user => Assert.True(user.IsActive));
        Assert.Contains(activeUsers, user => user.Name == "Alice");
        Assert.Contains(activeUsers, user => user.Name == "Charlie");
        Assert.DoesNotContain(activeUsers, user => user.Name == "Bob");
    }

    // Part 3: Exception Handling
    
    [Fact]
    public void Calculator_DivideByZero_ThrowsException()
    {
        // Arrange
        var calculator = new Calculator();
        
        // Act & Assert
        var exception = Assert.Throws<DivideByZeroException>(() => calculator.Divide(10, 0));
        Assert.Equal("Cannot divide by zero", exception.Message);
    }

    [Fact]
    public async Task UserService_CreateDuplicateUser_ThrowsException()
    {
        // Arrange
        var userService = new UserService();
        await userService.CreateUserAsync("john@test.com");
        
        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => userService.CreateUserAsync("john@test.com"));
        Assert.Contains("already exists", exception.Message);
    }

    // Additional demonstration tests showing more assertion types
    
    [Fact]
    public void UserService_CreateUser_EmailMatchesPattern()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        var user = userService.CreateUser("Jane", "Smith");
        
        // Assert - Using regex pattern matching
        Assert.Matches(@"^[a-z]+\.[a-z]+@company\.com$", user.Email);
    }

    [Fact]
    public void Calculator_Add_MultipleScenarios()
    {
        // Arrange
        var calculator = new Calculator();
        
        // Act & Assert - Multiple scenarios
        Assert.Equal(0, calculator.Add(0, 0));
        Assert.Equal(5, calculator.Add(2, 3));
        Assert.Equal(-1, calculator.Add(-5, 4));
        Assert.Equal(0, calculator.Add(10, -10));
        
        // Range assertion
        var result = calculator.Add(15, 25);
        Assert.InRange(result, 30, 50);
    }

    [Fact]
    public void UserService_EmptyUserList_ReturnsEmptyCollection()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        var activeUsers = userService.GetActiveUsers();
        
        // Assert
        Assert.Empty(activeUsers);
        Assert.NotNull(activeUsers);
    }

    [Fact]
    public void User_Properties_BooleanAssertions()
    {
        // Arrange & Act
        var user = new User { Name = "Test", IsActive = true };
        
        // Assert
        Assert.True(user.IsActive);
        Assert.False(string.IsNullOrEmpty(user.Name));
    }
}
