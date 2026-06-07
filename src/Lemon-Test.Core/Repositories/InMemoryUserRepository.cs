using Lemon_Test.Core.Domain;

namespace Lemon_Test.Core.Repositories;

/// <summary>
/// In-memory implementation for demos
/// </summary>
public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new();
    private int _nextId = 1;

    public User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);

    public IList<User> GetActiveUsers() => _users.Where(u => u.IsActive).ToList();

    public User Create(User user)
    {
        user.Id = _nextId++;
        user.CreatedAt = DateTime.UtcNow;
        _users.Add(user);
        return user;
    }

    public void Update(User user)
    {
        var existing = GetById(user.Id);
        if (existing != null)
        {
            existing.Name = user.Name;
            existing.Email = user.Email;
            existing.IsActive = user.IsActive;
        }
    }

    public void Delete(int id)
    {
        var user = GetById(id);
        if (user != null)
        {
            _users.Remove(user);
        }
    }
}
