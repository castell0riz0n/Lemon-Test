using System.Diagnostics;
using Lemon_Test.Core.DebuggingTechniques;

namespace Lemon_Test.Core.Tests.DebuggingTechniques;

public class DataProcessorTests
{
    private readonly ITestOutputHelper _output;

    public DataProcessorTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ProcessData_WithValidInput_SystematicDebugging()
    {
        // Step 1: Log the test scenario being executed
        _output.WriteLine("=== DEBUGGING: Valid Data Processing Scenario ===");
        
        // Step 2: Set up test data with explicit logging
        var input = "hello world";
        _output.WriteLine($"Test input: '{input}'");
        
        // Step 3: Create system under test with logging
        var processor = new DataProcessor();
        _output.WriteLine("DataProcessor instance created");
        
        // Step 4: Execute with detailed logging
        _output.WriteLine("Executing data processing...");
        var stopwatch = Stopwatch.StartNew();
        
        var result = processor.ProcessData(input);
        
        stopwatch.Stop();
        _output.WriteLine($"Processing completed in {stopwatch.ElapsedMilliseconds}ms");
        
        // Step 5: Log result details
        LogProcessingResult(result);
        
        // Step 6: Verify with diagnostic context
        Assert.Equal("Success", result.Status);
        Assert.Contains("HELLO WORLD", result.Message);
        Assert.True(result.ProcessingTimeMs > 0, "Processing time should be recorded");
        
        _output.WriteLine("=== DEBUGGING: Test completed successfully ===");
    }

    [Theory]
    [InlineData("", "Input cannot be empty")]
    [InlineData("   ", "Input cannot be empty")]
    [InlineData("error data", "Input contains error keyword")]
    [InlineData("ERROR in processing", "Input contains error keyword")]
    public void ProcessData_WithInvalidInput_DebuggingFailureScenarios(string input, string expectedError)
    {
        // Debugging strategy: Use conditional breakpoints for specific scenarios
        _output.WriteLine($"=== DEBUGGING FAILURE SCENARIO: '{input}' ===");
        _output.WriteLine($"Expected error: {expectedError}");
        
        var processor = new DataProcessor();
        
        // Add timing information for debugging
        var stopwatch = Stopwatch.StartNew();
        var result = processor.ProcessData(input);
        stopwatch.Stop();
        
        // Log comprehensive failure information
        _output.WriteLine($"Processing time: {stopwatch.ElapsedMilliseconds}ms");
        LogProcessingResult(result);
        
        // Conditional debugging information
        if (string.IsNullOrWhiteSpace(input))
        {
            _output.WriteLine("Debugging: Empty/whitespace input scenario");
        }
        else if (input.ToUpper().Contains("ERROR"))
        {
            _output.WriteLine("Debugging: Error keyword detected in input");
        }
        
        // Assert with rich diagnostic context
        Assert.Equal("Failed", result.Status);
        Assert.Contains(expectedError, result.Message);
    }

    [Fact]
    public async Task ProcessDataAsync_DebuggingAsyncOperations()
    {
        // Debugging async operations requires special attention
        _output.WriteLine("=== DEBUGGING: Async Data Processing ===");
        
        var processor = new DataProcessor();
        var input = "async test data";
        
        _output.WriteLine($"Input: '{input}'");
        _output.WriteLine($"Thread ID before async call: {Thread.CurrentThread.ManagedThreadId}");
        
        // Create task and inspect its state
        var task = processor.ProcessDataAsync(input);
        _output.WriteLine($"Task created, Status: {task.Status}");
        
        // Await the task
        var result = await task;
        _output.WriteLine($"Thread ID after async call: {Thread.CurrentThread.ManagedThreadId}");
        
        // Log async operation results
        LogProcessingResult(result);
        _output.WriteLine($"Task final status: {task.Status}");
        
        // Assert
        Assert.Equal("Success", result.Status);
        Assert.Contains("ASYNC TEST DATA", result.Message);
    }

    private void LogProcessingResult(ProcessingResult result)
    {
        _output.WriteLine($"--- PROCESSING RESULT ---");
        _output.WriteLine($"Status: {result.Status}");
        _output.WriteLine($"Message: {result.Message}");
        _output.WriteLine($"Processed At: {result.ProcessedAt:yyyy-MM-dd HH:mm:ss.fff}");
        _output.WriteLine($"Processing Time: {result.ProcessingTimeMs}ms");
    }
}
