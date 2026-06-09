namespace Lemon_Test.Core.Context;

/// <summary>
/// Business service that can be configured for different testing scenarios
/// </summary>
public class BusinessService
{
    private readonly ILogger _logger;
    private readonly bool _isDebugMode;

    public BusinessService(ILogger logger, bool isDebugMode = false)
    {
        _logger = logger;
        _isDebugMode = isDebugMode;
    }

    public string ProcessData(string input)
    {
        _logger.LogInfo($"Processing data: {input}");
        
        if (_isDebugMode)
        {
            _logger.LogDebug($"Debug: Input length = {input.Length}");
            _logger.LogDebug("Debug: Starting validation");
        }

        if (string.IsNullOrEmpty(input))
        {
            _logger.LogError("Invalid input: null or empty");
            throw new ArgumentException("Input cannot be null or empty");
        }

        var result = $"Processed_{input.ToUpper()}";
        
        if (_isDebugMode)
        {
            _logger.LogDebug($"Debug: Generated result = {result}");
        }

        _logger.LogInfo("Data processing completed successfully");
        return result;
    }

    public async Task<string> ProcessDataAsync(string input, CancellationToken cancellationToken = default)
    {
        _logger.LogInfo($"Starting async processing for: {input}");
        
        // Simulate async work with cancellation support
        await Task.Delay(100, cancellationToken);
        
        var result = ProcessData(input);
        
        _logger.LogInfo("Async processing completed");
        return result;
    }
}

/// <summary>
/// Simple logger interface for demonstration
/// </summary>
public interface ILogger
{
    void LogInfo(string message);
    void LogDebug(string message);
    void LogError(string message);
}

/// <summary>
/// Performance monitor that tracks operation timings
/// </summary>
public class PerformanceMonitor
{
    private readonly Dictionary<string, long> _timings = new();

    public void StartTiming(string operationName)
    {
        _timings[operationName] = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    public long StopTiming(string operationName)
    {
        if (!_timings.ContainsKey(operationName))
            throw new InvalidOperationException($"No timing started for operation: {operationName}");

        var startTime = _timings[operationName];
        var endTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var duration = endTime - startTime;
        
        _timings.Remove(operationName);
        return duration;
    }

    public Dictionary<string, long> GetAllTimings()
    {
        return new Dictionary<string, long>(_timings);
    }
}
