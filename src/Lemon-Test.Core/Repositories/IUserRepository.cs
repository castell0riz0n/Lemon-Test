using Lemon_Test.Core.Domain;

namespace Lemon_Test.Core.Repositories;

/// <summary>
/// Simple repository interface for dependency injection demos
/// </summary>
public interface IUserRepository
{
    User? GetById(int id);
    IList<User> GetActiveUsers();
    User Create(User user);
    void Update(User user);
    void Delete(int id);
}
