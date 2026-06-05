namespace Lemon_Test.Core.InlineData;

public class EmailValidator
{
    public bool IsValid(string? email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        if (!email.Contains("@"))
            return false;

        var parts = email.Split('@');
        if (parts.Length != 2)
            return false;

        var localPart = parts[0];
        var domainPart = parts[1];

        if (string.IsNullOrEmpty(localPart) || string.IsNullOrEmpty(domainPart))
            return false;

        if (!domainPart.Contains("."))
            return false;

        // Check for invalid domain patterns like ".com" or "domain."
        if (domainPart.StartsWith(".") || domainPart.EndsWith("."))
            return false;

        return true;
    }
}
