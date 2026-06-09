namespace Lemon_Test.Core.TimeoutsCancellation;

/// <summary>
/// Data processor that demonstrates long-running operations and cancellation support
/// </summary>
public class DataProcessor
{
    public async Task<string> ProcessDataAsync(string data, CancellationToken cancellationToken = default)
    {
        // Simulate long-running data processing
        for (int i = 0; i < 10; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            // Simulate processing each chunk
            await Task.Delay(500, cancellationToken);
        }

        return $"Processed: {data}";
    }

    public string ProcessDataSync(string data)
    {
        // Simulate synchronous processing that might take too long
        Thread.Sleep(3000); // This could cause timeout issues
        return $"Sync Processed: {data}";
    }

    public async Task<string> ProcessLargeDatasetAsync(string[] data, CancellationToken cancellationToken = default)
    {
        var results = new List<string>();

        foreach (var item in data)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            // Process each item
            await Task.Delay(200, cancellationToken);
            results.Add($"Processed: {item}");
        }

        return string.Join(", ", results);
    }
}

/// <summary>
/// Network service that simulates potentially hanging operations
/// </summary>
public class NetworkService
{
    public async Task<string> DownloadDataAsync(string url, CancellationToken cancellationToken = default)
    {
        // Simulate network delay that might hang
        await Task.Delay(2000, cancellationToken);
        
        if (url.Contains("slow"))
        {
            // Simulate very slow response
            await Task.Delay(8000, cancellationToken);
        }

        return $"Downloaded data from {url}";
    }

    public async Task<bool> PingServiceAsync(string serviceName, CancellationToken cancellationToken = default)
    {
        try
        {
            // Simulate service ping with potential timeout
            await Task.Delay(1000, cancellationToken);
            
            // Simulate service availability check
            return !serviceName.Contains("unavailable");
        }
        catch (OperationCanceledException)
        {
            return false;
        }
    }
}

/// <summary>
/// File processor that demonstrates resource cleanup during cancellation
/// </summary>
public class FileProcessor : IDisposable
{
    private bool _isProcessing = false;
    private string? _tempFile;

    public async Task<string> ProcessFileAsync(string filePath, CancellationToken cancellationToken = default)
    {
        _isProcessing = true;
        _tempFile = Path.GetTempFileName();

        try
        {
            // Simulate file processing with cancellation support
            for (int i = 0; i < 20; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(250, cancellationToken);
            }

            return $"File {filePath} processed successfully";
        }
        catch (OperationCanceledException)
        {
            // Clean up on cancellation
            CleanupTempFile();
            throw;
        }
        finally
        {
            _isProcessing = false;
        }
    }

    public bool IsProcessing => _isProcessing;

    private void CleanupTempFile()
    {
        if (!string.IsNullOrEmpty(_tempFile) && File.Exists(_tempFile))
        {
            File.Delete(_tempFile);
            _tempFile = null;
        }
    }

    public void Dispose()
    {
        CleanupTempFile();
    }
}
