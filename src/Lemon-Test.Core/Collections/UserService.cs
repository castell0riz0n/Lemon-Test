namespace Lemon_Test.Core.Collections;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public override bool Equals(object? obj)
    {
        if (obj is not User other) return false;
        return Id == other.Id && Name == other.Name && Email == other.Email;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Email);
    }
}

public class UserService
{
    private readonly List<User> _users = new();
    private readonly HashSet<string> _existingEmails = new();

    public IEnumerable<User> GetAllUsers()
    {
        return _users.AsEnumerable();
    }

    public IEnumerable<User> GetUsersByRole(string role)
    {
        return _users.Where(u => u.Role.Equals(role, StringComparison.OrdinalIgnoreCase));
    }

    public User? FindUserByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<string> GetUserEmails()
    {
        return _users.Select(u => u.Email);
    }

    public Dictionary<string, List<User>> GroupUsersByRole()
    {
        return _users.GroupBy(u => u.Role)
                    .ToDictionary(g => g.Key, g => g.ToList());
    }
    
    public User CreateUser(string firstName, string lastName)
    {
        var email = $"{firstName.ToLower()}.{lastName.ToLower()}@company.com";
        var fullName = $"{firstName} {lastName}";
        
        var user = new User
        {
            Name = firstName,
            Email = email,
            FullName = fullName,
            IsActive = true
        };
        
        return user;
    }

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public List<User> GetActiveUsers()
    {
        return _users.Where(u => u.IsActive).ToList();
    }

    public async Task CreateUserAsync(string email)
    {
        // Simulate async operation
        await Task.Delay(10);
        
        if (_existingEmails.Contains(email))
        {
            throw new InvalidOperationException($"User with email {email} already exists");
        }
        
        _existingEmails.Add(email);
        
        var user = new User
        {
            Email = email,
            Name = email.Split('@')[0],
            IsActive = true
        };
        
        _users.Add(user);
    }
}
