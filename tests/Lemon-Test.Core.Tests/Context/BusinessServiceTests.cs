using Lemon_Test.Core.Context;

namespace Lemon_Test.Core.Tests.Context;

public class BusinessServiceTests
{
    [Fact]
    public void ProcessData_WithDiagnosticMessages_SendsDebugInfo()
    {
        var context = TestContext.Current;
        
        // xUnit v3 feature: Send diagnostic messages from anywhere in the test
        context.SendDiagnosticMessage("Starting ProcessData test with diagnostic messaging");
        context.SendDiagnosticMessage($"Test started at: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} UTC");
        
        var logger = new TestContextLogger(nameof(ProcessData_WithDiagnosticMessages_SendsDebugInfo));
        var service = new BusinessService(logger);

        context.SendDiagnosticMessage("About to process test data");
        var result = service.ProcessData("diagnostic test");
        context.SendDiagnosticMessage($"Processing completed with result: {result}");

        Assert.Equal("Processed_DIAGNOSTIC TEST", result);
        
        context.SendDiagnosticMessage("Test completed successfully");
    }

    [Fact]
    public void ProcessData_WithKeyValueStorage_SharesDataBetweenStages()
    {
        var context = TestContext.Current;
        
        // xUnit v3 feature: Store information that can be used across test pipeline stages
        context.KeyValueStorage["test_start_time"] = DateTime.UtcNow.ToString("O");
        context.KeyValueStorage["test_environment"] = "Development";
        context.KeyValueStorage["test_category"] = "Unit";
        
        context.SendDiagnosticMessage("Stored test metadata in KeyValueStorage");
        
        var logger = new TestContextLogger(nameof(ProcessData_WithKeyValueStorage_SharesDataBetweenStages));
        var service = new BusinessService(logger);

        // Retrieve stored information
        var startTime = context.KeyValueStorage["test_start_time"];
        var environment = context.KeyValueStorage["test_environment"];
        
        context.SendDiagnosticMessage($"Retrieved start time: {startTime}");
        context.SendDiagnosticMessage($"Retrieved environment: {environment}");
        
        var result = service.ProcessData($"storage test in {environment}");

        Assert.Equal("Processed_STORAGE TEST IN DEVELOPMENT", result);
        
        // Store completion information
        context.KeyValueStorage["test_result"] = result;
        context.KeyValueStorage["test_end_time"] = DateTime.UtcNow.ToString("O");
    }

    [Fact]
    public void ProcessData_WithAttachments_AddsTestResults()
    {
        var context = TestContext.Current;
        
        var logger = new TestContextLogger(nameof(ProcessData_WithAttachments_AddsTestResults));
        var service = new BusinessService(logger);

        // xUnit v3 feature: Add attachments to test results
        var testData = "This is test input data for processing";
        context.AddAttachment("test-input.txt", testData);
        
        context.SendDiagnosticMessage("Added test input as attachment");
        
        var result = service.ProcessData("attachment test");
        
        // Add result as attachment
        var resultData = $"Test Result: {result}\nProcessed at: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} UTC";
        context.AddAttachment("test-output.txt", resultData);
        
        // Add JSON attachment with test metadata
        var metadata = $$"""
        {
            "testName": "{{nameof(ProcessData_WithAttachments_AddsTestResults)}}",
            "input": "attachment test",
            "output": "{{result}}",
            "timestamp": "{{DateTime.UtcNow:O}}",
            "success": true
        }
        """;
        var metadataBytes = System.Text.Encoding.UTF8.GetBytes(metadata);
        context.AddAttachment("test-metadata.json", metadataBytes, "application/json");
        
        context.SendDiagnosticMessage("Added test output and metadata as attachments");

        Assert.Equal("Processed_ATTACHMENT TEST", result);
    }

    [Fact]
    public void ProcessData_WithWarnings_ReportsIssues()
    {
        var context = TestContext.Current;
        
        var logger = new TestContextLogger(nameof(ProcessData_WithWarnings_ReportsIssues));
        var service = new BusinessService(logger);

        // xUnit v3 feature: Add warnings to test results
        context.AddWarning("This test is using deprecated input format");
        context.SendDiagnosticMessage("Added warning about deprecated format");
        
        var result = service.ProcessData("warning test");
        
        // Check for potential issues and add warnings
        if (result.Contains("WARNING"))
        {
            context.AddWarning("Result contains warning indicators");
        }
        
        if (DateTime.UtcNow.Hour < 9 || DateTime.UtcNow.Hour > 17)
        {
            context.AddWarning("Test running outside business hours - results may vary");
        }
        
        context.SendDiagnosticMessage("Completed warning demonstration");

        Assert.Equal("Processed_WARNING TEST", result);
    }

