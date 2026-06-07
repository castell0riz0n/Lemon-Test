using Lemon_Test.Core.Domain;
using Lemon_Test.Core.Repositories;

namespace Lemon_Test.Core.Tests.ClassFixtures;

public class UserRepositoryFixture : IDisposable
{
    private readonly List<User> _seedUsers;
    public IUserRepository UserRepository;

    public UserRepositoryFixture()
    {
        // Simulate expensive database setup (this runs only ONCE per test class)
        Thread.Sleep(200); // Simulate database creation time
        _seedUsers = new();
        UserRepository = new InMemoryUserRepository();
        SeedTestData();
    }
    
    private void SeedTestData()
    {
        var usersToCreate = new[]
        {
            new User { Name = "Alice Johnson", Email = "alice@fixture.com", IsActive = true },
            new User { Name = "Bob Smith", Email = "bob@fixture.com", IsActive = true },
            new User { Name = "Charlie Brown", Email = "charlie@fixture.com", IsActive = false }
        };

        foreach (var user in usersToCreate)
        {
            var createdUser = UserRepository.Create(user);
            _seedUsers.Add(createdUser);
        }
    }

    public void Dispose()
    {
        _seedUsers.Clear();
    }
}