using System.Text.RegularExpressions;

namespace Lemon_Test.Core.StringAndNumeric;

public class EmailValidator
{
    public ValidationResult Validate(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Email address cannot be null or empty"
            };
        }

        if (!email.Contains("@"))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Email address must contain an @ symbol"
            };
        }

        var emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        if (!Regex.IsMatch(email, emailRegex))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Email address format is invalid"
            };
        }

        return new ValidationResult { IsValid = true };
    }
}

public class ValidationResult
{
    public bool IsValid { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}
