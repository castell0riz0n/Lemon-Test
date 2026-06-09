using Lemon_Test.Core.ParallelExecution;

namespace Lemon_Test.Core.Tests.ParallelExecution;

public class FileManagerTests
{
    private readonly FileManager _fileManager = new();

    [Fact]
    public void CreateAndReadFile_ValidContent_ReturnsExpectedContent()
    {
        // These tests will conflict when run in parallel because they use the same file name
        var fileName = "test-file.txt";
        var expectedContent = "Hello World";

        _fileManager.CreateFile(fileName, expectedContent);
        var actualContent = _fileManager.ReadFile(fileName);

        Assert.Equal(expectedContent, actualContent);

        // Cleanup
        _fileManager.DeleteFile(fileName);
    }

    [Fact]
    public void FileExists_AfterCreation_ReturnsTrue()
    {
        var fileName = "test-file.txt";

        _fileManager.CreateFile(fileName, "Some content");
        var exists = _fileManager.FileExists(fileName);

        Assert.True(exists);

        // Cleanup
        _fileManager.DeleteFile(fileName);
    }
}

public class AnotherFileManagerTests
{
    private readonly FileManager _fileManager = new();

    [Fact]
    public void ProcessMultipleFiles_ValidInput_CreatesAllFiles()
    {
        // This will also conflict with the above tests
        var fileNames = new[] { "test-file.txt", "another-file.txt" };

        _fileManager.ProcessFiles(fileNames);

        foreach (var fileName in fileNames)
        {
            Assert.True(_fileManager.FileExists(fileName));
            _fileManager.DeleteFile(fileName); // Cleanup
        }
    }
}