    [Fact]
    public async Task ProcessDataAsync_WithTestCancellation_DemonstratesCancelCurrentTest()
    {
        var context = TestContext.Current;
        
        context.SendDiagnosticMessage("Starting cancellation demonstration");
        
        var logger = new TestContextLogger(nameof(ProcessDataAsync_WithTestCancellation_DemonstratesCancelCurrentTest));
        var service = new BusinessService(logger);

        // Store test state
        context.KeyValueStorage["cancellation_test"] = "started";
        
        try
        {
            // Simulate a condition that would require test cancellation
            var shouldCancel = false; // In real scenarios, this might be based on environment conditions
            
            if (shouldCancel)
            {
                context.SendDiagnosticMessage("Cancelling test due to environment conditions");
                context.AddWarning("Test cancelled due to external conditions");
                
                // xUnit v3 feature: Cancel the current test
                context.CancelCurrentTest();
            }
            
            var result = await service.ProcessDataAsync("cancellation test");
            
            context.KeyValueStorage["cancellation_test"] = "completed";
            context.SendDiagnosticMessage("Test completed without cancellation");
            
            Assert.Equal("Processed_CANCELLATION TEST", result);
        }
        catch (OperationCanceledException)
        {
            context.SendDiagnosticMessage("Test was cancelled as expected");
            context.KeyValueStorage["cancellation_test"] = "cancelled";
            throw;
        }
    }

    [Fact]
    public void ProcessData_ComprehensiveTestContextDemo_ShowsAllFeatures()
    {
        var context = TestContext.Current;
        
        // 1. Diagnostic Messages
        context.SendDiagnosticMessage("=== Comprehensive TestContext Demo ===");
        context.SendDiagnosticMessage($"Test: {nameof(ProcessData_ComprehensiveTestContextDemo_ShowsAllFeatures)}");
        
        // 2. Key-Value Storage
        context.KeyValueStorage["demo_phase"] = "initialization";
        context.KeyValueStorage["demo_start"] = DateTime.UtcNow.ToString("O");
        
        // 3. Warnings
        context.AddWarning("This is a comprehensive demo - expect multiple attachments");
        
        // 4. Test execution
        var logger = new TestContextLogger("ComprehensiveDemo");
        var service = new BusinessService(logger);
        
        context.KeyValueStorage["demo_phase"] = "execution";
        context.SendDiagnosticMessage("Starting business service processing");
        
        var result = service.ProcessData("comprehensive demo");
        
        // 5. Attachments
        var executionLog = $"""
        Comprehensive TestContext Demo Log
        ==================================
        Test Name: {nameof(ProcessData_ComprehensiveTestContextDemo_ShowsAllFeatures)}
        Start Time: {context.KeyValueStorage["demo_start"]}
        Execution Phase: {context.KeyValueStorage["demo_phase"]}
        Input: comprehensive demo
        Output: {result}
        End Time: {DateTime.UtcNow:O}
        
        Features Demonstrated:
        - SendDiagnosticMessage: ✓
        - KeyValueStorage: ✓
        - AddWarning: ✓
        - AddAttachment: ✓
        - Test pipeline information: ✓
        """;
        
        context.AddAttachment("comprehensive-demo-log.txt", executionLog);
        
        // Binary attachment example
        var binaryData = System.Text.Encoding.UTF8.GetBytes("Binary test data for comprehensive demo");
        context.AddAttachment("binary-data.bin", binaryData, "application/octet-stream");
        
        // 6. Final state
        context.KeyValueStorage["demo_phase"] = "completed";
        context.KeyValueStorage["demo_result"] = result;
        context.SendDiagnosticMessage("All TestContext features demonstrated successfully");
        
        Assert.Equal("Processed_COMPREHENSIVE DEMO", result);
    }
}

/// <summary>
/// Test logger implementation that uses TestContext information
/// </summary>
public class TestContextLogger : ILogger
{
    private readonly string _testName;
    private readonly bool _isVerbose;

    public TestContextLogger(string testName, bool isVerbose = false)
    {
        _testName = testName;
        _isVerbose = isVerbose;
    }

    public void LogInfo(string message)
    {
        Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss.fff}] [{_testName}] INFO: {message}");
    }

    public void LogDebug(string message)
    {
        if (_isVerbose)
        {
            Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss.fff}] [{_testName}] DEBUG: {message}");
        }
    }

    public void LogError(string message)
    {
        Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss.fff}] [{_testName}] ERROR: {message}");
    }
}
