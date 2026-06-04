using Lemon_Test.Core.Exceptions;

namespace Lemon_Test.Core.Tests.Exceptions;

public class BankAccountTests
{
    [Fact]
    public void Constructor_WithValidData_CreatesAccount()
    {
        // Arrange & Act
        var account = new BankAccount("ACC-001", 100m);

        // Assert - Verify successful creation (no exception thrown)
        Assert.Equal("ACC-001", account.AccountId);
        Assert.Equal(100m, account.Balance);
        Assert.False(account.IsFrozen);
    }

    [Fact]
    public void Constructor_WithNullAccountId_ThrowsArgumentException()
    {
        // Act & Assert - Test exception throwing
        var exception = Assert.Throws<ArgumentException>(() => new BankAccount(null!));

        // Validate exception details
        Assert.Equal("accountId", exception.ParamName);
        Assert.Contains("cannot be null or empty", exception.Message);
    }

    [Fact]
    public void Constructor_WithNegativeBalance_ThrowsArgumentOutOfRangeException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            new BankAccount("ACC-001", -100m));

        // Validate exception properties
        Assert.Equal("initialBalance", exception.ParamName);
        Assert.Contains("cannot be negative", exception.Message);
    }

    [Fact]
    public void Withdraw_WithInsufficientFunds_ThrowsInsufficientFundsException()
    {
        // Arrange
        var account = new BankAccount("ACC-001", 50m);

        // Act & Assert - Test custom exception
        var exception = Assert.Throws<InsufficientFundsException>(() =>
            account.Withdraw(100m));

        // Validate custom exception properties
        Assert.Equal("ACC-001", exception.AccountId);
        Assert.Equal(50m, exception.AvailableBalance);
        Assert.Equal(100m, exception.RequestedAmount);
        Assert.StartsWith("Insufficient funds in account", exception.Message);
    }

    [Fact]
    public void Withdraw_FromFrozenAccount_ThrowsAccountFrozenException()
    {
        // Arrange
        var account = new BankAccount("ACC-001", 100m);
        account.FreezeAccount("Suspicious activity detected");

        // Act & Assert
        var exception = Assert.Throws<AccountFrozenException>(() =>
            account.Withdraw(50m));

        // Validate exception details
        Assert.Equal("ACC-001", exception.AccountId);
        Assert.Equal("Suspicious activity detected", exception.Reason);
        Assert.Contains("is frozen", exception.Message);
    }

    [Fact]
    public void Deposit_WithNegativeAmount_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var account = new BankAccount("ACC-001", 100m);

        // Act & Assert - Test boundary condition
        Assert.Throws<ArgumentOutOfRangeException>(() => account.Deposit(-10m));
        Assert.Throws<ArgumentOutOfRangeException>(() => account.Deposit(0m));
    }

    [Fact]
    public void ValidOperations_DoNotThrowExceptions()
    {
        // Arrange
        var account = new BankAccount("ACC-001", 100m);

        // Act & Assert - Verify operations that should NOT throw
        account.Deposit(50m); // Should not throw
        account.Withdraw(25m); // Should not throw

        // Verify final state
        Assert.Equal(125m, account.Balance);
    }

    // TODO: Add more exception test cases:
    // - Test exception conditions vs successful operations
    // - Practice testing exception message content with Assert.Contains
    // - Test multiple exception scenarios in boundary testing
    // - Validate exception data dictionary when relevant
}