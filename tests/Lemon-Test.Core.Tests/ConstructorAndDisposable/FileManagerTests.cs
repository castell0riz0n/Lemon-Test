using Lemon_Test.Core.FileSystem;

namespace Lemon_Test.Core.Tests.ConstructorAndDisposable;

public class FileManagerTests : IDisposable
{
    private readonly FileManager _fileManager;
    private readonly string _tempDirectory;

    // SETUP
    public FileManagerTests()
    {
        _tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_tempDirectory);
        _fileManager = new FileManager(_tempDirectory);
    }

    [Fact]
    public void CreateFile_ValidName_CreatesFile()
    {
        // Arrange
        // Act
        _fileManager.CreateFile("test.txt", "Hello World");

        // Assert
        var filePath = Path.Combine(_tempDirectory, "test.txt");
        Assert.True(File.Exists(filePath));
        Assert.Equal("Hello World", File.ReadAllText(filePath));
    }

    [Fact]
    public void ReadFile_ExistingFile_ReturnsContent()
    {
        // Arrange
        _fileManager.CreateFile("test.txt", "Updated Content");

        // Act
        var content = _fileManager.ReadFile("test.txt");

        // Assert
        Assert.Equal("Updated Content", content);
    }

    // TEARDOWN
    public void Dispose()
    {
        Directory.Delete(_tempDirectory, recursive: true);
    }
}