using Lemon_Test.Core.Domain;

namespace Lemon_Test.Core.Tests.CollectionFixtures;

[Collection("IntegrationTestCollection")]
public class UserIntegrationTests
{
    private readonly IntegrationTestFixture _fixture;

    public UserIntegrationTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
        Console.WriteLine($"UserIntegrationTests constructor - Using shared integration fixture");
    }

    [Fact]
    public void GetActiveUsers_WithIntegrationData_ReturnsActiveUsers()
    {
        // Act
        var activeUsers = _fixture.UserRepository.GetActiveUsers().ToList();

        // Assert - Collection fixtures are shared, so count may vary due to other test classes
        Assert.True(activeUsers.Count >= 2); // At least our 2 active seed users
        Assert.Contains(activeUsers, u => u.Name == "Integration User 1");
        Assert.Contains(activeUsers, u => u.Name == "Integration User 2");
        Assert.All(activeUsers, user => Assert.True(user.IsActive));
    }

    [Fact]
    public void Create_NewUser_AddsToSharedRepository()
    {
        // Arrange
        var initialCount = _fixture.UserRepository.GetActiveUsers().Count;
        var newUser = new User { Name = "Collection User", Email = "collection@test.com", IsActive = true };

        // Act
        var createdUser = _fixture.UserRepository.Create(newUser);

        // Assert
        Assert.NotNull(createdUser);
        Assert.True(createdUser.Id > 0);
        
        // Verify it's in the shared repository (affects ALL test classes in this collection!)
        var currentCount = _fixture.UserRepository.GetActiveUsers().Count;
        Assert.Equal(initialCount + 1, currentCount);
    }

    [Fact]
    public void SeedData_FromIntegrationFixture_IsAccessible()
    {
        // Assert that seed data from the collection fixture is available
        Assert.NotEmpty(_fixture.SeedUsers);
        Assert.Equal(3, _fixture.SeedUsers.Count);
        
        var integrationUser = _fixture.SeedUsers.FirstOrDefault(u => u.Name == "Integration User 1");
        Assert.NotNull(integrationUser);
        Assert.Equal("user1@integration.com", integrationUser.Email);
    }
}
