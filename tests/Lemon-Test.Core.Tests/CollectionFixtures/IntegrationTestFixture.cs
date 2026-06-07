using Lemon_Test.Core.Domain;
using Lemon_Test.Core.Repositories;

namespace Lemon_Test.Core.Tests.CollectionFixtures;

public class IntegrationTestFixture : IDisposable
{
    public IUserRepository UserRepository { get; }
    public List<User> SeedUsers { get; private set; } = new();

    public IntegrationTestFixture()
    {
        Console.WriteLine("🔧 IntegrationTestFixture - Starting VERY expensive setup...");
        
        // Simulate very expensive setup (database, external services, etc.)
        Thread.Sleep(500); // Simulate expensive integration setup
        
        UserRepository = new InMemoryUserRepository();
        SeedTestData();
        
        Console.WriteLine($"✅ Integration setup complete. Ready with {SeedUsers.Count} users");
    }

    private void SeedTestData()
    {
        var usersToCreate = new[]
        {
            new User { Name = "Integration User 1", Email = "user1@integration.com", IsActive = true },
            new User { Name = "Integration User 2", Email = "user2@integration.com", IsActive = true },
            new User { Name = "Inactive User", Email = "inactive@integration.com", IsActive = false }
        };

        foreach (var user in usersToCreate)
        {
            var createdUser = UserRepository.Create(user);
            SeedUsers.Add(createdUser);
        }
    }

    public void Dispose()
    {
        Console.WriteLine("🧹 IntegrationTestFixture - Cleaning up...");
        Thread.Sleep(100); // Simulate cleanup time
        Console.WriteLine("✅ Integration cleanup complete");
    }
}
