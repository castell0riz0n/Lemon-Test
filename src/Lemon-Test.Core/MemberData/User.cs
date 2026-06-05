namespace Lemon_Test.Core.MemberData;

public class User
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Age { get; set; }

    public User(string name, string email, int age)
    {
        Name = name;
        Email = email;
        Age = age;
    }
}
