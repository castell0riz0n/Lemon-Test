using Lemon_Test.Core.TimeoutsCancellation;

namespace Lemon_Test.Core.Tests.TimeoutsCancellation;

public class DataProcessorTests
{
    [Fact(Timeout = 6000)]
    public async Task ProcessDataAsync_WithTimeout_CompletesWithinTimeLimit()
    {
        var processor = new DataProcessor();
        
        var result = await processor.ProcessDataAsync("test data");
        
        Assert.Equal("Processed: test data", result);
    }

    [Fact (Timeout = 2000)]
    public void ProcessDataSync_TooSlow_CausesTimeout()
    {
        var processor = new DataProcessor();
        
        // This will timeout because ProcessDataSync takes 3 seconds but timeout is 2 seconds
        var result = processor.ProcessDataSync("slow data");
        
        Assert.Equal("Sync Processed: slow data", result);
    }

    [Fact(Timeout = 8000)] 
    public async Task ProcessDataAsync_WithCancellationToken_HandlesGracefully()
    {
        var processor = new DataProcessor();
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
        
        // This should be cancelled after 3 seconds
        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => processor.ProcessDataAsync("data that takes too long", cts.Token));
    }
    

    [Fact(Timeout = 3000)]
    public async Task ProcessLargeDataset_WithCancellation_CancelsGracefully()
    {
        var processor = new DataProcessor();
        var largeDataset = Enumerable.Range(1, 50).Select(i => $"item{i}").ToArray();
        
        // Should cancel before processing all 50 items
        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => processor.ProcessLargeDatasetAsync(largeDataset, TestContext.Current.CancellationToken));
    }
}

public class NetworkServiceTests
{
    [Fact]
    public async Task DownloadDataAsync_FastUrl_CompletesQuickly()
    {
        var service = new NetworkService();
        
        var result = await service.DownloadDataAsync("http://fast-service.com", TestContext.Current.CancellationToken);
        
        Assert.Contains("Downloaded data from", result);
    }

    [Fact]
    public async Task DownloadDataAsync_SlowUrl_EventuallyCompletes()
    {
        var service = new NetworkService();
        
        var result = await service.DownloadDataAsync("http://slow-service.com", TestContext.Current.CancellationToken);
        
        Assert.Contains("Downloaded data from", result);
    }

    [Fact]
    public async Task DownloadDataAsync_SlowUrlWithCancellation_CancelsEarly()
    {
        var service = new NetworkService();
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
        
        // Cancel after 1 second, before the slow service can respond
        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => service.DownloadDataAsync("http://slow-service.com", cts.Token));
    }

    [Fact] 
    public async Task PingService_WithCancellation_ReturnsQuickly()
    {
        var service = new NetworkService();
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));
        
        var result = await service.PingServiceAsync("available-service", cts.Token);
        
        Assert.True(result);
    }
}

