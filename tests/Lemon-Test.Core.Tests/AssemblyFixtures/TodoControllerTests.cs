using System.Net;
using System.Net.Http.Json;
using Lemon_Test.Core.Tests.AssemblyFixtures.Fixtures;
using LifecycleFeatures.Api.Models;

namespace Lemon_Test.Core.Tests.AssemblyFixtures;

public class TodoTests
{
    private readonly HttpClient _client;

    public TodoTests(ApiTestFixture fixture)
    {
        _client = fixture.CreateClient();
    }

    [Fact]
    public async Task CreateTodo_ShouldReturnCreatedTodo()
    {
        // Arrange
        var todo = new TodoItem
        {
            Title = "Test Todo",
            Description = "Test Description",
            IsCompleted = false
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/todos", todo);
        var createdTodo = await response.Content.ReadFromJsonAsync<TodoItem>();

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(createdTodo);
        Assert.Equal(todo.Title, createdTodo.Title);
        Assert.Equal(todo.Description, createdTodo.Description);
        Assert.False(createdTodo.IsCompleted);
    }

    [Fact]
    public async Task GetTodo_ShouldReturnTodo()
    {
        // Arrange
        var todo = new TodoItem
        {
            Title = "Another Todo",
            Description = "Test Description",
            IsCompleted = false
        };
        
        var createResponse = await _client.PostAsJsonAsync("/api/todos", todo);
        var createdTodo = await createResponse.Content.ReadFromJsonAsync<TodoItem>();

        // Act
        var response = await _client.GetAsync($"/api/todos/{createdTodo!.Id}", TestContext.Current.CancellationToken);
        var retrievedTodo = await response.Content.ReadFromJsonAsync<TodoItem>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(retrievedTodo);
        Assert.Equal(createdTodo.Id, retrievedTodo.Id);
        Assert.Equal(createdTodo.Title, retrievedTodo.Title);
        Assert.Equal(createdTodo.Description, retrievedTodo.Description);
    }
}