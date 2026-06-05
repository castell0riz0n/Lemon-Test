using Lemon_Test.Core.MatrixTheoryData;

namespace Lemon_Test.Core.Tests.MatrixTheoryData;

public class ConfigurationTests
{
    [Theory]
    [MemberData(nameof(ConfigurationMatrix))]
    public void ProcessRequest_ShouldHandleAllConfigurationCombinations(
        bool isHttps,
        string environment,
        int timeout)
    {
        // Arrange
        var config = new Configuration(isHttps, environment, timeout);
        var processor = new RequestProcessor(config);

        // Act & Assert
        if (environment == "Production" && !isHttps)
        {
            // Production must use HTTPS
            var result = processor.ProcessRequest("GET", true);
            Assert.False(result.Success);
            Assert.Contains("Invalid configuration", result.Message);
        }
        else if (timeout <= 0)
        {
            // Invalid timeout
            var result = processor.ProcessRequest("GET", true);
            Assert.False(result.Success);
        }
        else
        {
            // Valid configuration
            var result = processor.ProcessRequest("GET", true);
            Assert.True(result.Success);
        }
    }

    [Theory]
    [MemberData(nameof(RequestProcessingMatrix))]
    public void ProcessRequest_ShouldHandleAllRequestCombinations(
        string requestType,
        bool hasAuth,
        string environment)
    {
        // Arrange
        var config = new Configuration(true, environment, 30); // Valid config
        var processor = new RequestProcessor(config);

        // Act
        var result = processor.ProcessRequest(requestType, hasAuth);

        // Assert
        if (requestType == "DELETE" && !hasAuth)
        {
            Assert.False(result.Success);
            Assert.Contains("Authentication required", result.Message);
        }
        else if (requestType == "DEBUG" && environment == "Production")
        {
            Assert.False(result.Success);
            Assert.Contains("not allowed in Production", result.Message);
        }
        else
        {
            Assert.True(result.Success);
        }
    }

    // Creates all combinations: 2 × 3 × 3 = 18 test cases using MemberData

    public static MatrixTheoryData<bool, string, int> ConfigurationMatrix =>
        new(
            [true, false],
            ["Development", "Staging", "Production"],
            [30, 60, 120]);

    // Creates request processing combinations: 4 × 2 × 3 = 24 test cases
    public static MatrixTheoryData<string, bool, string> RequestProcessingMatrix
        => new(["GET", "POST", "DELETE", "DEBUG"],
            [true, false],
            ["Development", "Staging", "Production"]);
}