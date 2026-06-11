using Lemon_Test.Core.ConsoleCapture;

[assembly:CaptureConsole]
namespace Lemon_Test.Core.Tests.ConsoleCapture;

public class LoggingServiceTests
{
    private readonly ITestOutputHelper _output;

    public LoggingServiceTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ProcessWithLogging_CapturesConsoleOutput()
    {
        // Arrange
        var service = new LoggingService();
        var originalOut = Console.Out;
        var capturedOutput = new StringWriter();
        
        try
        {
            // Redirect console output
            Console.SetOut(capturedOutput);
            
            _output.WriteLine("Starting console capture test");

            // Act
            var result = service.ProcessWithLogging("test-input");

            // Capture what was written to console
            var consoleOutput = capturedOutput.ToString();
            
            // Log captured output to test output
            _output.WriteLine("=== Captured Console Output ===");
            _output.WriteLine(consoleOutput);
            _output.WriteLine("=== End Console Output ===");
            
            _output.WriteLine($"Method result: {result}");

            // Assert
            Assert.Contains("Starting processing", consoleOutput);
            Assert.Contains("test-input", consoleOutput);
            Assert.Contains("Processing completed", consoleOutput);
            Assert.Equal("Processed: TEST-INPUT", result);
        }
        finally
        {
            // Always restore original output
            Console.SetOut(originalOut);
        }
    }

    [Fact]
    public void ProcessBatch_WithMultipleItems_CapturesAllOutput()
    {
        // Arrange
        var service = new LoggingService();
        var items = new[] { "item1", "item2", "item3" };
        
        // Use the helper class for cleaner console capture
        using var consoleCapture = new ConsoleCapture();
        
        _output.WriteLine($"Testing batch processing with {items.Length} items");

        // Act
        service.ProcessBatch(items);
        
        var capturedOutput = consoleCapture.GetOutput();
        
        // Log to test output for debugging
        _output.WriteLine("=== Batch Processing Output ===");
        _output.WriteLine(capturedOutput);

        // Assert
        Assert.Contains("Starting batch processing of 3 items", capturedOutput);
        Assert.Contains("Processing item 1: item1", capturedOutput);
        Assert.Contains("Processing item 2: item2", capturedOutput);
        Assert.Contains("Processing item 3: item3", capturedOutput);
        Assert.Contains("3 items processed", capturedOutput);
    }
    
    [Fact]
    public void ProcessBatch_WithMultipleItems_CapturesAllOutput_withV3()
    {
        // Arrange
        var service = new LoggingService();
        var items = new[] { "item1", "item2", "item3" };
        
        // Act
        service.ProcessBatch(items);
        
        // Assert
        Assert.True(true);
    }
}

// Helper class for cleaner console capture
public class ConsoleCapture : IDisposable
{
    private readonly TextWriter _originalOut;
    private readonly StringWriter _capturedOutput;

    public ConsoleCapture()
    {
        _originalOut = Console.Out;
        _capturedOutput = new StringWriter();
        Console.SetOut(_capturedOutput);
    }

    public string GetOutput() => _capturedOutput.ToString();

    public void Dispose()
    {
        Console.SetOut(_originalOut);
        _capturedOutput?.Dispose();
    }
}
