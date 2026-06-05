namespace Lemon_Test.Core.MemberData;

public class UserValidator
{
    public ValidationResult Validate(User user)
    {
        if (string.IsNullOrEmpty(user.Name))
            return new ValidationResult(false, "Name is required");

        if (string.IsNullOrEmpty(user.Email))
            return new ValidationResult(false, "Email is required");

        if (!user.Email.Contains("@") || !user.Email.Contains("."))
            return new ValidationResult(false, "Invalid email format");

        if (user.Age < 0)
            return new ValidationResult(false, "Age must be positive");

        if (user.Age < 18)
            return new ValidationResult(false, "Must be 18 or older");

        return new ValidationResult(true, "");
    }
}

public class ValidationResult
{
    public bool IsValid { get; }
    public string ErrorMessage { get; }

    public ValidationResult(bool isValid, string errorMessage)
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }
}
