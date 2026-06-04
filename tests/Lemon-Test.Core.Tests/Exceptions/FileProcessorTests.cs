using Lemon_Test.Core.Exceptions;
using FileNotFoundException = Lemon_Test.Core.Exceptions.FileNotFoundException;

namespace Lemon_Test.Core.Tests.Exceptions;

public class FileProcessorTests
{
    [Fact]
    public void ProcessFile_WithValidFile_ReturnsProcessedResult()
    {
        // Arrange
        var processor = new FileProcessor();
        var fileName = "valid-file.txt";

        // Act
        var result = processor.ProcessFile(fileName);

        // Assert - Verify successful processing (no exception)
        Assert.StartsWith("Processed:", result);
        Assert.Contains(fileName, result);
    }

    [Fact]
    public void ProcessFile_WithInvalidExtension_ThrowsArgumentException()
    {
        // Arrange
        var processor = new FileProcessor();
        var fileName = "document.pdf";

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            processor.ProcessFile(fileName));

        // Validate exception details
        Assert.Equal("fileName", exception.ParamName);
        Assert.Contains("Only .txt files are supported", exception.Message);
    }

    [Fact]
    public void ProcessFile_WithMissingFile_ThrowsFileNotFoundException()
    {
        // Arrange
        var processor = new FileProcessor();
        var fileName = "missing-file.txt";

        // Act & Assert
        var exception = Assert.Throws<FileNotFoundException>(() =>
            processor.ProcessFile(fileName));

        // Validate custom exception properties
        Assert.Equal(fileName, exception.FileName);
        Assert.Contains($"'{fileName}' was not found", exception.Message);
    }

    [Fact]
    public void ProcessFile_WithCorruptFile_ThrowsProcessingExceptionWithInnerException()
    {
        // Arrange
        var processor = new FileProcessor();
        var fileName = "corrupt-file.txt";

        // Act & Assert
        var exception = Assert.Throws<ProcessingException>(() =>
            processor.ProcessFile(fileName));

        // Validate outer exception
        Assert.Equal(fileName, exception.FileName);
        Assert.Equal(42, exception.LineNumber);
        Assert.Contains("Data corruption detected", exception.Message);

        // Validate inner exception chain
        Assert.NotNull(exception.InnerException);
        var innerException = Assert.IsType<InvalidDataException>(exception.InnerException);
        Assert.Contains("invalid data", innerException.Message);

    }

    [Fact]
    public async Task ProcessFileAsync_WithValidFile_ReturnsResult()
    {
        // Arrange
        var processor = new FileProcessor();
        var fileName = "async-file.txt";

        // Act
        var result = await processor.ProcessFileAsync(fileName);

        // Assert - Successful async operation
        Assert.StartsWith("Async processed:", result);
        Assert.Contains(fileName, result);
    }

    [Fact]
    public async Task ProcessFileAsync_WithTimeoutFile_ThrowsTimeoutException()
    {
        // Arrange
        var processor = new FileProcessor();
        var fileName = "timeout-file.txt";

        // Act & Assert - Async exception testing
        var exception = await Assert.ThrowsAsync<TimeoutException>(() => processor.ProcessFileAsync(fileName));
        
        // Validate async exception
        Assert.Contains("timed out", exception.Message);
        Assert.Contains(fileName, exception.Message);
    }

    [Fact]
    public async Task ProcessFileAsync_WithNetworkFile_ThrowsHttpRequestException()
    {
        // Arrange
        var processor = new FileProcessor();
        var fileName = "network-file.txt";

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() =>
            processor.ProcessFileAsync(fileName));
    }

    [Fact]
    public void ValidateAge_WithBoundaryValues_ThrowsForInvalidAges()
    {
        // Arrange
        var processor = new FileProcessor();

        // Act & Assert - Test boundary conditions
        Assert.Throws<ArgumentOutOfRangeException>(() => processor.ValidateAge(-1));
        Assert.Throws<ArgumentOutOfRangeException>(() => processor.ValidateAge(151));

        // Test that valid ages don't throw
        processor.ValidateAge(0); // Should not throw
        processor.ValidateAge(25); // Should not throw
        processor.ValidateAge(150); // Should not throw
    }

    // TODO: Add more async exception test cases:
    // - Test cancellation token scenarios
    // - Practice testing exception conditions vs successful operations
    // - Test exception message validation with Assert.Contains
    // - Validate exception properties for custom exceptions
}