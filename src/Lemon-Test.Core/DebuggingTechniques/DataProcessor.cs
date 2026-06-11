namespace Lemon_Test.Core.DebuggingTechniques;

public class DataProcessor
{
    public ProcessingResult ProcessData(string input)
    {
        // Step 1: Validate input
        if (string.IsNullOrWhiteSpace(input))
        {
            return new ProcessingResult
            {
                Status = "Failed",
                Message = "Input cannot be empty",
                ProcessedAt = DateTime.UtcNow
            };
        }
        
        // Step 2: Transform data
        var transformed = input.Trim().ToUpper();
        
        // Step 3: Apply business rules
        if (transformed.Contains("ERROR"))
        {
            return new ProcessingResult
            {
                Status = "Failed",
                Message = "Input contains error keyword",
                ProcessedAt = DateTime.UtcNow
            };
        }
        
        // Step 4: Generate result
        return new ProcessingResult
        {
            Status = "Success",
            Message = $"Processed: {transformed}",
            ProcessedAt = DateTime.UtcNow,
            ProcessingTimeMs = Random.Shared.Next(50, 200)
        };
    }
    
    public async Task<ProcessingResult> ProcessDataAsync(string input)
    {
        // Simulate async work
        await Task.Delay(100);
        return ProcessData(input);
    }
}

public class ProcessingResult
{
    public string Status { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime ProcessedAt { get; set; }
    public int ProcessingTimeMs { get; set; }
}
