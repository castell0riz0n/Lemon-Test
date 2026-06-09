using Lemon_Test.Core.ParallelExecution;

// Disable parallelization entirely for this assembly
// [assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Lemon_Test.Core.Tests.ParallelExecution;


[Collection("File System Tests")]
public class FileManagerCollectionTests
{
    private readonly FileManager _fileManager = new();

    [Fact]
    public void CreateFile_WithCollection_NoConflicts()
    {
        var fileName = "collection-test-file.txt";
        var content = "Collection test content";

        _fileManager.CreateFile(fileName, content);
        var result = _fileManager.ReadFile(fileName);

        Assert.Equal(content, result);

        // Cleanup
        _fileManager.DeleteFile(fileName);
    }

    [Fact]
    public void ProcessFiles_WithCollection_WorksReliably()
    {
        var fileNames = new[] { "file1.txt", "file2.txt", "file3.txt" };

        _fileManager.ProcessFiles(fileNames);

        foreach (var fileName in fileNames)
        {
            Assert.True(_fileManager.FileExists(fileName));
            _fileManager.DeleteFile(fileName);
        }
    }
}

[Collection("File System Tests")]
public class AnotherFileManagerCollectionTests
{
    private readonly FileManager _fileManager = new();

    [Fact]
    public void FileOperations_InSameCollection_RunSequentially()
    {
        var fileName = "sequential-test.txt";

        _fileManager.CreateFile(fileName, "Sequential content");
        
        // This test can safely use the same file operations because it's in the same collection
        Assert.True(_fileManager.FileExists(fileName));
        
        var content = _fileManager.ReadFile(fileName);
        Assert.Equal("Sequential content", content);

        _fileManager.DeleteFile(fileName);
    }
}
