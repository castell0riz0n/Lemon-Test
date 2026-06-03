namespace Lemon_Test.Core.TestNaming;

public class EmailValidator
{
    public bool IsValid(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        return email.Contains("@");
    }
}
