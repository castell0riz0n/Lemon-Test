namespace Lemon_Test.Core.MatrixTheoryData;

public class RequestProcessor
{
    private readonly Configuration _config;

    public RequestProcessor(Configuration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public ProcessingResult ProcessRequest(string requestType, bool hasAuthentication)
    {
        if (!_config.IsValid())
            return new ProcessingResult(false, "Invalid configuration");

        if (requestType == "DELETE" && !hasAuthentication)
            return new ProcessingResult(false, "Authentication required for DELETE operations");

        if (_config.Environment == "Production" && requestType == "DEBUG")
            return new ProcessingResult(false, "DEBUG operations not allowed in Production");

        return new ProcessingResult(true, "Request processed successfully");
    }
}

public class ProcessingResult
{
    public bool Success { get; }
    public string Message { get; }

    public ProcessingResult(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}
