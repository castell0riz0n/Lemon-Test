using System.Text.RegularExpressions;
using Xunit.Sdk;
using Xunit.v3;

namespace Lemon_Test.Core.Tests.ParallelExecution;

public class PriorityOrderer: ITestCaseOrderer
{
    public IReadOnlyCollection<TTestCase> OrderTestCases<TTestCase>(IReadOnlyCollection<TTestCase> testCases) where TTestCase : notnull, ITestCase
    {
        return testCases
            .OrderByDescending(testCase => GetTestPriority(testCase.TestMethod?.MethodName ?? string.Empty))
            .ToList();
    }

    private static int GetTestPriority(string testMethodName)
    {
        // Extract number from test method name (e.g., "Test1_CreateFile" -> 1)
        var match = Regex.Match(testMethodName, @"Test(\d+)");
        return match.Success ? int.Parse(match.Groups[1].Value) : int.MaxValue;
    }
}