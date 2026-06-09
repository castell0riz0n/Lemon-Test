using Lemon_Test.Core.ParallelExecution;

namespace Lemon_Test.Core.Tests.ParallelExecution;

[Collection("Ordered File Tests")]
[TestCaseOrderer(typeof(PriorityOrderer))]
public class OrderedTests
{
    private readonly FileManager _fileManager = new();

    [Fact]
    public void Test1_CreateFile_SetsUpForNextTests()
    {
        Thread.Sleep(2000);
        var fileName = "test1-file.txt";

        _fileManager.CreateFile(fileName, "Test 1 content");

        Assert.True(_fileManager.FileExists(fileName));

        // Cleanup
        _fileManager.DeleteFile(fileName);
    }

    [Fact]
    public void Test2_ProcessFile_ModifiesContent()
    {
        Thread.Sleep(2000);
        var fileName = "test2-file.txt";

        _fileManager.CreateFile(fileName, "Initial content");
        _fileManager.CreateFile(fileName, "Modified content"); // Overwrite

        var content = _fileManager.ReadFile(fileName);
        Assert.Equal("Modified content", content);

        // Cleanup
        _fileManager.DeleteFile(fileName);
    }

    [Fact]
    public void Test3_VerifyOperations_ChecksResults()
    {
        Thread.Sleep(2000);
        var fileName = "test3-file.txt";

        _fileManager.CreateFile(fileName, "Verification content");

        Assert.True(_fileManager.FileExists(fileName));
        var content = _fileManager.ReadFile(fileName);
        Assert.Equal("Verification content", content);

        // Cleanup
        _fileManager.DeleteFile(fileName);
    }
}