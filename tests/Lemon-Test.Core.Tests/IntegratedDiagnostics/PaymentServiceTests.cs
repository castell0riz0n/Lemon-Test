using System.Diagnostics;
using System.Text;
using Lemon_Test.Core.IntegratedDiagnostics;

namespace Lemon_Test.Core.Tests.IntegratedDiagnostics;

public class PaymentServiceTests
{
    private readonly ITestOutputHelper _output;

    public PaymentServiceTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(299.99, 100, true, "Standard payment processing")]
    [InlineData(1299.99, 200, true, "High-value payment processing")]
    [InlineData(-50.00, 300, false, "Negative amount validation")]
    [InlineData(15000.00, 400, false, "Amount exceeds limit validation")]
    public void ProcessPayment_ComprehensiveScenario_WithIntegratedDiagnostics(
        decimal amount, int customerId, bool expectedSuccess, string scenario)
    {
        // === PHASE 1: Test Setup and Environment Diagnostics ===
        _output.WriteLine("=".PadRight(60, '='));
        _output.WriteLine($"PAYMENT PROCESSING TEST: {scenario.ToUpper()}");
        _output.WriteLine("=".PadRight(60, '='));
        
        LogTestEnvironment();
        LogTestParameters(amount, customerId, expectedSuccess, scenario);
        
        var testStartTime = DateTime.UtcNow;
        var stopwatch = Stopwatch.StartNew();
        var initialMemory = GC.GetTotalMemory(false);
        
        // === PHASE 2: System Under Test Setup ===
        _output.WriteLine("\n--- SYSTEM SETUP ---");
        var paymentService = new PaymentService();
        _output.WriteLine("PaymentService instance created");
        
        // === PHASE 3: Execution with Comprehensive Monitoring ===
        PaymentResult? result = null;
        Exception? caughtException = null;
        
        try
        {
            _output.WriteLine($"\n--- EXECUTION PHASE ---");
            _output.WriteLine($"Processing payment: Amount={amount:C}, Customer={customerId}");
            
            result = paymentService.ProcessPayment(amount, customerId);
            
            _output.WriteLine($"Payment processing completed: Success={result.IsSuccess}");
        }
        catch (Exception ex)
        {
            caughtException = ex;
            _output.WriteLine($"Payment processing failed with exception: {ex.Message}");
        }
        finally
        {
            stopwatch.Stop();
        }
        
        // === PHASE 4: Performance and Memory Analysis ===
        var finalMemory = GC.GetTotalMemory(false);
        var memoryDelta = finalMemory - initialMemory;
        
        LogPerformanceMetrics(stopwatch.ElapsedMilliseconds, memoryDelta);
        
        // === PHASE 5: Comprehensive Diagnostic Reporting ===
        var diagnosticReport = GenerateComprehensiveDiagnosticReport(
            amount, customerId, result, caughtException, stopwatch.ElapsedMilliseconds, memoryDelta);
        
        _output.WriteLine(diagnosticReport);
        
        // === PHASE 6: Assertions with Rich Context ===
        if (expectedSuccess)
        {
            Assert.Null(caughtException);
            
            Assert.NotNull(result);
            Assert.True(result.IsSuccess, 
                $"Payment should succeed for {scenario}. " +
                $"Amount: {amount:C}, Customer: {customerId}, " +
                $"Error: {result.ErrorMessage}");
            
            Assert.NotNull(result.TransactionId);
            Assert.True(result.ProcessingTimeMs > 0, "Processing time should be recorded");
            Assert.Equal(amount, result.ProcessedAmount);
        }
        else
        {
            Assert.NotNull(result);
            Assert.False(result.IsSuccess, 
                $"Payment should fail for {scenario}. " +
                $"Amount: {amount:C}, Customer: {customerId}");
            
            Assert.NotNull(result.ErrorMessage);
            Assert.Null(result.TransactionId);
            _output.WriteLine($"Expected failure occurred: {result.ErrorMessage}");
        }
        
        // === PHASE 7: Service State Verification ===
        var processedPayments = paymentService.GetProcessedPayments();
        _output.WriteLine($"\n--- SERVICE STATE ---");
        _output.WriteLine($"Total processed payments in service: {processedPayments.Count}");
        
        if (expectedSuccess)
        {
            Assert.True(processedPayments.Count > 0, "Successful payments should be recorded");
        }
        
        _output.WriteLine($"Test completed in {stopwatch.ElapsedMilliseconds}ms");
        _output.WriteLine("=".PadRight(60, '='));
    }

    [Fact]
    public void ProcessPayment_PerformanceUnderLoad_WithDetailedMetrics()
    {
        _output.WriteLine("=== PERFORMANCE LOAD TEST ===");
        
        var paymentService = new PaymentService();
        var paymentCount = 50;
        var results = new List<PaymentResult>();
        
        var overallStopwatch = Stopwatch.StartNew();
        var initialMemory = GC.GetTotalMemory(false);
        
        _output.WriteLine($"Processing {paymentCount} payments...");
        
        for (int i = 1; i <= paymentCount; i++)
        {
            var amount = Random.Shared.Next(100, 1000);
            var customerId = 1000 + i;
            
            var result = paymentService.ProcessPayment(amount, customerId);
            results.Add(result);
            
            // Log progress every 10 payments
            if (i % 10 == 0)
            {
                var currentMemory = GC.GetTotalMemory(false);
                _output.WriteLine($"Processed {i}/{paymentCount} payments. " +
                                $"Memory: {currentMemory / 1024:N0} KB");
            }
        }
        
        overallStopwatch.Stop();
        var finalMemory = GC.GetTotalMemory(false);
        
        // Analyze results
        var successfulPayments = results.Count(r => r.IsSuccess);
        var failedPayments = results.Count(r => !r.IsSuccess);
        var avgProcessingTime = results.Average(r => r.ProcessingTimeMs);
        var totalProcessingTime = results.Sum(r => r.ProcessingTimeMs);
        
        _output.WriteLine($"\n=== PERFORMANCE ANALYSIS ===");
        _output.WriteLine($"Total payments: {paymentCount}");
        _output.WriteLine($"Successful: {successfulPayments}");
        _output.WriteLine($"Failed: {failedPayments}");
        _output.WriteLine($"Overall execution time: {overallStopwatch.ElapsedMilliseconds}ms");
        _output.WriteLine($"Total simulated processing time: {totalProcessingTime}ms");
        _output.WriteLine($"Average processing time per payment: {avgProcessingTime:F2}ms");
        _output.WriteLine($"Throughput: {paymentCount / (overallStopwatch.ElapsedMilliseconds / 1000.0):F1} payments/second");
        _output.WriteLine($"Memory usage: {(finalMemory - initialMemory) / 1024:N0} KB");
        
        // Performance assertions
        Assert.True(successfulPayments > 0, "At least some payments should succeed");
        Assert.True(overallStopwatch.ElapsedMilliseconds < 10000, 
            $"Load test should complete within 10 seconds. Actual: {overallStopwatch.ElapsedMilliseconds}ms");
    }

    private void LogTestEnvironment()
    {
        _output.WriteLine($"\n--- ENVIRONMENT DIAGNOSTICS ---");
        _output.WriteLine($"Machine: {Environment.MachineName}");
        _output.WriteLine($"OS: {Environment.OSVersion}");
        _output.WriteLine($"Process ID: {Environment.ProcessId}");
        _output.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}");
        _output.WriteLine($"UTC Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff}");
    }

    private void LogTestParameters(decimal amount, int customerId, bool expectedSuccess, string scenario)
    {
        _output.WriteLine($"\n--- TEST PARAMETERS ---");
        _output.WriteLine($"Scenario: {scenario}");
        _output.WriteLine($"Amount: {amount:C}");
        _output.WriteLine($"Customer ID: {customerId}");
        _output.WriteLine($"Expected Success: {expectedSuccess}");
    }

    private void LogPerformanceMetrics(long elapsedMs, long memoryDelta)
    {
        _output.WriteLine($"\n--- PERFORMANCE METRICS ---");
        _output.WriteLine($"Execution Time: {elapsedMs}ms");
        _output.WriteLine($"Memory Delta: {memoryDelta / 1024:N0} KB");
        _output.WriteLine($"GC Collections: Gen0={GC.CollectionCount(0)}, Gen1={GC.CollectionCount(1)}, Gen2={GC.CollectionCount(2)}");
    }

    private string GenerateComprehensiveDiagnosticReport(decimal amount, int customerId, 
        PaymentResult? result, Exception? exception, long elapsedMs, long memoryDelta)
    {
        var report = new StringBuilder();
        
        report.AppendLine($"\n" + "=".PadRight(60, '='));
        report.AppendLine($"COMPREHENSIVE DIAGNOSTIC REPORT");
        report.AppendLine($"=".PadRight(60, '='));
        
        report.AppendLine($"Timestamp: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} UTC");
        report.AppendLine($"Customer ID: {customerId}");
        report.AppendLine($"Amount: {amount:C}");
        
        report.AppendLine($"\n--- EXECUTION SUMMARY ---");
        report.AppendLine($"Status: {(exception != null ? "FAILED" : result?.IsSuccess.ToString() ?? "UNKNOWN")}");
        report.AppendLine($"Duration: {elapsedMs}ms");
        report.AppendLine($"Memory Impact: {memoryDelta / 1024:N0} KB");
        
        if (exception != null)
        {
            report.AppendLine($"Exception: {exception.GetType().Name}: {exception.Message}");
        }
        else if (result != null)
        {
            report.AppendLine($"Transaction ID: {result.TransactionId ?? "N/A"}");
            report.AppendLine($"Processing Time: {result.ProcessingTimeMs}ms");
            report.AppendLine($"Processed Amount: {result.ProcessedAmount:C}");
            report.AppendLine($"Processed At: {result.ProcessedAt:yyyy-MM-dd HH:mm:ss.fff}");
            
            if (!result.IsSuccess)
            {
                report.AppendLine($"Error Message: {result.ErrorMessage}");
            }
        }
        
        return report.ToString();
    }
}
