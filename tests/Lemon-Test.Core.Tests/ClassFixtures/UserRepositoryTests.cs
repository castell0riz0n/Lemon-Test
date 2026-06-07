using Lemon_Test.Core.Domain;

namespace Lemon_Test.Core.Tests.ClassFixtures;

// Fixtures -> Setup + Teardown
// ClassFixture

public class UserRepositoryTests : IClassFixture<UserRepositoryFixture>
{
    private readonly UserRepositoryFixture _fixture;

    public UserRepositoryTests(UserRepositoryFixture  fixture)
    {
        _fixture = fixture;
    }
  
    
    [Fact]
    public void GetActiveUsers_WithSeededData_ReturnsActiveUsers()
    {
        // Act
        var activeUsers = _fixture.UserRepository.GetActiveUsers();

        // Assert
        Assert.Equal(2, activeUsers.Count); // Alice and Bob are active
        Assert.Contains(activeUsers, u => u.Name == "Alice Johnson");
        Assert.Contains(activeUsers, u => u.Name == "Bob Smith");
        Assert.All(activeUsers, user => Assert.True(user.IsActive));
    }

    [Fact]
    public void GetActiveUsers_WithSeededData_ReturnsOnlyActive()
    {
        // Act
        var activeUsers = _fixture.UserRepository.GetActiveUsers().ToList();

        // Assert
        Assert.Equal(2, activeUsers.Count); // Alice and Bob are active
        Assert.All(activeUsers, user => Assert.True(user.IsActive));
    }

    [Fact]
    public void Create_NewUser_AddsToSharedRepository()
    {
        // Arrange
        var initialCount = _fixture.UserRepository.GetActiveUsers().Count;
        var newUser = new User { Name = "David Wilson", Email = "david@test.com", IsActive = true };

        // Act
        var createdUser = _fixture.UserRepository.Create(newUser);

        // Assert
        Assert.NotNull(createdUser);
        Assert.True(createdUser.Id > 0);
        Assert.Equal("David Wilson", createdUser.Name);
        
        // Verify it was added to shared repository (affects other tests in this class!)
        var currentActiveCount = _fixture.UserRepository.GetActiveUsers().Count;
        Assert.Equal(initialCount + 1, currentActiveCount);
    }
    
}