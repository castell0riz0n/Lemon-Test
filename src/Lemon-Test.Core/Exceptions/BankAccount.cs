namespace Lemon_Test.Core.Exceptions;

public class InsufficientFundsException : Exception
{
    public decimal AvailableBalance { get; }
    public decimal RequestedAmount { get; }
    public string AccountId { get; }

    public InsufficientFundsException(string accountId, decimal availableBalance, decimal requestedAmount)
        : base($"Insufficient funds in account {accountId}. Available: {availableBalance:C}, Requested: {requestedAmount:C}")
    {
        AccountId = accountId;
        AvailableBalance = availableBalance;
        RequestedAmount = requestedAmount;
    }
}

public class AccountFrozenException : Exception
{
    public string AccountId { get; }
    public string Reason { get; }

    public AccountFrozenException(string accountId, string reason)
        : base($"Account {accountId} is frozen: {reason}")
    {
        AccountId = accountId;
        Reason = reason;
    }
}

public class BankAccount
{
    public string AccountId { get; private set; }
    public decimal Balance { get; private set; }
    public bool IsFrozen { get; private set; }
    public string? FreezeReason { get; private set; }

    public BankAccount(string accountId, decimal initialBalance = 0)
    {
        if (string.IsNullOrWhiteSpace(accountId))
            throw new ArgumentException("Account ID cannot be null or empty", nameof(accountId));
        
        if (initialBalance < 0)
            throw new ArgumentOutOfRangeException(nameof(initialBalance), "Initial balance cannot be negative");

        AccountId = accountId;
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be positive");

        if (IsFrozen)
            throw new AccountFrozenException(AccountId, FreezeReason ?? "Account is frozen");

        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be positive");

        if (IsFrozen)
            throw new AccountFrozenException(AccountId, FreezeReason ?? "Account is frozen");

        if (amount > Balance)
            throw new InsufficientFundsException(AccountId, Balance, amount);

        Balance -= amount;
    }

    public void FreezeAccount(string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Freeze reason cannot be null or empty", nameof(reason));

        IsFrozen = true;
        FreezeReason = reason;
    }
}
